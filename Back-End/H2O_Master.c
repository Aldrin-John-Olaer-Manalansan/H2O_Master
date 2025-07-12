/*
 * @File: H2O_Master.c
 * @Author: Aldrin John O. Manalansan (ajom)
 * @Email: aldrinjohnolaermanalansan@gmail.com
 * @Brief: native C library functions used by the main app for Performance purposes
 * @LastUpdate: June 7, 2025
 * 
 * Copyright (C) 2025  Aldrin John O. Manalansan  <aldrinjohnolaermanalansan@gmail.com>
 * 
 * This Source Code is served under Open-Source AJOM License
 * You should have received a copy of License_OS-AJOM
 * along with this source code. If not, see:
 * <https://raw.githubusercontent.com/Aldrin-John-Olaer-Manalansan/AJOM_License/refs/heads/main/LICENSE_AJOM-OS>
 * 
 * Credits:
 * 	Battle Realms Community for their shared informations through reverse engineering
 */

#include "H2O_Master.h"

#include "Cryptography/CRC.h" // https://github.com/Aldrin-John-Olaer-Manalansan/Cryptography
#include "DynamicDataStructures/BinaryBuilder.h" // https://github.com/Aldrin-John-Olaer-Manalansan/DynamicDataStructures
#include "DynamicDataStructures/StringBuilder.h" // https://github.com/Aldrin-John-Olaer-Manalansan/DynamicDataStructures

#include "Compression_Manager.h"
#include "File_Manager.h"
#include "String16.h"

#include <ctype.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

static t_api_info g_APIInfo;

static binarybuilder_t g_SharedBinaryBuilder;
static binarydata_t g_SharedBuffer;
static binarydata_t g_DecompressedFolderNames;
static binarydata_t g_DecompressedFileNames;
static binarydata_t g_DecompressedBinary;
static binarydata_t g_Directories;
static stringdata_t g_inspectedFilePath;

static void __attribute__((constructor)) RuntimeInit(void) {
	BinaryBuilder_Init(&g_SharedBinaryBuilder);
	BinaryData_Init(&g_SharedBuffer);
	BinaryData_Init(&g_DecompressedFolderNames);
	BinaryData_Init(&g_DecompressedFileNames);
	BinaryData_Init(&g_DecompressedBinary);
	BinaryData_Init(&g_Directories);
	StringData_Init(&g_inspectedFilePath);
}

// Reads the entire contents of a binary file, then stores it to a manageable binary buffer
// static bool ReadFile(const char* restrict const fromFilePath, void *restrict *restrict const buffer, size_t* restrict const bufferSize) {
// 	FILE* file;
// 	size_t fileSize;

// 	file = fopen(fromFilePath, "rb");
// 	if (!file) {
// 		return false; // failed to open file
// 	}

// 	fseeko(file, 0, SEEK_END); // move pointer to EOF
// 	fileSize = (size_t)ftello(file);
// 	if (fileSize != SIZE_MAX) {
// 		size_t requiredSize = fileSize + 1; // +1 for null terminator string compatibility
// 		if (requiredSize > *bufferSize) {
// 			char* expandedBuffer = malloc(requiredSize);
// 			if (!expandedBuffer) {
// 				fclose(file);
// 				return false; // failed expanding memory
// 			}
// 			if (*buffer) {
// 				free(*buffer); // free the old buffer if it exists
// 			}
// 			*buffer = expandedBuffer;
// 			*bufferSize = requiredSize;
// 		}
// 		if (*buffer) {
// 			rewind(file); // move pointer to SOF
// 			// Read file into buffer
// 			if (fileSize == fread(*buffer, 1, fileSize, file)) {
// 				fclose(file);

// 				((char*)*buffer)[fileSize] = 0; // null terminator at the end for string compatibility
// 				return true; // successfully read the file
// 			}
// 		}
// 	}

// 	fclose(file);
// 	return false; // Something went wrong
// }

/* retrieves the chunk data of the chunk found at the the current seek pointer of the inspected file object
 * out_decompressedSize can be set to NULL, otherwise specify a pointer where the size of the decompressed data will be written
 * out_decompressedStorage will be used to store the decompressed data
 * if out_decompressedStorage = NULL, g_DecompressedBinary global variable will be used as storage instead.
 * Be mindful that the shared buffer's contents could be overwritten by calls outside of this function.
 * returns the decompressed data of the chunk, or NULL if the chunk is corrupted or not found
 */
