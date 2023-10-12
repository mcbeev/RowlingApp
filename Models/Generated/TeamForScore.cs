using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontent.Ai.Delivery;
using Kontent.Ai.Delivery.Abstractions;

namespace RowlingApp.Models.Generated
{
    public partial class TeamForScore
    {
        public const string Codename = "team";
        public const string TeamNameCodename = "teamname";
        public const string TeamScoreCodename = "teamscore";
        public const string TeamFramesleftCodename = "teamframesleft";

        // public string TeamName { get; set; }
        public decimal? teamframesleft { get; set; }
        public decimal? teamscore { get; set; }
        public IContentItemSystemAttributes System { get; set; }
    }
}
