using RLaunch;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        

        public MainWindow() {
            InitializeComponent();
            
            if (!Directory.Exists("data\\meta")) {
                Directory.CreateDirectory("data\\meta");
            }

            addGamesToList();
        }

        void addInstance_Click(object sender, RoutedEventArgs e) {
            Window1 addInst = new Window1();
            addInst.ShowDialog();
        }

        void play_Click(object sender, RoutedEventArgs e) {
            //var anonType = ((Button)sender).DataContext;
            Button b = sender as Button;
            Game g = b.DataContext as Game;
            Process.Start(AppDomain.CurrentDomain.BaseDirectory + $"/data/games/{g.Name}{g.Ver}/"+g.Exe);
        }

        void addGamesToList() {

            foreach (string file in Directory.GetFiles("data\\meta")) {
                Game game = Game.Load(file);
                
                Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "data/meta/" + game.Img, UriKind.Relative);
                game.Img = BitmapFrame.Create(uri);
                GameList.Items.Add(game);

            }
        }

    }
}
