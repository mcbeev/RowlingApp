using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;

namespace RowlingApp.Models.Generated
{
    public partial class TeamForScore
    {
        public const string Codename = "team";
        public const string TeamNameCodename = "teamname";
        public const string TeamScoreCodename = "teamscore";
        public const string TeamFramesleftCodename = "teamframesleft";

        public string TeamName { get; set; }
        public decimal? TeamFramesleft { get; set; }
        public decimal? TeamScore { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}
