using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseCompare
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            // Initialize the form
            InitializeComponent();

            // Initialize the database
            new MigrationManager();

        }
    }
}
