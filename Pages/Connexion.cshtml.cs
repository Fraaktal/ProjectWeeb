using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Client;
using ProjectWeeb.GameCard.Control;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.Pages
{
    public class ConnectionModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            string login = Request.Form["login"];
            string password = Request.Form["password"];
            
            bool result = CWebSite.GetInstance().WebSiteManager.UserController.TryLogUser(login, password);
            if (result)
            {
                Response.Redirect("/Profile");
            }
        }

        
    }
}
