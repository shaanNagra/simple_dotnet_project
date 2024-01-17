using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;

namespace staff_contact_app_winform
{
    public static class DatabaseManager
    {

        static SQLiteConnection _dbConn;
        static string _dbConnString;

        static DatabaseManager()
        {
            if (null == _dbConn)
            {
                _dbConnString = "Data Source= "+System.IO.Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "staff_db.sqlite" );
                //_dbConnString = "Data Source= C:\\Users\\shaan\\OneDrive\\Documents\\radfords_winform\\staff_contact_app_winform\\Database\\staff_contacts.sqlite";
                _dbConn = new SQLiteConnection(_dbConnString);

                _dbConn.Open();
                var query = "SELECT EXISTS( SELECT name FROM sqlite_schema WHERE type = 'table' AND name = 'staff');";
                SQLiteCommand command = new SQLiteCommand(query, _dbConn);
                long exists = (long) command.ExecuteScalar();
                if ((long)0 == exists)
                {
                    Debug.WriteLine("Table Does Not Exist");
                    initTable();
                }
                Debug.WriteLine("Table Does Exist");
                _dbConn.Close();
            }
        }
        public static void sayHi() 
        {
            
        }

        /// <summary>
        /// loads all staff from database into staffContactList. Intended use,
        /// to load database into local copy at start of application.
        /// </summary>
        public static void loadStaffContacts(List<StaffContact> staffContactsList)
        {
            const string query = "SELECT * FROM staff";

            using (SQLiteConnection connection = new SQLiteConnection(_dbConnString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = ParseNullValue<long>(reader, "id");
                        string staffType = ParseNullValue<string>(reader, "staff_type");
                        string title = ParseNullValue<string>(reader, "title");
                        string firstName = ParseNullValue<string>(reader, "first_name");
                        string lastName = ParseNullValue<string>(reader, "last_name");
                        string middleInitial = ParseNullValue<string>(reader, "middle_initial");
                        string homePhone = ParseNullValue<string>(reader, "home_phone");
                        string cellPhone = ParseNullValue<string>(reader, "cell_phone");
                        string officeExt = ParseNullValue<string>(reader, "office_extension");
                        string irdNumber = ParseNullValue<string>(reader, "ird_number");
                        string status = ParseNullValue<string>(reader, "status");
                        long manager_id = ParseNullValue<long>(reader, "manager_id");

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
        public static void addStaffContact(List<StaffContact> staffContactsList, StaffContact contact)
        {
            const string query = "INSERT into staff " +
                "(staff_type, title, first_name, last_name, middle_initial, " +
                "home_phone, cell_phone, office_extension, ird_number, status, manager_id) " +
                "VALUES (@staff_type, @title, @first_name, @last_name, @middle_initial, " +
                "@home_phone, @cell_phone, @office_extension, @ird_number, @status, @manager_id);" +
                "SELECT last_insert_rowid()";

            using (SQLiteConnection connection = new SQLiteConnection(_dbConnString))
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

        public static void updateStaffContact(StaffContact contact)
        {

            string query = "UPDATE staff SET staff_type=@staff_type, title=@title" +
                ", first_name=@first_name, last_name=@last_name" +
                ", middle_initial=@middle_initial" +
                ", home_phone=@home_phone, cell_phone=@cell_phone, office_extension=@office_extension" +
                ", ird_number=@ird_number" +
                ", status=@status, manager_id=@manager_id " +
                "WHERE id = @id";

            using (SQLiteConnection connection = new SQLiteConnection(_dbConnString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(query, connection);
                Debug.WriteLine(contact.officeExt);
                Debug.WriteLine(contact.manager_id);
                Debug.WriteLine(contact.irdNumber);
                Debug.WriteLine(contact.lastName);

                command.Parameters.AddWithValue("@staff_type", contact.staffType ?? null);
                command.Parameters.AddWithValue("@title", contact.title ?? null);
                command.Parameters.AddWithValue("@first_name", contact.firstName ?? null);
                command.Parameters.AddWithValue("@last_name", contact.lastName ?? null);
                command.Parameters.AddWithValue("@middle_initial", contact.middleInitial ?? null);
                command.Parameters.AddWithValue("@home_phone", contact.homePhone ?? null);
                command.Parameters.AddWithValue("@cell_phone", contact.cellPhone ?? null);
                command.Parameters.AddWithValue("@office_extension", contact.officeExt ?? null);
                command.Parameters.AddWithValue("@ird_number", contact.irdNumber ?? null);
                command.Parameters.AddWithValue("@status", contact.status ?? null);
                command.Parameters.AddWithValue("@manager_id", contact.manager_id);

                command.Parameters.AddWithValue("@id", contact.id);

                command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contact"></param>
        public static void deleteStaffContact(List<StaffContact> staffContactsList, StaffContact contact)
        {
            const string query = "DELETE FROM staff WHERE id=@id";

            using (SQLiteConnection connection = new SQLiteConnection(_dbConnString))
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
        public static void loadStaffManager(List<StaffManager> staffManagerList)
        {
            staffManagerList.Clear();
            const string query = "SELECT * FROM staff WHERE staff.staff_type = 'Manager'";
            using (SQLiteConnection connection = new SQLiteConnection(_dbConnString))
            {
                connection.Open();
                var table = connection.GetSchema();
                Debug.Print(table.ToString());
                var command = new SQLiteCommand(query, connection);

                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        long id = ParseNullValue<long>(reader, "id");
                        string title = ParseNullValue<string>(reader, "title");
                        string firstName = ParseNullValue<string>(reader, "first_name");
                        string lastName = ParseNullValue<string>(reader, "last_name");
                        string middleInitial = ParseNullValue<string>(reader, "middle_initial");

                        StaffManager manager = new StaffManager(title, firstName, lastName, middleInitial, id);
                        staffManagerList.Add(manager);
                    }
                }
            }
        }

        public static void initTable()
        {
            string createQuery = "CREATE TABLE \"staff\" (" +
                " \"id\" INTEGER NOT NULL UNIQUE," +
                " \"staff_type\" TEXT NOT NULL DEFAULT 'Employee', " +
                " \"title\" TEXT, " +
                " \"first_name\" TEXT, " +
                " \"last_name\" TEXT, " +
                " \"middle_initial\" TEXT, " +
                " \"home_phone\" TEXT, " +
                " \"cell_phone\" TEXT, " +
                " \"office_extension\" TEXT, " +
                " \"ird_number\" TEXT, " +
                " \"status\" TEXT NOT NULL DEFAULT 'Pending', " +
                " \"manager_id\" INTEGER, " +
                " FOREIGN KEY(\"manager_id\") REFERENCES \"staff\"(\"id\"), " +
                " PRIMARY KEY(\"id\" AUTOINCREMENT) )";

            string DirtyInsertQuery = "INSERT INTO \"main\".\"staff" +
                "\" (\"id\", \"staff_type\", \"title\", \"first_name\", \"last_name\", \"middle_initial\", \"home_phone\", \"cell_phone\", \"office_extension\", \"ird_number\", \"status\", \"manager_id\") \r\nVALUES \r\n('1', 'Employee', 'Sir', 'Lewis', 'Hamilton', 'P', '', '', '', '', 'Active', '2'),\r\n('2', 'Manager', 'Mr', 'Josh', 'Bunning', 'G', '', '', '', '', 'Active', '0'),\r\n('3', 'Manager', 'Mrs', 'Jen', 'Jackson', 'S', '', '', '', '', 'Active', '0'),\r\n('4', 'Employee', 'Miss', 'Frances', 'Rhodes', 'K', '', '3424435555', '', '999888777', 'Active', '2'),\r\n('5', 'Manager', 'Mr', 'Daniel', 'Wilson', 'T', '', '3437292184', '3143', '888777666', 'Active', '0'),\r\n('6', 'Employee', 'Mrs', 'Susan', 'Pearson', 'D', '', '4222222222', '', '', 'Inactive', '5'),\r\n('7', 'Manager', 'Sir', 'Sony', 'Williams', 'B', '', '3333333333', '', '', 'Pending', '0');";

            SQLiteCommand command = new SQLiteCommand(createQuery, _dbConn);
            command.ExecuteNonQuery();

            command = new SQLiteCommand(DirtyInsertQuery, _dbConn);
            command.ExecuteNonQuery();
        }

        private static T ParseNullValue<T>(this SQLiteDataReader reader, string columnName)
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
