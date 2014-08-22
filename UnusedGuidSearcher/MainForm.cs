using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UnusedGuidSearcher
{
    public partial class MainForm : Form
    {
        private static string _connectionString;

        public MainForm(string connectionString)
        {
            _connectionString = connectionString;
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            // Defaults
            TableComboBox.Items.AddRange(SearchGuids.GetSupportedTableNames().Cast<object>().ToArray());
            TableComboBox.SelectedIndex = 0;
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            var options = new SearchGuids.Options
            {
                ConnectionString = _connectionString,
                Consecutive = consecutiveCheckBox.Checked,
                Count = (int) GuidCountUpDown.Value,
                Minimum = (int) minNumericUpDown.Value,
                Table = TableComboBox.Text
            };

            IEnumerable<int> guids;

            try
            {
                guids = SearchGuids.Search(options);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            var resultForm = new ResultForm(guids);
            resultForm.Show();
        }
    }
}
