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

#pragma once

#include <stdbool.h>
#include <stdint.h>
#include <uchar.h>

bool save_binary_utf16le_path(const char16_t* const filePath, const void* const data, const size_t dataSize);
int wmkdirs(const wchar_t* const path);