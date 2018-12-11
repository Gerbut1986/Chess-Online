using ChessRules_ClassLibrary_;
using System;
using System.Data.Entity;
using System.Linq;

namespace ChessAPI_LocalDB_.Models
{
    public class Logic
    {
        private ChessContext db;
        public Logic()
        {
            db = new ChessContext();
        }
        public Game GetCurrentGame()
        {
            Game game;
            var currentGames = db.Games.Where(g => g.Status == "play").ToList();
            if (currentGames.Count() > 0)
                game = currentGames.First();
            else
                game = NewGame();
            return game;
        }
        public Game GetGame(int id)
        {
            Game game = db.Games.Find(id);
            return game;
        }
        Game NewGame()
        {
            Chess chess = new Chess();
            Game game = new Game();
            game.FEN = chess.fen;
            game.Status = "play";

            db.Games.Add(game);
            db.SaveChanges();

            return game;
        }
        public Game MakeMove(int id, string move)
        {
            Game game = GetGame(id);
            if (game == null) return game;
            if (game.Status != "play") return game;
            Chess chess = new Chess(game.FEN);

            if (!chess.IsValidMove(move)) // Являеться ли ход правильным?
                return game;

            chess = chess.Move(move);

            game.FEN = chess.fen;

            if (chess.IsCheckmate || chess.IsStalemate) // Если МАТ, или ПАТ
                game.Status = "done";

            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();

            return game;
        }
        public Game ResignGame(int id)
        {
            Game game = GetGame(id);
            if (game == null) return game;
            if (game.Status != "play") return game;
            game.Status = "done";
            db.Entry(game).State = EntityState.Modified;
            db.SaveChanges();
            return game;
        }
    }
}