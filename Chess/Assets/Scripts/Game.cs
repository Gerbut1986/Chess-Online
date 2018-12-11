using ChessRules_ClassLibrary_;
using System.Collections.Generic;

namespace Assets.Scripts
{
    class Game
    {
        private Client client; 

        public Chess chess { get; private set; }
        public string figure { get; private set; }
        public bool isPromotionMove { get; private set; }
        public string promotionMove { get; private set; } // какой ход еще не был сделан до конца

        public void Init()
        {
            client = new Client();
            chess = new Chess();
            Refresh();
        }
        public bool Refresh()
        {
            string fen = client.GetFenFromServer();
            if (chess.fen == fen) return false;   // Если ничего не изменилось, ничего не делаем
            chess = new Chess(fen);
            return true;
        }
        public void NextMove(string e2, string e4)
        {
            figure = chess.GetFigureAt(e2).ToString();
            string move = figure + e2 + e4;
            if (move.Length != 5) return; // Проверка длины хода
            if (!chess.IsValidMove(move)) // Если ход некоректный
                return;
            if (figure == "P" && e4[1] == '8' ||    // e4 = "d8"
                figure == "p" && e4[1] == '1')
                if (chess.Move(move) != chess)
                {
                    // Pd7d8  +  Q
                    isPromotionMove = true;
                    promotionMove = move;  // содержиться строка, какой ход был сделан
                    return;
                }
                Move(move);
        }
        public void NextPromotionMove(string promotionFigure)
        {
            Move(promotionMove + promotionFigure);
            isPromotionMove = false;
            promotionMove = "";
        }
        public IEnumerable<string> GetMoves()
        {
            return chess.YieldValidMoves();
        }
        public string GetFigureAt(int x, int y)
        {
            return chess.GetFigureAt(x, y).ToString();
        }
        public void Move(string move) // Выполняет в шахматах следующий ход
        {
            string fen = client.SendMove(move);
            if (fen == chess.fen) return;  // Если fen с сервера == fen с наших шахмат, то мы возвращаемся
            chess = new Chess(fen); // Иначе мы создаем новые шахматы на основании серверного fen'a
        }
    }
}
