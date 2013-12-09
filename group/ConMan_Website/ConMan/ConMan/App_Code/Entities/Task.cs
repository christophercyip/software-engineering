using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConMan.App_Code.Entities
{
    public class Task
    {
        private Int64 taskID;
        private Int64 teamID;
        private String name;
        private DateTime dueDate;
        private String description;

        public Task()
        {
            taskID = 0;
            teamID = 0;
            name = "";
            dueDate = DateTime.Now;
            description = "";
        }

        public Task(Int64 taskID, Int64 teamID, String name, DateTime dueDate, String description)
        {
            this.taskID = taskID;
            this.teamID = teamID;
            this.name = name;
            this.dueDate = dueDate;
            this.description = description;
        }

        public Int64 TaskID { get; set; }

        public Int64 TeamID { get; set; }

        public String Name { get; set; }

        public DateTime DueDate { get; set; }

        public String Description { get; set; }
    }
}