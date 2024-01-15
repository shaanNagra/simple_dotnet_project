using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace staff_contact_app_winform
{
    public partial class EditContactControl : UserControl
    {
        List<StaffManager> staffManagerList;
        public EditContactControl()
        {
            staffManagerList = new List<StaffManager>();
            InitializeComponent();
        }

        private void radioButtonEmployee_CheckedChanged(object sender, EventArgs e)
        {
            // Make a manager selectable only if staff(type) is a employee
            if (true == radioButtonEmployee.Checked)
            {
                labelManager.Visible = true;
                comboBoxStaffsManager.Visible = true;
                return;
            }
            labelManager.Visible = false;
            comboBoxStaffsManager.Visible = false;
            comboBoxStaffsManager.SelectedIndex = -1;
        }

        private void buttonSaveContact_Click(object sender, EventArgs e)
        {

            // Link selected manager if staff is employee
            if (true == radioButtonEmployee.Checked)
            {

            }
            saveContact_Clicked?.Invoke(this, e);
        }
        public event EventHandler saveContact_Clicked;

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(comboBoxStaffsManager.SelectedValue);
            var mb = MessageBox.Show("Are you sure you want to cancel?", "Cancel edit/add contact", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mb == DialogResult.Yes)
            {
                clearContactDetails();
                cancelSave_Clicked?.Invoke(this, e);
            }
        }
        public event EventHandler cancelSave_Clicked;

        public void updateManagers(List<StaffManager> staffManagerList)
        {
            this.staffManagerList = staffManagerList;
            comboBoxStaffsManager.Items.Clear();
            List<Object> items = new List<Object>();
            comboBoxStaffsManager.DisplayMember = "Text";
            comboBoxStaffsManager.ValueMember = "Value";
            foreach (StaffManager manager in this.staffManagerList)
            {
                items.Add(new { Text = manager.fullName, Value = manager.manager_id });
                //comboBoxStaffsManager.Items.Add(
                //    new { Text = manager.fullName, Value = manager.manager_id }
                //    );
            }
            comboBoxStaffsManager.DataSource = items;
            return;
        }

        public void loadContact(StaffContact contact)
        {
            comboBoxStaffTitle.SelectedIndex = comboBoxStaffTitle.FindString(contact.title);

            if ("Employee" == contact.staffType)
            {
                radioButtonEmployee.Checked = true;
            }
            else
            {
                radioButtonManager.Checked = true;
            }
            
            textBoxFirstName.Text = contact.firstName;
            textBoxLastName.Text = contact.lastName;
            textBoxMiddleInitial.Text = contact.middleInitial;

            textBoxHomePhone.Text = contact.homePhone;
            textBoxCellPhone.Text = contact.cellPhone;
            textBoxOfficeExt.Text = contact.officeExt;
            textBoxIRDNumber.Text = contact.irdNumber;

            if ("Active" == contact.status)
            {
                radioButtonActive.Checked = true;
            }
            else if ("Inactive" == contact.status)
            {
                radioButtonInactive.Checked = true;
            }
            else 
            {
                radioButtonPending.Checked = true;
            }
            Debug.WriteLine(contact.manager_id);
            comboBoxStaffsManager.SelectedValue = contact.manager_id;

        }

        private void clearContactDetails()
        {
            textBoxFirstName.Text = string.Empty;
            textBoxMiddleInitial.Text = string.Empty;
            textBoxLastName.Text = string.Empty;
            comboBoxStaffTitle.SelectedIndex = -1;
            comboBoxStaffsManager.SelectedIndex = -1;
            textBoxHomePhone.Text = string.Empty;
            textBoxCellPhone.Text = string.Empty;  
            textBoxIRDNumber.Text = string.Empty;
        }

    }
}
