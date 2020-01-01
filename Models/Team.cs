using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery;

namespace RowlingApp.Models
{
    public partial class Team
    {
        public const string Codename = "team";
        public const string TeamMembersCodename = "teammembers";
        public const string TeamLogoCodename = "teamlogo";
        public const string TeamNameCodename = "teamname";
        public const string TeamCaptianCodename = "teamcaptian";
        public const string PageUrlCodename = "pageurl";

        public IEnumerable<object> TeamNembers { get; set; }
        public IEnumerable<Asset> TeamLogo { get; set; }
        public string TeamName { get; set; }
        public IEnumerable<object> TeamCaptian { get; set; }
        public string PageUrl { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}
