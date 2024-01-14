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
        public EditContactControl()
        {
            InitializeComponent();
        }

        private void radioButtonEmployee_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonEmployee.Checked == true)
            {
                labelManager.Visible = true;
                comboBoxStaffsManager.Visible = true;
                return;
            }
            labelManager.Visible = false;
            comboBoxStaffsManager.Visible = false;
        }
    }
}
