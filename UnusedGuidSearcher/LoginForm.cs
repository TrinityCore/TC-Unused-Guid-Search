using System;
using System.Threading;
using System.Windows.Forms;

namespace UnusedGuidSearcher
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginFormLoad(object sender, EventArgs e)
        {
            UserBox.Text = Settings.GetSetting("User", "root");
            PasswordBox.Text = Settings.GetSetting("Password", string.Empty);
            DBBox.Text = Settings.GetSetting("DB", "world");
            HostBox.Text = Settings.GetSetting("Host", "localhost");
        }

        private void SaveButtonClick(object sender, EventArgs e)
        {
            Settings.PutSetting("User", UserBox.Text);
            Settings.PutSetting("Password", PasswordBox.Text);
            Settings.PutSetting("DB", DBBox.Text);
            Settings.PutSetting("Host", HostBox.Text);
        }

        private void OkButtonClick(object sender, EventArgs e)
        {
            SaveButtonClick(sender, e);

            Close();

            new Thread(StartMainForm).Start();
        }

        private static void StartMainForm()
        {
            Application.Run(new MainForm());
        }
    }
}