static void* GetChunkData(FILE* restrict const inspectedFileObject,
	uint32_t* restrict const out_decompressedSize, binarydata_t* restrict const out_decompressedStorage
) {
	t_chunk_header chunkHeader;
	if (fread(&chunkHeader, sizeof(t_chunk_header), 1, inspectedFileObject) != 1) {
		return NULL; // failed to read the chunk header
	}
	const long endOfData = ftell(inspectedFileObject) + sizeof(t_chunk_header) + (2 * chunkHeader.compressedSize);
	if ((chunkHeader.compressedSize <= sizeof(t_chunk_data)) || (chunkHeader.rawSize <= sizeof(t_chunk_data))) {
		fseek(inspectedFileObject, endOfData, SEEK_SET);
		return NULL; // corrupted chunk header
	}
	// assure the minimum size of the decompressed storage
	void* decompressedChunkData;
	if (out_decompressedStorage) {
		BinaryData_SetMinSize(out_decompressedStorage, chunkHeader.rawSize);
		decompressedChunkData = out_decompressedStorage->data;
	} else {
		BinaryData_InitWithMinSize(&g_DecompressedBinary, chunkHeader.rawSize);
		decompressedChunkData = g_DecompressedBinary.data;
	}
	if (chunkHeader.compressedSize == chunkHeader.rawSize) { // not compressed
		// read the raw data directly
		if ((fread(decompressedChunkData, chunkHeader.rawSize, 1, inspectedFileObject) != 1) // failed to read the raw data
		|| (CRC32(decompressedChunkData, chunkHeader.rawSize) != chunkHeader.checksum) // checksum does not matche
		) {
			fseek(inspectedFileObject, endOfData, SEEK_SET);
			return NULL; // something went wrong
		}
	} else {
		BinaryData_SetMinSize(&g_SharedBuffer, chunkHeader.compressedSize); // assure the minimum size of the compressed storage
		void* const compressedChunkData = g_SharedBuffer.data;
		if ((fread(compressedChunkData, chunkHeader.compressedSize, 1, inspectedFileObject) != 1) // failed to read the compressed data
		|| !DecompressData( // failed to decompress data
			decompressedChunkData, chunkHeader.rawSize,
			compressedChunkData, chunkHeader.compressedSize,
			chunkHeader.checksum
		)) {
			fseek(inspectedFileObject, endOfData, SEEK_SET);
			return NULL; // failed to read the compressed data
		}
	}
	
	fseek(inspectedFileObject, endOfData, SEEK_SET);
	if (out_decompressedSize) {
		*out_decompressedSize = chunkHeader.rawSize; // write the decompressed size
	}
	return decompressedChunkData;
}

