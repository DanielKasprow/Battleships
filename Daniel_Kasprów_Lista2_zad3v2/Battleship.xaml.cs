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
    /// Interaction logic for Battleship.xaml
    /// </summary>
    public partial class Battleship : UserControl
    {
        public Grid[] playerGrid;
        public Grid[] compGrid;

        public Random random = new Random();
        RoutedEventArgs e;

        public event EventHandler replay;

        private int ship = 3, endship = 0;
        private int[] sizeship = new int[4] { 1, 2, 2, 3 };
        private int licz = 0,licz2 = 0,start;

        public Battleship(Grid[] playerGrid)
        {
            InitializeComponent();

            start = random.Next(0, 2);

            initiateSetup(playerGrid);
        }
        private void initiateSetup(Grid[] userGrid)
        {
            compGrid = new Grid[25];
            CompGrid.Children.CopyTo(compGrid, 0);
            for (int i = 0; i < 25; i++)
            {
                compGrid[i].Tag = "water";
            }
            setupCompGrid();

            playerGrid = new Grid[25];
            PlayerGrid.Children.CopyTo(playerGrid, 0);

            for (int i = 0; i < 25; i++)
            {
                playerGrid[i].Background = userGrid[i].Background;
                playerGrid[i].Tag = userGrid[i].Tag;
            }
            if (start == 0)
            {
                computerTurn();
            }
        }
        private void gridMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid square = (Grid)sender;
            switch (square.Tag.ToString())
            {
                case "water":
                    square.Tag = "miss";
                    square.Background = new SolidColorBrush(Colors.LightGray);
                    computerTurn();
                    return;

                case "ship":
                    square.Tag = "hit";
                    square.Background = new SolidColorBrush(Colors.Red);
                    licz++;
                    wygranagracza();
                    computerTurn();
                    return;
            }
        }

        private void setupCompGrid()
        {
            Random random = new Random();
            int index;
            do
            {
                index = random.Next(0, 25);
                if (compGrid[index].Tag.Equals("water"))
                {
                    endship = 0;
                    firstplaceship(index);
                    placeship(index);
                    grayfield();
                }
            } while (ship >= 0);
            clear();
        }

        private void firstplaceship(int index)
        {
                if (ship == 0)
                {
                    compGrid[index].Background = Brushes.Green;
                    compGrid[index].Tag = "ship";
                    ship--;
                }
                else
                {
                    int good = 1;
                    for (int x = 1; x < sizeship[ship]; x++)
                    {
                        if ((index + (5 * x)) < 25)
                        {
                            if (compGrid[index].Tag.Equals("water"))
                            {
                                good++;
                                if (good == sizeship[ship])
                                {
                                    if (compGrid[index + (5 * x)].Tag != "Gray")
                                    {
                                        compGrid[index].Background = Brushes.Green;
                                        compGrid[index + (5 * x)].Background = Brushes.Blue;
                                        compGrid[index + (5 * x)].Tag = "possible";
                                        endship = 1;
                                    }
                                }
                            }
                        }
                    }
                    good = 1;
                    for (int x = 1; x < sizeship[ship]; x++)
                    {
                        if ((index - (5 * x)) >= 0)
                        {
                            if (compGrid[index].Tag.Equals("water"))
                            {
                                good++;
                                if (good == sizeship[ship])
                                {
                                    if (compGrid[index - (5 * x)].Tag != "Gray")
                                    {
                                        compGrid[index].Background = Brushes.Green;
                                        compGrid[index - (5 * x)].Background = Brushes.Blue;
                                        compGrid[index - (5 * x)].Tag = "possible";
                                        endship = 1;
                                    }
                                }
                            }
                        }
                    }
                    good = 1;
                    for (int x = 1; x < sizeship[ship]; x++)
                    {
                        if ((index / 5) == (((index + sizeship[ship] - 1) / 5)))
                        {
                            if (compGrid[index].Tag.Equals("water") && (index + sizeship[ship] - 1) < 25)
                            {
                                good++;
                                if (good == sizeship[ship])
                                {
                                    if (compGrid[index + x].Tag != "Gray")
                                    {
                                        compGrid[index].Background = Brushes.Green;
                                        compGrid[index + x].Background = Brushes.Blue;
                                        compGrid[index + x].Tag = "possible";
                                        endship = 1;
                                    }
                                }
                            }
                        }
                    }
                    good = 1;
                    for (int x = 1; x < sizeship[ship]; x++)
                    {
                        if ((index / 5) == (((index + 1 - sizeship[ship]) / 5)))
                        {
                            if (compGrid[index].Tag.Equals("water") && (index - sizeship[ship] + 1) >= 0)
                            {
                                good++;
                                if (good == sizeship[ship])
                                {
                                    if (compGrid[index - x].Tag != "Gray")
                                    {
                                        compGrid[index].Background = Brushes.Green;
                                        compGrid[index - x].Background = Brushes.Blue;
                                        compGrid[index - x].Tag = "possible";
                                        endship = 1;
                                    }
                                }
                            }
                        }
                    }
                if (endship == 1)
                {
                    compGrid[index].Tag = "shipp";
                }
                else ship++;
                
            }
        }

        void grayfield()
        {
            for (int x = 0; x < 25; x++)
            {
                if (compGrid[x].Tag == "ship")
                {
                    if (compGrid[x].Tag == "ship")
                    {
                        if (x < 20 && compGrid[x + 5].Tag == "water")
                        {
                            compGrid[x + 5].Background = Brushes.Gray;
                            compGrid[x + 5].Tag = "Gray";
                        }
                        if (x > 4 && compGrid[x - 5].Tag == "water")
                        {
                            compGrid[x - 5].Background = Brushes.Gray;
                            compGrid[x - 5].Tag = "Gray";
                        }
                        if (((x / 5) == ((x + 1) / 5)) && x < 24 && compGrid[x + 1].Tag == "water")
                        {
                            compGrid[x + 1].Background = Brushes.Gray;
                            compGrid[x + 1].Tag = "Gray";
                        }
                        if (((x / 5) == ((x - 1) / 5)) && x > 0 && compGrid[x - 1].Tag == "water")
                        {
                            compGrid[x - 1].Background = Brushes.Gray;
                            compGrid[x - 1].Tag = "Gray";
                        }
                    }
                }
            }
        }

        private void placeship(int index)
        {
            do
            {
                index = random.Next(0, 25);
                if (compGrid[index].Tag == "possible")
                {
                    endship = 0;
                    compGrid[index].Tag = "ship";
                    compGrid[index].Background = Brushes.Green;
                    for (int x = 0; x < 25; x++)
                    {
                        if (compGrid[x].Tag == "shipp")
                        {
                            compGrid[x].Tag = "ship";
                            if (ship == 3)
                            {
                                compGrid[(index + x) / 2].Tag = "ship";
                                compGrid[(index + x) / 2].Background = Brushes.Green;
                            }
                        }
                        if (compGrid[x].Tag == "possible")
                        {
                            compGrid[x].Tag = "water";
                            compGrid[x].Background = Brushes.White;
                        }
                    }
                }
            } while (endship == 1);
            ship--;
        }

        void clear()
        {
            for (int x = 0; x < 25; x++)
            {
                if (compGrid[x].Tag == "Gray")
                {
                    compGrid[x].Tag = "water";
                    compGrid[x].Background = Brushes.White;
                }
                if (compGrid[x].Tag == "ship")
                {
                    compGrid[x].Background = Brushes.White;
                }
            }
        }

        private void computerTurn()
        {
            int position,yes=1;
            position = random.Next(0, 25);
                if (playerGrid[position].Tag.Equals("water"))
                {
                    playerGrid[position].Tag = "miss";
                    playerGrid[position].Background = new SolidColorBrush(Colors.LightGray);
                yes = 0;
                }
                else if (playerGrid[position].Tag.Equals("ship"))
                {
                    playerGrid[position].Tag = "hit";
                    playerGrid[position].Background = new SolidColorBrush(Colors.Red);
                yes = 0;
                licz2++;
                przegranagracza();
            }
           if(yes==1) computerTurn();

        }

        private void wygranagracza()
        {
            if(licz==8)
            {
                MessageBox.Show("Wygrales");
                reset(this, e);
            }
        }
        private void przegranagracza()
        {
            if (licz2 == 8)
            {
                MessageBox.Show("Przegrales");
                reset(this, e);
            }
        }
        private void reset(object sender, RoutedEventArgs e)
        {
            replay(this, e);
        }
    }
}
