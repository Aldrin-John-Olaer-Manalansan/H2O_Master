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
 * 	Battle Realms Community for their shared informations through reverse engineering of C&C games
 */

#include "H2O_Master.h"

#include "Compression_Manager.h"

#include <ctype.h>
#include <stdlib.h>
#include <stdio.h>
#include <string.h>

static void* g_InspectedBinary;
static size_t g_inspectedBinarySize;

static t_chunk_data* g_DecompressedDirectories;
static size_t g_DecompressedDirectoriesSize;

static t_chunk_data* g_DecompressedNames;
static size_t g_DecompressedNamesSize;

// Reads the entire contents of a binary file, then stores it to a manageable binary buffer
static bool ReadFile(const char* restrict const fromFilePath, void *restrict *restrict const buffer, size_t* restrict const bufferSize) {
	FILE* file;
	size_t fileSize;

	file = fopen(fromFilePath, "rb");
	if (!file) {
		return false; // failed to open file
	}

	fseeko(file, 0, SEEK_END); // move pointer to EOF
	fileSize = (size_t)ftello(file);
	if (fileSize != SIZE_MAX) {
		size_t requiredSize = fileSize + 1; // +1 for null terminator string compatibility
		if (requiredSize > *bufferSize) {
			char* expandedBuffer = malloc(requiredSize);
			if (!expandedBuffer) {
				fclose(file);
				return false; // failed expanding memory
			}
			if (*buffer) {
				free(*buffer); // free the old buffer if it exists
			}
			*buffer = expandedBuffer;
			*bufferSize = requiredSize;
		}
		if (*buffer) {
			rewind(file); // move pointer to SOF
			// Read file into buffer
			if (fileSize == fread(*buffer, 1, fileSize, file)) {
				fclose(file);

				((char*)*buffer)[fileSize] = 0; // null terminator at the end for string compatibility
				return true; // successfully read the file
			}
		}
	}

	fclose(file);
	return false; // Something went wrong
}

// Returns the chunk data from the chunk header, decompresses it if necessary
// decompressedStorage and decompressedStorageSize pointers will be used as reference storage of possible reallocation of decompressed data
// the decompressedStorage and decompressedStorageSize is automatically managed by this function, so it is not necessary to free the decompressedStorage pointer
// returns NULL if the chunk data is not found or if the decompression failed
// returns the decompressed chunk data if successful
static t_chunk_data* GetChunkData(
	const t_chunk_header* const chunkHeader,
	t_chunk_data *restrict *restrict const decompressedStorage,
	size_t* restrict const decompressedStorageSize
) {
	if (chunkHeader->rawSize <= sizeof(t_chunk_data)) {
		return NULL; // no data
	}
	if (chunkHeader->compressedSize == chunkHeader->rawSize) {
		return (t_chunk_data*)(chunkHeader + 1); // no compression, just return the data
	}
	t_chunk_data* decompressedChunkData = *decompressedStorage;
	if (chunkHeader->rawSize > *decompressedStorageSize) { // requires more space
		if (decompressedChunkData) {
			free(decompressedChunkData);
		}
		decompressedChunkData = malloc(chunkHeader->rawSize);
		if (!decompressedChunkData) {
			return NULL; // failed to expand memory for decompressed storage
		}
		*decompressedStorage = decompressedChunkData;
		*decompressedStorageSize = chunkHeader->rawSize;
	}
	return DecompressData(decompressedChunkData, chunkHeader->rawSize, (void*)(chunkHeader + 1), chunkHeader->compressedSize, chunkHeader->checksum)
	? decompressedChunkData // return the decompressed data
	: NULL; // Decompression failed
}

// Loads an H2O Archive into memory for future use
// returns all relevant informations about it
t_api_info Load(const char* const filePath, t_api_entry** out_Entries, size_t* const out_Count) {
	t_api_info apiInfo = {0};

	if (!ReadFile(filePath, &g_InspectedBinary, &g_inspectedBinarySize)) {
		return apiInfo; // failed to load the file
	}

	const t_header* const header_Archive = (t_header*)((uint8_t*)memchr(g_InspectedBinary, 0x1A, g_inspectedBinarySize) + 1);
	if (header_Archive == (t_header*)1) {
		return apiInfo; // bytemark not found;
	}

	t_api_entry* entriesAPI = malloc(header_Archive->fileCount * sizeof(t_api_entry));
	if (!entriesAPI) {
		return apiInfo; // failed to allocate memory
	}
	
	const t_entry* const entries = (t_entry*)(header_Archive + 1);
	const t_chunk_header* const header_Directories = (t_chunk_header*)(entries + (header_Archive->fileCount * 2));
	const t_chunk_header* const header_Names = (t_chunk_header*)((uint8_t*)header_Directories + ((sizeof(t_chunk_header) + header_Directories->compressedSize) * 2));

	const t_chunk_data* const chunkData_Directories = GetChunkData(header_Directories, &g_DecompressedDirectories, &g_DecompressedDirectoriesSize);
	const t_chunk_data* const chunkData_Names = GetChunkData(header_Names, &g_DecompressedNames, &g_DecompressedNamesSize);

	apiInfo.version = header_Archive->version;
	apiInfo.fileCount = header_Archive->fileCount;
	apiInfo.compressedSize = header_Archive->compressedSize;
	apiInfo.rawSize = header_Archive->rawSize;

	if (out_Entries) {
		*out_Entries = entriesAPI;
	}
	if (out_Count) {
		*out_Count = header_Archive->fileCount;
	}

	if (chunkData_Directories) {
		printf("dirs Count=%u Size=%u\n", chunkData_Directories->count, chunkData_Directories->size);
	}
	if (chunkData_Names) {
		printf("names Count=%u Size=%u\n", chunkData_Names->count, chunkData_Names->size);
	}
	printf("Done\n");

	return apiInfo;
}