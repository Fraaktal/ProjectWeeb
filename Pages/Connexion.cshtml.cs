using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Client;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.Pages
{
    public class ConnectionModel : PageModel
    {
        public void OnGet()
        {
            var connexion = new HubConnectionBuilder().WithUrl("http://localhost:50322/GameHub").Build();
        }

        public void SubmitBtn_Click()
        {
            string login = "";
            string password = "";

            CWebSite.GetInstance().WebSiteManager.UserController.TryLogUser(login,password);
        }
    }
}
