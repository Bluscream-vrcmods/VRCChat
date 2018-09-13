using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VRChatChat
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            var username = this.textBox1.Text;
            if (string.IsNullOrWhiteSpace(username)) { MessageBox.Show("Username can't be empty!"); return; }
            var password = this.textBox2.Text;
            if (string.IsNullOrWhiteSpace(password)) { MessageBox.Show("Password can't be empty!"); return; }
            /*var success = */ Program.Login(username, password);
            //if (!success) { MessageBox.Show("Login failed!"); return; }
            Properties.Settings.Default.Username = username;
            Properties.Settings.Default.Password = password;
            Properties.Settings.Default.Save(); 
            this.Hide();
            Program.MainForm.Closed += (s, args) => this.Close(); 
            Program.MainForm.Show();
        }
    }
}
