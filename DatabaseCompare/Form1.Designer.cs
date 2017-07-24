namespace DatabaseCompare
{
    partial class FormMain
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.remoteDatabaseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.filterResultsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDifferencesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showAllFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FieldsView = new System.Windows.Forms.ListView();
            this.columnHeaderDatabase = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderSchema = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderTable = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderField = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeaderDataType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.remoteDatabaseToolStripMenuItem,
            this.filterToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(910, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // quitToolStripMenuItem
            // 
            this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
            this.quitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.quitToolStripMenuItem.Size = new System.Drawing.Size(139, 22);
            this.quitToolStripMenuItem.Text = "&Quit";
            this.quitToolStripMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // remoteDatabaseToolStripMenuItem
            // 
            this.remoteDatabaseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshFieldsToolStripMenuItem});
            this.remoteDatabaseToolStripMenuItem.Name = "remoteDatabaseToolStripMenuItem";
            this.remoteDatabaseToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.remoteDatabaseToolStripMenuItem.Text = "Remote &Database";
            // 
            // refreshFieldsToolStripMenuItem
            // 
            this.refreshFieldsToolStripMenuItem.Name = "refreshFieldsToolStripMenuItem";
            this.refreshFieldsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.refreshFieldsToolStripMenuItem.Size = new System.Drawing.Size(189, 22);
            this.refreshFieldsToolStripMenuItem.Text = "&Refresh Fields";
            this.refreshFieldsToolStripMenuItem.Click += new System.EventHandler(this.refreshFieldsToolStripMenuItem_Click);
            // 
            // filterToolStripMenuItem
            // 
            this.filterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.filterResultsToolStripMenuItem,
            this.showDifferencesToolStripMenuItem,
            this.showAllFieldsToolStripMenuItem});
            this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
            this.filterToolStripMenuItem.Size = new System.Drawing.Size(45, 20);
            this.filterToolStripMenuItem.Text = "F&ilter";
            // 
            // filterResultsToolStripMenuItem
            // 
            this.filterResultsToolStripMenuItem.Name = "filterResultsToolStripMenuItem";
            this.filterResultsToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.filterResultsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.filterResultsToolStripMenuItem.Text = "Filter &Results";
            this.filterResultsToolStripMenuItem.Click += new System.EventHandler(this.filterResultsToolStripMenuItem_Click);
            // 
            // showDifferencesToolStripMenuItem
            // 
            this.showDifferencesToolStripMenuItem.Name = "showDifferencesToolStripMenuItem";
            this.showDifferencesToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.showDifferencesToolStripMenuItem.Text = "Show Differences";
            this.showDifferencesToolStripMenuItem.Click += new System.EventHandler(this.showDifferencesToolStripMenuItem_Click);
            // 
            // showAllFieldsToolStripMenuItem
            // 
            this.showAllFieldsToolStripMenuItem.Checked = true;
            this.showAllFieldsToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showAllFieldsToolStripMenuItem.Name = "showAllFieldsToolStripMenuItem";
            this.showAllFieldsToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.showAllFieldsToolStripMenuItem.Text = "&Show all Fields";
            this.showAllFieldsToolStripMenuItem.Click += new System.EventHandler(this.showAllFieldsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 432);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(910, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(39, 17);
            this.toolStripStatusLabel.Text = "Ready";
            // 
            // FieldsView
            // 
            this.FieldsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeaderDatabase,
            this.columnHeaderSchema,
            this.columnHeaderTable,
            this.columnHeaderField,
            this.columnHeaderDataType});
            this.FieldsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FieldsView.FullRowSelect = true;
            this.FieldsView.GridLines = true;
            this.FieldsView.Location = new System.Drawing.Point(0, 24);
            this.FieldsView.MultiSelect = false;
            this.FieldsView.Name = "FieldsView";
            this.FieldsView.Size = new System.Drawing.Size(910, 408);
            this.FieldsView.TabIndex = 2;
            this.FieldsView.UseCompatibleStateImageBehavior = false;
            this.FieldsView.View = System.Windows.Forms.View.Details;
            // 
            // columnHeaderDatabase
            // 
            this.columnHeaderDatabase.Text = "Database";
            // 
            // columnHeaderSchema
            // 
            this.columnHeaderSchema.Text = "Schema";
            // 
            // columnHeaderTable
            // 
            this.columnHeaderTable.Text = "Table";
            // 
            // columnHeaderField
            // 
            this.columnHeaderField.Text = "Field";
            // 
            // columnHeaderDataType
            // 
            this.columnHeaderDataType.Text = "DataType";
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 454);
            this.Controls.Add(this.FieldsView);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMain";
            this.Text = "Database Compare";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem remoteDatabaseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshFieldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem filterResultsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDifferencesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showAllFieldsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ListView FieldsView;
        private System.Windows.Forms.ColumnHeader columnHeaderDatabase;
        private System.Windows.Forms.ColumnHeader columnHeaderSchema;
        private System.Windows.Forms.ColumnHeader columnHeaderTable;
        private System.Windows.Forms.ColumnHeader columnHeaderField;
        private System.Windows.Forms.ColumnHeader columnHeaderDataType;
    }
}

