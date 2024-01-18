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


        //Subcotrollers
        EditContactControl editControl = new EditContactControl();
        ContactDetailsControl detailsControl = new ContactDetailsControl();

        //Lists (Local Copy of database)
        private readonly List<StaffContact> staffContactsList;
        private readonly List<StaffManager> staffManagerList;
        private DataGridView dgvs;
        //Fields
        private StaffContact selectedContact;
        private bool filterByActive;

        //Define the connection string in the settings of the application
        //private string connectionString = Properties.Settings.Default.Database;
        //private const string ConnectionString = "Data Source=Properties.Settings.Default.DatabaseFile";
        private const string ConnectionString = "Data Source= C:\\Users\\shaan\\OneDrive\\Documents\\radfords_winform\\staff_contact_app_winform\\Database\\staff_contacts.sqlite";
        //private const string ConnectionString1 = Path.Combine(AppContext.BaseDirectory)
        #endregion


        #region Constructors


        public Form1()
        {

            InitializeComponent();

            //Load subcontrols into form
            splitContainerForm.Panel2.Controls.Add(editControl);
            splitContainerForm.Panel2.Controls.Add(detailsControl);
            dgvs = new DataGridView();

            dgvs.AutoGenerateColumns = false;
            splitContainerForm.Panel1.Controls.Add(dgvs);
            dgvs.Dock = DockStyle.Bottom;
            editControl.Dock = DockStyle.Fill;
            detailsControl.Dock = DockStyle.Fill;
            editControl.Visible = false;

            staffManagerList = new List<StaffManager>();
            staffContactsList = new List<StaffContact>();

            //set list contact views difault state to filter by active status
            filterByActive = true;

            //Set Event hanlders for subcontrol raised events
            editControl.saveContact_Clicked += EditControl_saveContact_Clicked;
            editControl.cancelSave_Clicked += EditControl_cancelSave_Clicked;
        }
        #endregion


        #region Events


        /// <summary>
        /// Load data from database on first load and setup UI with said data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            DatabaseManager.sayHi();
            try
            {
                DatabaseManager.loadStaffContacts(staffContactsList);
                //loadStaffContacts();
                DatabaseManager.loadStaffManager(staffManagerList);
                updateContactListView(filterByActive);
                editControl.updateManagers(staffManagerList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// load empty editControl to allow user to add new contact.
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
        /// load editCotrol with details of selected user to edit contact.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditContact_Click(object sender, EventArgs e)
        {
            setupEditingFormUI();
            editControl.editExistingContact(selectedContact);
        }


        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteContact_Click(object sender, EventArgs e)
        {
            if (1 == listViewContactList.SelectedIndices.Count)
            {
                var mb = MessageBox.Show(
                    "Are you sure you want to delete contact?",
                    "Delete Contact",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (DialogResult.Yes == mb)
                {
                    DatabaseManager.deleteStaffContact(staffContactsList, selectedContact);
                    updateContactListView(filterByActive);
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilterActive_Click(object sender, EventArgs e)
        {
            // Filter by active
            if (buttonFilterActive.Text.Equals("Show Active"))
            {
                buttonFilterActive.Text = "Show All";
                listViewContactList.Items.Clear();
                filterByActive = true;
                updateContactListView(filterByActive);
            }
            // Filter by all
            else
            {
                buttonFilterActive.Text = "Show Active";
                filterByActive = false;
                updateContactListView(filterByActive);
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewContactList_SelectedIndexChanged(object sender, EventArgs e)
        {

            // No contact selected
            if (0 == listViewContactList.SelectedIndices.Count)
            {
                //prevent UI actions that require selected contact.
                buttonEditContact.Enabled = false;
                buttonDeleteContact.Enabled = false;
                //clear and display contact details (in detialControl).
                detailsControl.clearContact();

                selectedContact = null;
            }
            // A contact selected
            else
            {
                //allow UI actions that require selected contact.
                buttonEditContact.Enabled = true;
                buttonDeleteContact.Enabled = true;
                //load selected contact and display details (in detialControl).
                StaffContact contact = (StaffContact)listViewContactList.SelectedItems[0].Tag;
                detailsControl.displayContact(contact, staffManagerList);

                selectedContact = contact;
            }
        }


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
        #endregion


        #region Handling Subcontrols Events


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditControl_cancelSave_Clicked(object? sender, EventArgs e)
        {
            //Update UI
            setupViewingFormUI();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditControl_saveContact_Clicked(object? sender, EventArgs e)
        {

            var editedContact = editControl.getEditedContact();

            if (true == editControl.isContactNew())
            {
                DatabaseManager.addStaffContact(staffContactsList, editedContact);
            }
            else
            {
                DatabaseManager.updateStaffContact(editedContact);
            }

            DatabaseManager.loadStaffManager(staffManagerList);

            //Update UI
            editControl.updateManagers(staffManagerList);
            detailsControl.displayContact(editedContact, staffManagerList);
            setupViewingFormUI();
            updateContactListView(filterByActive);

        }
        #endregion


        #region Private Methods


        /// <summary>
        /// UPDATE CONTACT LIST VIEW
        /// updates the listview to be in parity with staffContactList, addionally 
        /// allows to filter list to only contain contacts with the status active.
        /// </summary>
        /// <param name="isActive"></param>
        private void updateContactListView(bool isActive)
        {
            listViewContactList.Items.Clear();
            //NOTE there is definitly a better way to do this but I need to not waist to much time either.

            foreach (StaffContact contact in staffContactsList)
            {
                if (false == isActive)
                {
                    var listViewItem = new ListViewItem(contact.fullName);
                    listViewItem.SubItems.Add(contact.status);
                    listViewItem.Tag = contact;
                    listViewContactList.Items.Add(listViewItem);
                }
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

        private DataTable buildDataTable()
        {
            DataTable dt = new DataTable("Staff Contacts");
            //dt.Columns.Add(new DataColumn("FirstName"));
            //dt.Columns.Add(new DataColumn("MiddleInitial"));
            //dt.Columns.Add(new DataColumn("LastName"));
            //dt.Columns.Add(new DataColumn("Title"));
            //dt.Columns.Add(new DataColumn("StaffType"));
            //dt.Columns.Add(new DataColumn("HomePhone"));
            //dt.Columns.Add(new DataColumn("CellPhone"));
            //dt.Columns.Add(new DataColumn("OfficeExtension"));
            //dt.Columns.Add(new DataColumn("IRDNumber"));
            //dt.Columns.Add(new DataColumn("Status"));
            //dt.Columns.Add(new DataColumn("ManagerID"));
            //dt.Columns.Add(new DataColumn("ID"));
            dt.Columns.Add("FirstName", typeof(string));
            dt.Columns.Add("MiddleInitial", typeof(string));
            dt.Columns.Add("LastName", typeof(string));
            dt.Columns.Add("Title", typeof(string));
            dt.Columns.Add("StaffType", typeof(string));
            dt.Columns.Add("HomePhone", typeof(string));
            dt.Columns.Add("CellPhone", typeof(string));
            dt.Columns.Add("OfficeExtension", typeof(string));
            dt.Columns.Add("IRDNumber", typeof(string));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("ManagerID", typeof(long));
            dt.Columns.Add("ID", typeof(long));
            List<StaffContact> sortedContacts = staffContactsList
                .OrderBy(x => x.firstName)
                .GroupBy(x => x.staffType).SelectMany(x => x).ToList();

            foreach (StaffContact sc in sortedContacts)
            {
                Debug.WriteLine(sc.fullName);
                dt.Rows.Add(sc.firstName,sc.middleInitial,sc.lastName,sc.title,
                    sc.staffType,sc.homePhone,sc.cellPhone,sc.officeExt,
                    sc.irdNumber,sc.status,sc.manager_id,sc.id);
            }
            return dt;
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            try
            {
                PrintDialog printDialog = new PrintDialog();
                PrintDocument printDocument = new PrintDocument();
                printDialog.Document = printDocument;
                printDocument.PrintPage += printDocument_PrintPage;

                DialogResult result = printDialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    printDocument.Print();
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            DataTable dt = buildDataTable();
            
            //splitContainerForm.Panel1.Controls.Add(dgv);
            dgvs.AutoGenerateColumns = true;


            List<StaffContact> sortedContacts = staffContactsList
                .OrderBy(x => x.firstName)
                .GroupBy(x => x.staffType).SelectMany(x => x).ToList();
            BindingSource bs = new BindingSource();
            bs.DataSource = dt;
            dgvs.DataSource = sortedContacts;
            
            dgvs.DataSource = dt;
            dgvs.AutoResizeColumns();
            dgvs.AutoResizeRows();
            //dgvs.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvs.Refresh();


            Bitmap bm = new Bitmap(dgvs.Width, dgvs.Height);
            dgvs.DrawToBitmap(bm, new Rectangle(0, 0, dgvs.Width, dgvs.Height));
            e.Graphics.DrawImage(bm, 0, 0);

            Graphics graphic = e.Graphics;
            foreach(DataGridViewRow row in dgvs.Rows)
            {
                Debug.WriteLine(row.ToString());
                string text = row.ToString(); //or whatever you want from the current row
                graphic.DrawString(text, new Font("Times New Roman", 14, FontStyle.Bold), Brushes.Black, 20, 225);
            }
        }
    }
}

