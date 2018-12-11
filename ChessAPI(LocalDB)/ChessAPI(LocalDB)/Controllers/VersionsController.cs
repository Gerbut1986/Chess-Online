using System.Web.Http;

namespace ChessAPI_LocalDB_.Controllers
{
    public class Version
    {
        public string name;
        public string version;
    }
    public class VersionsController : ApiController
    {
        public Version GetVersion()
        {
            Version version = new Version()
            {
                name = "ChessAPI",
                version = "1.0"
            };
            return version;
        }
    }
}