// Loads an H2O Archive into memory for future use
// returns all relevant informations about it
// if the function returns with rawSize = 0, it means that the function failed to load the H2O archive
t_api_info Load(const char* const filePath) {
	g_APIInfo.rawSize = 0;

	FILE* inspectedFileObject = fopen(filePath, "rb");
	if (!inspectedFileObject) {
		return g_APIInfo; // failed to open file
	}
	StringData_Set(&g_inspectedFilePath, filePath); // store the file path as reference to what was loaded

	// search for the byte mark of the H2O archive
	for (;;) {
		int byteChecker = fgetc(inspectedFileObject);
		if (byteChecker == 0x1A) { // 0x1A is the byte mark for H2O files indicating the start of the archive header
			break; // found the byte mark, continue reading
		} else if (byteChecker == EOF) {
			fclose(inspectedFileObject);
			return g_APIInfo; // reached EOF without finding the byte mark
		}
	}

	// read the header of the H2O archive
	t_header header_Archive;
	if ((fread(&header_Archive, sizeof(t_header), 1, inspectedFileObject) != 1) // failed to read the header
	|| (header_Archive.fileCount == 0 || header_Archive.compressedSize == 0 || header_Archive.rawSize == 0) // invalid archive, no files or sizes are zero
	) {
		fclose(inspectedFileObject);
		return g_APIInfo; // something went wrong
	}

	// expand the memory storage if necessary
	if (header_Archive.fileCount > g_APIInfo.maxEntryCount) { // requires more space for entries
		if (g_APIInfo.entries) {
			free(g_APIInfo.entries); // free the old entries if it exists
		}
		t_api_entry* const expandedEntries = malloc(header_Archive.fileCount * sizeof(t_api_entry));
		if (!expandedEntries) {
			fclose(inspectedFileObject);
			return g_APIInfo; // failed to expand memory for entries
		}
		g_APIInfo.entries = expandedEntries;
		g_APIInfo.maxEntryCount = header_Archive.fileCount;
	}
	g_APIInfo.entryCount = header_Archive.fileCount;

	// read the entries of the H2O archive
	for (t_api_entry* apiEntry = g_APIInfo.entries; apiEntry < g_APIInfo.entries + header_Archive.fileCount; apiEntry++) {
		t_entry entry;
		if (fread(&entry, sizeof(t_entry), 1, inspectedFileObject) != 1) { // failed to read the entry
			fclose(inspectedFileObject);
			return g_APIInfo; // something went wrong
		}

		apiEntry->directoryNode = NULL;
		memcpy(&apiEntry->directoryNode, &entry.directoryNodeIndex, sizeof(entry.directoryNodeIndex)); // temporary storage, will be replaced later
		apiEntry->name = NULL;
		memcpy(&apiEntry->name, &entry.nameIndex, sizeof(entry.nameIndex)); // temporary storage, will be replaced later

		apiEntry->offset = entry.offset;
		apiEntry->compressedSize = entry.compressedSize;
		apiEntry->rawSize = entry.rawSize;	
		apiEntry->checksum = entry.checksum;
		apiEntry->hasHeader = entry.hasChunkHeader;
	}
	fseek(inspectedFileObject, header_Archive.fileCount * sizeof(t_entry), SEEK_CUR); // skip redundant duplicate

	// process the foldername chunk
	const t_chunk_data* const chunkData_FolderNames = GetChunkData(inspectedFileObject, NULL, &g_DecompressedFolderNames);
	// process the filename chunk
	const t_chunk_data* const chunkData_FileNames = GetChunkData(inspectedFileObject, NULL, &g_DecompressedFileNames);
	
	const char16_t* char16Seeker;
	uintptr_t outOfBoundsPtr;
	uint32_t index;

	if (chunkData_FolderNames) {
		// read number of possible directory combinations
		uint32_t directoryNodeCount;
		if (fread(&directoryNodeCount, sizeof(directoryNodeCount), 1, inspectedFileObject) != 1) {
			fclose(inspectedFileObject);
			return g_APIInfo; // failed to read directoryNodeCount
		}
		fseek(inspectedFileObject, sizeof(directoryNodeCount), SEEK_CUR); // skip redundant duplicate
		// These two values are always equal, but we should boundary check just to be safe
		if (directoryNodeCount > chunkData_FolderNames->count) {
			directoryNodeCount = chunkData_FolderNames->count; // make sure we don't exceed boundary
		}

		// process all directory combinations
		BinaryData_SetMinSize(&g_Directories, sizeof(t_directorynode) * directoryNodeCount);
		t_directorynode* const directoryNodes = (t_directorynode*)g_Directories.data;
		outOfBoundsPtr = (uintptr_t)chunkData_FolderNames + chunkData_FolderNames->size;
		index = 0;
		for (char16Seeker = (char16_t*)&chunkData_FolderNames[1];
			((uintptr_t)char16Seeker < outOfBoundsPtr) && (index < directoryNodeCount);
			char16Seeker++
		) {
			uint32_t folderIndex;
			if (fread(&folderIndex, sizeof(folderIndex), 1, inspectedFileObject) != 1) {
				fclose(inspectedFileObject);
				return g_APIInfo; // failed to read folderIndex
			}
			directoryNodes[index].parent = (folderIndex < directoryNodeCount) ? directoryNodes + folderIndex : NULL;
			directoryNodes[index].name = char16Seeker;
			for (;(uintptr_t)char16Seeker < outOfBoundsPtr; char16Seeker++) {
				if (*char16Seeker == 0) { // find the end of the string
					break; // end of string found
				}
			}
			index++; // next node
		}
		if (index < directoryNodeCount) { // set all uninitialized elements to NULL
			memset(directoryNodes + index, 0, (directoryNodeCount - index) * sizeof(t_directorynode*));
		}

		// map the directoryNodes to the entries
		for (t_api_entry* apiEntry = g_APIInfo.entries; apiEntry < g_APIInfo.entries + header_Archive.fileCount; apiEntry++) {
			memcpy(&index, &apiEntry->directoryNode, sizeof(index)); // recover the directoryNode index from the temporary storage
			apiEntry->directoryNode = (index < directoryNodeCount) ? (t_directorynode*)g_Directories.data + index : NULL;
		}
	}

	// map the filenames to the entries
	if (chunkData_FileNames) {
		// map strings
		BinaryData_SetMinSize(&g_SharedBuffer, chunkData_FileNames->count * sizeof(char16_t*));
		const char16_t** const char16Map = g_SharedBuffer.data;
		char16Seeker = (char16_t*)&chunkData_FileNames[1];
		outOfBoundsPtr = (uintptr_t)chunkData_FileNames + chunkData_FileNames->size;
		index = 0;
		for (;((uintptr_t)char16Seeker < outOfBoundsPtr) && (index < chunkData_FileNames->count); char16Seeker++) {
			char16Map[index] = char16Seeker;
			for (;(uintptr_t)char16Seeker < outOfBoundsPtr; char16Seeker++) {
				if (*char16Seeker == 0) { // find the end of the string
					index++; // map to next entry
					break; // end of string found
				}
			}
		}
		if (index < chunkData_FileNames->count) { // set all uninitialized elements to NULL
			memset(char16Map + index, 0, (chunkData_FileNames->count - index) * sizeof(char16_t*));
		}

		for (t_api_entry* apiEntry = g_APIInfo.entries; apiEntry < g_APIInfo.entries + header_Archive.fileCount; apiEntry++) {
			memcpy(&index, &apiEntry->name, sizeof(index)); // recover the filename index from the temporary storage
			apiEntry->name = (index < chunkData_FileNames->count) ? char16Map[index] : NULL; // link the filename
		}
	}

	fclose(inspectedFileObject);
	g_APIInfo.version = header_Archive.version;
	g_APIInfo.compressedSize = header_Archive.compressedSize;
	g_APIInfo.rawSize = header_Archive.rawSize;
	return g_APIInfo; // successfully loaded the H2O archive
}

