namespace H2O_Master {
	partial class MainGUI {
		/// <summary>
		///  Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		///  Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		///  Required method for Designer support - do not modify
		///  the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			components = new System.ComponentModel.Container();
			DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle7 = new DataGridViewCellStyle();
			DataGridViewCellStyle dataGridViewCellStyle8 = new DataGridViewCellStyle();
			tlp_OuterContainer = new TableLayoutPanel();
			dgv_ArchiveEntryList = new DataGridView();
			ArchiveEntry_Index = new DataGridViewTextBoxColumn();
			ArchiveEntry_DeleteCheckBox = new DataGridViewCheckBoxColumn();
			ArchiveEntry_Directory = new DataGridViewTextBoxColumn();
			ArchiveEntry_Name = new DataGridViewTextBoxColumn();
			ArchiveEntry_Offset = new DataGridViewTextBoxColumn();
			ArchiveEntry_CompressedSize = new DataGridViewTextBoxColumn();
			ArchiveEntry_RawSize = new DataGridViewTextBoxColumn();
			ArchiveEntry_Checksum = new DataGridViewTextBoxColumn();
			ArchiveEntry_HasHeader = new DataGridViewTextBoxColumn();
			cms_ArchiveEntries = new ContextMenuStrip(components);
			extractToolStripMenuItem = new ToolStripMenuItem();
			bds_ArchiveEntries = new BindingSource(components);
			dgv_AddedEntryList = new DataGridView();
			AddedEntry_Directory = new DataGridViewTextBoxColumn();
			AddedEntry_Name = new DataGridViewTextBoxColumn();
			AddedEntry_Size = new DataGridViewTextBoxColumn();
			AddedEntry_Checksum = new DataGridViewTextBoxColumn();
			AddedEntry_Compress = new DataGridViewTextBoxColumn();
			AddedEntry_UseHeader = new DataGridViewTextBoxColumn();
			bds_AddedEntries = new BindingSource(components);
			tlp_PropertiesContainer = new TableLayoutPanel();
			tlp_RawSizeContainer = new TableLayoutPanel();
			lbl_RawSize = new Label();
			tbx_RawSize = new TextBox();
			tlp_CompressedSizeContainer = new TableLayoutPanel();
			tbx_CompressedSize = new TextBox();
			lbl_CompressedSize = new Label();
			tlp_VersionContainer = new TableLayoutPanel();
			tbx_Version = new TextBox();
			lbl_Version = new Label();
			btn_SaveAs = new Button();
			btn_Browse = new Button();
			tbx_LoadedArchive = new TextBox();
			tableLayoutPanel1 = new TableLayoutPanel();
			flp_ArchiveTextContainer = new FlowLayoutPanel();
			cbx_ToggleArchiveEntriesList = new CheckBox();
			lbl_ArchiveEntryCount = new Label();
			lbl_ArchiveEntriesText = new Label();
			tbx_SearchArchiveEntries = new TextBox();
			tableLayoutPanel2 = new TableLayoutPanel();
			tbx_SearchAddedEntries = new TextBox();
			flp_AddedEntriesTextContainer = new FlowLayoutPanel();
			cbx_ToggleAddedEntriesList = new CheckBox();
			lbl_AddedEntryCount = new Label();
			lbl_AddedEntriesText = new Label();
			ttp_SearchQueryInfo = new ToolTip(components);
			tlp_OuterContainer.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)dgv_ArchiveEntryList).BeginInit();
			cms_ArchiveEntries.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)bds_ArchiveEntries).BeginInit();
			((System.ComponentModel.ISupportInitialize)dgv_AddedEntryList).BeginInit();
			((System.ComponentModel.ISupportInitialize)bds_AddedEntries).BeginInit();
			tlp_PropertiesContainer.SuspendLayout();
			tlp_RawSizeContainer.SuspendLayout();
			tlp_CompressedSizeContainer.SuspendLayout();
			tlp_VersionContainer.SuspendLayout();
			tableLayoutPanel1.SuspendLayout();
			flp_ArchiveTextContainer.SuspendLayout();
			tableLayoutPanel2.SuspendLayout();
			flp_AddedEntriesTextContainer.SuspendLayout();
			SuspendLayout();
			// 
			// tlp_OuterContainer
			// 
			tlp_OuterContainer.BackColor = Color.FromArgb(48, 56, 65);
			tlp_OuterContainer.ColumnCount = 1;
			tlp_OuterContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tlp_OuterContainer.Controls.Add(dgv_ArchiveEntryList, 0, 3);
			tlp_OuterContainer.Controls.Add(dgv_AddedEntryList, 0, 5);
			tlp_OuterContainer.Controls.Add(tlp_PropertiesContainer, 0, 1);
			tlp_OuterContainer.Controls.Add(tbx_LoadedArchive, 0, 0);
			tlp_OuterContainer.Controls.Add(tableLayoutPanel1, 0, 2);
			tlp_OuterContainer.Controls.Add(tableLayoutPanel2, 0, 4);
			tlp_OuterContainer.Dock = DockStyle.Fill;
			tlp_OuterContainer.Location = new Point(0, 0);
			tlp_OuterContainer.Name = "tlp_OuterContainer";
			tlp_OuterContainer.RowCount = 6;
			tlp_OuterContainer.RowStyles.Add(new RowStyle());
			tlp_OuterContainer.RowStyles.Add(new RowStyle());
			tlp_OuterContainer.RowStyles.Add(new RowStyle());
			tlp_OuterContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tlp_OuterContainer.RowStyles.Add(new RowStyle());
			tlp_OuterContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
			tlp_OuterContainer.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
			tlp_OuterContainer.Size = new Size(841, 580);
			tlp_OuterContainer.TabIndex = 0;
			// 
			// dgv_ArchiveEntryList
			// 
			dgv_ArchiveEntryList.AllowUserToAddRows = false;
			dgv_ArchiveEntryList.AllowUserToDeleteRows = false;
			dgv_ArchiveEntryList.AllowUserToResizeColumns = false;
			dgv_ArchiveEntryList.AllowUserToResizeRows = false;
			dataGridViewCellStyle1.BackColor = Color.FromArgb(40, 40, 40);
			dataGridViewCellStyle1.ForeColor = Color.White;
			dataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle1.SelectionForeColor = SystemColors.HighlightText;
			dgv_ArchiveEntryList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
			dgv_ArchiveEntryList.AutoGenerateColumns = false;
			dgv_ArchiveEntryList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgv_ArchiveEntryList.BackgroundColor = Color.FromArgb(31, 31, 31);
			dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle2.BackColor = Color.FromArgb(63, 63, 63);
			dataGridViewCellStyle2.Font = new Font("Segoe UI", 10F);
			dataGridViewCellStyle2.ForeColor = Color.White;
			dataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle2.WrapMode = DataGridViewTriState.False;
			dgv_ArchiveEntryList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
			dgv_ArchiveEntryList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv_ArchiveEntryList.Columns.AddRange(new DataGridViewColumn[] { ArchiveEntry_Index, ArchiveEntry_DeleteCheckBox, ArchiveEntry_Directory, ArchiveEntry_Name, ArchiveEntry_Offset, ArchiveEntry_CompressedSize, ArchiveEntry_RawSize, ArchiveEntry_Checksum, ArchiveEntry_HasHeader });
			dgv_ArchiveEntryList.ContextMenuStrip = cms_ArchiveEntries;
			dgv_ArchiveEntryList.DataSource = bds_ArchiveEntries;
			dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle5.BackColor = Color.FromArgb(31, 31, 31);
			dataGridViewCellStyle5.Font = new Font("Segoe UI", 10F);
			dataGridViewCellStyle5.ForeColor = Color.White;
			dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle5.SelectionForeColor = Color.White;
			dataGridViewCellStyle5.WrapMode = DataGridViewTriState.False;
			dgv_ArchiveEntryList.DefaultCellStyle = dataGridViewCellStyle5;
			dgv_ArchiveEntryList.Dock = DockStyle.Fill;
			dgv_ArchiveEntryList.EnableHeadersVisualStyles = false;
			dgv_ArchiveEntryList.GridColor = Color.White;
			dgv_ArchiveEntryList.Location = new Point(3, 120);
			dgv_ArchiveEntryList.Name = "dgv_ArchiveEntryList";
			dgv_ArchiveEntryList.ReadOnly = true;
			dgv_ArchiveEntryList.RowHeadersVisible = false;
			dgv_ArchiveEntryList.RowHeadersWidth = 51;
			dgv_ArchiveEntryList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgv_ArchiveEntryList.Size = new Size(835, 206);
			dgv_ArchiveEntryList.TabIndex = 1;
			// 
			// ArchiveEntry_Index
			// 
			ArchiveEntry_Index.HeaderText = "Index";
			ArchiveEntry_Index.MinimumWidth = 6;
			ArchiveEntry_Index.Name = "ArchiveEntry_Index";
			ArchiveEntry_Index.ReadOnly = true;
			ArchiveEntry_Index.Width = 81;
			// 
			// ArchiveEntry_DeleteCheckBox
			// 
			ArchiveEntry_DeleteCheckBox.FalseValue = "No";
			ArchiveEntry_DeleteCheckBox.HeaderText = "Delete?";
			ArchiveEntry_DeleteCheckBox.MinimumWidth = 6;
			ArchiveEntry_DeleteCheckBox.Name = "ArchiveEntry_DeleteCheckBox";
			ArchiveEntry_DeleteCheckBox.ReadOnly = true;
			ArchiveEntry_DeleteCheckBox.Resizable = DataGridViewTriState.True;
			ArchiveEntry_DeleteCheckBox.SortMode = DataGridViewColumnSortMode.Automatic;
			ArchiveEntry_DeleteCheckBox.TrueValue = "Yes";
			ArchiveEntry_DeleteCheckBox.Width = 96;
			// 
			// ArchiveEntry_Directory
			// 
			dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleRight;
			ArchiveEntry_Directory.DefaultCellStyle = dataGridViewCellStyle3;
			ArchiveEntry_Directory.HeaderText = "Directory";
			ArchiveEntry_Directory.MinimumWidth = 6;
			ArchiveEntry_Directory.Name = "ArchiveEntry_Directory";
			ArchiveEntry_Directory.ReadOnly = true;
			ArchiveEntry_Directory.Width = 108;
			// 
			// ArchiveEntry_Name
			// 
			dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft;
			ArchiveEntry_Name.DefaultCellStyle = dataGridViewCellStyle4;
			ArchiveEntry_Name.HeaderText = "Name";
			ArchiveEntry_Name.MinimumWidth = 6;
			ArchiveEntry_Name.Name = "ArchiveEntry_Name";
			ArchiveEntry_Name.ReadOnly = true;
			ArchiveEntry_Name.Width = 85;
			// 
			// ArchiveEntry_Offset
			// 
			ArchiveEntry_Offset.HeaderText = "Offset";
			ArchiveEntry_Offset.MinimumWidth = 6;
			ArchiveEntry_Offset.Name = "ArchiveEntry_Offset";
			ArchiveEntry_Offset.ReadOnly = true;
			ArchiveEntry_Offset.Width = 84;
			// 
			// ArchiveEntry_CompressedSize
			// 
			ArchiveEntry_CompressedSize.HeaderText = "Compressed Size";
			ArchiveEntry_CompressedSize.MinimumWidth = 6;
			ArchiveEntry_CompressedSize.Name = "ArchiveEntry_CompressedSize";
			ArchiveEntry_CompressedSize.ReadOnly = true;
			ArchiveEntry_CompressedSize.Width = 168;
			// 
			// ArchiveEntry_RawSize
			// 
			ArchiveEntry_RawSize.HeaderText = "Raw Size";
			ArchiveEntry_RawSize.MinimumWidth = 6;
			ArchiveEntry_RawSize.Name = "ArchiveEntry_RawSize";
			ArchiveEntry_RawSize.ReadOnly = true;
			ArchiveEntry_RawSize.Width = 105;
			// 
			// ArchiveEntry_Checksum
			// 
			ArchiveEntry_Checksum.HeaderText = "Checksum";
			ArchiveEntry_Checksum.MinimumWidth = 6;
			ArchiveEntry_Checksum.Name = "ArchiveEntry_Checksum";
			ArchiveEntry_Checksum.ReadOnly = true;
			ArchiveEntry_Checksum.Width = 117;
			// 
			// ArchiveEntry_HasHeader
			// 
			ArchiveEntry_HasHeader.HeaderText = "Has Header?";
			ArchiveEntry_HasHeader.MinimumWidth = 6;
			ArchiveEntry_HasHeader.Name = "ArchiveEntry_HasHeader";
			ArchiveEntry_HasHeader.ReadOnly = true;
			ArchiveEntry_HasHeader.Width = 135;
			// 
			// cms_ArchiveEntries
			// 
			cms_ArchiveEntries.BackColor = Color.FromArgb(191, 191, 191);
			cms_ArchiveEntries.ImageScalingSize = new Size(20, 20);
			cms_ArchiveEntries.Items.AddRange(new ToolStripItem[] { extractToolStripMenuItem });
			cms_ArchiveEntries.Name = "cms_ArchiveEntries";
			cms_ArchiveEntries.Size = new Size(124, 28);
			// 
			// extractToolStripMenuItem
			// 
			extractToolStripMenuItem.Name = "extractToolStripMenuItem";
			extractToolStripMenuItem.Size = new Size(123, 24);
			extractToolStripMenuItem.Text = "Extract";
			extractToolStripMenuItem.Click += ExtractFiles;
			// 
			// dgv_AddedEntryList
			// 
			dgv_AddedEntryList.AllowUserToAddRows = false;
			dgv_AddedEntryList.AllowUserToDeleteRows = false;
			dgv_AddedEntryList.AllowUserToResizeColumns = false;
			dgv_AddedEntryList.AllowUserToResizeRows = false;
			dataGridViewCellStyle6.BackColor = Color.FromArgb(40, 40, 40);
			dataGridViewCellStyle6.ForeColor = Color.White;
			dataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText;
			dgv_AddedEntryList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle6;
			dgv_AddedEntryList.AutoGenerateColumns = false;
			dgv_AddedEntryList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
			dgv_AddedEntryList.BackgroundColor = Color.FromArgb(31, 31, 31);
			dataGridViewCellStyle7.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle7.BackColor = Color.FromArgb(63, 63, 63);
			dataGridViewCellStyle7.Font = new Font("Segoe UI", 10F);
			dataGridViewCellStyle7.ForeColor = Color.White;
			dataGridViewCellStyle7.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle7.SelectionForeColor = SystemColors.HighlightText;
			dataGridViewCellStyle7.WrapMode = DataGridViewTriState.False;
			dgv_AddedEntryList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle7;
			dgv_AddedEntryList.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			dgv_AddedEntryList.Columns.AddRange(new DataGridViewColumn[] { AddedEntry_Directory, AddedEntry_Name, AddedEntry_Size, AddedEntry_Checksum, AddedEntry_Compress, AddedEntry_UseHeader });
			dgv_AddedEntryList.DataSource = bds_AddedEntries;
			dataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleCenter;
			dataGridViewCellStyle8.BackColor = Color.FromArgb(31, 31, 31);
			dataGridViewCellStyle8.Font = new Font("Segoe UI", 10F);
			dataGridViewCellStyle8.ForeColor = Color.White;
			dataGridViewCellStyle8.SelectionBackColor = SystemColors.Highlight;
			dataGridViewCellStyle8.SelectionForeColor = Color.White;
			dataGridViewCellStyle8.WrapMode = DataGridViewTriState.False;
			dgv_AddedEntryList.DefaultCellStyle = dataGridViewCellStyle8;
			dgv_AddedEntryList.Dock = DockStyle.Fill;
			dgv_AddedEntryList.EnableHeadersVisualStyles = false;
			dgv_AddedEntryList.GridColor = Color.White;
			dgv_AddedEntryList.Location = new Point(3, 371);
			dgv_AddedEntryList.Name = "dgv_AddedEntryList";
			dgv_AddedEntryList.ReadOnly = true;
			dgv_AddedEntryList.RowHeadersVisible = false;
			dgv_AddedEntryList.RowHeadersWidth = 51;
			dgv_AddedEntryList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			dgv_AddedEntryList.Size = new Size(835, 206);
			dgv_AddedEntryList.TabIndex = 1;
			dgv_AddedEntryList.KeyDown += DGV_AddedEntryList_KeyDown;
			// 
			// AddedEntry_Directory
			// 
			AddedEntry_Directory.HeaderText = "Directory";
			AddedEntry_Directory.MinimumWidth = 6;
			AddedEntry_Directory.Name = "AddedEntry_Directory";
			AddedEntry_Directory.ReadOnly = true;
			AddedEntry_Directory.Width = 108;
			// 
			// AddedEntry_Name
			// 
			AddedEntry_Name.HeaderText = "Name";
			AddedEntry_Name.MinimumWidth = 6;
			AddedEntry_Name.Name = "AddedEntry_Name";
			AddedEntry_Name.ReadOnly = true;
			AddedEntry_Name.Width = 85;
			// 
			// AddedEntry_Size
			// 
			AddedEntry_Size.HeaderText = "Size";
			AddedEntry_Size.MinimumWidth = 6;
			AddedEntry_Size.Name = "AddedEntry_Size";
			AddedEntry_Size.ReadOnly = true;
			AddedEntry_Size.Width = 69;
			// 
			// AddedEntry_Checksum
			// 
			AddedEntry_Checksum.HeaderText = "Checksum";
			AddedEntry_Checksum.MinimumWidth = 6;
			AddedEntry_Checksum.Name = "AddedEntry_Checksum";
			AddedEntry_Checksum.ReadOnly = true;
			AddedEntry_Checksum.Width = 117;
			// 
			// AddedEntry_Compress
			// 
			AddedEntry_Compress.HeaderText = "Compress?";
			AddedEntry_Compress.MinimumWidth = 6;
			AddedEntry_Compress.Name = "AddedEntry_Compress";
			AddedEntry_Compress.ReadOnly = true;
			AddedEntry_Compress.Width = 122;
			// 
			// AddedEntry_UseHeader
			// 
			AddedEntry_UseHeader.HeaderText = "Use Header?";
			AddedEntry_UseHeader.MinimumWidth = 6;
			AddedEntry_UseHeader.Name = "AddedEntry_UseHeader";
			AddedEntry_UseHeader.ReadOnly = true;
			AddedEntry_UseHeader.Width = 135;
			// 
			// tlp_PropertiesContainer
			// 
			tlp_PropertiesContainer.AutoSize = true;
			tlp_PropertiesContainer.BackColor = Color.FromArgb(48, 56, 65);
			tlp_PropertiesContainer.ColumnCount = 9;
			tlp_PropertiesContainer.ColumnStyles.Add(new ColumnStyle());
			tlp_PropertiesContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
			tlp_PropertiesContainer.ColumnStyles.Add(new ColumnStyle());
			tlp_PropertiesContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
			tlp_PropertiesContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
			tlp_PropertiesContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
			tlp_PropertiesContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
			tlp_PropertiesContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 20F));
			tlp_PropertiesContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 40F));
			tlp_PropertiesContainer.Controls.Add(tlp_RawSizeContainer, 8, 0);
			tlp_PropertiesContainer.Controls.Add(tlp_CompressedSizeContainer, 6, 0);
			tlp_PropertiesContainer.Controls.Add(tlp_VersionContainer, 4, 0);
			tlp_PropertiesContainer.Controls.Add(btn_SaveAs, 2, 0);
			tlp_PropertiesContainer.Controls.Add(btn_Browse, 0, 0);
			tlp_PropertiesContainer.Dock = DockStyle.Fill;
			tlp_PropertiesContainer.Location = new Point(3, 36);
			tlp_PropertiesContainer.Name = "tlp_PropertiesContainer";
			tlp_PropertiesContainer.RowCount = 1;
			tlp_PropertiesContainer.RowStyles.Add(new RowStyle());
			tlp_PropertiesContainer.Size = new Size(835, 39);
			tlp_PropertiesContainer.TabIndex = 2;
			// 
			// tlp_RawSizeContainer
			// 
			tlp_RawSizeContainer.AutoSize = true;
			tlp_RawSizeContainer.ColumnCount = 2;
			tlp_RawSizeContainer.ColumnStyles.Add(new ColumnStyle());
			tlp_RawSizeContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tlp_RawSizeContainer.Controls.Add(lbl_RawSize, 0, 0);
			tlp_RawSizeContainer.Controls.Add(tbx_RawSize, 1, 0);
			tlp_RawSizeContainer.Dock = DockStyle.Fill;
			tlp_RawSizeContainer.Location = new Point(616, 3);
			tlp_RawSizeContainer.Name = "tlp_RawSizeContainer";
			tlp_RawSizeContainer.RowCount = 1;
			tlp_RawSizeContainer.RowStyles.Add(new RowStyle());
			tlp_RawSizeContainer.Size = new Size(216, 33);
			tlp_RawSizeContainer.TabIndex = 6;
			// 
			// lbl_RawSize
			// 
			lbl_RawSize.Anchor = AnchorStyles.Left;
			lbl_RawSize.AutoSize = true;
			lbl_RawSize.BackColor = Color.FromArgb(48, 56, 65);
			lbl_RawSize.Location = new Point(3, 6);
			lbl_RawSize.Name = "lbl_RawSize";
			lbl_RawSize.Size = new Size(71, 20);
			lbl_RawSize.TabIndex = 0;
			lbl_RawSize.Text = "Raw Size:";
			// 
			// tbx_RawSize
			// 
			tbx_RawSize.BackColor = Color.FromArgb(48, 56, 65);
			tbx_RawSize.BorderStyle = BorderStyle.None;
			tbx_RawSize.Dock = DockStyle.Fill;
			tbx_RawSize.ForeColor = Color.White;
			tbx_RawSize.Location = new Point(80, 7);
			tbx_RawSize.Margin = new Padding(3, 7, 3, 3);
			tbx_RawSize.Name = "tbx_RawSize";
			tbx_RawSize.ReadOnly = true;
			tbx_RawSize.Size = new Size(133, 20);
			tbx_RawSize.TabIndex = 6;
			tbx_RawSize.Text = "0";
			tbx_RawSize.WordWrap = false;
			// 
			// tlp_CompressedSizeContainer
			// 
			tlp_CompressedSizeContainer.AutoSize = true;
			tlp_CompressedSizeContainer.ColumnCount = 2;
			tlp_CompressedSizeContainer.ColumnStyles.Add(new ColumnStyle());
			tlp_CompressedSizeContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tlp_CompressedSizeContainer.Controls.Add(tbx_CompressedSize, 1, 0);
			tlp_CompressedSizeContainer.Controls.Add(lbl_CompressedSize, 0, 0);
			tlp_CompressedSizeContainer.Dock = DockStyle.Fill;
			tlp_CompressedSizeContainer.Location = new Point(374, 3);
			tlp_CompressedSizeContainer.Name = "tlp_CompressedSizeContainer";
			tlp_CompressedSizeContainer.RowCount = 1;
			tlp_CompressedSizeContainer.RowStyles.Add(new RowStyle());
			tlp_CompressedSizeContainer.Size = new Size(216, 33);
			tlp_CompressedSizeContainer.TabIndex = 5;
			// 
			// tbx_CompressedSize
			// 
			tbx_CompressedSize.BackColor = Color.FromArgb(48, 56, 65);
			tbx_CompressedSize.BorderStyle = BorderStyle.None;
			tbx_CompressedSize.Dock = DockStyle.Fill;
			tbx_CompressedSize.ForeColor = Color.White;
			tbx_CompressedSize.Location = new Point(134, 7);
			tbx_CompressedSize.Margin = new Padding(3, 7, 3, 3);
			tbx_CompressedSize.Name = "tbx_CompressedSize";
			tbx_CompressedSize.ReadOnly = true;
			tbx_CompressedSize.Size = new Size(79, 20);
			tbx_CompressedSize.TabIndex = 6;
			tbx_CompressedSize.Text = "0";
			tbx_CompressedSize.WordWrap = false;
			// 
			// lbl_CompressedSize
			// 
			lbl_CompressedSize.Anchor = AnchorStyles.Left;
			lbl_CompressedSize.AutoSize = true;
			lbl_CompressedSize.BackColor = Color.FromArgb(48, 56, 65);
			lbl_CompressedSize.Location = new Point(3, 6);
			lbl_CompressedSize.Name = "lbl_CompressedSize";
			lbl_CompressedSize.Size = new Size(125, 20);
			lbl_CompressedSize.TabIndex = 0;
			lbl_CompressedSize.Text = "Compressed Size:";
			// 
			// tlp_VersionContainer
			// 
			tlp_VersionContainer.AutoSize = true;
			tlp_VersionContainer.ColumnCount = 2;
			tlp_VersionContainer.ColumnStyles.Add(new ColumnStyle());
			tlp_VersionContainer.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tlp_VersionContainer.Controls.Add(tbx_Version, 1, 0);
			tlp_VersionContainer.Controls.Add(lbl_Version, 0, 0);
			tlp_VersionContainer.Dock = DockStyle.Fill;
			tlp_VersionContainer.Location = new Point(243, 3);
			tlp_VersionContainer.Name = "tlp_VersionContainer";
			tlp_VersionContainer.RowCount = 1;
			tlp_VersionContainer.RowStyles.Add(new RowStyle());
			tlp_VersionContainer.Size = new Size(105, 33);
			tlp_VersionContainer.TabIndex = 3;
			// 
			// tbx_Version
			// 
			tbx_Version.BackColor = Color.FromArgb(31, 31, 31);
			tbx_Version.Dock = DockStyle.Fill;
			tbx_Version.ForeColor = Color.White;
			tbx_Version.Location = new Point(69, 3);
			tbx_Version.Name = "tbx_Version";
			tbx_Version.Size = new Size(33, 27);
			tbx_Version.TabIndex = 6;
			tbx_Version.Text = "7";
			tbx_Version.TextAlign = HorizontalAlignment.Center;
			tbx_Version.WordWrap = false;
			// 
			// lbl_Version
			// 
			lbl_Version.Anchor = AnchorStyles.Left;
			lbl_Version.AutoSize = true;
			lbl_Version.BackColor = Color.FromArgb(48, 56, 65);
			lbl_Version.Location = new Point(3, 6);
			lbl_Version.Name = "lbl_Version";
			lbl_Version.Size = new Size(60, 20);
			lbl_Version.TabIndex = 0;
			lbl_Version.Text = "Version:";
			// 
			// btn_SaveAs
			// 
			btn_SaveAs.Anchor = AnchorStyles.Top;
			btn_SaveAs.ForeColor = Color.Black;
			btn_SaveAs.Location = new Point(123, 3);
			btn_SaveAs.Name = "btn_SaveAs";
			btn_SaveAs.Size = new Size(94, 29);
			btn_SaveAs.TabIndex = 1;
			btn_SaveAs.Text = "Save As";
			btn_SaveAs.UseVisualStyleBackColor = true;
			btn_SaveAs.Click += SaveAs;
			// 
			// btn_Browse
			// 
			btn_Browse.Anchor = AnchorStyles.Top;
			btn_Browse.BackColor = Color.White;
			btn_Browse.ForeColor = Color.Black;
			btn_Browse.Location = new Point(3, 3);
			btn_Browse.Name = "btn_Browse";
			btn_Browse.Size = new Size(94, 29);
			btn_Browse.TabIndex = 0;
			btn_Browse.Text = "Browse";
			btn_Browse.UseVisualStyleBackColor = false;
			btn_Browse.Click += BrowseArchive;
			// 
			// tbx_LoadedArchive
			// 
			tbx_LoadedArchive.BackColor = Color.FromArgb(48, 56, 65);
			tbx_LoadedArchive.Dock = DockStyle.Fill;
			tbx_LoadedArchive.ForeColor = Color.White;
			tbx_LoadedArchive.Location = new Point(3, 3);
			tbx_LoadedArchive.Name = "tbx_LoadedArchive";
			tbx_LoadedArchive.ReadOnly = true;
			tbx_LoadedArchive.Size = new Size(835, 27);
			tbx_LoadedArchive.TabIndex = 4;
			tbx_LoadedArchive.WordWrap = false;
			// 
			// tableLayoutPanel1
			// 
			tableLayoutPanel1.AutoSize = true;
			tableLayoutPanel1.ColumnCount = 2;
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel1.Controls.Add(flp_ArchiveTextContainer, 0, 0);
			tableLayoutPanel1.Controls.Add(tbx_SearchArchiveEntries, 1, 0);
			tableLayoutPanel1.Dock = DockStyle.Fill;
			tableLayoutPanel1.Location = new Point(3, 81);
			tableLayoutPanel1.Name = "tableLayoutPanel1";
			tableLayoutPanel1.RowCount = 1;
			tableLayoutPanel1.RowStyles.Add(new RowStyle());
			tableLayoutPanel1.Size = new Size(835, 33);
			tableLayoutPanel1.TabIndex = 5;
			// 
			// flp_ArchiveTextContainer
			// 
			flp_ArchiveTextContainer.Controls.Add(cbx_ToggleArchiveEntriesList);
			flp_ArchiveTextContainer.Controls.Add(lbl_ArchiveEntryCount);
			flp_ArchiveTextContainer.Controls.Add(lbl_ArchiveEntriesText);
			flp_ArchiveTextContainer.Location = new Point(3, 3);
			flp_ArchiveTextContainer.Name = "flp_ArchiveTextContainer";
			flp_ArchiveTextContainer.Size = new Size(159, 23);
			flp_ArchiveTextContainer.TabIndex = 0;
			// 
			// cbx_ToggleArchiveEntriesList
			// 
			cbx_ToggleArchiveEntriesList.AutoSize = true;
			cbx_ToggleArchiveEntriesList.Checked = true;
			cbx_ToggleArchiveEntriesList.CheckState = CheckState.Checked;
			cbx_ToggleArchiveEntriesList.Location = new Point(3, 3);
			cbx_ToggleArchiveEntriesList.Name = "cbx_ToggleArchiveEntriesList";
			cbx_ToggleArchiveEntriesList.Size = new Size(18, 17);
			cbx_ToggleArchiveEntriesList.TabIndex = 4;
			cbx_ToggleArchiveEntriesList.UseVisualStyleBackColor = true;
			cbx_ToggleArchiveEntriesList.CheckedChanged += Toggle_ArchiveEntryListContainer;
			// 
			// lbl_ArchiveEntryCount
			// 
			lbl_ArchiveEntryCount.AutoSize = true;
			lbl_ArchiveEntryCount.Location = new Point(27, 0);
			lbl_ArchiveEntryCount.Name = "lbl_ArchiveEntryCount";
			lbl_ArchiveEntryCount.Size = new Size(17, 20);
			lbl_ArchiveEntryCount.TabIndex = 2;
			lbl_ArchiveEntryCount.Text = "0";
			// 
			// lbl_ArchiveEntriesText
			// 
			lbl_ArchiveEntriesText.AutoSize = true;
			lbl_ArchiveEntriesText.Location = new Point(50, 0);
			lbl_ArchiveEntriesText.Name = "lbl_ArchiveEntriesText";
			lbl_ArchiveEntriesText.Size = new Size(106, 20);
			lbl_ArchiveEntriesText.TabIndex = 0;
			lbl_ArchiveEntriesText.Text = "Archive Entries";
			// 
			// tbx_SearchArchiveEntries
			// 
			tbx_SearchArchiveEntries.BackColor = Color.FromArgb(31, 31, 31);
			tbx_SearchArchiveEntries.Dock = DockStyle.Fill;
			tbx_SearchArchiveEntries.ForeColor = Color.White;
			tbx_SearchArchiveEntries.Location = new Point(168, 3);
			tbx_SearchArchiveEntries.Name = "tbx_SearchArchiveEntries";
			tbx_SearchArchiveEntries.Size = new Size(664, 27);
			tbx_SearchArchiveEntries.TabIndex = 1;
			tbx_SearchArchiveEntries.WordWrap = false;
			tbx_SearchArchiveEntries.KeyDown += TBX_SearchArchiveEntries_KeyDown;
			// 
			// tableLayoutPanel2
			// 
			tableLayoutPanel2.AutoSize = true;
			tableLayoutPanel2.ColumnCount = 2;
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle());
			tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			tableLayoutPanel2.Controls.Add(tbx_SearchAddedEntries, 1, 0);
			tableLayoutPanel2.Controls.Add(flp_AddedEntriesTextContainer, 0, 0);
			tableLayoutPanel2.Dock = DockStyle.Fill;
			tableLayoutPanel2.Location = new Point(3, 332);
			tableLayoutPanel2.Name = "tableLayoutPanel2";
			tableLayoutPanel2.RowCount = 1;
			tableLayoutPanel2.RowStyles.Add(new RowStyle());
			tableLayoutPanel2.Size = new Size(835, 33);
			tableLayoutPanel2.TabIndex = 6;
			// 
			// tbx_SearchAddedEntries
			// 
			tbx_SearchAddedEntries.BackColor = Color.FromArgb(31, 31, 31);
			tbx_SearchAddedEntries.Dock = DockStyle.Fill;
			tbx_SearchAddedEntries.ForeColor = Color.White;
			tbx_SearchAddedEntries.Location = new Point(164, 3);
			tbx_SearchAddedEntries.Name = "tbx_SearchAddedEntries";
			tbx_SearchAddedEntries.Size = new Size(668, 27);
			tbx_SearchAddedEntries.TabIndex = 2;
			tbx_SearchAddedEntries.WordWrap = false;
			// 
			// flp_AddedEntriesTextContainer
			// 
			flp_AddedEntriesTextContainer.Controls.Add(cbx_ToggleAddedEntriesList);
			flp_AddedEntriesTextContainer.Controls.Add(lbl_AddedEntryCount);
			flp_AddedEntriesTextContainer.Controls.Add(lbl_AddedEntriesText);
			flp_AddedEntriesTextContainer.Location = new Point(3, 3);
			flp_AddedEntriesTextContainer.Name = "flp_AddedEntriesTextContainer";
			flp_AddedEntriesTextContainer.Size = new Size(155, 23);
			flp_AddedEntriesTextContainer.TabIndex = 0;
			// 
			// cbx_ToggleAddedEntriesList
			// 
			cbx_ToggleAddedEntriesList.AutoSize = true;
			cbx_ToggleAddedEntriesList.Checked = true;
			cbx_ToggleAddedEntriesList.CheckState = CheckState.Checked;
			cbx_ToggleAddedEntriesList.Location = new Point(3, 3);
			cbx_ToggleAddedEntriesList.Name = "cbx_ToggleAddedEntriesList";
			cbx_ToggleAddedEntriesList.Size = new Size(18, 17);
			cbx_ToggleAddedEntriesList.TabIndex = 3;
			cbx_ToggleAddedEntriesList.UseVisualStyleBackColor = true;
			cbx_ToggleAddedEntriesList.CheckedChanged += Toggle_AddedEntryListContainer;
			// 
			// lbl_AddedEntryCount
			// 
			lbl_AddedEntryCount.AutoSize = true;
			lbl_AddedEntryCount.Location = new Point(27, 0);
			lbl_AddedEntryCount.Name = "lbl_AddedEntryCount";
			lbl_AddedEntryCount.Size = new Size(17, 20);
			lbl_AddedEntryCount.TabIndex = 2;
			lbl_AddedEntryCount.Text = "0";
			// 
			// lbl_AddedEntriesText
			// 
			lbl_AddedEntriesText.AutoSize = true;
			lbl_AddedEntriesText.Location = new Point(50, 0);
			lbl_AddedEntriesText.Name = "lbl_AddedEntriesText";
			lbl_AddedEntriesText.Size = new Size(102, 20);
			lbl_AddedEntriesText.TabIndex = 0;
			lbl_AddedEntriesText.Text = "Added Entries";
			// 
			// ttp_SearchQueryInfo
			// 
			ttp_SearchQueryInfo.BackColor = Color.FromArgb(31, 31, 31);
			ttp_SearchQueryInfo.ForeColor = Color.White;
			// 
			// MainGUI
			// 
			AutoScaleDimensions = new SizeF(8F, 20F);
			AutoScaleMode = AutoScaleMode.Font;
			BackColor = Color.FromArgb(31, 31, 31);
			ClientSize = new Size(841, 580);
			Controls.Add(tlp_OuterContainer);
			ForeColor = Color.White;
			Name = "MainGUI";
			Text = "AJOM's H2O Master beta v0.0.0";
			Load += GUI_Load;
			tlp_OuterContainer.ResumeLayout(false);
			tlp_OuterContainer.PerformLayout();
			((System.ComponentModel.ISupportInitialize)dgv_ArchiveEntryList).EndInit();
			cms_ArchiveEntries.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)bds_ArchiveEntries).EndInit();
			((System.ComponentModel.ISupportInitialize)dgv_AddedEntryList).EndInit();
			((System.ComponentModel.ISupportInitialize)bds_AddedEntries).EndInit();
			tlp_PropertiesContainer.ResumeLayout(false);
			tlp_PropertiesContainer.PerformLayout();
			tlp_RawSizeContainer.ResumeLayout(false);
			tlp_RawSizeContainer.PerformLayout();
			tlp_CompressedSizeContainer.ResumeLayout(false);
			tlp_CompressedSizeContainer.PerformLayout();
			tlp_VersionContainer.ResumeLayout(false);
			tlp_VersionContainer.PerformLayout();
			tableLayoutPanel1.ResumeLayout(false);
			tableLayoutPanel1.PerformLayout();
			flp_ArchiveTextContainer.ResumeLayout(false);
			flp_ArchiveTextContainer.PerformLayout();
			tableLayoutPanel2.ResumeLayout(false);
			tableLayoutPanel2.PerformLayout();
			flp_AddedEntriesTextContainer.ResumeLayout(false);
			flp_AddedEntriesTextContainer.PerformLayout();
			ResumeLayout(false);
		}

		#endregion

		private TableLayoutPanel tlp_OuterContainer;
		private Button btn_Browse;
		private Button btn_SaveAs;
		private TableLayoutPanel tlp_PropertiesContainer;
		private TableLayoutPanel tlp_VersionContainer;
		private TextBox tbx_Version;
		private Label lbl_Version;
		private TableLayoutPanel tlp_RawSizeContainer;
		private TextBox tbx_RawSize;
		private Label lbl_RawSize;
		private TableLayoutPanel tlp_CompressedSizeContainer;
		private TextBox tbx_CompressedSize;
		private Label lbl_CompressedSize;
		private FlowLayoutPanel flp_ArchiveTextContainer;
		private Label lbl_ArchiveEntriesText;
		private Label lbl_ArchiveEntryCount;
		private DataGridView dgv_ArchiveEntryList;
		private FlowLayoutPanel flp_AddedEntriesTextContainer;
		private Label lbl_AddedEntryCount;
		private Label lbl_AddedEntriesText;
		private DataGridView dgv_AddedEntryList;
		private TextBox tbx_LoadedArchive;
		private CheckBox cbx_ToggleAddedEntriesList;
		private CheckBox cbx_ToggleArchiveEntriesList;
		private TableLayoutPanel tableLayoutPanel1;
		private TableLayoutPanel tableLayoutPanel2;
		private TextBox tbx_SearchArchiveEntries;
		private TextBox tbx_SearchAddedEntries;
		private ToolTip ttp_SearchQueryInfo;
		private BindingSource bds_ArchiveEntries;
		private BindingSource bds_AddedEntries;
		private DataGridViewTextBoxColumn AddedEntry_Directory;
		private DataGridViewTextBoxColumn AddedEntry_Name;
		private DataGridViewTextBoxColumn AddedEntry_Size;
		private DataGridViewTextBoxColumn AddedEntry_Checksum;
		private DataGridViewTextBoxColumn AddedEntry_Compress;
		private DataGridViewTextBoxColumn AddedEntry_UseHeader;
		private ContextMenuStrip cms_ArchiveEntries;
		private ToolStripMenuItem extractToolStripMenuItem;
		private DataGridViewTextBoxColumn ArchiveEntry_Index;
		private DataGridViewCheckBoxColumn ArchiveEntry_DeleteCheckBox;
		private DataGridViewTextBoxColumn ArchiveEntry_Directory;
		private DataGridViewTextBoxColumn ArchiveEntry_Name;
		private DataGridViewTextBoxColumn ArchiveEntry_Offset;
		private DataGridViewTextBoxColumn ArchiveEntry_CompressedSize;
		private DataGridViewTextBoxColumn ArchiveEntry_RawSize;
		private DataGridViewTextBoxColumn ArchiveEntry_Checksum;
		private DataGridViewTextBoxColumn ArchiveEntry_HasHeader;
	}
}
