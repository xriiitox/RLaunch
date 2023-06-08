using RLaunch;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RLaunchWPF {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        // public Game[] games = Array.Empty<Game>();

        public MainWindow() {
            InitializeComponent();
            
            if (!Directory.Exists("data/meta")) {
                Directory.CreateDirectory("data/meta");
            }

            foreach (string file in Directory.GetFiles("data/meta")) {
                Game game = Game.Load(file);
                GameList.Items.Add(game); 
            }


        }

        void addInstance_Click(object sender, RoutedEventArgs e) {
            Window1 addInst = new Window1();
            addInst.ShowDialog();
        }
    }
}
