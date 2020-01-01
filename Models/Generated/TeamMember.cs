using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kentico.Kontent.Delivery;

namespace RowlingApp.Models.Generated
{
    public class TeamMember
    {
        public const string Codename = "teammember";
        public const string TeamMemberEmailCodename = "teammemberemail";
        public const string TeamMemberNameCodename = "teammembername";

        public string TeamMemberEmail { get; set; }
        public string TeamMemberName { get; set; }
        public ContentItemSystemAttributes System { get; set; }
    }
}
