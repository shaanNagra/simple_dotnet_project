using System.Data.SQLite;
using System.Diagnostics;

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

        private void loadStaffManager()
        {
            //const string query = "SELECT * FROM staff WHERE staff.staff_type = 'Manager'";
            const string query = "SELECT * FROM staff";
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
                        long id = (long)reader["id"];
                        string firstName = (string)reader["first_name"];
                        string lastName = (string)reader["last_name"];
                        string middleInitial = (string)reader["middle_initial"];

                        StaffManager manager = new StaffManager(firstName, lastName, middleInitial, id);
                        staffManagerList.Add(manager);
                    }
                }
            }
        }

        private void loadStaffContacts()
        {
            const string query = "SELECT * FROM staff_contact";

            using (SQLiteConnection connection = new SQLiteConnection(ConnectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {

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

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                loadStaffManager();
                editControl.updateManagers(staffManagerList);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
