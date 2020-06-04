using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ProjectWeeb.GameCard.Business;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Control;
using ProjectWeeb.GameCard.EventArg;

namespace ProjectWeeb.Pages
{
    public class MatchingQueueModel : PageModel
    {
        public void OnGet()
        {
            string s = HttpContext.Session.GetString("user");
            User user = JsonConvert.DeserializeObject<User>(s);

            Player p = new Player(user.UserName,user.Level,user.Id,user.SelectedDeck);

            CWebSite.GetInstance().GameManager.ConnectPlayerToQueue(p);

            CWebSite.GetInstance().GameManager.PlayerConnectedToGame += ConnectPlayerToGame;
        }

        public int UserId { get; set; }
        
        private async void ConnectPlayerToGame(object sender, ConnectionEventArg e)
        {
            await Response.StartAsync();

            Response.Redirect("/GamePlateau");
        }
    }
}