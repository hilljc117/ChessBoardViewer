using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ChessBoardViewer
{
    public partial class ChessBoard : Form
    {
        private const int SQUARE_SIZE = 125;
        private const int CHESS_BOARD_SIZE = 8;
        private const string LETTERS = "ABCDEFGH";
        private Square[,] squares = new Square[CHESS_BOARD_SIZE,CHESS_BOARD_SIZE];
        private Piece selectedPiece;
        private List<Piece> whitePieces = new List<Piece>();
        private List<Piece> blackPieces = new List<Piece>();
        private bool whiteTurn = true;

        public ChessBoard()
        {
            InitializeComponent();
            DrawSquares(CHESS_BOARD_SIZE);
            PlacePieces();

            SetTurn();
        }

        private void SetTurn()
        {
            if (whiteTurn) {
                foreach (Piece p in whitePieces) {
                    p.Enabled = true;
                }

                foreach (Piece p in blackPieces) {
                    p.Enabled = false;
                }
                whiteTurn = false;
            } else {
                foreach (Piece p in whitePieces) {
                    p.Enabled = false;
                }

                foreach (Piece p in blackPieces) {
                    p.Enabled = true;
                }
                whiteTurn = true;
            }
        }

        private void DrawSquares(int size)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    DrawSquare(i, j);
                }
            }
            Size = new Size(SQUARE_SIZE * size, SQUARE_SIZE * size);
        }

        private void DrawSquare(int x, int y)
        {
            Square square = new Square {
                Location = new Point(x * SQUARE_SIZE, y * SQUARE_SIZE),
                Size = new Size(SQUARE_SIZE, SQUARE_SIZE),
                BackColor = (x + y) % 2 == 0 ? Color.NavajoWhite : Color.Tan,
                x = x,
                y = y
            };
            square.Click += ClickSquare;
            Controls.Add(square);
            squares[x, y] = square;
        }

        private void PlacePieces()
        {
            PlacePiece(PieceType.Rook, PieceColor.White, "WhiteRookA", 0, 7);
            PlacePiece(PieceType.Knight, PieceColor.White,"WhiteKnightB", 1, 7);
            PlacePiece(PieceType.Bishop, PieceColor.White, "WhiteBishopC", 2, 7);
            PlacePiece(PieceType.Queen, PieceColor.White, "WhiteQueen", 3, 7);
            PlacePiece(PieceType.King, PieceColor.White, "WhiteKing", 4, 7);
            PlacePiece(PieceType.Bishop, PieceColor.White, "WhiteBishopF", 5, 7);
            PlacePiece(PieceType.Knight, PieceColor.White, "WhiteKnightG", 6, 7);
            PlacePiece(PieceType.Rook, PieceColor.White, "WhiteRookH", 7, 7);

            for (int i = 0; i < CHESS_BOARD_SIZE; i++) {
                PlacePiece(PieceType.Pawn, PieceColor.White, string.Concat("WhitePawn", LETTERS[i]), i, 6);
            }

            PlacePiece(PieceType.Rook, PieceColor.Black, "BlackRookA", 0, 0);
            PlacePiece(PieceType.Bishop, PieceColor.Black, "BlackBishopC", 2, 0);
            PlacePiece(PieceType.Knight, PieceColor.Black, "BlackKnightB", 1, 0);
            PlacePiece(PieceType.Queen, PieceColor.Black, "BlackQueen", 3, 0);
            PlacePiece(PieceType.King, PieceColor.Black, "BlackKing", 4, 0);
            PlacePiece(PieceType.Bishop, PieceColor.Black, "BlackBishopF", 5, 0);
            PlacePiece(PieceType.Knight, PieceColor.Black, "BlackKnightG", 6, 0);
            PlacePiece(PieceType.Rook, PieceColor.Black, "BlackRookH", 7, 0);

            for (int i = 0; i < CHESS_BOARD_SIZE; i++) {
                PlacePiece(PieceType.Pawn, PieceColor.Black, string.Concat("BlackPawn", LETTERS[i]), i, 1);
            }
        }


        private void PlacePiece(PieceType pt, PieceColor color, string name, int x, int y)
        {
            Piece piece;
            switch (pt) {
                case PieceType.King:
                     piece = new King(squares,color, x,y) {
                        Name = name,
                        Size = squares[x, y].Size,
                        BackColor = Color.Transparent,
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };
                    if (color == PieceColor.White) {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/white_king.png"));  
                    } else {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/black_king.png"));
                    }
                    break;
                case PieceType.Queen:
                    piece = new Queen(squares, color, x, y) {
                        Name = name,
                        Size = squares[x, y].Size,
                        BackColor = Color.Transparent,
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };
                    if (color == PieceColor.White) {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/white_queen.png"));
                    } else {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/black_queen.png"));
                    }
                    break;
                case PieceType.Rook:
                    piece = new Rook(squares,color, x,y) {
                        Name = name,
                        Size = squares[x, y].Size,
                        BackColor = Color.Transparent,
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };
                    if (color == PieceColor.White) {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/white_rook.png"));
                    } else {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/black_rook.png"));
                    }
                    break;
                case PieceType.Bishop:
                    piece = new Bishop(squares, color, x, y) {
                        Name = name,
                        Size = squares[x, y].Size,
                        BackColor = Color.Transparent,
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };
                    if (color == PieceColor.White) {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/white_bishop.png"));
                    } else {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/black_bishop.png"));
                    }
                    break;
                case PieceType.Knight:
                    piece = new Knight(squares, color, x, y) {
                        Name = name,
                        Size = squares[x, y].Size,
                        BackColor = Color.Transparent,
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };
                    if (color == PieceColor.White) {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/white_knight.png"));
                    } else {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/black_knight.png"));
                    }
                    break;
                default:
                    piece = new Pawn(squares, color, x, y) {
                        Name = name,
                        Size = squares[x, y].Size,
                        BackColor = Color.Transparent,
                        BackgroundImageLayout = ImageLayout.Stretch,
                    };
                    if (color == PieceColor.White) {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/white_pawn.png"));
                    } else {
                        piece.BackgroundImage = Image.FromFile(Path.Combine(Environment.CurrentDirectory, "img/black_pawn.png"));
                    }
                    break;
            }
            piece.Click += ClickPiece;
            squares[x,y].Controls.Add(piece);
            if (color == PieceColor.White) {
                whitePieces.Add(piece);
            } else {
                blackPieces.Add(piece);
            }
        }

        public enum PieceType
        {
            King,
            Queen,
            Rook,
            Bishop,
            Knight,
            Pawn
        }

        public enum PieceColor
        {
            White,
            Black
        }

        private void ClickPiece(object sender, EventArgs e)
        {
            if (selectedPiece != null) {
                Piece clickedPiece = sender as Piece;
                Square clickedSquare = clickedPiece.Parent as Square;
                if (clickedSquare.isHighlighted) {
                    MovePiece(selectedPiece, clickedSquare);
                    selectedPiece = null;
                    ResetSquares();
                    return;
                }
            } 
            ResetSquares();
            var piece = sender as Piece;
            Console.WriteLine(piece.Name);
            if (piece == selectedPiece) {
                selectedPiece = null;
                return;
            }
            selectedPiece = piece;

            Square[] potentialMoves = piece.GetMoves();
            foreach (Square s in potentialMoves) {
                if (s is null) continue;
                s.BackColor = Color.Red;
                s.isHighlighted = true;
            }
        }

        private void ClickSquare(object sender, EventArgs e)
        {
            Square square = sender as Square;
            if (!square.isHighlighted) return;
            MovePiece(selectedPiece, square);
            selectedPiece = null;
            ResetSquares();
        }

        private void MovePiece(Piece piece, Square square)
        {
            Square originalSquare = piece.Parent as Square;
            originalSquare.Controls.Clear();
            square.Controls.Clear();
            square.Controls.Add(selectedPiece);
            if (piece.GetType() == typeof(Pawn)) {
                Pawn p = selectedPiece as Pawn;
                p.FirstMoveOver();
            }
            piece.x = square.x;
            piece.y = square.y;

            SetTurn();
        }

        private void ResetSquares()
        {
            foreach (Square s in squares) {
                s.isHighlighted = false;
                s.BackColor = (s.x+ s.y) % 2 == 0 ? Color.NavajoWhite : Color.Tan;
            }
        }
    }
}
