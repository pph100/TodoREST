﻿namespace TodoREST
{
    public static class Constants
    {
        // Cryptonator API
        public static string CN_BaseURL = "https://api.cryptonator.com/api/ticker/{0}";

        // my own Crypto-Rate API
        public static string BaseURL = "http://home.ulip.org";
        public static string UrlPathOrdered = "/api/todo_entry?_sort=Done,-Urgent,Duedate,Author";
        public static string UrlPathUnOrdered = "/api/todo_entry/{0}";
        public static string UrlPort = ":3000";
        public static string RestUrlOrdered = BaseURL + UrlPort + UrlPathOrdered;
        public static string RestUrlUnOrdered = BaseURL + UrlPort + UrlPathUnOrdered;
        public static string RestUrl = RestUrlUnOrdered;

        // Rate Api
        public static string RateURL = "http://home.ulip.org";
        public static string RateXAG = "/api/rates_view_silber/";
        public static string RateXAU = "/api/rates_view_gold/";
        public static string RatePort = ":3001";
        public static string GoldUrl = RateURL + RatePort + RateXAU;
        public static string SilberUrl = RateURL + RatePort + RateXAG;

        // public static string PersonUrl = "http://home.ulip.org:3000/api/person/{0}";
        public static string UrlPersonPath = "/api/person/{0}";
        public static string PersonUrl = BaseURL + UrlPort + UrlPersonPath;

        // Asset
        public static string UrlAssetPath = "/api/asset/{0}";
        public static string AssetUrl = BaseURL + UrlPort + UrlAssetPath;

        // Stock
        public static string UrlStockPath = "/api/stock/{0}";
        public static string StockUrl = BaseURL + UrlPort + UrlStockPath;
    }
}