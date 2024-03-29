﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kontent.Ai.Delivery;

namespace RowlingApp.Models
{
    public class Team
    {
        public IEnumerable<object> TeamMembers { get; set; }
        public string TeamLogo { get; set; }
        public string TeamName { get; set; }
        public IEnumerable<object> TeamCaptian { get; set; }
        public string PageUrl { get; set; }
        public int TeamScore { get; set; }
        public int TeamFramesLeft { get; set; }

        public string Id { get; set; }
        public string CodeName { get; set; }
    }
}
