using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UnusedGuidSearcher
{
    public partial class MainForm : Form
    {
        private readonly object[] _supportedTables = {"`creature`", "`gameobject`", "`waypoint_scripts`"};

        private readonly string _username;
        private readonly string _password;
        private readonly string  _database;
        private readonly string _host;
        private static string _connectionString;

        public MainForm(string username, string password, string database, string host)
        {
            _username = username;
            _password = password;
            _database = database;
            _host = host;

            InitializeComponent();
        }

        private void MainFormLoad(object sender, EventArgs e)
        {
            // Defaults
            TableComboBox.Items.AddRange(_supportedTables);
            TableComboBox.Text = _supportedTables[0] as string;
            RandomRadio.Checked = true;

            _connectionString =
                string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", _host, _database, _username, _password);

            TestConnection();
        }

        private static void TestConnection()
        {
            MySqlConnection connection = null;
            try
            {
                connection = new MySqlConnection(_connectionString);
                connection.Open();
            }
            catch (Exception ex)
            {
// ReSharper disable LocalizableElement
                MessageBox.Show(ex.Message, "Could not connect.", MessageBoxButtons.RetryCancel,
                    MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
// ReSharper restore LocalizableElement
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }
        }

        private void GoButtonClick(object sender, EventArgs e)
        {
            var selectedTable = TableComboBox.Text;

            MySqlDataReader reader = null;
            try
            {
                var connection = new MySqlConnection(_connectionString);
                connection.Open();
                var query = new MySqlCommand(string.Format("SELECT `guid` FROM {0}", selectedTable), connection);
                reader = query.ExecuteReader();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            /*
            var table = new DataTable();
            if (reader != null) table.Load(reader);
            var existingGuids = new HashSet<int>();
            var col = table.Columns[0]; // guid is always first collumn
            foreach (DataRow row in table.Rows)
                existingGuids.Add((int)row[col]);
             */

            var existingGuids = new List<int>();
            while (reader != null && reader.Read())
                existingGuids.Add(reader.GetInt32(0));

            var possibleGuids = Enumerable.Range(1, existingGuids.Last());
            IEnumerable<int> missingGuids = possibleGuids.Except(existingGuids);
            IEnumerable<int> selectedMissingGuids = null;

            if (RandomRadio.Checked)
                selectedMissingGuids = missingGuids.Take((int)GuidCountUpDown.Value);
            else if (ConsecutiveRadio.Checked)
                selectedMissingGuids = MahMethod(missingGuids.ToArray(), (int) GuidCountUpDown.Value);
            else
                MessageBox.Show("Gratz, you just opened a black hole.");

            var resultForm = new ResultForm(selectedMissingGuids);
            resultForm.Show();
        }

        private static IEnumerable<int> MahMethod(int[] input, int minimum)
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
                else break;

                if (count > minimum)
                    return result;
            }

            return result.Count < minimum ? null : result;
        }
    }
}
