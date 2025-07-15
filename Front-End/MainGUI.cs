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

using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace H2O_Master {
	public partial class MainGUI : Form {
		private static readonly string filterQueryPlaceHolder = "\"Column Header SubString\":\"Searched SubString\"";
		private static readonly string filterQueryTooltip =
			"Specify Filter Query. Syntax: \"Column Name\":\"Searched SubString\"" +
			"\n\nFor example, this Search Query that contains two search fields separated by space" +
			"\n\"Raw Size\":\"182349\" \"Name\":\"warlock\"" +
			"\nFinds the row with both:" +
			"\n* Raw Size containing the substring \"182349\". Complete strings like \"31823491\" or \"18234942\" will satisfy this requirement" +
			"\n* Name containing the substring \"warlock\". Complete strings like \"Warlock.hva\" or \"Master Warlock.mdl\" will satisfy this requirement";

		private static readonly DataTable archiveEntries_Data = new();
		private static readonly DataTable addedEntries_Data = new();

		public MainGUI() {
			InitializeComponent();
		}

		private void GUI_Load(object _, EventArgs __) {
			// initialize tooltip and placeholder of search query controls
			tbx_SearchArchiveEntries.PlaceholderText = filterQueryPlaceHolder;
			tbx_SearchAddedEntries.PlaceholderText = filterQueryPlaceHolder;

			ttp_SearchQueryInfo.SetToolTip(tbx_SearchArchiveEntries, filterQueryTooltip);
			ttp_SearchQueryInfo.SetToolTip(tbx_SearchAddedEntries, filterQueryTooltip);

			foreach (DataGridViewColumn col in dgv_ArchiveEntryList.Columns) {
				col.DataPropertyName = col.Name; // the DataPropertyName needs to be equal to the archiveEntries->column->ColumnName
				archiveEntries_Data.Columns.Add(col.Name, col.ValueType ?? typeof(object));
			}

			foreach (DataGridViewColumn col in dgv_AddedEntryList.Columns) {
				col.DataPropertyName = col.Name; // the DataPropertyName needs to be equal to the archiveEntries->column->ColumnName
				addedEntries_Data.Columns.Add(col.Name, col.ValueType ?? typeof(object));
			}

			bds_ArchiveEntries.DataSource = archiveEntries_Data; // link to bindingsource to support query searching later
			bds_AddedEntries.DataSource = addedEntries_Data; // link to bindingsource to support query searching later

			// hide addedEntriesList by default
			cbx_ToggleAddedEntriesList.Checked = false;
			Toggle_AddedEntryListContainer(_, EventArgs.Empty);
		}

		private void BrowseArchive(object _, EventArgs __) {
			using OpenFileDialog openFileDialog = new();
			openFileDialog.Title = "Select H2O File";
			openFileDialog.Filter = "H2O Archive|*.H2O|Any Files|*.*";
			openFileDialog.Multiselect = false;
			if (openFileDialog.ShowDialog() != DialogResult.OK)
				return;
			string targetArchive = openFileDialog.FileName;

			H2O_Master_dll.API_Info apiInfo = H2O_Master_dll.Load(targetArchive);
			if (apiInfo.rawSize == 0) {
				MessageBox.Show("Failed to Load H2O Archive", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}

			tbx_LoadedArchive.Text = targetArchive;
			tbx_Version.Text = apiInfo.version.ToString();
			tbx_CompressedSize.Text = GetSuffixedFileSize(apiInfo.compressedSize);
			tbx_RawSize.Text = GetSuffixedFileSize(apiInfo.rawSize);
			lbl_ArchiveEntryCount.Text = apiInfo.entryCount.ToString();

			archiveEntries_Data.Rows.Clear();
			unsafe {
				for (int i = 0; i < apiInfo.entryCount; i++) {
					H2O_Master_dll.API_Entry* entry = &apiInfo.entries[i];
					archiveEntries_Data.Rows.Add(
						i,
						false,
						Marshal.PtrToStringUni((IntPtr)entry->directory) ?? "",
						Marshal.PtrToStringUni((IntPtr)entry->name) ?? "",
						$"0x{entry->offset:X8}",
						GetSuffixedFileSize(entry->compressedSize),
						GetSuffixedFileSize(entry->rawSize),
						$"0x{entry->checksum:X8}",
						(entry->hasHeader == 1) ? "Yes" : "No"
					);
				}
			}
		}

		private void SaveAs(object _, EventArgs __) {
			MessageBox.Show("Building H2O Files is not yet supported. Sorry :)", "Not yet Implemented");
		}

		private void Toggle_ArchiveEntryListContainer(object _, EventArgs __) {
			ToggleDGV(dgv_ArchiveEntryList, tlp_OuterContainer.RowStyles[3], cbx_ToggleArchiveEntriesList.Checked);
		}

		private void Toggle_AddedEntryListContainer(object _, EventArgs __) {
			ToggleDGV(dgv_AddedEntryList, tlp_OuterContainer.RowStyles[5], cbx_ToggleAddedEntriesList.Checked);
		}

		// Checks if Enter is pressed on the filter query
		private void TBX_SearchArchiveEntries_KeyDown(object _, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				e.Handled = true; // stops bubbling
				e.SuppressKeyPress = true; // prevents "ding" & char insert
				DGVFilterQuery(dgv_ArchiveEntryList, tbx_SearchArchiveEntries.Text);
			}
		}

		// Checks if Enter is pressed on the filter query
		private void DGV_AddedEntryList_KeyDown(object _, KeyEventArgs e) {
			if (e.KeyCode == Keys.Enter) {
				e.Handled = true; // stops bubbling
				e.SuppressKeyPress = true; // prevents "ding" & char insert
				DGVFilterQuery(dgv_AddedEntryList, tbx_SearchAddedEntries.Text);
			}
		}

		private void ExtractFiles(object _, EventArgs __) {
			DataGridViewSelectedRowCollection selectedRows = dgv_ArchiveEntryList.SelectedRows;
			if (selectedRows.Count == 0) {
				return; // no selected rows
			}
			using FolderBrowserDialog folderExplorer = new();
			if (folderExplorer.ShowDialog() != DialogResult.OK)
				return; // user failed to select folder
			string baseFolder = folderExplorer.SelectedPath;
			if (string.IsNullOrWhiteSpace(baseFolder)) {
				return; // folder is invalid
			}

			// initialize the targetIndeces array and targetStatuses
			UInt32 selectedRowsCount = (UInt32)selectedRows.Count;
			UInt32[] targetIndeces = new UInt32[selectedRowsCount];
			Byte[] targetsStatus = new Byte[selectedRowsCount];
			for (int i = 0; i < selectedRowsCount; i++) {
				// fill targetIndeces
				var row = selectedRows[i];
				targetIndeces[i] = Convert.ToUInt32(row.Cells["ArchiveEntry_Index"].Value);
			}

			H2O_Master_dll.Extract(baseFolder, selectedRowsCount, targetIndeces, targetsStatus);

			string remainingFiles = string.Empty;
			for (int i = 0; i < selectedRowsCount; i++) {
				if (targetsStatus[i] == 0) { // failed to extract file
					var row = selectedRows[i];
					string? directory = Convert.ToString(row.Cells["Directory"].Value);
					string? name = Convert.ToString(row.Cells["Name"].Value);
					remainingFiles += (string.IsNullOrWhiteSpace(directory) ? string.Empty : (directory + "\\"))
									+ (string.IsNullOrWhiteSpace(name) ? string.Empty : name) + "\n";
				}
			}
			if (remainingFiles != string.Empty) {
				MessageBox.Show(
					"Failed to Extract the Following Files\n\n" + remainingFiles,
					"File Extractor",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
				);
			}
		}

		private void VisitIssuesPage(object _, EventArgs __) {
			try {
				Process.Start(new ProcessStartInfo {
					FileName = "https://github.com/Aldrin-John-Olaer-Manalansan/H2O_Master/issues",
					UseShellExecute = true
				});
			}
			catch (Exception ex) {
				MessageBox.Show($"Failed to open browser: {ex.Message}");
			}
		}

		// hides/shows the specified datagridview in the entrylistcontainer
		private static void ToggleDGV(DataGridView dgv, RowStyle style, bool isVisible) {
			dgv.Visible = isVisible;
			if (isVisible) {
				style.SizeType = SizeType.Percent;
				style.Height = 50.0f;
			} else {
				style.SizeType = SizeType.Absolute;
				style.Height = 0;
			}
		}

		[GeneratedRegex("\"([^\"]+)\":\"([^\"]+)\"")]
		private static partial Regex SearchQueryPattern();
		// parses the filterQuery then filters the DataGridView
		private static void DGVFilterQuery(DataGridView dgv, string filterQuery) {
			// Parse query syntax like: "Raw Size":"182349" "Name":"warlock"
			if (dgv.DataSource is not BindingSource bds) {
				return;
			}

			var filters = new List<string>();
			var matches = SearchQueryPattern().Matches(filterQuery);

			foreach (Match match in matches) {
				string columnName = match.Groups[1].Value.Replace("'", "''");
				foreach (DataGridViewColumn column in dgv.Columns) {
					// check if the provided columnName matches a specific column header
					if (CultureInfo.InvariantCulture.CompareInfo
					.IndexOf(column.HeaderText, columnName, CompareOptions.IgnoreCase) >= 0) {
						// add the query
						string value = match.Groups[2].Value.Replace("'", "''");
						filters.Add($"[{column.Name}] LIKE '%{value}%'");
						break;
					}
				}
			}

			// Join with AND and apply filter
			try { // suppress exception caused by wrong filter query
				bds.Filter = string.Join(" AND ", filters);
			}
			catch { }
		}

		// converts byte balues into KB/MB/GB/etc suffixed value
		private static readonly string[] sizeSuffix = ["KB", "MB", "GB", "TB", "PB"];
		private static string GetSuffixedFileSize(UInt64 fileSize) {
			if (fileSize == 0) {
				return "0";
			}
			int index = -1;
			UInt64 dividend = 1;
			for (; ; ) {
				if (dividend << 10 > fileSize) {
					break;
				}
				dividend <<= 10;
				index++;
				if (index >= sizeSuffix.Length - 1) {
					break;
				}
			}
			return ((float)fileSize / (float)dividend).ToString("0.##") + ((index >= 0) ? (sizeSuffix[index] + '(' + fileSize.ToString() + ')') : "");
		}
	}
}
