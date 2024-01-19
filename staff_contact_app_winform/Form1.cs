using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;


namespace staff_contact_app_winform
{
    public partial class Form1 : Form
    {
        #region Fields


        // Sub-controllers.
        EditContactControl editControl = new EditContactControl();
        ContactDetailsControl detailsControl = new ContactDetailsControl();

        // Lists (Local Copy of database).
        private List<StaffContact> staffContactsList;
        private List<StaffManager> staffManagerList;
        
        private StaffContact selectedContact;

        // Flag to track if should filter list view
        // to show only staff that have status set to active.
        private bool filterByActive;
        #endregion


        #region Constructors


        public Form1()
        {
            InitializeComponent();

            // Load sub-controller into form.
            splitContainerForm.Panel2.Controls.Add(editControl);
            splitContainerForm.Panel2.Controls.Add(detailsControl);
            // Allow sub-contorller to take full panel space.
            editControl.Dock = DockStyle.Fill;
            detailsControl.Dock = DockStyle.Fill;
            // Hide edit sub-controller.
            editControl.Visible = false;

            staffManagerList = new List<StaffManager>();
            staffContactsList = new List<StaffContact>();

            // Set default to flag to filter.
            filterByActive = true;

            // Set Event hanlders for subcontrol raised events.
            editControl.saveContact_Clicked += EditControl_saveContact_Clicked;
            editControl.cancelSave_Clicked += EditControl_cancelSave_Clicked;
        }
        #endregion


        #region Events


        /// <summary>
        /// Runs first time when form loads. Loads data from database on first 
        /// load and setups UI with said data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Loads list from database.
                staffContactsList = DatabaseManager.loadStaffContacts(staffContactsList);
                staffManagerList = DatabaseManager.loadStaffManager(staffManagerList);
                // Update listView.
                updateContactListView(filterByActive);
                // Update combobox to use manager from database.
                editControl.updateManagers(staffManagerList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Button click event hanlder, present user with an empty editControl 
        /// to allow user to add new contact.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddContact_Click(object sender, EventArgs e)
        {
            setupEditingFormUI();
            listViewContactList.SelectedItems.Clear();
            editControl.editNewContact();
        }


        /// <summary>
        /// Button click event hanlder, presents user with editCotrol filled 
        /// with details from the selected user to edit contact.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditContact_Click(object sender, EventArgs e)
        {
            setupEditingFormUI();
            editControl.editExistingContact(selectedContact);
        }


        /// <summary>
        /// Button click event handler, sets up process to delete a staffs 
        /// contact from database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteContact_Click(object sender, EventArgs e)
        {
            // Only run if user has selected a single contact.
            if (1 == listViewContactList.SelectedIndices.Count)
            {
                var mb = MessageBox.Show(
                    "Are you sure you want to delete contact?",
                    "Delete Contact",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);
                // If user selected to yes on the dialog box.
                if (DialogResult.Yes == mb)
                {
                    staffContactsList = DatabaseManager.deleteStaffContact(staffContactsList, selectedContact);
                    updateContactListView(filterByActive);
                }
            }
        }


        /// <summary>
        /// Button click event hanlder, toggles between showing only staff that
        /// have status active and all staff in contacts.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilterActive_Click(object sender, EventArgs e)
        {
            // Filter by active.
            if (buttonFilterActive.Text.Equals("Show Active"))
            {
                buttonFilterActive.Text = "Show All";
                listViewContactList.Items.Clear();
                filterByActive = true;
                updateContactListView(filterByActive);
            }
            // Filter by all.
            else
            {
                buttonFilterActive.Text = "Show Active";
                filterByActive = false;
                updateContactListView(filterByActive);
            }
        }


        /// <summary>
        /// ListView selected item changes event handler, loads the details of 
        /// the selected contact. Clears details if no contact is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewContactList_SelectedIndexChanged(object sender, EventArgs e)
        {

            // No contact selected.
            if (0 == listViewContactList.SelectedIndices.Count)
            {
                // Prevent UI actions that require selected contact.
                buttonEditContact.Enabled = false;
                buttonDeleteContact.Enabled = false;
                // Clear and display contact details (in detialControl).
                detailsControl.ClearContact();
                // NOTE: should not do this, dirty fail safe uncase there is state
                // where information is access when it should not. 
                selectedContact = null;
            }
            // A contact selected.
            else
            {
                // Allow UI actions that require selected contact.
                buttonEditContact.Enabled = true;
                buttonDeleteContact.Enabled = true;
                // Load selected contact and display details (in detialControl).
                StaffContact contact = (StaffContact)listViewContactList.SelectedItems[0].Tag;
                detailsControl.DisplayContact(contact, staffManagerList);

                selectedContact = contact;
            }
        }


        /// <summary>
        /// Button click event handler, start process to export data to a csv 
        /// file, presents user with dialog to select filepath and saves data.
        /// Data is saved grouped by staff type and order by first name.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonSaveToCSV_Click(object sender, EventArgs e)
        {
            Stream csvstream;
            SaveFileDialog saveFileDialogCSV = new SaveFileDialog();
            saveFileDialogCSV.Filter = "CSV file (*.csv)|*.csv| All Files (*.*)|*.*";
            saveFileDialogCSV.FilterIndex = 1;
            saveFileDialogCSV.RestoreDirectory = true;
            if (saveFileDialogCSV.ShowDialog() == DialogResult.OK)
            {
                if (null != (csvstream = saveFileDialogCSV.OpenFile()))
                {
                    List<StaffContact> sortedContacts = staffContactsList
                        .OrderBy(x => x.firstName)
                        .GroupBy(x => x.staffType).SelectMany(x => x).ToList();
                    using (StreamWriter sw = new StreamWriter(csvstream))
                    {
                        sw.WriteLine("first name,middle initial, last name, " +
                            "title, staff type, home phone, cell phone, office extension, " +
                            "ird number, status, manager id, id");
                        foreach (StaffContact contact in sortedContacts)
                        {
                            sw.WriteLine(String.Join(",", contact.getAsStringArray()));
                        }
                    }
                    csvstream.Close();
                }
            }
        }


        /// <summary>
        /// Button click event handler, show form with contacts as table to print.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            var printForm = new PrintForm(staffContactsList);
            // Call as dialog as user should only use form to quicly print data.
            // NOTE: dialog will keep focus and require interaction.
            printForm.ShowDialog();
        }
        #endregion


