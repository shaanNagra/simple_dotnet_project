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
    public partial class ContactDetailsControl : UserControl
    {
        public StaffContact selectedContact;
        public ContactDetailsControl()
        {
            InitializeComponent();
        }

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

        public void loadContact(StaffContact contact)
        {
            selectedContact = contact;
            textBoxDisplayName.Text = contact.fullName;
            textBoxDisplayStaffType.Text = contact.staffType;
            textBoxDisplayStatus.Text = contact.status;
            if("Manager" == contact.status )
            {
                textBoxDisplayManager.Visible = false;
            }
            else
            {
                textBoxDisplayManager.Visible = true;
                textBoxDisplayManager.Text = contact.manager;
            }
            textBoxDisplayHomePhone.Text = contact.homePhone;
            textBoxDisplayCellPhone.Text = contact.cellPhone;
            textBoxDisplayOfficeExt.Text = contact.officeExt;
            textBoxDisplayIRDNumber.Text = contact.irdNumber;
        }
        public StaffContact getSelectedContact()
        {
            return selectedContact;
        }
    }
}
