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
        //Declaration
        List<StaffManager> staffManagerList;

        private StaffContact contactInProcess;


        public EditContactControl()
        {
            staffManagerList = new List<StaffManager>();

            contactInProcess = null;

            InitializeComponent();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            if(false == isFormValid())
            {
                return;
            }
            loadFormIntoContact();
            clearContactDetails();
            saveContact_Clicked?.Invoke(this, e);
        }
        public event EventHandler saveContact_Clicked;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            Debug.WriteLine(comboBoxStaffsManager.SelectedValue);
            var mb = MessageBox.Show("Are you sure you want to cancel?", "Cancel edit/add contact", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (mb == DialogResult.Yes)
            {
                contactInProcess = null;
                clearContactDetails();
                cancelSave_Clicked?.Invoke(this, e);
            }
        }
        public event EventHandler cancelSave_Clicked;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="staffManagerList"></param>
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


        /// <summary>
        /// Loads a StaffContacts data into the editing form. Allows controller to submit
        /// data to the form.
        /// </summary>
        /// <param name="contact"></param>
        private void loadContactIntoForm()
        {

            if (null != contactInProcess.title)
            {
                comboBoxStaffTitle.SelectedIndex = comboBoxStaffTitle.FindString(contactInProcess.title);
            }

            if ("Employee" == contactInProcess.staffType)
            {
                radioButtonEmployee.Checked = true;
                comboBoxStaffsManager.SelectedValue = contactInProcess.manager_id;
            }
            else
            {
                radioButtonManager.Checked = true;
            }
            
            textBoxFirstName.Text = contactInProcess.firstName;
            textBoxLastName.Text = contactInProcess.lastName;
            textBoxMiddleInitial.Text = contactInProcess.middleInitial;

            textBoxHomePhone.Text = contactInProcess.homePhone;
            textBoxCellPhone.Text = contactInProcess.cellPhone;
            textBoxOfficeExt.Text = contactInProcess.officeExt;
            textBoxIRDNumber.Text = contactInProcess.irdNumber;

            if ("Active" == contactInProcess.status)
            {
                radioButtonActive.Checked = true;
            }
            else if ("Inactive" == contactInProcess.status)
            {
                radioButtonInactive.Checked = true;
            }
            else 
            {
                radioButtonPending.Checked = true;
            }
            return;
        }


        /// <summary>
        /// Loads data submitted in form into a staffContact Object. The Object can then 
        /// be accessed by the controller that implementes this control.
        /// </summary>
        private void loadFormIntoContact() 
        {
            if (null == contactInProcess) 
            {
                contactInProcess = new StaffContact();
            }

            if (true == radioButtonEmployee.Checked)
            {
                contactInProcess.staffType = "Employee";
                contactInProcess.manager_id = (long)comboBoxStaffsManager.SelectedValue;
            } 
            else
            {
                contactInProcess.staffType = "Manager";
            }

            contactInProcess.updateTitle(comboBoxStaffTitle.Text);
            contactInProcess.updateFirstName(textBoxFirstName.Text);
            contactInProcess.updateLastName(textBoxLastName.Text);
            contactInProcess.updateMiddleInitial(textBoxMiddleInitial.Text);
            contactInProcess.cellPhone = textBoxCellPhone.Text;

            contactInProcess.homePhone = textBoxHomePhone.Text;
            contactInProcess.irdNumber = textBoxIRDNumber.Text;
            contactInProcess.officeExt = textBoxOfficeExt.Text;

            if (true == radioButtonActive.Checked)
            {
                contactInProcess.status = "Active";
            }
            else if (true == radioButtonInactive.Checked)
            {
                contactInProcess.status = "Inactive";
            }
            else
            {
                contactInProcess.status = "Pending";
            }

            return;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        public void setContact(StaffContact contact)
        { 
            contactInProcess = contact;
            loadContactIntoForm();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public StaffContact getContact()
        {
            return contactInProcess;
        }


        /// <summary>
        /// 
        /// </summary>
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


        /// <summary>
        /// returns true if the inputed valeus into the form are acceptable by set standard.
        /// </summary>
        /// <returns></returns>
        private bool isFormValid() 
        {
            // FULL NAME VALIDATION
            if (string.Empty == textBoxFirstName.Text ||
                string.Empty == textBoxMiddleInitial.Text ||
                string.Empty == textBoxLastName.Text)
            {
                MessageBox.Show("Please provide full name", "Incomplete form",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // CELL PHONE VALIDATION
            if (string.Empty == textBoxCellPhone.Text)
            {
                MessageBox.Show("Please provide a cell phone number", "Incomplete form",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // IRD NUMBER VALIDATION
            // optional field so not required but it provided must be correct
            if (string.Empty != textBoxIRDNumber.Text)
            {
                if (textBoxIRDNumber.Text.Length > 10 || 7 > textBoxIRDNumber.Text.Length)
                {
                    MessageBox.Show("Please provide a valid IRD number \nIncorrect length", "Incomplete form",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                foreach (char c in textBoxIRDNumber.Text)
                {
                    if (c < '0' || c > '9')
                    {
                        MessageBox.Show("Please provide a valid IRD number \nNumbers only", "Incomplete form",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
            }

            // STATUS VALIDATION
            if (true != (radioButtonActive.Checked || radioButtonInactive.Checked || radioButtonPending.Checked))
            {
                MessageBox.Show("Status must be selected", "Incomplete form",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // MANAGER VALIDATION
            // if a employee it must link to a manager
            if (true == radioButtonEmployee.Checked)
            {
                if (-1 == comboBoxStaffsManager.SelectedIndex)
                {
                    MessageBox.Show("Must select manager if employee", "Incomplete form",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }
            // PASSED
            return true;
        }

    }
}
