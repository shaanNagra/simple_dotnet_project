using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        }

        private void buttonSaveContact_Click(object sender, EventArgs e)
        {
            
            // Link selected manager if staff is employee
            if (true == radioButtonEmployee.Checked)
            {
                
            }
        }
        public void updateManagers(List<StaffManager> staffManagerList )
        {
            this.staffManagerList = staffManagerList;
            foreach(StaffManager manager in this.staffManagerList)
            {
                comboBoxStaffsManager.Items.Add(manager.fullName);
            }
            return;
        }

        public void loadContact()
        {

        }
    }
}
