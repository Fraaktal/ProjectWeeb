﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using ProjectWeeb.GameCard.Manager;

namespace ProjectWeeb.GameCard.Control
{
    public class CWebSite
    {
        private static CWebSite _instance;

        private CWebSite()
        {
            WebSiteManager = new WebSiteManager();
        }
        
        public static CWebSite GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CWebSite();
            }

            return _instance;
        }

        public WebSiteManager WebSiteManager { get; set; }
    }
}