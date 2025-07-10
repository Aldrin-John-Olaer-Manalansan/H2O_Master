/*
 * @File: Compression_Manager.c
 * @Author: Aldrin John O. Manalansan (ajom)
 * @Email: aldrinjohnolaermanalansan@gmail.com
 * @Brief: Helper functions for compression and decompression of data
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

#include "Compression_Manager.h"

#include "Cryptography/CRC.h"

#include "blast.h"

#include <stdlib.h>
#include <string.h>

typedef struct {
	const uint8_t *compressedData;
	uint8_t *decompressedData;
	uint32_t compressedSize;
	uint32_t decompressedSize;
	uint32_t decompressedChecksum;
} t_blast_tracker;

// one shot callback
static unsigned DecompressorCallback_Input(void* how, unsigned char** buf) {
	t_blast_tracker *blastInfo = (t_blast_tracker*)how;
	*buf = (uint8_t*)blastInfo->compressedData;
	return blastInfo->compressedSize;
}

// one shot callback
static int DecompressorCallback_Output(void* how, unsigned char* buf, unsigned len) {
	t_blast_tracker *blastInfo = (t_blast_tracker*)how;
	memcpy(blastInfo->decompressedData, buf, (size_t)len);
	blastInfo->decompressedSize = len;
	blastInfo->decompressedChecksum = CRC32(buf, len);
	return 0;
}

bool DecompressData(void* const destination, const uint32_t destinationSize, const void* const source, const uint32_t sourceSize, const uint32_t expectedChecksum) {
	t_blast_tracker blastInfo = {
		.compressedData = source,
		.decompressedData = destination,
		.compressedSize = sourceSize,
		.decompressedSize = 0,
		.decompressedChecksum = 0
	};
	return !blast(DecompressorCallback_Input, &blastInfo, DecompressorCallback_Output, &blastInfo, NULL, NULL)
	&& (blastInfo.decompressedSize == destinationSize)
	&& (blastInfo.decompressedChecksum == expectedChecksum);
}