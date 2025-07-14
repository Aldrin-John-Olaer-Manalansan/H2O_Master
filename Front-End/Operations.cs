/*
 * @File: Operations.cs
 * @Author: Aldrin John O. Manalansan (ajom)
 * @Email: aldrinjohnolaermanalansan@gmail.com
 * @Brief: All processing operations using H2O_Master.dll
 * @LastUpdate: July 15, 2025
 * 
 * Copyright (C) 2025  Aldrin John O. Manalansan  <aldrinjohnolaermanalansan@gmail.com>
 * 
 * This Source Code is served under Open-Source AJOM License
 * You should have received a copy of License_OS-AJOM
 * along with this source code. If not, see:
 * <https://raw.githubusercontent.com/Aldrin-John-Olaer-Manalansan/AJOM_License/refs/heads/main/LICENSE_AJOM-OS>
 */

using System.Runtime.InteropServices;

namespace H2O_Master {
	/// <summary>
	/// P/Invoke wrapper for reMIXer.dll functions
	/// </summary>
	static class H2O_Master_dll {
		private const string DLL_NAME = "H2O_Master_Library.dll";

		#region Enums and Constants

		#endregion

		#region Structures

		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct API_Entry {
			public char* directory;
			public char* name;
			public UInt32 offset;
			public UInt32 compressedSize;
			public UInt32 rawSize;
			public UInt32 checksum;
			public Byte hasHeader;
		}

		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct API_Info {
			public UInt64 compressedSize;
			public UInt64 rawSize;
			public API_Entry* entries;
			public UInt32 entryCount;
			public UInt32 maxEntryCount;
			public UInt32 version;
		}

		#endregion

		#region P/Invoke Declarations

		[DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern API_Info Load([MarshalAs(UnmanagedType.LPStr)] string filePath);

		[DllImport(DLL_NAME, CallingConvention = CallingConvention.Cdecl)]
		public static extern void Extract(
			[MarshalAs(UnmanagedType.LPWStr)] string workingDirectory,
			UInt32 targetCount,
			[In] UInt32[] targetIndeces,
			[Out] Byte[] targetsStatus
		);

		#endregion
	}

	static class Operations {

	}
}
