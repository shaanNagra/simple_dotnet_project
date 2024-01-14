namespace staff_contact_app_winform
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            splitContainerForm = new SplitContainer();
            buttonAddContact = new Button();
            buttonEditContact = new Button();
            listView1 = new ListView();
            chName = new ColumnHeader();
            chStatus = new ColumnHeader();
            ((System.ComponentModel.ISupportInitialize)splitContainerForm).BeginInit();
            splitContainerForm.Panel1.SuspendLayout();
            splitContainerForm.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainerForm
            // 
            splitContainerForm.Dock = DockStyle.Fill;
            splitContainerForm.Location = new Point(0, 0);
            splitContainerForm.Name = "splitContainerForm";
            // 
            // splitContainerForm.Panel1
            // 
            splitContainerForm.Panel1.Controls.Add(buttonAddContact);
            splitContainerForm.Panel1.Controls.Add(buttonEditContact);
            splitContainerForm.Panel1.Controls.Add(listView1);
            splitContainerForm.Panel1.Padding = new Padding(5);
            // 
            // splitContainerForm.Panel2
            // 
            splitContainerForm.Panel2.Padding = new Padding(5);
            splitContainerForm.Size = new Size(800, 450);
            splitContainerForm.SplitterDistance = 374;
            splitContainerForm.TabIndex = 0;
            // 
            // buttonAddContact
            // 
            buttonAddContact.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAddContact.Location = new Point(291, 8);
            buttonAddContact.Name = "buttonAddContact";
            buttonAddContact.Size = new Size(75, 23);
            buttonAddContact.TabIndex = 2;
            buttonAddContact.Text = "Add";
            buttonAddContact.UseVisualStyleBackColor = true;
            buttonAddContact.Click += buttonAddContact_Click;
            // 
            // buttonEditContact
            // 
            buttonEditContact.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonEditContact.Enabled = false;
            buttonEditContact.Location = new Point(210, 8);
            buttonEditContact.Name = "buttonEditContact";
            buttonEditContact.Size = new Size(75, 23);
            buttonEditContact.TabIndex = 1;
            buttonEditContact.Text = "Edit";
            buttonEditContact.UseVisualStyleBackColor = true;
            buttonEditContact.Click += buttonEditContact_Click;
            // 
            // listView1
            // 
            listView1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listView1.Columns.AddRange(new ColumnHeader[] { chName, chStatus });
            listView1.Location = new Point(5, 61);
            listView1.Name = "listView1";
            listView1.Size = new Size(364, 384);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            // 
            // chName
            // 
            chName.Text = "Name";
            chName.Width = 120;
            // 
            // chStatus
            // 
            chStatus.Text = "Status";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainerForm);
            Name = "Form1";
            Text = "Staff Contacts";
            splitContainerForm.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerForm).EndInit();
            splitContainerForm.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainerForm;
        private ListView listView1;
        private ColumnHeader chName;
        private ColumnHeader chStatus;
        private Button buttonEditContact;
        private Button buttonAddContact;
    }
}
