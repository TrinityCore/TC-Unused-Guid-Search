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
        private string _port;
        private string _host;
        private string _pipe;

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
            PipeBox.Text = _settings.GetSetting("Pipe", "");
            PortBox.Text = _settings.GetSetting("Port", "3724");

            // sets the checkbox for a piped connection if the user has it saved in the settings
            if (PortBox.Text == "-1")
            {
                checkBox1.Checked = true;
            }
        }

        private void SaveSettings()
        {
            _username = UserBox.Text;
            _password = PasswordBox.Text;
            _port = PortBox.Text;
            _database = DBBox.Text;
            _host = HostBox.Text;
            _pipe = PipeBox.Text;


            _settings.PutSetting("User", _username);
            _settings.PutSetting("Password", _password);
            _settings.PutSetting("DB", _database);
            _settings.PutSetting("Host", _host);
            _settings.PutSetting("Pipe", _pipe);
            _settings.PutSetting("Port", _port);
        }

        private String _connectionString
        {
            get
            {
                if (PortBox.Text == "-1")
                    //Server=localhost;Pipe={0};UserID={1};Password={2};Database={3};CharacterSet=utf8;ConnectionTimeout=5;ConnectionProtocol=Pipe;
                    return String.Format("SERVER={0};PIPE={1};UID={2};PASSWORD={3};DATABASE={4};ConnectionProtocol=Pipe;", _host, _pipe, _database, _username, _password);

                return String.Format("SERVER={0};PORT={1};DATABASE={2};UID={3};PASSWORD={4};", _host, _port, _database, _username, _password);
            }
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
                result = false;
            }
            finally
            {
                if (connection != null)
                {
                    connection.Close();
                    connection.Dispose();
                }
            }

            return result;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                PortBox.Enabled = false;
                PipeBox.Enabled = true;
                PortBox.Text = ""; // clear the string
                PortBox.Text = "-1"; // sets port to -1 for a named pipe
            }

            if (checkBox1.Checked == false)
            {
                PortBox.Enabled = true;
                PipeBox.Enabled = false;
                PortBox.Text = ""; //clear the string
                PortBox.Text = _settings.GetSetting("Port", "3724"); // saved setting
            }
        }

        private void OkButton_Click(object sender, EventArgs e)
        {
            SaveSettings();

            if (!TestConnection())
                return;

            Close();
            new Thread(StartMainForm).Start();
        }
    }
}
