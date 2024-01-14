using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staff_contact_app_winform
{
    public class StaffContact
    {
        public long id {  get; set; }
        public string staffType { get; set; }
        public string fullName { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }  
        public string lastName { get; set; }
        public string middleInitial {  get; set; }
        public string homePhone { get; set; }
        public string cellPhone { get; set; }
        public string officeExt { get; set; }
        public string irdNumber { get; set; }
        public string status { get; set; }
        public string manager { get; set; }

        public StaffContact(long id, string staffType, string title, 
            string firstName, string lastName, string middleInitial, 
            string homePhone, string cellPhone, string officeExt,
            string irdNumber, string status, string manager)
        {
            this.fullName = title + ". " + firstName + " " + middleInitial + ". " + lastName;
            this.id = id;
            this.staffType = staffType;
            this.title = title;
            this.firstName = firstName;
            this.lastName = lastName;
            this.middleInitial = middleInitial;
            this.homePhone = homePhone;
            this.cellPhone = cellPhone;
            this.officeExt = officeExt;
            this.irdNumber = irdNumber;
            this.status = status;
            this.manager = manager;
        }
    }
}
