using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Puzzle15
{
    class Gameboard
    {
        private EmptyCell emptyCell = new EmptyCell { Row = 3, Column = 3 };
        public Button[,] tiles = new Button[4,4];
        public Grid board = new Grid();

        public Gameboard()
        {
            #region Initialize list of randomly arranged tile numbers
            List<int> randomTileNumbers = TileNumbersGenerator();
            #endregion
            
            #region Initialize array of tiles with buttons randomly signed from 1 to 15, last tile leaved with null
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!(i == 3 && j == 3))
                    {
                        tiles[i, j] = new Button { Width=100, Height=100, Background = Brushes.CadetBlue };
                        tiles[i, j].Content = randomTileNumbers[i*4+j];
                    }
                }
            }
            #endregion
           
            #region Inicialize game grid
            InicializeGrid();
            #endregion
        }

        private void InicializeGrid()
        {
            board.Background = Brushes.Black;
            for (int i = 0; i < 4; i++)
            {
                board.RowDefinitions.Add(new RowDefinition());
                board.RowDefinitions[i].Height = GridLength.Auto;

                for (int j = 0; j < 4; j++)
                {
                    board.ColumnDefinitions.Add(new ColumnDefinition());
                    board.ColumnDefinitions[j].Width = GridLength.Auto;

                    Button buttonAdd = tiles[i, j];

                    if (buttonAdd != null)
                    {
                        Grid.SetRow(buttonAdd, i);
                        Grid.SetColumn(buttonAdd, j);
                        board.Children.Add(buttonAdd);
                        //board.buttons[i, j].Click += new RoutedEventHandler(btnClick);
                    }
                }
            }



        }

        private List<int> TileNumbersGenerator()
        {
            #region Generating randomly arranged list of tile numbers
            const int numberOfTiles = 15;
            List<int> tileNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            List<int> randomlyArrangedTileNumbers = new List<int>();
            Random randomNumber = new Random();

            for (int i = 0; i < numberOfTiles; i++)
            {
                int randomElementFromTileNumbers = randomNumber.Next(tileNumbers.Count);
                randomlyArrangedTileNumbers.Add( tileNumbers[ randomElementFromTileNumbers ] );
                tileNumbers.RemoveAt(randomElementFromTileNumbers);
            }
            return randomlyArrangedTileNumbers;
            #endregion
        }
    }
}