        #region Handling Subcontrols Events


        /// <summary>
        /// Handler for event raised by sub-controller EditControl, sets UI to 
        /// not show the EditControl.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditControl_cancelSave_Clicked(object? sender, EventArgs e)
        {
            setupViewingFormUI();
        }

        /// <summary>
        /// Hanlder for event raised by sub-controller EditControl, The save 
        /// button on the control was clicked. Start process to either update 
        /// existing or add a new contact to database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditControl_saveContact_Clicked(object? sender, EventArgs e)
        {

            var editedContact = editControl.getEditedContact();
            // New contact to save into database.
            if (true == editControl.isContactNew())
            {
                staffContactsList = DatabaseManager.addStaffContact(staffContactsList, editedContact);
            }
            // Existing contact to update in database.
            else
            {
                DatabaseManager.updateStaffContact(editedContact);
            }
            // NOTE: quick way to keep managers in combobox insync with manager
            // in list.
            staffManagerList = DatabaseManager.loadStaffManager(staffManagerList);

            // Update UI.
            editControl.updateManagers(staffManagerList);
            detailsControl.DisplayContact(editedContact, staffManagerList);
            setupViewingFormUI();
            updateContactListView(filterByActive);
        }
        #endregion


        #region Private Methods


        /// <summary>
        /// Update the listview to be in sync with staffContactList, addionally 
        /// allows to filter list to only contain contacts with the status active.
        /// </summary>
        /// <param name="isActive">True: filter listView to only show staff with active status.</param>
        private void updateContactListView(bool isActive)
        {
            listViewContactList.Items.Clear();
            // NOTE there is definitly a better way to do this but I need to not
            // waist to much time either.
            foreach (StaffContact contact in staffContactsList)
            {
                // Do not filter.
                if (false == isActive)
                {
                    var listViewItem = new ListViewItem(contact.fullName);
                    listViewItem.SubItems.Add(contact.status);
                    listViewItem.Tag = contact;
                    listViewContactList.Items.Add(listViewItem);
                }
                // Filter by Active.
                else
                {
                    if (contact.status.Equals("Active"))
                    {
                        var listViewItem = new ListViewItem(contact.fullName);
                        listViewItem.SubItems.Add(contact.status);
                        listViewItem.Tag = contact;
                        listViewContactList.Items.Add(listViewItem);
                    }
                }

            }
        }


        /// <summary>
        /// Setup UI for editing/adding contacts. disables controls to prevent 
        /// unintended actions.
        /// </summary>
        private void setupEditingFormUI()
        {
            detailsControl.Visible = false;
            editControl.Visible = true;
            listViewContactList.Enabled = false;
            buttonAddContact.Enabled = false;
            buttonDeleteContact.Enabled = false;
            buttonEditContact.Enabled = false;
        }


        /// <summary>
        /// Undo any UI changes made for the editing/adding contacts state.
        /// </summary>
        private void setupViewingFormUI()
        {
            detailsControl.Visible = true;
            editControl.Visible = false;
            listViewContactList.Enabled = true;
            buttonAddContact.Enabled = true;
            buttonDeleteContact.Enabled = true;
            buttonEditContact.Enabled = true;
        }
        #endregion
    }
}

