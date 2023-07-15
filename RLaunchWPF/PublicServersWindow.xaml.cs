using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace RLaunchWPF {
    /// <summary>
    /// Interaction logic for PublicServersWindow.xaml
    /// </summary>
    public partial class PublicServersWindow : Window {
        public PublicServersWindow() {
            InitializeComponent();

            AddServersToList();
        }

        private void AddServersToList() {

            foreach (var file in Directory.GetFiles(@"servers")) {
                var game = Server.Load(file);
                ServersListBox.Items.Add(game);
            }
        }
        
        private void Connect_OnClick(object sender, RoutedEventArgs e) {
            var g = ServersListBox.SelectedItem as Server;
            if (g == null) {
                MessageBox.Show("Please select a server to connect to.");
                return;
            }
            switch (g.ConnectionMethod) {
                case Server.Connection.Ssh:
                    Process.Start("ssh.exe", $"-p {g.Port} {g.SshUser}@{g.IP}");
                    break;
                case Server.Connection.Web:
                    System.Diagnostics.Process.Start(new ProcessStartInfo
                    {
                        FileName = $"http://{g.IP}",
                        UseShellExecute = true
                    });
                    break;
                case Server.Connection.Telnet:
                    Process.Start("telnet.exe", $"{g.IP} {g.Port}");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        private void ServersListBox_OnSelectionChangedListBox_SelectedIndexChanged(object sender, SelectionChangedEventArgs e) {
            DescText.Text = (ServersListBox.SelectedItem as Server)?.Desc;
        }
    }
}