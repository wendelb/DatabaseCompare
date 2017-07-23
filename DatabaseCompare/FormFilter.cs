using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace DatabaseCompare
{
    public partial class FormFilter : Form
    {
        private DataContext db;

        public FormFilter(DataContext db)
        {
            this.db = db;
            InitializeComponent();
        }

        private void FormFilter_Load(object sender, EventArgs e)
        {
            populateSchema();
        }

        private void populateSchema()
        {
            // Clear the schema ComboBox
            CBxSchema.Items.Clear();

            // Reset all downstream Comboboxes
            CBxTable.Items.Clear();
            CBxTable.SelectedItem = null;
            CBxTable.Enabled = false;

            CBxField.Items.Clear();
            CBxField.SelectedItem = null;
            CBxField.Enabled = false;

            // Fetch new Data
            var query = db.Fields.Select(element => new
            {
                Schema = (string)element.Schema
            }).Distinct();

            foreach (var s in query)
            {
                CBxSchema.Items.Add(s.Schema);
            }

            // If there is only 1 Schema, skip to next filter
            if (query.Count() == 1)
            {
                CBxSchema.SelectedItem = CBxSchema.Items[0];
                populateTable();
            }
        }

        private void populateTable()
        {
            string schema = CBxSchema.SelectedItem.ToString();

            CBxTable.Items.Clear();
            CBxTable.SelectedItem = null;

            // Reset all downstream Comboboxes
            CBxField.Items.Clear();
            CBxField.SelectedItem = null;
            CBxField.Enabled = false;

            // Fetch new Data
            var query = db.Fields.Where(d => (d.Schema == schema)).Select(element => new
            {
                TableName = (string)element.TableName
            }).Distinct();

            foreach (var s in query)
            {
                CBxTable.Items.Add(s.TableName);
            }

            CBxTable.Enabled = true;

            // If there is only 1 Table, skip to next filter
            if (query.Count() == 1)
            {
                CBxTable.SelectedItem = CBxTable.Items[0];
                populateField();
            }

        }

        private void populateField()
        {
            string schema = CBxSchema.SelectedItem.ToString();
            string table = CBxTable.SelectedItem.ToString();

            CBxField.Items.Clear();
            CBxField.SelectedItem = null;
            // There is no downstream Combobox to reset

            // Fetch new data
            var query = db.Fields.Where(d => ((d.Schema == schema) && (d.TableName == table))).Select(element => new
            {
                FieldName = (string)element.FieldName
            }).Distinct();

            foreach (var s in query)
            {
                CBxField.Items.Add(s.FieldName);
            }

            CBxField.Enabled = true;

            // If there is only 1 Table, put it into the Combobox
            if (query.Count() == 1)
            {
                CBxField.SelectedItem = CBxField.Items[0];
            }
        }

        private void CBxSchema_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateTable();
        }

        private void CBxTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            populateField();
        }
    }
}
