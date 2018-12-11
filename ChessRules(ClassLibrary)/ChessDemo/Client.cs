using System;
using System.Net;

namespace ChessDemo
{
    class Client
    {
        public const string address = "http://chess-online.somee.com/api/Chess/";
        WebClient web;
        public int GameID { get; private set; }
        public Client()
        {
            web = new WebClient();
        }
        public string GetFenFromServer()
        {
            string json = web.DownloadString(address);
            GameID = GetIdFromJSON(json);
            string fen = GetFenFromJSON(json);
            return fen;
        }
        public string SendMove(string move)// Метод для отправки хода:
        {
            string json = web.DownloadString(address + GameID + "/" + move);
            string fen = GetFenFromJSON(json);
            return fen;
        }
        int GetIdFromJSON(string json)
        {
            // {"ID":5,"FEN":"rn1qkbnr/pppBpppp/8/8/8/2N5/PPPP1PPP/R1BQK1NR b KQkq - 0 5","Status":"play"}
            //  ^    ^^
            //  x    yz
            int x = json.IndexOf("\"ID\"");
            int y = json.IndexOf(":", x) + 1;
            int z = json.IndexOf(",", y);
            string id = json.Substring(y, z - y);
            return Convert.ToInt32(id);
        }
        string GetFenFromJSON(string json)
        {
            // {"ID":5,"FEN":"rn1qkbnr/pppBpppp/8/8/8/2N5/PPPP1PPP/R1BQK1NR b KQkq - 0 5","Status":"play"}
            //         ^    ^ ^                                                         ^
            //         x      y                                                         z
            int x = json.IndexOf("\"FEN\"");
            int y = json.IndexOf(":\"", x) + 2;
            int z = json.IndexOf("\"", y);
            string fen = json.Substring(y, z - y);
            return fen;
        }
    }
}
