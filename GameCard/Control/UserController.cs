using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MySqlX.XDevAPI;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control
{
    public class UserController
    {
        public UserController(WebSiteManager manager)
        {
            WebSiteManager = manager;
        }

        public WebSiteManager WebSiteManager { get; set; }

        public void TryLogUser(string login, string password)
        {
            var result = WebSiteManager.DatabaseController.LogUser(login, password);
            if (result == null)
            {
                //TODO erreur relog
            }
            else
            {
                WebSiteManager.CurrentUser = result;

                //TODO Aller au menu user
            }
        }
    }
}
