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
    public class Gameboard
    {
        private struct EmptyCell { internal int Row, Column;}
        private readonly Button[,] tiles = new Button[4,4];
        private EmptyCell emptyCell = new EmptyCell { Row = 3, Column = 3 };
        public Grid Board { get; } = new Grid();

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
                        tiles[i, j] = new Button { Width = 100, Height = 100, Focusable=false };
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
            Board.Background = Brushes.Black;
            for (int i = 0; i < 4; i++)
            {
                Board.RowDefinitions.Add(new RowDefinition());
                Board.RowDefinitions[i].Height = GridLength.Auto;

                for (int j = 0; j < 4; j++)
                {
                    Board.ColumnDefinitions.Add(new ColumnDefinition());
                    Board.ColumnDefinitions[j].Width = GridLength.Auto;

                    Button tileAdd = tiles[i, j];

                    if (tileAdd != null)
                    {
                        Grid.SetRow(tileAdd, i);
                        Grid.SetColumn(tileAdd, j);
                        Board.Children.Add(tileAdd);
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
                Board.Children.Remove(clickedButton);

                Grid.SetRow(clickedButton, emptyCell.Row);
                Grid.SetColumn(clickedButton, emptyCell.Column);

                Board.Children.Add(clickedButton);

                emptyCell.Row = btnRow;
                emptyCell.Column = btnCol;
            }
            if (IsPuzzleSolved()) DoWhenPuzzleSolved();
        }

        protected void DoWhenPuzzleSolved()
        {
            Board.Background = Brushes.Red;
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
