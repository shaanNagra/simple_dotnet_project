namespace staff_contact_app_winform
{
    partial class exportDataControl
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
            buttonPrint = new Button();
            buttonToCSV = new Button();
            SuspendLayout();
            // 
            // buttonPrint
            // 
            buttonPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonPrint.Location = new Point(271, 1);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new Size(75, 23);
            buttonPrint.TabIndex = 0;
            buttonPrint.Text = "Print";
            buttonPrint.UseVisualStyleBackColor = true;
            // 
            // buttonToCSV
            // 
            buttonToCSV.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            buttonToCSV.Location = new Point(0, 1);
            buttonToCSV.Name = "buttonToCSV";
            buttonToCSV.Size = new Size(75, 23);
            buttonToCSV.TabIndex = 1;
            buttonToCSV.Text = "CSV";
            buttonToCSV.UseVisualStyleBackColor = true;
            // 
            // exportDataControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(buttonToCSV);
            Controls.Add(buttonPrint);
            Name = "exportDataControl";
            Size = new Size(346, 24);
            ResumeLayout(false);
        }

        #endregion

        private Button buttonPrint;
        private Button buttonToCSV;
    }
}
