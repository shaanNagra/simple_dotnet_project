using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace staff_contact_app_winform
{
    /// <summary>
    /// Control to display a staff contacts full details.
    /// </summary>
    public partial class ContactDetailsControl : UserControl
    {
        public StaffContact selectedContact;
        public ContactDetailsControl()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        public void clearContact()
        {
            selectedContact = null;
            textBoxDisplayName.Text =   string.Empty;
            textBoxDisplayStaffType.Text = string.Empty;
            textBoxDisplayStatus.Text = string.Empty;
            textBoxDisplayManager.Text = string.Empty;
            textBoxDisplayHomePhone.Text = string.Empty;
            textBoxDisplayCellPhone.Text = string.Empty;
            textBoxDisplayOfficeExt.Text = string.Empty;
            textBoxDisplayIRDNumber.Text = string.Empty;
        }

        /// <summary>
        /// loads display fields with the details from the provided staffcontact.
        /// </summary>
        /// <param name="contact">The contact whos details are to be displayed.</param>
        /// <param name="managers">List to search, finds display name of manager in list that matches via id.</param>
        public void displayContact(StaffContact contact, List<StaffManager> managers)
        {
            selectedContact = contact;
            // Set name.
            textBoxDisplayName.Text = contact.fullName;
            // Set staff type.
            textBoxDisplayStaffType.Text = contact.staffType;
            // Set status.
            textBoxDisplayStatus.Text = contact.status;
            // Set manager.
            // No manager to set, staff member is a manager.
            if(contact.staffType.Equals("Manager"))
            {
                textBoxDisplayManager.Visible = false;
            }
            // Loook for manager in list to set.
            else
            {
                if (true == managers.Exists(x => x.manager_id == contact.manager_id))
                {
                    var manager = managers.Find(x => x.manager_id == contact.manager_id);
                    textBoxDisplayManager.Visible = true;
                    textBoxDisplayManager.Text = manager.fullName;
                }
            }
            textBoxDisplayHomePhone.Text = contact.homePhone;
            textBoxDisplayCellPhone.Text = contact.cellPhone;
            textBoxDisplayOfficeExt.Text = contact.officeExt;
            textBoxDisplayIRDNumber.Text = contact.irdNumber;
        }
    }
}
