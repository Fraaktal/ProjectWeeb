using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.Pages
{
    public class InscriptionModel : PageModel
    {
        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPost()
        {
            string login = Request.Form["login"];
            string password = Request.Form["password"];
            string confpassword = Request.Form["confPassword"];
            if (confpassword.Equals(password))
            {
                var result = CWebSite.GetInstance().WebSiteManager.UserController.TryRegisterUser(login, password);

                if (result)
                {
                    return RedirectToPage("Connexion", "idInfo", new { idInfo = 3 });
                }
                else
                {
                    return RedirectToPage("Inscription", "idInfo", new { idInfo = 2 });
                }
            }
            else
            {
                return RedirectToPage("Inscription", "idInfo", new { idInfo = 1 });
            }
        }
    }
}
