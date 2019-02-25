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
        private Button[,] tiles = new Button[4,4];
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
                        tiles[i, j] = new Button { Width = 100, Height = 100 };
                        if ( randomTileNumbers[i*4+j]%2 == 0 )
                        {
                            tiles[i, j].Background = Brushes.Gray;
                        }
                        else
                        {
                            tiles[i, j].Background = Brushes.DarkRed;
                        }
                            
                        tiles[i, j].Content = randomTileNumbers[i*4+j];
                        tiles[i, j].Click += new RoutedEventHandler(btnClick);
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

        private void btnClick(object sender, RoutedEventArgs e)
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

            if ( PlayerWins() ) board.Background = Brushes.Red;
        }

        private bool PlayerWins()
        {
            if (!(emptyCell.Row == 3 && emptyCell.Column == 3)) return false;
            int btnContent;
            int btnNum = 1;

            foreach (Button btn in tiles)
            {
                if (btn != null)
                {
                    btnContent = Int32.Parse(btn.Content.ToString());
                    if (btnContent != btnNum) return false;
                    btnNum += 1;
                }
            }
            return true;
        }
    }
}
