using System.Data.SQLite;
using System.Diagnostics;
using System.Runtime.CompilerServices;


namespace staff_contact_app_winform
{
    public partial class Form1 : Form
    {
        EditContactControl editControl = new EditContactControl();
        ContactDetailsControl detailsControl = new ContactDetailsControl();

        private readonly List<StaffContact> staffContactsList;
        private readonly List<StaffManager> staffManagerList;
        //Define the connection string in the settings of the application
        //private string connectionString = Properties.Settings.Default.Database;
        //private const string ConnectionString = "Data Source=Properties.Settings.Default.DatabaseFile";
        private const string ConnectionString = "Data Source=C:\\Users\\shaan\\OneDrive\\Documents\\radfords\\staff_contact_app_winform\\staff_contact_app_winform\\Database\\staff_contacts.sqlite";

        public Form1()
        {
            InitializeComponent();

            staffManagerList = new List<StaffManager>();
            staffContactsList = new List<StaffContact>();

            splitContainerForm.Panel2.Controls.Add(editControl);
            splitContainerForm.Panel2.Controls.Add(detailsControl);

            editControl.Dock = DockStyle.Fill;
            detailsControl.Dock = DockStyle.Fill;


            //detailsControl.Visible = false;
            editControl.Visible = false;

        }

        private void updateContactList(bool active)
        {
            listViewContactList.Items.Clear();

            foreach (StaffContact contact in staffContactsList)
            {
                var listViewItem = new ListViewItem(contact.fullName);
                listViewItem.SubItems.Add(contact.status);
                listViewItem.Tag = contact;

                listViewContactList.Items.Add(listViewItem);
            }
        }

        private void loadStaffManager()
        {
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
                        string manager = SqlNullParser.GetValue<string>(reader, "manager");

                        StaffContact contact = new StaffContact(id, staffType, title,
                            firstName, lastName, middleInitial,
                            homePhone, cellPhone, officeExt, irdNumber, status, manager);
                        staffContactsList.Add(contact);

                    }
                }
            }


        }

        private void buttonAddContact_Click(object sender, EventArgs e)
        {
            // load form that allows user to edit/add contact
            detailsControl.Visible = false;
            editControl.Visible = true;
        }

        private void buttonEditContact_Click(object sender, EventArgs e)
        {
            detailsControl.Visible = false;
            editControl.Visible = true;
            editControl.loadContact(detailsControl.getSelectedContact());
            listViewContactList.Enabled = false;
        }

        private void buttonDeleteContact_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                loadStaffContacts();
                loadStaffManager();
                updateContactList(true);
                editControl.updateManagers(staffManagerList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void listViewContactList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (0 == listViewContactList.SelectedIndices.Count)
            {
                buttonEditContact.Enabled = false;
                detailsControl.clearContact();

            }
            else
            {
                buttonEditContact.Enabled = true;
                StaffContact contact = (StaffContact)listViewContactList.SelectedItems[0].Tag;
                detailsControl.loadContact(contact);
            }
        }
    }

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

