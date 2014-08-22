using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UnusedGuidSearcher
{
    public partial class MainForm : Form
    {
        // table name, primary key
        private readonly Dictionary<string, string> _supportedTables = new Dictionary<string, string>
        {
            {"`creature`", "`guid`"},
            {"`gameobject`", "`guid`"},
            {"`waypoint_scripts`", "`guid`"},
            {"`pool_template`", "`entry`"},
            {"`game_event`", "`eventEntry`"},
            {"`creature_equip_template`", "`entry`"},
            {"`trinity_string`", "`entry`"},
        };

        private static string _connectionString;

        public MainForm(string connectionString)
        {
            _connectionString = connectionString;
            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            // Defaults
            TableComboBox.Items.AddRange(_supportedTables.Keys.Cast<object>().ToArray());
            TableComboBox.Text = (string)TableComboBox.Items[0];
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            var selectedTable = TableComboBox.Text;

            var existingGuids = new List<int>();
            try
            {
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var query = new MySqlCommand(string.Format("SELECT {0} FROM {1}", _supportedTables[selectedTable], selectedTable), connection))
                        using (var reader = query.ExecuteReader())
                            while (reader != null && reader.Read())
                                existingGuids.Add(reader.GetInt32(0));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }

            var minGuid = Convert.ToInt32(minNumericUpDown.Value);
            var possibleGuids = Enumerable.Range(1, existingGuids.Last());
            var missingGuids = possibleGuids.Except(existingGuids).SkipWhile(i => i < minGuid).ToArray();
            IEnumerable<int> selectedMissingGuids;

            if (missingGuids.Length == 0)
                selectedMissingGuids = Enumerable.Range(Math.Max(existingGuids.Last() + 1, minGuid), (int)GuidCountUpDown.Value);
            else if (consecutiveCheckBox.Checked)
                selectedMissingGuids = GetConsecutiveGuids(missingGuids, (int) GuidCountUpDown.Value) ??
                                       Enumerable.Range(Math.Max(existingGuids.Last() + 1, minGuid), (int)GuidCountUpDown.Value);
            else
                selectedMissingGuids = missingGuids.Take((int)GuidCountUpDown.Value);

            var resultForm = new ResultForm(selectedMissingGuids);
            resultForm.Show();
        }

        private static IEnumerable<int> GetConsecutiveGuids(IList<int> input, int minimum)
        {
            var count = 1;

            var result = new List<int>();

            for (var i = 0; i < input.Count - 1; i++)
            {
                if (input[i] == input[i + 1] - 1)
                {
                    count++;
                    result.Add(input[i]);
                }
                else
                {
                    result = new List<int>();
                    count = 1;
                }

                if (count > minimum)
                    return result;
            }

            return result.Count < minimum ? null : result;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
