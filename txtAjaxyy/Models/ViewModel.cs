using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace txtAjaxyy.Models
{
    public class ViewModel
    {
        public int work { get; set; }
        public string jobID { get; set; }

        public DateTime workTime { get; set; }

        public string punchBell { get; set; }

        public string name { get; set; }
        public ArrayList ayList { get; set; }

        public string getJobID { get; set; }
    }
}