using System;
using System.Threading;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UnusedGuidSearcher
{
    public partial class LoginForm : Form
    {
        private readonly Settings _settings = new Settings();

        private string _username;
        private string _password;
        private string _database;
        private string _host;
        private string _connectionString;

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginFormLoad(object sender, EventArgs e)
        {
            UserBox.Text = _settings.GetSetting("User", "root");
            PasswordBox.Text = _settings.GetSetting("Password", string.Empty);
            DBBox.Text = _settings.GetSetting("DB", "world");
            HostBox.Text = _settings.GetSetting("Host", "localhost");
        }

        private void SaveSettings()
        {
            _username = UserBox.Text;
            _password = PasswordBox.Text;
            _database = DBBox.Text;
            _host = HostBox.Text;
            _connectionString = string.Format("SERVER={0};DATABASE={1};UID={2};PASSWORD={3};", _host, _database, _username, _password);

            _settings.PutSetting("User", _username);
            _settings.PutSetting("Password", _password);
            _settings.PutSetting("DB", _database);
            _settings.PutSetting("Host", _host);
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            if (!TestConnection())
                return;

            SaveSettings();
            Close();
            new Thread(StartMainForm).Start();
        }

        private void StartMainForm()
        {
            Application.Run(new MainForm(_connectionString));
        }

        private bool TestConnection()
        {
            MySqlConnection connection = null;
            var result = true;

            try
            {
                connection = new MySqlConnection(_connectionString);
                connection.Open();
            }
            catch (MySqlException ex)
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
                    result = false;
                }
            }

            return result;
        }
    }
}
