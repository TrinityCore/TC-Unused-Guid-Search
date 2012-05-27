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
            RandomRadio.Checked = true;
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            var selectedTable = TableComboBox.Text;

            MySqlDataReader reader = null;
            try
            {
                var connection = new MySqlConnection(_connectionString);
                connection.Open();
                var query = new MySqlCommand(string.Format("SELECT {0} FROM {1}", _supportedTables[selectedTable], selectedTable), connection);
                reader = query.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            var existingGuids = new List<int>();
            while (reader != null && reader.Read())
                existingGuids.Add(reader.GetInt32(0));

            var possibleGuids = Enumerable.Range(1, existingGuids.Last());
            IEnumerable<int> missingGuids = possibleGuids.Except(existingGuids);
            IEnumerable<int> selectedMissingGuids = null;

            if (!missingGuids.Any())
                selectedMissingGuids = Enumerable.Range(existingGuids.Last() + 1, (int)GuidCountUpDown.Value);
            else if (RandomRadio.Checked)
                selectedMissingGuids = missingGuids.Take((int)GuidCountUpDown.Value);
            else if (ConsecutiveRadio.Checked)
                selectedMissingGuids = GetConsecutiveGuids(missingGuids.ToArray(), (int)GuidCountUpDown.Value);

            var resultForm = new ResultForm(selectedMissingGuids);
            resultForm.Show();
        }

        private static IEnumerable<int> GetConsecutiveGuids(int[] input, int minimum)
        {
            var count = 1;

            var result = new List<int>();

            for (var i = 0; i < input.Length - 1; i++)
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
    }
}
