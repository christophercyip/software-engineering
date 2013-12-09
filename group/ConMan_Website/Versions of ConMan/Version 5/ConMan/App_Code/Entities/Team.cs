using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConMan.App_Code.Entities
{
    public class Team
    {
        private Int64 teamID;
        private String teamName;
        private Int64 teamOwner;

        public Team()
        {
            this.teamID = 0;
            this.teamName = null;
            this.teamOwner = 0;
        }

        public Team(Int64 id, String name, Int64 ownerID)
        {
            this.teamID = id;
            this.teamName = name;
            this.teamOwner = ownerID;
        }

        public Int64 TeamID { get; set; }
        public String TeamName { get; set; }
        public Int64 TeamOwner { get; set; }
    }
}