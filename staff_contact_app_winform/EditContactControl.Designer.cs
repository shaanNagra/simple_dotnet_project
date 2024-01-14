namespace staff_contact_app_winform
{
    partial class EditContactControl
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            radioButtonManager = new RadioButton();
            panelStaffType = new Panel();
            radioButtonEmployee = new RadioButton();
            textBoxCellPhone = new TextBox();
            label8 = new Label();
            textBoxHomePhone = new TextBox();
            textBoxLastName = new TextBox();
            textBoxMiddleInitial = new TextBox();
            label7 = new Label();
            label6 = new Label();
            label5 = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label9 = new Label();
            textBoxFirstName = new TextBox();
            comboBoxStaffTitle = new ComboBox();
            textBoxOfficeExt = new TextBox();
            textBoxIRDNumber = new TextBox();
            panelStatusSelection = new Panel();
            radioButtonPending = new RadioButton();
            radioButtonInactive = new RadioButton();
            radioButtonActive = new RadioButton();
            label10 = new Label();
            labelManager = new Label();
            comboBoxStaffsManager = new ComboBox();
            buttonSaveContact = new Button();
            buttonCancel = new Button();
            panelStaffType.SuspendLayout();
            panelStatusSelection.SuspendLayout();
            SuspendLayout();
            // 
            // radioButtonManager
            // 
            radioButtonManager.AutoSize = true;
            radioButtonManager.Location = new Point(86, 1);
            radioButtonManager.Name = "radioButtonManager";
            radioButtonManager.Size = new Size(72, 19);
            radioButtonManager.TabIndex = 2;
            radioButtonManager.Text = "Manager";
            radioButtonManager.UseVisualStyleBackColor = true;
            // 
            // panelStaffType
            // 
            panelStaffType.Controls.Add(radioButtonManager);
            panelStaffType.Controls.Add(radioButtonEmployee);
            panelStaffType.Location = new Point(113, 38);
            panelStaffType.Name = "panelStaffType";
            panelStaffType.Size = new Size(230, 23);
            panelStaffType.TabIndex = 2;
            // 
            // radioButtonEmployee
            // 
            radioButtonEmployee.AutoSize = true;
            radioButtonEmployee.Checked = true;
            radioButtonEmployee.Location = new Point(3, 1);
            radioButtonEmployee.Name = "radioButtonEmployee";
            radioButtonEmployee.Size = new Size(77, 19);
            radioButtonEmployee.TabIndex = 0;
            radioButtonEmployee.TabStop = true;
            radioButtonEmployee.Text = "Employee";
            radioButtonEmployee.UseVisualStyleBackColor = true;
            radioButtonEmployee.CheckedChanged += radioButtonEmployee_CheckedChanged;
            // 
            // textBoxCellPhone
            // 
            textBoxCellPhone.Location = new Point(113, 233);
            textBoxCellPhone.Name = "textBoxCellPhone";
            textBoxCellPhone.Size = new Size(230, 23);
            textBoxCellPhone.TabIndex = 23;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(35, 306);
            label8.Name = "label8";
            label8.Size = new Size(72, 15);
            label8.TabIndex = 22;
            label8.Text = "IRD Number";
            label8.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBoxHomePhone
            // 
            textBoxHomePhone.Location = new Point(113, 204);
            textBoxHomePhone.Margin = new Padding(3, 15, 3, 3);
            textBoxHomePhone.Name = "textBoxHomePhone";
            textBoxHomePhone.Size = new Size(230, 23);
            textBoxHomePhone.TabIndex = 21;
            // 
            // textBoxLastName
            // 
            textBoxLastName.Location = new Point(114, 163);
            textBoxLastName.Name = "textBoxLastName";
            textBoxLastName.Size = new Size(229, 23);
            textBoxLastName.TabIndex = 20;
            // 
            // textBoxMiddleInitial
            // 
            textBoxMiddleInitial.Location = new Point(114, 134);
            textBoxMiddleInitial.Name = "textBoxMiddleInitial";
            textBoxMiddleInitial.Size = new Size(80, 23);
            textBoxMiddleInitial.TabIndex = 19;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(14, 265);
            label7.Name = "label7";
            label7.Size = new Size(93, 15);
            label7.TabIndex = 18;
            label7.Text = "Office Extension";
            label7.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(43, 236);
            label6.Name = "label6";
            label6.Size = new Size(64, 15);
            label6.TabIndex = 17;
            label6.Text = "Cell Phone";
            label6.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(30, 207);
            label5.Name = "label5";
            label5.Size = new Size(77, 15);
            label5.TabIndex = 16;
            label5.Text = "Home Phone";
            label5.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(49, 38);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 24;
            label1.Text = "Staff Type";
            label1.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(78, 79);
            label2.Name = "label2";
            label2.Size = new Size(29, 15);
            label2.TabIndex = 25;
            label2.Text = "Title";
            label2.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(44, 108);
            label3.Name = "label3";
            label3.Size = new Size(64, 15);
            label3.TabIndex = 26;
            label3.Text = "First Name";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(32, 137);
            label4.Name = "label4";
            label4.Size = new Size(76, 15);
            label4.TabIndex = 27;
            label4.Text = "Middle Initial";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(45, 166);
            label9.Name = "label9";
            label9.Size = new Size(63, 15);
            label9.TabIndex = 28;
            label9.Text = "Last Name";
            label9.TextAlign = ContentAlignment.MiddleRight;
            // 
            // textBoxFirstName
            // 
            textBoxFirstName.Location = new Point(114, 105);
            textBoxFirstName.Name = "textBoxFirstName";
            textBoxFirstName.Size = new Size(229, 23);
            textBoxFirstName.TabIndex = 29;
            // 
            // comboBoxStaffTitle
            // 
            comboBoxStaffTitle.FormattingEnabled = true;
            comboBoxStaffTitle.Items.AddRange(new object[] { "Mr", "Mrs", "Miss", "Sir" });
            comboBoxStaffTitle.Location = new Point(113, 76);
            comboBoxStaffTitle.Name = "comboBoxStaffTitle";
            comboBoxStaffTitle.Size = new Size(80, 23);
            comboBoxStaffTitle.TabIndex = 30;
            // 
            // textBoxOfficeExt
            // 
            textBoxOfficeExt.Location = new Point(113, 262);
            textBoxOfficeExt.Name = "textBoxOfficeExt";
            textBoxOfficeExt.Size = new Size(159, 23);
            textBoxOfficeExt.TabIndex = 31;
            // 
            // textBoxIRDNumber
            // 
            textBoxIRDNumber.Location = new Point(113, 303);
            textBoxIRDNumber.Margin = new Padding(3, 15, 3, 3);
            textBoxIRDNumber.Name = "textBoxIRDNumber";
            textBoxIRDNumber.Size = new Size(230, 23);
            textBoxIRDNumber.TabIndex = 32;
            // 
            // panelStatusSelection
            // 
            panelStatusSelection.Controls.Add(radioButtonPending);
            panelStatusSelection.Controls.Add(radioButtonInactive);
            panelStatusSelection.Controls.Add(radioButtonActive);
            panelStatusSelection.Location = new Point(113, 332);
            panelStatusSelection.Name = "panelStatusSelection";
            panelStatusSelection.Size = new Size(230, 23);
            panelStatusSelection.TabIndex = 33;
            // 
            // radioButtonPending
            // 
            radioButtonPending.AutoSize = true;
            radioButtonPending.Checked = true;
            radioButtonPending.Location = new Point(136, 0);
            radioButtonPending.Name = "radioButtonPending";
            radioButtonPending.Size = new Size(69, 19);
            radioButtonPending.TabIndex = 2;
            radioButtonPending.TabStop = true;
            radioButtonPending.Text = "Pending";
            radioButtonPending.UseVisualStyleBackColor = true;
            // 
            // radioButtonInactive
            // 
            radioButtonInactive.AutoSize = true;
            radioButtonInactive.Location = new Point(64, 0);
            radioButtonInactive.Name = "radioButtonInactive";
            radioButtonInactive.Size = new Size(66, 19);
            radioButtonInactive.TabIndex = 1;
            radioButtonInactive.Text = "Inactive";
            radioButtonInactive.UseVisualStyleBackColor = true;
            // 
            // radioButtonActive
            // 
            radioButtonActive.AutoSize = true;
            radioButtonActive.Location = new Point(0, 0);
            radioButtonActive.Name = "radioButtonActive";
            radioButtonActive.Size = new Size(58, 19);
            radioButtonActive.TabIndex = 0;
            radioButtonActive.Text = "Active";
            radioButtonActive.UseVisualStyleBackColor = true;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(68, 334);
            label10.Name = "label10";
            label10.Size = new Size(39, 15);
            label10.TabIndex = 34;
            label10.Text = "Status";
            label10.TextAlign = ContentAlignment.MiddleRight;
            // 
            // labelManager
            // 
            labelManager.AutoSize = true;
            labelManager.Location = new Point(53, 382);
            labelManager.Name = "labelManager";
            labelManager.Size = new Size(54, 15);
            labelManager.TabIndex = 35;
            labelManager.Text = "Manager";
            labelManager.TextAlign = ContentAlignment.MiddleRight;
            // 
            // comboBoxStaffsManager
            // 
            comboBoxStaffsManager.FormattingEnabled = true;
            comboBoxStaffsManager.Location = new Point(113, 379);
            comboBoxStaffsManager.Name = "comboBoxStaffsManager";
            comboBoxStaffsManager.Size = new Size(230, 23);
            comboBoxStaffsManager.TabIndex = 36;
            // 
            // buttonSaveContact
            // 
            buttonSaveContact.Location = new Point(282, 460);
            buttonSaveContact.Name = "buttonSaveContact";
            buttonSaveContact.Size = new Size(75, 23);
            buttonSaveContact.TabIndex = 37;
            buttonSaveContact.Text = "save";
            buttonSaveContact.UseVisualStyleBackColor = true;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new Point(201, 460);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new Size(75, 23);
            buttonCancel.TabIndex = 38;
            buttonCancel.Text = "cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // EditContactControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonCancel);
            Controls.Add(buttonSaveContact);
            Controls.Add(comboBoxStaffsManager);
            Controls.Add(labelManager);
            Controls.Add(label10);
            Controls.Add(panelStatusSelection);
            Controls.Add(textBoxIRDNumber);
            Controls.Add(textBoxOfficeExt);
            Controls.Add(comboBoxStaffTitle);
            Controls.Add(textBoxFirstName);
            Controls.Add(label9);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(textBoxCellPhone);
            Controls.Add(label8);
            Controls.Add(textBoxHomePhone);
            Controls.Add(textBoxLastName);
            Controls.Add(textBoxMiddleInitial);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(panelStaffType);
            Name = "EditContactControl";
            Size = new Size(360, 486);
            panelStaffType.ResumeLayout(false);
            panelStaffType.PerformLayout();
            panelStatusSelection.ResumeLayout(false);
            panelStatusSelection.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private RadioButton radioButtonManager;
        private Panel panelStaffType;
        private RadioButton radioButtonEmployee;
        private TextBox textBoxCellPhone;
        private Label label8;
        private TextBox textBoxHomePhone;
        private TextBox textBoxLastName;
        private TextBox textBoxMiddleInitial;
        private Label label7;
        private Label label6;
        private Label label5;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label9;
        private TextBox textBoxFirstName;
        private ComboBox comboBoxStaffTitle;
        private TextBox textBoxOfficeExt;
        private TextBox textBoxIRDNumber;
        private Panel panelStatusSelection;
        private RadioButton radioButtonPending;
        private RadioButton radioButtonInactive;
        private RadioButton radioButtonActive;
        private Label label10;
        private Label labelManager;
        private ComboBox comboBoxStaffsManager;
        private Button buttonSaveContact;
        private Button buttonCancel;
    }
}
