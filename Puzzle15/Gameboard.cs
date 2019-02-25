using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace Puzzle15
{
    class Gameboard
    {
        Button[,] tiles = new Button[4, 4];   //array of tiles (buttons)
        EmptyCell emptyCell = new EmptyCell { Row=3, Column=3 };   //place with no tile

        public Gameboard()
        {
            #region initialize array of tiles with buttons signed from 1 to 15, last tile leaved with null
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (!(i == 3 && j == 3)) tiles[i, j] = new Button {Content=(i*4+j+1).ToString(), Width=100, Height=100, Background=Brushes.CadetBlue };
                }
            }
            #endregion


        }
    }
}
