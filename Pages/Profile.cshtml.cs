using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.Pages
{
    public class ProfileModel : PageModel
    {
        public void OnGet()
        {
            string s = HttpContext.Session.GetString("user");

            if (s == null || s.Equals(""))
            {
                Response.Redirect("/Connexion");
            }
        }

        public int UserId { get; set; }
        
        public async Task<IActionResult> OnPostGamePlateau()
        {
            string s = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(s);
            string idGame = null;

            while (idGame == null)
            {
                idGame = CWebSite.GetInstance().GameManager.ConnectPlayerToGame(user);
                if (idGame == null)
                {
                    Thread.Sleep(1000);
                }
            }

            return RedirectToPage("GamePlateau", "ids", new {gameId = idGame, userId = user.Id});
        }
    }
}