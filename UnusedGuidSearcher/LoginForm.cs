using System;
using System.Threading;
using System.Windows.Forms;

namespace UnusedGuidSearcher
{
    public partial class LoginForm : Form
    {
        private readonly Settings _settings = new Settings();

        private string _username;
        private string _password;
        private string _database;
        private string _host;

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

            _settings.PutSetting("User", _username);
            _settings.PutSetting("Password", _password);
            _settings.PutSetting("DB", _database);
            _settings.PutSetting("Host", _host);
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            SaveSettings();
            Close();

            new Thread(StartMainForm).Start();
        }

        private void StartMainForm()
        {
            Application.Run(new MainForm(_username, _password, _database, _host));
        }
    }
}
