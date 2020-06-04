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
            
        }

        public int UserId { get; set; }
        
        public void OnPostMatchMaking()
        {
            Response.Redirect("/MatchingQueue");
        }


    }
}