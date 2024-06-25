﻿using JDM_Casus_Blok4.UserClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JDM_Casus_Blok4.Classes
{
    public class Researcher : User
    {
        public List<Assessment> AllAssessments { get; set; }

        public Researcher(int id, string firstname, string lastname) : base(id, firstname, lastname)
        {
            AllAssessments = Assessment.GetAllAssessments();
        }




    }
}