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
        private readonly Button[,] tiles = new Button[4,4];
        private EmptyCell emptyCell = new EmptyCell { Row = 3, Column = 3 };
        public Grid board = new Grid();

        public Gameboard()
        {
            InicializeTiles();
            InicializeGrid();
        }

        private void InicializeTiles()
        {
            List<int> randomTileNumbers = TileNumbersGenerator();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!(i == 3 && j == 3))
                    {
                        tiles[i, j] = new Button { Width = 100, Height = 100 };
                        if (randomTileNumbers[i * 4 + j] % 2 == 0) tiles[i, j].Background = Brushes.Gray;
                        else tiles[i, j].Background = Brushes.DarkRed;

                        tiles[i, j].Content = randomTileNumbers[i * 4 + j];
                        tiles[i, j].Click += new RoutedEventHandler(TileClick);
                    }
                }
            }
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

                    Button tileAdd = tiles[i, j];

                    if (tileAdd != null)
                    {
                        Grid.SetRow(tileAdd, i);
                        Grid.SetColumn(tileAdd, j);
                        board.Children.Add(tileAdd);
                    }
                }
            }
        }

        private List<int> TileNumbersGenerator()
        {
            const int numberOfTiles = 15;
            List<int> tileNumbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            List<int> randomlyArrangedTileNumbers = new List<int>();
            Random randomNumber = new Random();

            for (int i = 0; i < numberOfTiles; i++)
            {
                int randomElementFromTileNumbers = randomNumber.Next(tileNumbers.Count);
                randomlyArrangedTileNumbers.Add(tileNumbers[randomElementFromTileNumbers]);
                tileNumbers.RemoveAt(randomElementFromTileNumbers);
            }
            return randomlyArrangedTileNumbers;
        }

        private void TileClick(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int btnRow = Grid.GetRow(clickedButton);
            int btnCol = Grid.GetColumn(clickedButton);

            if ((Math.Abs(btnRow - emptyCell.Row) + Math.Abs(btnCol - emptyCell.Column)) == 1)
            {
                board.Children.Remove(clickedButton);

                Grid.SetRow(clickedButton, emptyCell.Row);
                Grid.SetColumn(clickedButton, emptyCell.Column);

                board.Children.Add(clickedButton);

                emptyCell.Row = btnRow;
                emptyCell.Column = btnCol;
            }
            if ( IsPuzzleSolved() ) board.Background = Brushes.Red;
        }

        private bool IsPuzzleSolved()
        {
            if (!(emptyCell.Row == 3 && emptyCell.Column == 3)) return false;

            foreach (Button tile in tiles)
            {
                if (tile != null)
                {
                    int auxVal = 4 * Grid.GetRow(tile) + Grid.GetColumn(tile) + 1;
                    int tileContent = Int32.Parse(tile.Content.ToString());
                    if (tileContent != auxVal) return false;
                }
            }
            return true;
        }
    }
}
