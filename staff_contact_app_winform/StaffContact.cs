using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace staff_contact_app_winform
{
    /// <summary>
    /// Utilised to mainatin all details about every staff member in the 
    /// system.
    /// </summary>
    public class StaffContact
    {
        public long id {  get; set; }
        public string staffType { get; set; }
        public string fullName { get; set; }
        public string title { get; set; }
        public string firstName { get; set; }  
        public string lastName { get; set; }
        public string middleInitial { get; set; }
        public string homePhone { get; set; }
        public string cellPhone { get; set; }
        public string officeExt { get; set; }
        public string irdNumber { get; set; }
        public string status { get; set; }
        public long manager_id { get; set; }

        public StaffContact() 
        {
            this.fullName = string.Empty;
            this.staffType = string.Empty;
            this.title = string.Empty;
            this.firstName = string.Empty;
            this.lastName = string.Empty;
            this.middleInitial = string.Empty;
            this.homePhone = string.Empty;
            this.cellPhone = string.Empty;
            this.officeExt = string.Empty;
            this.irdNumber = string.Empty;
            this.status = string.Empty;
        }

        public StaffContact(string staffType, string title,
            string firstName, string lastName, string middleInitial,
            string homePhone, string cellPhone, string officeExt,
            string irdNumber, string status, long manager_id)
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
            this.manager_id = manager_id;
        }

        public StaffContact(long id, string staffType, string title, 
            string firstName, string lastName, string middleInitial, 
            string homePhone, string cellPhone, string officeExt,
            string irdNumber, string status, long manager_id)
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
            this.manager_id = manager_id;
        }

        private void updateFullName()
        {
            fullName = title + ". " + firstName + " " + middleInitial + ". " + lastName;
        }

        public void updateTitle(string title) 
        {
            this.title = title;
            updateFullName();
        }

        public void updateFirstName(string firstName) 
        {
            this.firstName = firstName;
            updateFullName();
        }

        public void updateLastName(string lastName)
        {
            this.lastName = lastName;
            updateFullName();
        }

        public void updateMiddleInitial(string middleInitial)
        {
            this.middleInitial = middleInitial;
            updateFullName();
        }
    }
}
