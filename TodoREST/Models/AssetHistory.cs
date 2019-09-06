
using System;

namespace TodoREST
{
    public class AssetHistory
    {
        public double daily_avg { get; set; }
        public DateTime dt { get; set; }
        public string DT_YMD { get; set; }
        public string DT_DMY { get; set; }
        public string AssetTicker { get; set; }
        public string AssetName { get; set; }
        public string AssetStock { get; set; }
        public string avg_NK_2 { get; set; }
        public string avg_NK_4 { get; set; }
        public string daily_value_formatted { get; set; }
        public string daily_value { get; set; }
    }
}
