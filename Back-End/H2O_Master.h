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

#pragma once

// #if defined(__STDC_VERSION__)
//   #if __STDC_VERSION__ >= 202000L
//     #warning "Using C2x or newer"
//   #elif __STDC_VERSION__ >= 201710L
//     #warning "Using C17"
//   #elif __STDC_VERSION__ >= 201112L
//     #warning "Using C11"
//   #elif __STDC_VERSION__ >= 199901L
//     #warning "Using C99"
//   #else
//     #warning "Using pre-C99 (likely C90)"
//   #endif
// #else
//   #warning "__STDC_VERSION__ not defined; likely C90"
// #endif

#include <stdbool.h>
#include <stdint.h>
#include <uchar.h>

typedef struct {
	uint32_t version;			// Version.
	uint32_t fileCount;			// Number of files in archive.
	uint32_t unknown1;			// Unknown. Seems to always be 0.
	uint32_t unknown2;			// Unknown.
	uint64_t compressedSize;	// Compressed size of archive.
	uint64_t rawSize;			// Raw size of archive.
} t_header;

typedef struct {
	uint32_t hasChunkHeader;	// Entry type. (Raw = 0), (Compressed = 1)
	uint32_t directoryNodeIndex;	// element index at directory array. -1 if no directory.
	uint32_t nameIndex;			// element index at name array. -1 if entry has no name(therefore unused).
	uint32_t rawSize;			// Raw size of file.
	uint32_t compressedSize;	// Compressed size of file.
	uint32_t unknown1;			// Unknown(always 0x7CFC1200).
	uint32_t offset;			// Offset
	uint32_t unknown2;			// Unknown(always 0).
	uint32_t checksum;			// Checksum. Computed with crc32
	uint32_t unknown3;			// Unknown(always 0x90189200).
} t_entry;

typedef struct {
	uint32_t compressedSize;	// Compressed size of chunk.
	uint32_t rawSize;			// Raw size of chunk. It includes the t_chunk_data so its value is always >= sizeof(t_chunk_data).
	uint32_t checksum;			// Checksum
} t_chunk_header;

typedef struct {
	uint32_t count;	// Number of strings.
	uint32_t size;	// Size of string block including this header.
					// Array of strings
} t_chunk_data;

typedef struct _directory_t {
	struct _directory_t* parent;
	const char16_t* name;
} t_directorynode;

typedef struct {
	const t_directorynode* directoryNode;
	const char16_t* name;
	uint32_t offset;
	uint32_t compressedSize;
	uint32_t rawSize;
	uint32_t checksum;
	bool hasHeader; // 0 if the binary at the offset is the actual data, 1 if it has t_chunk_header before the actual data
} t_api_entry;

typedef struct {
	uint64_t compressedSize;
	uint64_t rawSize;
	t_api_entry* entries;
	uint32_t entryCount;
	uint32_t maxEntryCount;
	uint32_t version;
} t_api_info;