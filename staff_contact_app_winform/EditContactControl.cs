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
    /// <summary>
    /// Control that manager to process to add/update a staff contact fields.
    /// </summary>
    public partial class EditContactControl : UserControl
    {
        List<StaffManager> staffManagerList;
        List<ComboboxManagerItem> cmiList;

        private StaffContact contactInProcess;
        /// <summary>
        /// Flag for if it is a new contact.
        /// </summary>
        private bool isNewContact;
        

        public EditContactControl()
        {
            staffManagerList = new List<StaffManager>();
            cmiList = new List<ComboboxManagerItem>();

            contactInProcess = null;
            isNewContact = true;
            

            InitializeComponent();
        }

        #region Events


        /// <summary>
        /// Radio button check change event handler, when contact is change to 
        /// being an employee update form to show a combobox to select a 
        /// manager.
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
            // Make manager not selectable
            labelManager.Visible = false;
            comboBoxStaffsManager.Visible = false;
            comboBoxStaffsManager.SelectedIndex = -1;
        }


        /// <summary>
        /// Button click event hanlder, before saving new contact it parses 
        /// form to prevent incorrect data, if correct it raises an event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveContact_Click(object sender, EventArgs e)
        {
            // Form is not valid
            if(false == isFormValid())
            {
                return;
            }
            //Form is valid, save contact information.
            loadFormIntoContact();
            clearContactDetails();
            saveContact_Clicked?.Invoke(this, e);
        }
        /// <summary>
        /// Raises an event for parent control to handle when saving a new 
        /// contact.
        /// </summary>
        public event EventHandler saveContact_Clicked;


        /// <summary>
        /// Button click event handler, Allow user to cancel editing a contact 
        /// and exit contorller.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            var mb = MessageBox.Show("Are you sure you want to cancel?", "Cancel edit/add contact", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            // Clicked yes on messagebox to cancel.
            if (mb == DialogResult.Yes)
            {
                contactInProcess = null;
                isNewContact = true;
                clearContactDetails();

                cancelSave_Clicked?.Invoke(this, e);
            }
        }
        /// <summary>
        /// Raises an event for parent control to handle when canceling.
        /// </summary>
        public event EventHandler cancelSave_Clicked;


        #endregion


        #region Public Methods


        /// <summary>
        /// Called to set details in input fields of the editControl from an 
        /// exisitng contact.
        /// </summary>
        /// <param name="contact">The contact thats information is to be loaded in.</param>
        public void editExistingContact(StaffContact contact)
        { 
            contactInProcess = contact;
            isNewContact = false;
            loadManagerCombobox();
            loadContactIntoForm();
        }


        /// <summary>
        /// Called to set input fields for a new contact to be edited and 
        /// added.
        /// </summary>
        public void editNewContact()
        {
            contactInProcess = null;
            isNewContact = true;
        }


        /// <summary>
        /// Update the list of managers. This list is used to populate combobox
        /// of managers.
        /// </summary>
        /// <param name="staffManagerList">The updated list of managers.</param>
        public void updateManagers(List<StaffManager> staffManagerList)
        {
            this.staffManagerList = staffManagerList;
            comboBoxStaffsManager.Items.Clear();
            loadManagerCombobox();
            comboBoxStaffsManager.SelectedIndex = -1;
        }


        /// <summary>
        /// Returns the contact that was being edited inside the editControl.
        /// </summary>
        /// <returns></returns>
        public StaffContact getEditedContact()
        {
            return contactInProcess;
        }


        /// <summary>
        /// Get if contact in the editControl was called to be a new contact.
        /// </summary>
        /// <returns>True: contact is new, False: it is an existing contact.</returns>
        public bool isContactNew() 
        {  
            return isNewContact; 
        }


        #endregion


        #region Private Methods
        /// <summary>
        /// Empty editControls input fields.
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
        /// Loads the combobox with contacts that are managers, prevents 
        /// self-reference.
        /// </summary>
        private void loadManagerCombobox()
        {
            comboBoxStaffsManager.Items.Clear();
            cmiList = new List<ComboboxManagerItem>();

            string compareString = "";
            // If the editControll was called to edit an existing contact.
            if (false == isNewContact)
            {
                // Get contacts full name as string for comparison.
                compareString = contactInProcess.fullName;
            }

            foreach (StaffManager manager in this.staffManagerList)
            {
                // Make sure the contact cannot select itself as manager. 
                if (false == manager.fullName.Equals(compareString))
                {
                    // Add manager to combobox.
                    int insertIndex = comboBoxStaffsManager.Items.Add(manager.fullName);
                    cmiList.Add(new ComboboxManagerItem { fullname = manager.fullName, manager_id = manager.manager_id, indexInCombobox = insertIndex });
                }
            }
        }


        /// <summary>
        /// Loads a StaffContacts data into the editing form. Allows controller to submit
        /// data to the form.
        /// </summary>
        private void loadContactIntoForm()
        {
            // Set title
            if (null != contactInProcess.title)
            {
                comboBoxStaffTitle.SelectedIndex = comboBoxStaffTitle.FindString(contactInProcess.title);
            }

            // Set Staff Type
            if(contactInProcess.staffType.Equals("Employee"))
            {
                radioButtonEmployee.Checked = true;
                var cmi = cmiList.Find(x => x.manager_id == contactInProcess.manager_id);
                // Set manager of contact.
                if (null == cmi)
                {
                    comboBoxStaffsManager.SelectedIndex = -1;
                }
                else
                {
                    comboBoxStaffsManager.SelectedIndex = cmi.indexInCombobox;
                }
            }
            else
            {
                radioButtonManager.Checked = true;
            }

            // Set name.
            textBoxFirstName.Text = contactInProcess.firstName;
            textBoxLastName.Text = contactInProcess.lastName;
            textBoxMiddleInitial.Text = contactInProcess.middleInitial;

            // Set phone detials.
            textBoxHomePhone.Text = contactInProcess.homePhone;
            textBoxCellPhone.Text = contactInProcess.cellPhone;
            textBoxOfficeExt.Text = contactInProcess.officeExt;

            // Set IRD number.
            textBoxIRDNumber.Text = contactInProcess.irdNumber;

            // Set status.
            if (contactInProcess.status.Equals("Active"))
            {
                radioButtonActive.Checked = true;
            }
            else if (contactInProcess.status.Equals("Inactive"))
            {
                radioButtonInactive.Checked = true;
            }
            else
            {
                radioButtonPending.Checked = true;
            }
            // Return.
            return;
        }


        /// <summary>
        /// Loads data submitted in form into a staffContact Object. The Object can then 
        /// be accessed by the controller that implementes this control.
        /// </summary>
        private void loadFormIntoContact()
        {
            // If contact is a new contact to be added.
            if (null == contactInProcess)
            {
                contactInProcess = new StaffContact();
            }

            // Get staff type.
            if (true == radioButtonEmployee.Checked)
            {
                contactInProcess.staffType = "Employee";
                // Get manager.
                if (-1 != comboBoxStaffsManager.SelectedIndex)
                {
                    contactInProcess.manager_id = cmiList.Find(x => x.indexInCombobox == comboBoxStaffsManager.SelectedIndex).manager_id;
                }
            }
            else
            {
                contactInProcess.staffType = "Manager";
                contactInProcess.manager_id = 0;
            }

            // Get name.
            contactInProcess.updateTitle(comboBoxStaffTitle.Text);
            contactInProcess.updateFirstName(textBoxFirstName.Text);
            contactInProcess.updateLastName(textBoxLastName.Text);
            contactInProcess.updateMiddleInitial(textBoxMiddleInitial.Text);

            // Get phone details.
            contactInProcess.cellPhone = textBoxCellPhone.Text;
            contactInProcess.homePhone = textBoxHomePhone.Text;
            contactInProcess.officeExt = textBoxOfficeExt.Text;
            
            // Get IRD number.
            contactInProcess.irdNumber = textBoxIRDNumber.Text;

            // Get staff status.
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
            // Return.
            return;
        }


        /// <summary>
        /// IS FORM VALID
        /// returns true if the inputed valeus into the form are acceptable by set standard.
        /// </summary>
        /// <returns></returns>
        private bool isFormValid() 
        {
            // Full name validation.
            if (string.Empty == textBoxFirstName.Text ||
                string.Empty == textBoxMiddleInitial.Text ||
                string.Empty == textBoxLastName.Text)
            {
                MessageBox.Show("Please provide full name", "Incomplete form",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Cell phone validation.
            if (string.Empty == textBoxCellPhone.Text)
            {
                MessageBox.Show("Please provide a cell phone number", "Incomplete form",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // IRD number validation.
            // optional field so not required but it provided must be correct
            if (string.Empty != textBoxIRDNumber.Text)
            {
                // Make sure it is in correct range to be IRD number.
                if (textBoxIRDNumber.Text.Length > 10 || 7 > textBoxIRDNumber.Text.Length)
                {
                    MessageBox.Show("Please provide a valid IRD number \nIncorrect length", "Incomplete form",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
                // Make sure it is only made up of digits.
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

            // Staff Status validation.
            if (true != (radioButtonActive.Checked || radioButtonInactive.Checked || radioButtonPending.Checked))
            {
                MessageBox.Show("Status must be selected", "Incomplete form",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            // Manager validation.
            // If a employee, it must link to a manager.
            if (true == radioButtonEmployee.Checked)
            {
                if (-1 == comboBoxStaffsManager.SelectedIndex)
                {
                    MessageBox.Show("Must select manager if employee", "Incomplete form",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return false;
                }
            }

            // Passed validation return true.
            return true;
        }


        #endregion
    }


    /// <summary>
    /// Object to keep track of manager name, id and postion in combobox.
    /// </summary>
    public class ComboboxManagerItem
    {
        // NOTE: was getting weird issues that were at time belive to be because 
        // of the way combobox was returning the values associated with text. so
        // a custom object was used to quickly get around this.
        public string fullname { get; set; }
        public long manager_id { get; set; }
        public int indexInCombobox { get; set; }
    }
}
