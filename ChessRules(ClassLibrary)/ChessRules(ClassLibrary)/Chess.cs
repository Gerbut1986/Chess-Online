using System.Collections.Generic;
namespace ChessRules_ClassLibrary_
{
    public class Chess
    {
        Board board;
        Moves moves;
        public string fen
        {
            get
            {
                return board.fen;
            }
        }
        // ШАХ:
        public bool IsCheck { get; private set; }
        // МАТ:
        public bool IsCheckmate { get; private set; }
        // ПАТ:
        public bool IsStalemate { get; private set; }
        public Chess(string fen = "rnbqkbnr/pppppppp/8/8/8/8/PPPPPPPP/RNBQKBNR w KQkq - 0 1")
        {
            board = new Board(fen);
            moves = new Moves(board);
            SetCheckFlags();
        }
        Chess(Board board)
        {
            this.board = board;
            moves = new Moves(board);
            SetCheckFlags();
        }
        void SetCheckFlags()
        {
            IsCheck = board.IsCheck();
            IsCheckmate = false;
            IsStalemate = false;
            foreach (string moves in YieldValidMoves())
                return;
            if (IsCheck)
                IsCheckmate = true;
            else IsStalemate = true;
        }
        public bool IsValidMove(string move)
        {
            if (move.Length < 5) return false;
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm))
                return false;
            if (board.IsCheckAfter(fm))
                return false;
            return true;
        }
        public Chess Move(string move)
        {
            //if (!IsValidMove(move))
            //    return this;    
            FigureMoving fm = new FigureMoving(move);
            if (!moves.CanMove(fm))
                return this;
            if (board.IsCheckAfter(fm)) // Если шах после хода белых
                return this;
            Board nextBoard = board.Move(fm);
            Chess nextChess = new Chess(nextBoard); 
            return nextChess;
        }
        public char GetFigureAt(int x, int y)
        {
            Square square = new Square(x, y);
            Figure figure = board.GetFigureAt(square);

            return figure == Figure.none ? '.' : (char)figure;
        }
        public char GetFigureAt(string xy)
        {
            Square square = new Square(xy);
            Figure figure = board.GetFigureAt(square);

            return figure == Figure.none ? '.' : (char)figure;
        }
        // Перебрать(генерируем) все возможные ходы (клетки):
        public IEnumerable<string> YieldValidMoves()
        {
            foreach(FigureOnSquare fs in board.YieldMyFigureOnSquares())
            {
                foreach(Square to in Square.YieldBoardSquares())
                {
                    foreach(Figure promotion in fs.figure.YieldPromotions(to))
                    {
                        FigureMoving fm = new FigureMoving(fs, to, promotion);
                        if (moves.CanMove(fm))
                            if (!board.IsCheckAfter(fm))
                                yield return fm.ToString();
                    }
                  
                }
            }
        }
    }
}
