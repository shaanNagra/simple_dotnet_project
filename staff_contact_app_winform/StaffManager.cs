﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staff_contact_app_winform
{
    public class StaffManager
    {
        public string fullName {  get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string middleInitial {  get; set; }
        public long manager_id { get; set; }

        public StaffManager(string firstName, string lastName, 
            string middleInitial, long manager_id)
        {
            this.fullName = firstName + " " + middleInitial + ". " + lastName;
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleInitial = middleInitial;
            this.manager_id = manager_id;
        }
    }
}