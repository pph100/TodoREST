namespace TodoREST
{
    public static class Constants
    {
        // CoinMarketCap
        public static string CMC_AuthKey = "X-CMC_PRO_API_KEY";
        public static string CMC_AuthValue = "bebc3dfa-005c-4734-8662-dfbe6c50e084";
        public static string CMC_BaseURL = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest";


        // Cryptonator API
        public static string CN_BaseURL = "https://api.cryptonator.com/api/ticker/{0}";


        // Crypto Compare API
        public static string CC_BaseURL = "https://min-api.cryptocompare.com/data/pricemulti?{0}";
        public static string CC_AuthKey = "Apikey";
        public static string CC_AuthValue = "b4077178900c5e5e0bfe58060506951612994019de3139009d223a45690e0840";
        public static string CC_FromSym = "fsyms=";
        public static string CC_ToSym = "tsyms=";

        // my own API
        public static string BaseURL = "http://home.ulip.org";
        public static string UrlPathOrdered = "/api/todo_entry?_sort=Done,-Urgent,Duedate,Author";
        public static string UrlPathUnOrdered = "/api/todo_entry/{0}";
        public static string UrlPort = ":3000";
        public static string RestUrlOrdered = BaseURL + UrlPort + UrlPathOrdered;
        public static string RestUrlUnOrdered = BaseURL + UrlPort + UrlPathUnOrdered;
        public static string RestUrl = RestUrlUnOrdered;

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