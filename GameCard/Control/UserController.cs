﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MySqlX.XDevAPI;
using Org.BouncyCastle.Asn1.Ocsp;
using ProjectWeeb.GameCard.Business.BusinessData;
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

        public bool TryLogUser(string login, string password)
        {
            var result = WebSiteManager.DatabaseController.LogUser(login, password);

            if (result != null)
            {
                WebSiteManager.CurrentUser = result;
                return true;
            }

            return false;
        }

        public bool TryRegisterUser(string login, string password)
        {
            User user = new User()
            {
                Password = password,
                UserName = login,
                Level = 1
            };

            if (!WebSiteManager.DatabaseController.DoesUserExist(login))
            {
                WebSiteManager.DatabaseController.RegisterUser(user);

                return true;
            }

            return false;
        }
    }
}
