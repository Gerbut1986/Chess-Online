using System.Web.Http;
using ChessAPI_LocalDB_.Models;

namespace ChessAPI_LocalDB_.Controllers
{
    public class ChessController : ApiController
    {
        // GET: api/Chess
        public Game GetCurrentGame()
        {
            Logic logic = new Logic();
            return logic.GetCurrentGame();
        }
        public Game GetGameById(int id)
        {
            Logic logic = new Logic();
            return logic.GetGame(id);
        }
        public Game GetMoves(int id, string move) // Pe2e4
        {
            Logic logic = new Logic();
            if (move == "resign")
                return logic.ResignGame(id);
            else
                return logic.MakeMove(id, move);
        }
    }
}