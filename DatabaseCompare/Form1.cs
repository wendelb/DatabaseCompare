using DatabaseCompare.RemoteDataProvider;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseCompare
{
    public partial class FormMain : Form
    {
        /// <summary>
        /// Instance of the filter class
        /// </summary>
        private DisplayFilter filter;

        public FormMain()
        {
            // Initialize the form
            InitializeComponent();

            // Initialize the database
            try
            {
                new MigrationManager();
                this.db = new DataContext();
                this.filter = new DisplayFilter(this.db.Columns);
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured during initialization:" + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace, "Database Compare: Failed to load from database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(1);
            }
        }

        /// <summary>
        /// Private reference to the Database Connection
        /// </summary>
        private DataContext db;

        /// <summary>
        /// Action Handler for the Click Event on the <i>Fiel</i> -> <i>Quit</i> Menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Action Handler for the Click Event on the <i>Filter</i> -> <i>Filter Results</i> Menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void filterResultsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (FormFilter formFilter = new FormFilter(db))
            {
                if (formFilter.ShowDialog() == DialogResult.OK)
                {
                    filter.filterSchema = formFilter.filterSchema;
                    filter.filterTable = formFilter.filterTable;
                    filter.filterColumn = formFilter.filterColumn;
                    filter.filterForDifferences = false;

                    // Update Checkboxes
                    showAllFieldsToolStripMenuItem.Checked = false;
                    filterResultsToolStripMenuItem.Checked = true;
                    showDifferencesToolStripMenuItem.Checked = false;

                    // After the filter has been updated, refresh the display
                    RefreshFieldsView();
                }
            }
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            // Display all Data on Load
            RefreshFieldsView();
        }

        /// <summary>
        /// Clear and load the filtered data into the main control.
        /// Also takes care of column resizing
        /// </summary>
        private void RefreshFieldsView()
        {
            FieldsView.BeginUpdate();

            FieldsView.Items.Clear();
            foreach (var field in filter.applyFilter())
            {
                string[] data = { field.DatabaseName, field.Schema, field.TableName, field.ColumnName, field.DataType };
                FieldsView.Items.Add(new ListViewItem(data));
            }

            FieldsView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            FieldsView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

            FieldsView.EndUpdate();
        }

        /// <summary>
        /// Action Handler for the Click Event on the <i>Remote Database</i> -> <i>Refresh Fields</i> Menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void refreshFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "Loading Databases...";
            try
            {
                var DataPrivoder = DataProviderFactory.createDataProvider(this.db);
                //DataPrivoder.OnMaxKnown
                DataPrivoder.OnProgress = ((i, database) => { toolStripStatusLabel.Text = "Loading Columns from " + database; });
                DataPrivoder.OnSave = (() => { toolStripStatusLabel.Text = "Saving..."; });

                await Task.Run(() => DataPrivoder.RefreshColumns());
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occured while loading data from the remote database" + Environment.NewLine + ex.Message + Environment.NewLine + Environment.NewLine + ex.StackTrace, "Database Compare: Failed to load from remote database", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                toolStripStatusLabel.Text = "Ready";
                RefreshFieldsView();
            }
        }

        /// <summary>
        /// Action Handler for the Click Event on the <i>Filter</i> -> <i>Show all Fields</i> Menu Item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void showAllFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Update Checkboxes
            showAllFieldsToolStripMenuItem.Checked = true;
            filterResultsToolStripMenuItem.Checked = false;
            showDifferencesToolStripMenuItem.Checked = false;

            // Clear the filter
            filter.Clear();

            // Refresh the View
            RefreshFieldsView();
        }

        private void showDifferencesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Update Checkboxes
            showAllFieldsToolStripMenuItem.Checked = false;
            filterResultsToolStripMenuItem.Checked = false;
            showDifferencesToolStripMenuItem.Checked = true;

            // Tell the filter
            filter.filterForDifferences = true;

            // Refresh the View
            RefreshFieldsView();
        }
    }
}
