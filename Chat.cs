using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VRChatApi.Classes;
using Flurl.Http;
using Flurl;

namespace VRChatChat
{
    public partial class Chat : Form
    {
        static List<UserBriefResponse> friends;
        public Chat()
        {
            InitializeComponent();
            Setup();
        }

        private async Task Setup()
        {
            await Program.Login(Properties.Settings.Default.Username, Properties.Settings.Default.Password);
            friends = await Program.api.FriendsApi.Get(0, 100, true);
            listView1.Clear();
            friends.Add(Program.ownPlayer);
            foreach (var friend in friends)
            {
                //string[] row = { friend.displayName };
                var listViewItem = new ListViewItem(friend.displayName, friend.id);
                listViewItem.Tag = friend.id;
                listViewItem.ToolTipText = friend.status + "\n\n" + friend.statusDescription;
                listViewItem.ForeColor = System.Drawing.Color.Gray;
                if (friend.location != "offline") listViewItem.ForeColor = System.Drawing.Color.Green;
                listView1.Items.Add(listViewItem);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedIndices.Count < 1 || listView1.SelectedIndices.Count > 1) { MessageBox.Show("No Friend selected"); return; }
            foreach (var friend in friends)
            {
                if (listView1.SelectedItems[0].Text != friend.displayName) continue;
                string responseString = "";
                //var authEncoded = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Properties.Settings.Default.Username}:{Properties.Settings.Default.Password}"));
                try {
                    responseString = "https://api.vrchat.cloud/api/1/user/".AppendPathSegment(friend.id).AppendPathSegment("notification").SetQueryParam("apiKey", "JlE5Jldo5Jibnk5O5hTx6XVqsJu4WJ26")
                    .WithBasicAuth(Properties.Settings.Default.Username, Properties.Settings.Default.Password)// .WithHeader("Authorization", $"Basic {authEncoded}")
                    .PostUrlEncodedAsync(new { type = box_type.Text, message = richTextBox1.Text, details = txt_details.Text })
                    .ReceiveString().Result;
                } catch(Exception ex)
                {
                    responseString = ex.Message;
                }
                MessageBox.Show(responseString);
            }
        }

        private void button_reload_Click(object sender, EventArgs e)
        {
            Setup();
        }
    }
}
