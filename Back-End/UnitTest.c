/*
 * @File: UnitTest.c
 * @Author: Aldrin John O. Manalansan (ajom)
 * @Email: aldrinjohnolaermanalansan@gmail.com
 * @Brief: Perform a series of sanity tests before deployment 
 * @LastUpdate: June 7, 2025
 * 
 * Copyright (C) 2025  Aldrin John O. Manalansan  <aldrinjohnolaermanalansan@gmail.com>
 * 
 * This Source Code is served under Open-Source AJOM License
 * You should have received a copy of License_OS-AJOM
 * along with this source code. If not, see:
 * <https://raw.githubusercontent.com/Aldrin-John-Olaer-Manalansan/AJOM_License/refs/heads/main/LICENSE_AJOM-OS>
 * 
 * This Sourcecode is mean't for testing only, and is expendable
 */

#include "H2O_Master.c"

#include <time.h>
#include <stdlib.h>

#ifdef _WIN32
	#define DRIVE_B "B:"
	#define DRIVE_C "C:"
#else
	#define DRIVE_B "/mnt/b"
	#define DRIVE_C "/mnt/c"
#endif

#define TESTDIRECTORY DRIVE_B "/Programs/BR and WOTW(Latest Patch)/Levels/"

FILE* dumpedFileObject;

void printbytes(const void* const buffer, size_t size) {
	const uint8_t* const bytes = (uint8_t*)buffer;
	uint8_t pad = 0;
	for (size_t i = 0; i < size; i++) {
		printf("0x%.2X, ", bytes[i]);
		pad++;
		if (pad >= 16) {
			printf("\n");
			pad = 0;
		}
	}
	printf("\n");
}

int main (void) {
	// t_api_info apiInfo = Load(TESTDIRECTORY "Levels_Multi.H2O");
	// printf("Version: %u\n", apiInfo.version);
	// printf("File Count: %u\n", apiInfo.entryCount);
	// printf("Compressed Size: %llu\n", apiInfo.compressedSize);
	// printf("Raw Size: %llu\n", apiInfo.rawSize);
	
	// bool targetsStatus[7] = {false};
	// uint32_t targetIndexes[7] = {0, 5, 13, 23, 24, 31, 35};
	// Extract(u"C:/Users/Admin/OneDrive/Archive/Personal/Current/Data/Scripts/Battle_Realms/H2O_Master/Back-End/test", targetsStatus, targetIndexes, 7);

	printf("start\n");
	t_api_info apiInfo = Load("B:/Programs/BR and WOTW(Latest Patch)/Interface/Interface2.H2O");
	printf("Version: %u\n", apiInfo.version);
	printf("File Count: %u\n", apiInfo.entryCount);
	printf("Compressed Size: %llu\n", apiInfo.compressedSize);
	printf("Raw Size: %llu\n", apiInfo.rawSize);

	for (const t_api_entry* apiEntry = apiInfo.entries; apiEntry < apiInfo.entries + apiInfo.entryCount; apiEntry++) {
		if (apiEntry->directoryNode) {
			printf("%ls\\", apiEntry->directoryNode->name);
		}
		printf("%ls\n", apiEntry->name);
	}

	bool targetsStatus[7] = {false};
	uint32_t targetIndexes[7] = {62, 0, 13, 15, 8, 9, 11};
	Extract(u"C:\\Users\\Admin\\OneDrive\\Archive\\Personal\\Current\\Data\\Scripts\\Battle_Realms\\H2O_Master\\Back-End\\test", targetsStatus, targetIndexes, 7);
	return 0;
}
