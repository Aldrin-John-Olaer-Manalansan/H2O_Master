/*
 * @File: File_Manager.c
 * @Author: Aldrin John O. Manalansan (ajom)
 * @Email: aldrinjohnolaermanalansan@gmail.com
 * @Brief: Helper functions for Reading and Writing files
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

#include "File_Manager.h"

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#if defined(_WIN32)
    #include <wchar.h>
#else
#include <iconv.h>
// UTF-16LE to UTF-8 conversion (BMP only, no surrogate pair support)
static char* utf16le_to_utf8(const char16_t* input) {
    size_t len = 0;
    while (input[len] != 0) len++;

    char* utf8 = malloc(len * 3 + 1); // Max 3 bytes per UTF-16 code unit
    if (!utf8) return NULL;

    char* out = utf8;
    for (size_t i = 0; i < len; ++i) {
        uint16_t ch = input[i];

        if (ch < 0x80) {
            *out++ = (char)ch;
        } else if (ch < 0x800) {
            *out++ = 0xC0 | (ch >> 6);
            *out++ = 0x80 | (ch & 0x3F);
        } else {
            *out++ = 0xE0 | (ch >> 12);
            *out++ = 0x80 | ((ch >> 6) & 0x3F);
            *out++ = 0x80 | (ch & 0x3F);
        }
    }
    *out = '\0';
    return utf8;
}
#endif

// Saves binary data to a file
// The "filePath" must be in UTF-16 Little Endian Encoding
// The "data" is expected to be a raw binary data and does not need to be formatted
bool save_binary_utf16le_path(const char16_t* const filePath, const void* const data, const size_t dataSize) {
    if (!filePath || !data) return false;

    FILE* fp = NULL;

#ifdef _WIN32
    // Convert UTF-16LE (char16_t*) to wchar_t*
    size_t len = 0;
    while (filePath[len] != 0) len++;

    wchar_t* wpath = malloc((len + 1) * sizeof(wchar_t));
    if (!wpath) return false;

    for (size_t i = 0; i <= len; ++i)
        wpath[i] = (wchar_t)filePath[i];  // Windows: wchar_t == UTF-16LE

    fp = _wfopen(wpath, L"wb");
    free(wpath);

#else
    // Convert UTF-16LE (char16_t*) to UTF-8
    char* utf8Path = utf16le_to_utf8(filePath);
    if (!utf8Path) return false;

    fp = fopen(utf8Path, "wb");
    free(utf8Path);
#endif

    if (!fp) {
        // perror("Failed to open file");
        return false;
    }

    fwrite(data, 1, dataSize, fp);
    fclose(fp);
    return true;
}

// Creates a the entire directory if it doesn't exist
int wmkdirs(const wchar_t* const path) {
    wchar_t temp[512];
    wcscpy(temp, path);

    size_t len = wcslen(temp);
    if (len == 0) return -1;

    // Remove trailing backslash
    if (temp[len - 1] == L'\\' || temp[len - 1] == L'/')
        temp[len - 1] = L'\0';

    for (wchar_t* p = temp + 1; *p; p++) {
        if (*p == L'\\' || *p == L'/') {
            *p = L'\0';
            if (_waccess(temp, 0) != 0) {
                if (_wmkdir(temp) != 0 && errno != EEXIST)
                    return -1;
            }
            *p = L'\\';  // restore
        }
    }

    // Final directory
    if (_waccess(temp, 0) != 0) {
        if (_wmkdir(temp) != 0 && errno != EEXIST)
            return -1;
    }

    return 0;
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