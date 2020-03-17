using LiteDB;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.GameCard.Manager
{
    public class WebSiteManager
    {
        public WebSiteManager(LiteDatabase database)
        {
            DatabaseController = new DatabaseController(this, database);
        }

        public DatabaseController DatabaseController { get; set; }
    }
}
