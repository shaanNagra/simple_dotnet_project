namespace staff_contact_app_winform
{
    public partial class Form1 : Form
    {
        EditContactControl editControl = new EditContactControl();
        ContactDetailsControl detailsControl = new ContactDetailsControl();

        public Form1()
        {
            InitializeComponent();

            splitContainerForm.Panel2.Controls.Add(editControl);
            splitContainerForm.Panel2.Controls.Add(detailsControl);
            
            editControl.Dock = DockStyle.Fill; 
            detailsControl.Dock = DockStyle.Fill;
            
            //detailsControl.Visible = false;
            editControl.Visible = false;

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
    }
}
