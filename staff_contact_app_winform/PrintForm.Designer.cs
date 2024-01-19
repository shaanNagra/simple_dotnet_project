namespace staff_contact_app_winform
{
    partial class PrintForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            dataGridViewToPrint = new DataGridView();
            buttonStartPrint = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewToPrint).BeginInit();
            SuspendLayout();
            // 
            // dataGridViewToPrint
            // 
            dataGridViewToPrint.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridViewToPrint.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewToPrint.Location = new Point(10, 10);
            dataGridViewToPrint.Name = "dataGridViewToPrint";
            dataGridViewToPrint.Size = new Size(1001, 424);
            dataGridViewToPrint.TabIndex = 0;
            // 
            // buttonStartPrint
            // 
            buttonStartPrint.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            buttonStartPrint.Location = new Point(936, 440);
            buttonStartPrint.Name = "buttonStartPrint";
            buttonStartPrint.Size = new Size(75, 23);
            buttonStartPrint.TabIndex = 1;
            buttonStartPrint.Text = "Print";
            buttonStartPrint.UseVisualStyleBackColor = true;
            buttonStartPrint.Click += ButtonStartPrint_Click;
            // 
            // PrintForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1021, 473);
            Controls.Add(buttonStartPrint);
            Controls.Add(dataGridViewToPrint);
            Name = "PrintForm";
            Padding = new Padding(10);
            Text = "PrintForm";
            ((System.ComponentModel.ISupportInitialize)dataGridViewToPrint).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dataGridViewToPrint;
        private Button buttonStartPrint;
    }
}