using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectWeeb.GameCard.Control;

namespace ProjectWeeb.Pages
{
    public class InscriptionModel : PageModel
    {
        public void OnGet()
        {

        }

        public void OnPost()
        {
            string login = Request.Form["login"];
            string password = Request.Form["password"];
            string confpassword = Request.Form["confPassword"];
            if (confpassword.Equals(password))
            {
                var result = CWebSite.GetInstance().WebSiteManager.UserController.TryRegisterUser(login, password);

                if (result)
                {
                    Response.Redirect("/Connexion");
                }
            }
            else
            {
                
            }
            
            //TODO display errors

        }
    }
}
