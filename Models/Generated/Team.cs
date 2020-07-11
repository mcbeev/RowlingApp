using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery;
using Kentico.Kontent.Delivery.Abstractions;

namespace RowlingApp.Models.Generated
{
    public partial class Team
    {
        public const string Codename = "team";
        public const string TeamMembersCodename = "teammembers";
        public const string TeamLogoCodename = "teamlogo";
        public const string TeamNameCodename = "teamname";
        public const string TeamCaptianCodename = "teamcaptian";
        public const string PageUrlCodename = "pageurl";
        public const string TeamScoreCodename = "teamscore";
        public const string TeamFramesleftCodename = "teamframesleft";

        public IEnumerable<object> TeamMembers { get; set; }
        public IEnumerable<Asset> TeamLogo { get; set; }
        public string TeamName { get; set; }
        public IEnumerable<object> TeamCaptian { get; set; }
        public string PageUrl { get; set; }
        public decimal? TeamFramesleft { get; set; }
        public decimal? TeamScore { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}
