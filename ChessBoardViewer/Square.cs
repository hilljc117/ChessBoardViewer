using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessBoardViewer
{
    class Square : Panel
    {
        public bool isHighlighted;
        public int x;
        public int y;

        public Piece GetPiece()
        {
            if (Controls.Count > 0) {
                return Controls[0] as Piece;
            }
            return null;
        }
    }
}
