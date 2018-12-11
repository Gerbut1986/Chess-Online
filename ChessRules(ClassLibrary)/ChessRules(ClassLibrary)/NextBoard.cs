﻿using System.Text;

namespace ChessRules_ClassLibrary_
{
    class NextBoard : Board
    {
        FigureMoving fm;
        public NextBoard(string fen, FigureMoving fm)
            : base(fen)
        {
            this.fm = fm;
            MoveFigures();
             
            DropEnpassant();
            SetEnpassant();

            MoveCastlingRook();
            UpdateCastleFlags();

            MoveNumber();
            MoveColor();
            GenerateFEN();
        }
        void DropEnpassant()
        {
            if (fm.to == enpassant)
                if (fm.figure == Figure.whitePawn ||
                    fm.figure == Figure.blackPawn)
                    SetFigureAt(new Square(fm.to.x, fm.from.y), Figure.none);
        }
        void SetEnpassant()
        {
            enpassant = Square.none;
            if (fm.figure == Figure.whitePawn)
                if (fm.from.y == 1 && fm.to.y == 3)
                    enpassant = new Square(fm.from.x, 2);
            else  // здесь была ошибка..
            if (fm.figure == Figure.blackPawn)
                if (fm.from.y == 6 && fm.to.y == 4)
                    enpassant = new Square(fm.from.x, 5);
        }
        void MoveColor()
        {
            moveColor = moveColor.FlipColor();
        }
        void MoveCastlingRook()
        {   //
            // For WHITE Figures:-----------------------------------------------------------------------------------------
            // 
            if (fm.figure == Figure.whiteKing) 
            if (fm.from == new Square("e1")) 
            if (fm.to == new Square("g1")) 
             {
                  SetFigureAt(new Square("h1"), Figure.none);
                  SetFigureAt(new Square("f1"), Figure.whiteRook);
                        return;
             } 

            if (fm.figure == Figure.whiteKing) 
            if (fm.from == new Square("e1")) 
            if (fm.to == new Square("c1")) 
            {
                 SetFigureAt(new Square("a1"), Figure.none);
                 SetFigureAt(new Square("d1"), Figure.whiteRook);
                        return;
            }
            //
            // For BLACK Figures:-----------------------------------------------------------------------------------------
            //
            if (fm.figure == Figure.blackKing)
            if (fm.from == new Square("e8")) 
            if (fm.to == new Square("g8")) 
            {
                SetFigureAt(new Square("h8"), Figure.none);
                SetFigureAt(new Square("f8"), Figure.blackRook);
                        return;
            }

            if (fm.figure == Figure.blackKing) 
            if (fm.from == new Square("e8")) 
            if (fm.to == new Square("c8")) 
            {
                SetFigureAt(new Square("a8"), Figure.none);
                SetFigureAt(new Square("d8"), Figure.blackRook);
                        return;
            }
        }
        void MoveNumber()
        {
            if (moveColor == Color.black)
                moveNumber++;
        }
        void MoveFigures()
        {
            SetFigureAt(fm.from, Figure.none);
            SetFigureAt(fm.to, fm.PlacedFigure);
        }
        void SetFigureAt(Square square, Figure figure)
        {
            if (square.OnBoard())
            {
                figures[square.x, square.y] = figure;
            }
        }
        void UpdateCastleFlags()
        {
            switch (fm.figure)
            {
                case Figure.whiteKing:
                    canCastleA1 = false;
                    canCastleH1 = false;
                    return;

                case Figure.whiteRook:
                    if (fm.from == new Square("a1"))
                        canCastleA1 = false;
                    if (fm.from == new Square("h1"))
                        canCastleH1 = false;
                    return;

                case Figure.blackKing:
                    canCastleA8 = false;
                    canCastleH8 = false;
                    return;

                case Figure.blackRook:
                    if (fm.from == new Square("a8"))
                        canCastleA8 = false;
                    if (fm.from == new Square("h8"))
                        canCastleH8 = false;
                    return;

                default:
                    return;
            }
        }
        void GenerateFEN()
        {
            fen = FenFigures() + " " +
                  FenMoveColor() + " " +
                  FenCastleFlags() + " " +
                  FenEspassant() + " " +
                  FenDrawNumber() + " " +
                  FenMoveNumber();
        }
        string FenFigures()
        {
            StringBuilder sb = new StringBuilder();
            for (int y = 7; y >= 0; y--)
            {
                for (int x = 0; x < 8; x++)
                {
                    // Якщо зустрічаємо 0 , то заміняємо їх на '1' иначе потрібним символом:
                    sb.Append(figures[x, y] == Figure.none ? '1' : (char)figures[x, y]);
                }
                if (y > 0)
                    sb.Append("/");
            }
            string eight = "11111111";
            for (int j = 8; j >= 2; j--)
            {
                sb.Replace(eight.Substring(0, j), j.ToString());
            }
            return sb.ToString();
        }
        string FenMoveColor()
        {
            return moveColor == Color.white ? "w" : "b";
        }
        string FenCastleFlags()
        {
            string flags =
                 (canCastleA1 ? "Q" : "") +
                 (canCastleH1 ? "K" : "") +
                 (canCastleA8 ? "q" : "") +
                 (canCastleH8 ? "k" : "");
            return flags == "" ? "-" : flags;
        }
        string FenEspassant()
        {
            return enpassant.Name;
        }
        string FenDrawNumber()
        {
            return drawNumber.ToString();
        }
        string FenMoveNumber()
        {
            return moveNumber.ToString();
        }
    }
}