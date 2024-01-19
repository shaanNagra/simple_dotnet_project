using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace staff_contact_app_winform
{
    public partial class PrintForm : Form
    {

        private List<StaffContact> contacts;

        public PrintForm(List<StaffContact> contacts)
        {
            InitializeComponent();
            // Init contacts.
            this.contacts = contacts;
            // 
            DataTable dt = buildDataTable();
            BindingSource bs = new BindingSource();
            // Allow dataGridView to generate the columns and column headers
            // utomatically.
            dataGridViewToPrint.AutoGenerateColumns = true;
            // Bind data source to dataGridView.
            bs.DataSource = dt;
            dataGridViewToPrint.DataSource = dt;
            // Set font of cells and Column Header cell.
            dataGridViewToPrint.DefaultCellStyle.Font = new Font("Arial", 10F, GraphicsUnit.Pixel);
            dataGridViewToPrint.ColumnHeadersDefaultCellStyle.Font = new Font("Arial", 12F, GraphicsUnit.Pixel);
            
            dataGridViewToPrint.AutoResizeRows();
            dataGridViewToPrint.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dataGridViewToPrint.AutoResizeColumns();
            // Update dataGridView to display the data.
            dataGridViewToPrint.Refresh();
        }


        /// <summary>
        /// Button click event handler called to start print dialog and 
        /// execute printing to pdf.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonStartPrint_Click(object sender, EventArgs e)
        {
            DGVPrinterHelper.DGVPrinter printerHelper = new DGVPrinterHelper.DGVPrinter();
            // Set title.
            printerHelper.Title = "Staff Contacts";
            // Add page number to bottm of page.
            printerHelper.PageNumbers = true;
            printerHelper.PageNumberInHeader = false;
            // Align table center of page and text in column header cell left.
            printerHelper.TableAlignment = DGVPrinterHelper.DGVPrinter.Alignment.Center;
            printerHelper.HeaderCellAlignment = StringAlignment.Near;
            // Set columnWidth to shrink to datawidth.
            printerHelper.ColumnWidth = DGVPrinterHelper.DGVPrinter.ColumnWidthSetting.DataWidth;
            // Increase Margins from default to use more of the page.
            printerHelper.PrintMargins = new Margins(40, 40, 20, 20);
            // Print without full dialog.
            // NOTE: Right now if printed with PrintDataGridView function      
            // after printing document main form and printing form are
            // minimized.
            printerHelper.PrintNoDisplay(dataGridViewToPrint);
            // Close printing form.
            this.Close();
        }


        /// <summary>
        /// Builds a datatable from the contacts list that the contstructor is 
        /// called with.
        /// </summary>
        /// <returns>Datatable with data from List of contacts.</returns>
        private DataTable buildDataTable()
        {
            // Setup datatable columns.
            DataTable dt = new DataTable("Staff Contacts");
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

            // Group list by staff type, orderd by first name.
            List<StaffContact> sortedContacts = contacts
                .OrderBy(x => x.firstName)
                .GroupBy(x => x.staffType).SelectMany(x => x).ToList();

            // Add the contact from list to the datatable.
            foreach (StaffContact sc in sortedContacts)
            {
                Debug.WriteLine(sc.fullName);
                dt.Rows.Add(sc.firstName, sc.middleInitial, sc.lastName, sc.title,
                    sc.staffType, sc.homePhone, sc.cellPhone, sc.officeExt,
                    sc.irdNumber, sc.status, sc.manager_id, sc.id);
            }
            
            return dt;
        }
    }
}
