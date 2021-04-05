﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DISFinalProject.Models
{
    public class Park
    {
        public string ID { get; set; }
        public string url { get; set; }
        public string fullName { get; set; }
        public string parkCode { get; set; }
        public string description { get; set; }
        public string states { get; set; }
        public ICollection<ParkActivity> activities { get; set; }
        public ICollection<ParkTopic> topics { get; set; }
        
    }

    public class Activity
    {
        public string ID { get; set; }
        public string name { get; set; }
        public ICollection<ParkActivity> parks { get; set; }
    }

    public class ParkActivity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Activity activity { get; set; }
        public Park park { get; set; }
    }

    public class ParkTopic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public Topic topic { get; set; }
        public Park park { get; set; }
    }
    public class Topic
    {
        public string ID { get; set; }
        public string name { get; set; }
        public ICollection<ParkTopic> parks { get; set; }
    }
}



