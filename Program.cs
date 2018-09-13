using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using VRChatApi;
using VRChatApi.Classes;
using VRChatApi.Endpoints;

namespace VRChatChat
{
    static class Program
    {
        public static Form MainForm;
        public static Form LoginForm;
        public static VRChatApi.VRChatApi api;
        public static UserResponse ownPlayer;
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            LoginForm = new LoginForm();
            MainForm = new Chat();
            if (string.IsNullOrWhiteSpace(Properties.Settings.Default.Username) || string.IsNullOrWhiteSpace(Properties.Settings.Default.Password)) {
                Application.Run(LoginForm);
            } else {
                Application.Run(MainForm);
            }
        }
        public static async Task Login(string username, string password)
        {
            api = new VRChatApi.VRChatApi(username, password);
            ownPlayer = await api.UserApi.Login();
            /*
            if (string.IsNullOrEmpty(ownPlayer.displayName)) return false;
            return true;
            */
        }
    }
}
