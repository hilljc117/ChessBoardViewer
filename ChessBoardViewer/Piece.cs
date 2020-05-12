using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static ChessBoardViewer.ChessBoard;

namespace ChessBoardViewer
{
    abstract partial class Piece : Panel
    {
        protected Square[,] board;
        protected double len;
        public int x, y;
        protected PieceColor color;

        protected Piece(Square[,] board, PieceColor color, int x, int y)
        {
            this.board = board;
            this.len = Math.Sqrt(board.Length);
            this.x = x;
            this.y = y;
            this.color = color;
        }
        public abstract Square[] GetMoves();

        protected Square GetSquare(int x, int y)
        {
            double len = Math.Sqrt(board.Length);
            if (x >= 0 && x < len) {
                if (y >= 0 && y < len) {
                    Square s = board[x, y];
                    if (s.Controls.Count > 0) {
                        Piece p = s.Controls[0] as Piece;
                        if (p.color == color) {
                            return null;
                        }
                    }
                    return s;
                }
            }
            return null;
        }

        protected Square[] GetDiagonalSquares()
        {
            List<Square> moves = new List<Square>();

            for (int i = 1; i < (len - x); i++) {
                Square s = GetSquare(x + i, y + i);
                if (s is null) break;
                moves.Add(s);
                if (s.GetPiece() != null) {
                    break;
                }
            }

            for (int i = 1; i < (len - x); i++) {
                Square s = GetSquare(x + i, y - i);
                if (s is null) break;
                moves.Add(s);
                if (s.GetPiece() != null) {
                    break;
                }
            }

            for (int i = 1; i <= x; i++) {
                Square s = GetSquare(x - i, y + i);
                if (s is null) break;
                moves.Add(s);
                if (s.GetPiece() != null) {
                    break;
                }
            }

            for (int i = 1; i <= x; i++) {
                Square s = GetSquare(x - i, y - i);
                if (s is null) break;
                moves.Add(s);
                if (s.GetPiece() != null) {
                    break;
                }
            }

            return moves.ToArray();
        }

        protected Square[] GetOrthogonalSquares()
        {
            List<Square> moves = new List<Square>();

            for (int i = x + 1; i < len; i++) {
                Square s = GetSquare(i, y);
                if (s is null) break;
                moves.Add(s);
                if (s.GetPiece() != null) {
                    break;
                }
            }

            for (int i = 1; i <= x; i++) {
                Square s = GetSquare(x - i, y);
                if (s is null) break;
                moves.Add(s);
                if (s.GetPiece() != null) {
                    break;
                }
            }

            for (int i = y + 1; i < len; i++) {
                Square s = GetSquare(x, i);
                if (s is null) break;
                moves.Add(s);
                if (s.GetPiece() != null) {
                    break;
                }
            }

            for (int i = 1; i <= y; i++) {
                Square s = GetSquare(x, y - i);
                if (s is null) break;
                moves.Add(s);
                if (s.GetPiece() != null) {
                    break;
                }
            }

            return moves.ToArray();
        }
    }

    class Rook : Piece
    {
        public Rook(Square[,] board, PieceColor color, int x, int y) : base(board, color, x, y)
        {
            
        }

        public override Square[] GetMoves()
        {
            return GetOrthogonalSquares();
        }
    }

    class Bishop : Piece
    {
        public Bishop(Square[,] board, PieceColor color, int x, int y) : base(board, color, x, y)
        {
            
        }

        public override Square[] GetMoves()
        {
            return GetDiagonalSquares();
        }
    }

    class Knight : Piece
    {
        public Knight(Square[,] board, PieceColor color, int x, int y) : base(board, color, x, y)
        {
            
        }

        public override Square[] GetMoves()
        {
            List<Square> moves = new List<Square>();

            moves.Add(GetSquare(x + 1, y + 2));
            moves.Add(GetSquare(x + 2, y + 1));

            moves.Add(GetSquare(x + 2, y - 1));
            moves.Add(GetSquare(x + 1, y - 2));

            moves.Add(GetSquare(x - 1, y - 2));
            moves.Add(GetSquare(x - 2, y - 1));
           
            moves.Add(GetSquare(x - 2, y + 1));
            moves.Add(GetSquare(x - 1, y + 2));

            return moves.ToArray();
        }
    }

    class Queen : Piece
    {
        public Queen(Square[,] board, PieceColor color, int x, int y) : base(board, color, x, y)
        {
            
        }

        public override Square[] GetMoves()
        {
            List<Square> moves = new List<Square>();

            moves.AddRange(GetDiagonalSquares());
            moves.AddRange(GetOrthogonalSquares());

            return moves.ToArray();
        }
    }

    class King : Piece
    {
        public King(Square[,] board, PieceColor color, int x, int y) : base(board, color, x, y)
        {
            
        }

        public override Square[] GetMoves()
        {
            List<Square> moves = new List<Square>();

            moves.Add(GetSquare(x, y - 1));
            moves.Add(GetSquare(x + 1, y - 1));
            moves.Add(GetSquare(x + 1, y));
            moves.Add(GetSquare(x + 1, y + 1));
            moves.Add(GetSquare(x, y + 1));
            moves.Add(GetSquare(x - 1, y + 1));
            moves.Add(GetSquare(x - 1, y));
            moves.Add(GetSquare(x - 1, y - 1));

            return moves.ToArray();
        }
    }

    class Pawn : Piece
    {
        private bool firstMove = true;
        public Pawn(Square[,] board, PieceColor color, int x, int y) : base(board, color, x, y)
        {
            
        }

        public override Square[] GetMoves()
        {
            List<Square> moves = new List<Square>();
            if (color == PieceColor.White) {
                Square s = GetSquare(x, y - 1);
                if (s.GetPiece() == null) {
                    moves.Add(s);
                    if (firstMove) {
                        s = GetSquare(x, y - 2);
                        if (s.GetPiece() == null) {
                            moves.Add(s);
                        }
                    }
                }
                

                s = GetSquare(x+1, y-1);
                if (s != null) {
                    Piece p = s.GetPiece();
                    if (p != null) {
                        moves.Add(s);
                    }
                }

                s = GetSquare(x - 1, y - 1);
                if (s != null) {
                    Piece p = s.GetPiece();
                    if (p != null) {
                        moves.Add(s);
                    }
                }
            } else {
                Square s = GetSquare(x, y + 1);
                if (s.GetPiece() == null) {
                    moves.Add(s);
                    if (firstMove) {
                        s = GetSquare(x, y + 2);
                        if (s.GetPiece() == null) {
                            moves.Add(s);
                        }
                    }
                }


                s = GetSquare(x + 1, y + 1);
                if (s != null) {
                    Piece p = s.GetPiece();
                    if (p != null) {
                        moves.Add(s);
                    }
                }

                s = GetSquare(x - 1, y + 1);
                if (s != null) {
                    Piece p = s.GetPiece();
                    if (p != null) {
                        moves.Add(s);
                    }
                }
            }

            return moves.ToArray();
        }

        public void FirstMoveOver()
        {
            firstMove = false;
        }
    }
}
