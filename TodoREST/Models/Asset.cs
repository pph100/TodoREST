using System;
using Xamarin.Forms;

namespace TodoREST
{

    // for CryptoNator API
    public class TickerItem
    {
        public string @base { get; set; }
        public string cryptoCode { get; set; }
        public string target { get; set; }
        public string price { get; set; }
        public string volume { get; set; }
        public string change { get; set; }
    }

    public class CryptoItem
    {
        public TickerItem ticker { get; set; }
        public int timestamp { get; set; }
        public bool success { get; set; }
        public string error { get; set; }
        // added by me
        public string stock { get; set; }
        public string value { get; set; }
        public string cryptoName { get; set; }
        public string DttmLastUpdated { get; set; }
    }
}
