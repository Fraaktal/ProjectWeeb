using System.Text.Encodings.Web;
using System.Threading.Tasks;
using LiteDB.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.Pages
{
    public class ConnectionModel : PageModel
    {
        public void OnGet()
        {
            
        }

        public async Task<IActionResult> OnPost()
        {
            string login = Request.Form["login"];
            string password = Request.Form["password"];
            
            User user = CWebSite.GetInstance().WebSiteManager.UserController.TryLogUser(login, password);
            if (user != null)
            {
                string s = JsonConvert.SerializeObject(user);

                HttpContext.Session.SetString("user",s);

                Response.Redirect("/Profile");
            }
            else
            {
                
                return RedirectToPage("Connexion", "idInfo", new { idInfo = 4});
            }

            return null;
        }
    }
}
