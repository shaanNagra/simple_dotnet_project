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
        /// <summary>
        /// Parsers form to prevent incorrect data before raising an event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveContact_Click(object sender, EventArgs e)
        {
            // FULL NAME VALIDATION
            if (string.Empty == textBoxFirstName.Text || 
                string.Empty == textBoxMiddleInitial.Text || 
                string.Empty == textBoxLastName.Text) 
            {
                MessageBox.Show("Please provide full name", "Incomplete form",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // CELL PHONE VALIDATION
            if (string.Empty == textBoxCellPhone.Text)
            {
                MessageBox.Show("Please provide a cell phone number", "Incomplete form",
                MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return;
            }

            // IRD NUMBER VALIDATION
            // optional field so not required but it provided must be correct
            if (string.Empty != textBoxIRDNumber.Text)
            {
                if (textBoxIRDNumber.Text.Length > 10 || 7 > textBoxIRDNumber.Text.Length)
                {
                    MessageBox.Show("Please provide a valid IRD number \nIncorrect length", "Incomplete form",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                foreach (char c in textBoxIRDNumber.Text)
                {
                    if (c < '0' || c > '9')
                    {
                        MessageBox.Show("Please provide a valid IRD number \nNumbers only", "Incomplete form",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }

            // STATUS VALIDATION
            if (true != (radioButtonActive.Checked || radioButtonInactive.Checked || radioButtonPending.Checked))
            {
                MessageBox.Show("Status must be selected", "Incomplete form",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // MANAGER VALIDATION
            // if a employee it must link to a manager
            if (true == radioButtonEmployee.Checked)
            {
                if(-1 == comboBoxStaffsManager.SelectedIndex)
                {
                    MessageBox.Show("Must select manager if employee", "Incomplete form", 
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
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
            }
            comboBoxStaffsManager.DataSource = items;
            comboBoxStaffsManager.SelectedIndex = -1;
            return;
        }

        public void loadContact(StaffContact contact)
        {
            if (null != contact.title)
            {
                comboBoxStaffTitle.SelectedIndex = comboBoxStaffTitle.FindString(contact.title);
            }

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
