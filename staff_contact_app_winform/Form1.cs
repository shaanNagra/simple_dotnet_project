using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace staff_contact_app_winform
{
    public partial class Form1 : Form
    {
        #region Declaration
        EditContactControl editControl = new EditContactControl();
        ContactDetailsControl detailsControl = new ContactDetailsControl();

        private readonly List<StaffContact> staffContactsList;
        private readonly List<StaffManager> staffManagerList;

        private StaffContact inProcessContact;
        private bool filterByActive;

        //Define the connection string in the settings of the application
        //private string connectionString = Properties.Settings.Default.Database;
        //private const string ConnectionString = "Data Source=Properties.Settings.Default.DatabaseFile";
        private const string ConnectionString = "Data Source=C:\\Users\\shaan\\OneDrive\\Documents\\radfords\\staff_contact_app_winform\\staff_contact_app_winform\\Database\\staff_contacts.sqlite";
        #endregion


        #region Constructors


        public Form1()
        {
            InitializeComponent();

            staffManagerList = new List<StaffManager>();
            staffContactsList = new List<StaffContact>();

            splitContainerForm.Panel2.Controls.Add(editControl);
            splitContainerForm.Panel2.Controls.Add(detailsControl);

            editControl.Dock = DockStyle.Fill;
            detailsControl.Dock = DockStyle.Fill;

            filterByActive = true;
            //detailsControl.Visible = false;
            editControl.Visible = false;

            editControl.saveContact_Clicked += EditControl_saveContact_Clicked;
            editControl.cancelSave_Clicked += EditControl_cancelSave_Clicked;
        }


        #endregion


        /// <summary>
        /// LOAD FORM
        /// Load data from database on first load and setup UI with said data.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                loadStaffContacts();
                loadStaffManager();
                updateContactListView(filterByActive);
                editControl.updateManagers(staffManagerList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// EDIT CONTROLS CANCEL EDIT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditControl_cancelSave_Clicked(object? sender, EventArgs e)
        {
            setupViewingFormUI();
        }

        /// <summary>
        /// EDIT CONTROLS SAVE EDIT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditControl_saveContact_Clicked(object? sender, EventArgs e)
        {
            if(true == editControl.isContactNew())
            {
                addStaffContact(editControl.getEditedContact());
            }
            else
            {  
                updateStaffContact(editControl.getEditedContact());
            }

            setupViewingFormUI();
            updateContactListView(filterByActive);
        }
        

        /// <summary>
        /// ADD NEW CONTACT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonAddContact_Click(object sender, EventArgs e)
        {
            // load form that allows user to edit/add contact
            setupEditingFormUI();
            editControl.editNewContact();
        }


        /// <summary>
        /// EDIT EXSITING CONTACT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonEditContact_Click(object sender, EventArgs e)
        {
            setupEditingFormUI();
            editControl.editExistingContact(inProcessContact);
        }


        /// <summary>
        /// DELETE CONTACT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonDeleteContact_Click(object sender, EventArgs e)
        {
            if (1 == listViewContactList.SelectedIndices.Count)
            {
                var mb = MessageBox.Show("Are you sure?", "Delete Contact", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (DialogResult.Yes == mb)
                {

                }
            }
        }


        /// <summary>
        /// FILTER CONTACT LIST
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonFilterActive_Click(object sender, EventArgs e)
        {
            if ("Show Active" == buttonFilterActive.Text)
            {
                buttonFilterActive.Text = "Show All";
                listViewContactList.Items.Clear();
                filterByActive = true;
                updateContactListView(filterByActive);
                return;

            }
            buttonFilterActive.Text = "Show Active";
            filterByActive = false;
            updateContactListView(filterByActive);
        }


        /// <summary>
        /// SELECT CONTACT FROM LIST
        /// Update contact details display in panel2 when a contact is selected.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listViewContactList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (0 == listViewContactList.SelectedIndices.Count)
            {
                buttonEditContact.Enabled = false;
                buttonDeleteContact.Enabled = false;
                detailsControl.clearContact();
                inProcessContact = null;

            }
            else
            {
                buttonEditContact.Enabled = true;
                buttonDeleteContact.Enabled = true;
                StaffContact contact = (StaffContact)listViewContactList.SelectedItems[0].Tag;
                detailsControl.loadContact(contact, staffManagerList);
                inProcessContact = contact;
            }
        }



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
                    if ("Active" == contact.status)
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


        #region SQL Queries

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        private void updateStaffContact(StaffContact contact)
        {
            //var scIndex = staffContactsList.FindIndex(x => x.id == contact.id);
            //staffContactsList[scIndex] = contact;

            var query = "UPDATE staff SET staff_type=@staff_type, title=@title" +
                ", first_name=@first_name, last_name=@last_name" +
                ", middle_initial=@middle_initial" +
                ", home_phone=@home_phone, cell_phone=@cell_phone, office_extension=@office_extension" +
                ", ird_number=@ird_number" + 
                ", status=@status , manager_id=@manager_id " +
                "WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                Debug.WriteLine(contact.officeExt);
                Debug.WriteLine(contact.manager_id);
                Debug.WriteLine(contact.irdNumber);
                Debug.WriteLine(contact.lastName);

                command.Parameters.Add(new SQLiteParameter("@staff_type", DbType.String, contact.staffType));
                command.Parameters.Add(new SQLiteParameter("@title", DbType.String, contact.title));
                command.Parameters.Add(new SQLiteParameter("@first_name", DbType.String, contact.firstName));
                command.Parameters.Add(new SQLiteParameter("@last_name", DbType.String, contact.lastName));
                command.Parameters.Add(new SQLiteParameter("@middle_initial", DbType.String, contact.middleInitial));
                command.Parameters.Add(new SQLiteParameter("@home_phone", DbType.String, contact.homePhone));
                command.Parameters.Add(new SQLiteParameter("@cell_phone", DbType.String, contact.cellPhone));
                command.Parameters.Add(new SQLiteParameter("@office_extension", DbType.String, contact.officeExt));
                command.Parameters.Add(new SQLiteParameter("@ird_number", DbType.String, contact.irdNumber));
                command.Parameters.Add(new SQLiteParameter("@status", DbType.String, contact.status));
                command.Parameters.Add(new SQLiteParameter("@manager_id", DbType.Int64, contact.manager_id.ToString()));

                command.Parameters.Add(new SQLiteParameter("@id", DbType.Int64, contact.id.ToString()));

                command.ExecuteNonQuery();
            }
            //updateStaffContactManager(contact);
        }

        //private void updateStaffContact(StaffContact contact)
        //{
        //    //var scIndex = staffContactsList.FindIndex(x => x.id == contact.id);
        //    //staffContactsList[scIndex] = contact;

        //    string query = "UPDATE staff SET staff_type=@staff_type, title=@title" +
        //        ", first_name=@first_name, last_name=@last_name" +
        //        ", middle_initial=@middle_initial" +
        //        ", home_phone=@home_phone, cell_phone=@cell_phone, office_extension=@office_extension" +
        //        ", ird_number=@ird_number" +
        //        ", status=@status" +
        //        "WHERE id = @id";

        //    using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
        //    {
        //        connection.Open();
        //        SQLiteCommand command = new SQLiteCommand(query, connection);
        //        Debug.WriteLine(contact.officeExt);
        //        Debug.WriteLine(contact.manager_id);
        //        Debug.WriteLine(contact.irdNumber);
        //        Debug.WriteLine(contact.lastName);

        //        command.Parameters.AddWithValue("@staff_type", contact.staffType ?? null);
        //        command.Parameters.AddWithValue("@title", contact.title ?? null);
        //        command.Parameters.AddWithValue("@first_name", contact.firstName ?? null);
        //        command.Parameters.AddWithValue("@last_name", contact.lastName ?? null);
        //        command.Parameters.AddWithValue("@middle_initial", contact.middleInitial ?? null);
        //        command.Parameters.AddWithValue("@home_phone", contact.homePhone ?? null);
        //        command.Parameters.AddWithValue("@cell_phone", contact.cellPhone ?? null);
        //        command.Parameters.AddWithValue("@office_extension", contact.officeExt ?? null);
        //        command.Parameters.AddWithValue("@ird_number", contact.irdNumber ?? null);
        //        command.Parameters.AddWithValue("@status", contact.status ?? null);

        //        command.Parameters.AddWithValue("@id", contact.id);

        //        command.ExecuteNonQuery();
        //    }
        //    //updateStaffContactManager(contact);
        //}


        /// <summary>
        /// 
        /// </summary>
        private void updateStaffContactManager(StaffContact contact)
        {
            string query = "UPDATE staff SET manager_id=@manager_id WHERE id = @id";
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@status", contact.manager_id);
                command.ExecuteNonQuery();
            }    
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        private void addStaffContact(StaffContact contact)
        {
            const string query = "INSERT into staff " +
                "(staff_type, title, first_name, last_name, middle_initial, " +
                "home_phone, cell_phone, office_extension, ird_number, status, manager_id) " +
                "VALUES (@staff_type, @title, @first_name, @last_name, @middle_initial, " +
                "@home_phone, @cell_phone, @office_extension, @ird_number, @status, @manager_id);" +
                "SELECT last_insert_rowid()";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);

                command.Parameters.AddWithValue("@staff_type", contact.staffType);
                command.Parameters.AddWithValue("@title", contact.title);
                command.Parameters.AddWithValue("@first_name", contact.firstName);
                command.Parameters.AddWithValue("@last_name", contact.lastName);
                command.Parameters.AddWithValue("@middle_initial", contact.middleInitial);
                command.Parameters.AddWithValue("@home_phone", contact.homePhone);
                command.Parameters.AddWithValue("@cell_phone", contact.cellPhone);
                command.Parameters.AddWithValue("@office_extension", contact.officeExt);
                command.Parameters.AddWithValue("@ird_number", contact.irdNumber);
                command.Parameters.AddWithValue("@status", contact.status);
                command.Parameters.AddWithValue("@manager_id", contact.manager_id);

                contact.id = (long)command.ExecuteScalar();

                staffContactsList.Add(contact);
            }
        }


        /// <summary>
        /// loads all staff from database into staffContactList. Intended use,
        /// to load database into local copy at start of application.
        /// </summary>
        private void loadStaffContacts()
        {
            const string query = "SELECT * FROM staff";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = SqlNullParser.GetValue<long>(reader, "id");
                        string staffType = SqlNullParser.GetValue<string>(reader, "staff_type");
                        string title = SqlNullParser.GetValue<string>(reader, "title");
                        string firstName = SqlNullParser.GetValue<string>(reader, "first_name");
                        string lastName = SqlNullParser.GetValue<string>(reader, "last_name");
                        string middleInitial = SqlNullParser.GetValue<string>(reader, "middle_initial");
                        string homePhone = SqlNullParser.GetValue<string>(reader, "home_phone");
                        string cellPhone = SqlNullParser.GetValue<string>(reader, "cell_phone");
                        string officeExt = SqlNullParser.GetValue<string>(reader, "office_extension");
                        string irdNumber = SqlNullParser.GetValue<string>(reader, "ird_number");
                        string status = SqlNullParser.GetValue<string>(reader, "status");
                        long manager_id = SqlNullParser.GetValue<long>(reader, "manager_id");

                        StaffContact contact = new StaffContact(id, staffType, title,
                            firstName, lastName, middleInitial,
                            homePhone, cellPhone, officeExt, irdNumber, status, manager_id);
                        staffContactsList.Add(contact);

                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        private void deleteStaffContact(StaffContact contact)
        {
            const string query = "DELETE FROM staff WHERE id=@id";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();

                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@id", contact.id);

                command.ExecuteNonQuery();
                staffContactsList.Remove(contact);
            }
        }
 

        /// <summary>
        /// loads management staff from database into staffManagerList, clears
        /// list first. Allows for multiple calls to keep in sync w/ database.
        /// </summary>
        private void loadStaffManager()
        {
            staffManagerList.Clear();
            const string query = "SELECT * FROM staff WHERE staff.staff_type = 'Manager'";
            Debug.WriteLine(ConnectionString.ToString());
            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                var table = connection.GetSchema();
                Debug.Print(table.ToString());
                var command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = SqlNullParser.GetValue<long>(reader, "id");
                        string title = SqlNullParser.GetValue<string>(reader, "title");
                        string firstName = SqlNullParser.GetValue<string>(reader, "first_name");
                        string lastName = SqlNullParser.GetValue<string>(reader, "last_name");
                        string middleInitial = SqlNullParser.GetValue<string>(reader, "middle_initial");

                        StaffManager manager = new StaffManager(title, firstName, lastName, middleInitial, id);
                        staffManagerList.Add(manager);
                    }
                }
            }
        }


        #endregion
    }



    /// <summary>
    /// Custom static class provides functionality to deal with DBNulls from
    /// SQL query results
    /// </summary>
    public static class SqlNullParser
    {
        public static T GetValue<T>(this SQLiteDataReader reader, string columnName)
        {
            int columnIndex = reader.GetOrdinal(columnName);
            if (reader.IsDBNull(columnIndex))
            {
                return default(T);
            }

            return (T)reader.GetValue(columnIndex);
        }

    }
}