void Extract(const char16_t* restrict const workingDirectory, bool* restrict targetsStatus, const uint32_t* restrict targetIndexes, uint32_t indexCount) {
	if (!g_APIInfo.entryCount) {
		return; // no entries to extract
	}

	if (!str16len(workingDirectory)) {
		return; // invalid directory
	}

	FILE* inspectedFileObject = fopen(g_inspectedFilePath.string, "rb");
	if (!inspectedFileObject) {
		return; // failed to open file
	}

	if (_waccess(workingDirectory, 0)) { // workingDirectory does not exist
		wmkdirs(workingDirectory); // creates the workingDirectory
	}
	_wchdir(workingDirectory); // change current working directory(cwd)

	while (indexCount--) {
		const uint32_t index = *targetIndexes++;
		if (index >= g_APIInfo.entryCount) {
			continue; // index out of bounds, skip this entry
		}
		const t_api_entry* const apiEntry = &g_APIInfo.entries[index];
		if (!apiEntry->name || !apiEntry->compressedSize || !apiEntry->rawSize) {
			continue; // corrupted entry, skip this entry
		}

		if (fseek(inspectedFileObject, apiEntry->offset, SEEK_SET) != 0) {
			continue; // failed to seek to the entry's offset, skip this entry
		}
		
		void* decompressedData;
		uint32_t decompressedSize;
		if (apiEntry->hasHeader) {
			decompressedData = GetChunkData(inspectedFileObject, &decompressedSize, NULL);
			if (!decompressedData) {
				continue; // failed to retrieve data from;
			}
		} else {
			BinaryData_SetMinSize(&g_DecompressedBinary, apiEntry->rawSize);
			decompressedData = g_DecompressedBinary.data;
			if (apiEntry->compressedSize == apiEntry->rawSize) { // not compressed
				// read the raw data directly
				if (fread(decompressedData, apiEntry->rawSize, 1, inspectedFileObject) != 1) {
					continue; // failed to read the data, skip this entry
				}
			} else {
				BinaryData_SetMinSize(&g_SharedBuffer, apiEntry->compressedSize);
				void* const compressedData = g_SharedBuffer.data;
				if ((fread(compressedData, apiEntry->compressedSize, 1, inspectedFileObject) != 1) // failed to read the compressed data
				|| !DecompressData( // failed to decompress data
					decompressedData, apiEntry->rawSize,
					compressedData, apiEntry->compressedSize,
					apiEntry->checksum)
				) {
					continue; // failed to read the compressed data, skip this entry
				}
				decompressedSize = apiEntry->rawSize; // set the decompressed size
			}
		}

		BinaryBuilder_Clear(&g_SharedBinaryBuilder);
		if (apiEntry->directoryNode && apiEntry->directoryNode->name) {
			// append directory of this file
			BinaryBuilder_InsertBytes(&g_SharedBinaryBuilder, ".\x00\\", 4);
			BinaryBuilder_InsertBytes(&g_SharedBinaryBuilder, apiEntry->directoryNode->name, (str16len(apiEntry->directoryNode->name) + 1) * sizeof(char16_t));
			if (_waccess(g_SharedBinaryBuilder.data, 0)) { // directory does not exist
				wmkdirs(g_SharedBinaryBuilder.data); // create the directory
			}
			BinaryBuilder_Delete(&g_SharedBinaryBuilder, sizeof(char16_t)); // remove null terminator
			BinaryBuilder_InsertBytes(&g_SharedBinaryBuilder, "\\", sizeof(char16_t)); // replace with backslash
		}
		// append filefullname of this file
		BinaryBuilder_InsertBytes(&g_SharedBinaryBuilder, apiEntry->name, (str16len(apiEntry->name) + 1) * sizeof(char16_t));
		if (save_binary_utf16le_path(g_SharedBinaryBuilder.data, decompressedData, decompressedSize)) {
			*targetsStatus++ = true; // mark this entry as extracted
		}
	}

	fclose(inspectedFileObject);
}