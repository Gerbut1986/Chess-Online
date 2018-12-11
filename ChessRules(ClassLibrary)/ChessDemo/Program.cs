using System;
using System.Text;
using ChessRules_ClassLibrary_;
namespace ChessDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            string fen = client.GetFenFromServer();
            Console.WriteLine(client.GameID);
            Chess chess = new Chess(fen);
            // chess.GetFigureAt(3, 5);
            // For a test - https://chessprogramming.wikispaces.com/Perft+Results
            // Console.WriteLine(NextMoves(4, chess));
            // Console.ReadKey();
            // return;
            while (true)
            {
                Console.Clear();
                Console.WriteLine(chess.fen);
                Print(ChessToAscii(chess));
                foreach (string moves in chess.YieldValidMoves())
                    Console.Write(moves + "  ");
                Console.WriteLine();
                string move = Console.ReadLine();
                if (move == "") break;
                if (move == "s")
                {
                    fen = client.GetFenFromServer();
                    chess = new Chess(fen);
                    continue;
                }

                if (!chess.IsValidMove(move))
                    continue;
                fen = client.SendMove(move);
            }
        }
        static int NextMoves(int step, Chess chess)
        {
            if (step == 0) return 1;
            int count = 0;
            foreach(string moves in chess.YieldValidMoves())
                count += NextMoves(step - 1, chess.Move(moves));
            return count;
        }
        static string ChessToAscii(Chess chess)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("  +----------------+");
            for (int y = 7; y >= 0; y--)
            {
                sb.Append(y + 1);
                sb.Append(" |");
                for(int x = 0; x < 8; x++)
                {
                    sb.Append(chess.GetFigureAt(x, y) + " ");
                }
                sb.AppendLine("|");
            }
            Console.ForegroundColor = ConsoleColor.Green;
            sb.AppendLine("  +----------------+");
            sb.AppendLine("   a b c d e f g h ");
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (chess.IsCheck)  sb.AppendLine("IS CHECK");
            if (chess.IsCheckmate)  sb.AppendLine("IS CHECKMATE");
            if (chess.IsStalemate)  sb.AppendLine("IS SALEMATE");
            return sb.ToString(); // return our all of Board  
        }
        static void Print(string text)
        {
            ConsoleColor old = Console.ForegroundColor;
            foreach(char x in text)
            {
                if (x >= 'a' && x <= 'z')
                    Console.ForegroundColor = ConsoleColor.Red;
                else  if (x >= 'A' && x <= 'Z')
                    Console.ForegroundColor = ConsoleColor.White;
                else Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(x);
            }
            Console.ForegroundColor = old;
        }
    }
}
