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
    /// Interaction logic for SelectShip.xaml
    /// </summary>
    public partial class SelectShip : UserControl
    {
        Grid grid = new Grid();

        public Grid[] playerGrid;

        public event EventHandler play;

        private int ship = 3, endship=0;
        private int[] sizeship = new int[4] { 1,2,2,3 };

        public SelectShip()
        {
            
            InitializeComponent();
            playerGrid = new Grid[]
            {
                grid1,grid2,grid3,grid4,grid5,
                grid6,grid7,grid8,grid9,grid10,
                grid11,grid12,grid13,grid14,grid15
                ,grid16,grid17,grid18,grid19,grid20
                ,grid21,grid22,grid23,grid24,grid25
            };
            foreach (var element in playerGrid)
            {
                element.Tag = "water";
            }
        }

        private void gridMouseDown(object sender, MouseButtonEventArgs e)
        {
            Grid square = (Grid)sender;
            int index;
            index = Array.IndexOf(playerGrid, square);
            if (endship == 1)
            {
               placeship(index);
               grayfield();
            }
            else
            {
                firstplaceship(index);
            }
                if (ship < 0)
                {
                clear();
                    play(this, e);
                }
            
        }

        void clear()
        {
            for(int x=0; x<25; x++)
            {
                if(playerGrid[x].Tag == "Gray")
                {
                    playerGrid[x].Tag = "water";
                    playerGrid[x].Background = Brushes.White;
                }
            }
        }

        void grayfield()
        {
            for(int x=0;x<25;x++)
            {
                if(playerGrid[x].Tag == "ship")
                {
                    if (playerGrid[x].Tag == "ship")
                    {
                        if (x < 20 && playerGrid[x+5].Tag == "water")
                        {
                            playerGrid[x + 5].Background = Brushes.Gray;
                            playerGrid[x + 5].Tag = "Gray";
                        }
                        if (x > 4 && playerGrid[x - 5].Tag == "water")
                            {
                                playerGrid[x - 5].Background = Brushes.Gray;
                                playerGrid[x - 5].Tag = "Gray";
                            }
                        if (((x / 5) == ((x + 1) / 5)) && x < 24 && playerGrid[x + 1].Tag == "water")
                            {
                                playerGrid[x + 1].Background = Brushes.Gray;
                                playerGrid[x + 1].Tag = "Gray";
                            }
                        if (((x / 5) == ((x - 1) / 5)) && x > 0 && playerGrid[x - 1].Tag == "water")
                            {
                                playerGrid[x - 1].Background = Brushes.Gray;
                                playerGrid[x - 1].Tag = "Gray";
                            }
                    }
                }
            }
        }

        private void firstplaceship(int index)
        {
            if (playerGrid[index].Tag.Equals("water"))
            {
                if (ship == 0)
                {
                    playerGrid[index].Background = Brushes.Green;
                    playerGrid[index].Tag = "ship";
                    ship--;

                }
                else
                {
                    int good = 1;
                    for (int x = 1; x < sizeship[ship]; x++)
                    {
                        if ((index + (5 * x)) < 25)
                        {
                            if (playerGrid[index].Tag.Equals("water"))
                            {
                                good++;
                                if (good == sizeship[ship])
                                {
                                    if (playerGrid[index + (5 * x)].Tag != "Gray")
                                    {
                                        playerGrid[index].Background = Brushes.Green;
                                        playerGrid[index + (5 * x)].Background = Brushes.Blue;
                                        playerGrid[index + (5 * x)].Tag = "possible";
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
                            if (playerGrid[index].Tag.Equals("water"))
                            {
                                good++;
                                if (good == sizeship[ship])
                                {
                                    if (playerGrid[index - (5 * x)].Tag != "Gray")
                                    {
                                        playerGrid[index].Background = Brushes.Green;
                                        playerGrid[index - (5 * x)].Background = Brushes.Blue;
                                        playerGrid[index - (5 * x)].Tag = "possible";
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
                            if (playerGrid[index].Tag.Equals("water") && (index + sizeship[ship] - 1) < 25)
                            {
                                good++;
                                if (good == sizeship[ship])
                                {
                                    if (playerGrid[index + x].Tag != "Gray")
                                    {
                                        playerGrid[index].Background = Brushes.Green;
                                        playerGrid[index + x].Background = Brushes.Blue;
                                        playerGrid[index + x].Tag = "possible";
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
                            if (playerGrid[index].Tag.Equals("water") && (index - sizeship[ship] + 1) >= 0)
                            {
                                good++;
                                if (good == sizeship[ship])
                                {
                                    if (playerGrid[index - x].Tag != "Gray")
                                    {
                                        playerGrid[index].Background = Brushes.Green;
                                        playerGrid[index - x].Background = Brushes.Blue;
                                        playerGrid[index - x].Tag = "possible";
                                        endship = 1;
                                    }
                                }
                            }
                        }
                    }
                    if (endship == 1)
                    {
                        playerGrid[index].Tag = "shipp";
                    }
                }
            }
        }

        private void placeship(int index)
        {
            if (playerGrid[index].Tag == "possible")
            {
                playerGrid[index].Tag = "ship";
                playerGrid[index].Background = Brushes.Green;
                for (int x = 0; x < 25; x++)
                {
                    if (playerGrid[x].Tag == "shipp")
                    {
                        playerGrid[x].Tag = "ship";
                        if(ship==3)
                        {
                            playerGrid[(index+x)/2].Tag = "ship";
                            playerGrid[(index + x) / 2].Background = Brushes.Green;
                        }
                    }
                    if (playerGrid[x].Tag == "possible")
                    {
                        playerGrid[x].Tag = "water";
                        playerGrid[x].Background = Brushes.White;
                    }
                }
                endship = 0;
                ship--;
            }
        }
    }
}
