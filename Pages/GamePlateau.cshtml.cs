using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using ProjectWeeb.GameCard.Business.BusinessData;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.Pages
{
    public class GamePlateauModel : PageModel
    {
        public void OnGet()
        {
        }

        public async void OnGetIds(string id, int userId)
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("http://trucmachinbiduleOVH/GameHub")
                .Build();

#if DEBUG
            Connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:50322/GameHub")
                .Build();
#endif
            await Connect();

            //string s = HttpContext.Session.GetString("user");
            //User user = JsonConvert.DeserializeObject<User>(s);

            Connection.InvokeAsync("PlayerConnectedOnGame", id, userId);
        }

        public HubConnection Connection { get; set; }

        public GameManager GameManager { get; set; }
        

        public async Task Connect()
        {
            try
            {
                if (Connection.State == HubConnectionState.Disconnected)
                {
                    await Connection.StartAsync();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
