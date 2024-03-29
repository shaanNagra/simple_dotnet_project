﻿namespace staff_contact_app_winform
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
            buttonPrint = new Button();
            buttonSaveToCSV = new Button();
            label1 = new Label();
            buttonDeleteContact = new Button();
            buttonFilterActive = new Button();
            buttonAddContact = new Button();
            buttonEditContact = new Button();
            listViewContactList = new ListView();
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
            splitContainerForm.Panel1.Controls.Add(buttonPrint);
            splitContainerForm.Panel1.Controls.Add(buttonSaveToCSV);
            splitContainerForm.Panel1.Controls.Add(label1);
            splitContainerForm.Panel1.Controls.Add(buttonDeleteContact);
            splitContainerForm.Panel1.Controls.Add(buttonFilterActive);
            splitContainerForm.Panel1.Controls.Add(buttonAddContact);
            splitContainerForm.Panel1.Controls.Add(buttonEditContact);
            splitContainerForm.Panel1.Controls.Add(listViewContactList);
            splitContainerForm.Panel1.Padding = new Padding(5);
            // 
            // splitContainerForm.Panel2
            // 
            splitContainerForm.Panel2.Padding = new Padding(5);
            splitContainerForm.Size = new Size(800, 450);
            splitContainerForm.SplitterDistance = 374;
            splitContainerForm.TabIndex = 0;
            // 
            // buttonPrint
            // 
            buttonPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonPrint.Location = new Point(294, 419);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(75, 23);
            buttonPrint.TabIndex = 7;
            buttonPrint.Text = "Print";
            buttonPrint.UseVisualStyleBackColor = true;
            buttonPrint.Click += ButtonPrint_Click;
            // 
            // buttonSaveToCSV
            // 
            buttonSaveToCSV.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonSaveToCSV.Location = new Point(5, 419);
            buttonSaveToCSV.Name = "buttonSaveToCSV";
            buttonSaveToCSV.Size = new Size(75, 23);
            buttonSaveToCSV.TabIndex = 6;
            buttonSaveToCSV.Text = "csv";
            buttonSaveToCSV.UseVisualStyleBackColor = true;
            buttonSaveToCSV.Click += ButtonSaveToCSV_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 12);
            label1.Name = "label1";
            label1.Size = new Size(31, 15);
            label1.TabIndex = 5;
            label1.Text = "filter";
            // 
            // buttonDeleteContact
            // 
            buttonDeleteContact.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonDeleteContact.Enabled = false;
            buttonDeleteContact.Location = new Point(204, 8);
            buttonDeleteContact.Name = "buttonDeleteContact";
            buttonDeleteContact.Size = new Size(50, 23);
            buttonDeleteContact.TabIndex = 4;
            buttonDeleteContact.Text = "Delete";
            buttonDeleteContact.UseVisualStyleBackColor = true;
            buttonDeleteContact.Click += ButtonDeleteContact_Click;
            // 
            // buttonFilterActive
            // 
            buttonFilterActive.Location = new Point(45, 8);
            buttonFilterActive.Name = "buttonFilterActive";
            buttonFilterActive.Size = new Size(75, 23);
            buttonFilterActive.TabIndex = 3;
            buttonFilterActive.Text = "Show All";
            buttonFilterActive.UseVisualStyleBackColor = true;
            buttonFilterActive.Click += ButtonFilterActive_Click;
            // 
            // buttonAddContact
            // 
            buttonAddContact.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonAddContact.Location = new Point(316, 8);
            buttonAddContact.Name = "buttonAddContact";
            buttonAddContact.Size = new Size(50, 23);
            buttonAddContact.TabIndex = 2;
            buttonAddContact.Text = "Add";
            buttonAddContact.UseVisualStyleBackColor = true;
            buttonAddContact.Click += ButtonAddContact_Click;
            // 
            // buttonEditContact
            // 
            buttonEditContact.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            buttonEditContact.Enabled = false;
            buttonEditContact.Location = new Point(260, 8);
            buttonEditContact.Name = "buttonEditContact";
            buttonEditContact.Size = new Size(50, 23);
            buttonEditContact.TabIndex = 1;
            buttonEditContact.Text = "Edit";
            buttonEditContact.UseVisualStyleBackColor = true;
            buttonEditContact.Click += ButtonEditContact_Click;
            // 
            // listViewContactList
            // 
            listViewContactList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            listViewContactList.Columns.AddRange(new ColumnHeader[] { chName, chStatus });
            listViewContactList.FullRowSelect = true;
            listViewContactList.Location = new Point(5, 61);
            listViewContactList.MultiSelect = false;
            listViewContactList.Name = "listViewContactList";
            listViewContactList.Size = new Size(364, 352);
            listViewContactList.Sorting = SortOrder.Ascending;
            listViewContactList.TabIndex = 0;
            listViewContactList.UseCompatibleStateImageBehavior = false;
            listViewContactList.View = View.Details;
            listViewContactList.SelectedIndexChanged += ListViewContactList_SelectedIndexChanged;
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
            Load += Form1_Load;
            splitContainerForm.Panel1.ResumeLayout(false);
            splitContainerForm.Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerForm).EndInit();
            splitContainerForm.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainerForm;
        private ListView listViewContactList;
        private ColumnHeader chName;
        private ColumnHeader chStatus;
        private Button buttonEditContact;
        private Button buttonAddContact;
        private Button buttonFilterActive;
        private Button buttonDeleteContact;
        private Label label1;
        private Button buttonSaveToCSV;
        private Button buttonPrint;
    }
}
