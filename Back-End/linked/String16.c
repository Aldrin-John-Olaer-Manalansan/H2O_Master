/*
 * @File: String16.c
 * @Author: Aldrin John O. Manalansan (ajom)
 * @Email: aldrinjohnolaermanalansan@gmail.com
 * @Brief: Helper functions for Managing UTF-16LE strings
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

#include "String16.h"

size_t str16len(const char16_t* str) {
	size_t length = 0;
	for (;;) {
		if (*str == 0) {
			return length; // null terminator found, return the length
		}
		length++;
		str++;
	}
}