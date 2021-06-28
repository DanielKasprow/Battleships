using System;
using System.Collections.Generic;
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

namespace Daniel_Kasprów_Lista2_zad3v2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Grid grid = new Grid();

        private SelectShip SelectShip;
        private Battleship Battleship;

        public MainWindow()
        {
            InitializeComponent();
            initializeGame();
        }
        private void initializeGame()
        {
            Content = grid;
            this.Height = 500;
            this.Width = 500;

            SelectShip = new SelectShip();
            grid.Children.Add(SelectShip);

            SelectShip.play += new EventHandler(StartGame);
        }
        private void StartGame(object sender, EventArgs e)
        {
            grid.Children.Clear();

            this.Width = 700;
            this.Height = 450;

            Battleship = new Battleship(SelectShip.playerGrid);

            grid.Children.Add(Battleship);
            Battleship.replay += new EventHandler(replayGame);
        }
        private void replayGame(object sender, EventArgs e)
        {
            grid.Children.Clear();
            initializeGame();
        }
    }
}
