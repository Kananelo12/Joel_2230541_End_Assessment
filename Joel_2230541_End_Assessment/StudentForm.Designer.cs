namespace Joel_2230541_End_Assessment
{
    partial class StudentForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnViewResults = new System.Windows.Forms.Button();
            this.btnPayFees = new System.Windows.Forms.Button();
            this.pnlStudResults = new System.Windows.Forms.Panel();
            this.DGVResults = new System.Windows.Forms.DataGridView();
            this.panel1.SuspendLayout();
            this.pnlStudResults.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVResults)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.btnLogout);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1057, 100);
            this.panel1.TabIndex = 0;
            // 
            // btnLogout
            // 
            this.btnLogout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Ebrima", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Location = new System.Drawing.Point(912, 33);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Size = new System.Drawing.Size(112, 45);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "Logout";
            this.btnLogout.UseVisualStyleBackColor = false;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(25, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(236, 35);
            this.label1.TabIndex = 0;
            this.label1.Text = "Student Dashboard";
            // 
            // btnViewResults
            // 
            this.btnViewResults.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnViewResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewResults.Font = new System.Drawing.Font("Ebrima", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewResults.ForeColor = System.Drawing.Color.White;
            this.btnViewResults.Location = new System.Drawing.Point(308, 116);
            this.btnViewResults.Name = "btnViewResults";
            this.btnViewResults.Size = new System.Drawing.Size(176, 45);
            this.btnViewResults.TabIndex = 2;
            this.btnViewResults.Text = "View Results";
            this.btnViewResults.UseVisualStyleBackColor = false;
            this.btnViewResults.Click += new System.EventHandler(this.btnViewResults_Click);
            // 
            // btnPayFees
            // 
            this.btnPayFees.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnPayFees.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPayFees.Font = new System.Drawing.Font("Ebrima", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPayFees.ForeColor = System.Drawing.Color.White;
            this.btnPayFees.Location = new System.Drawing.Point(531, 116);
            this.btnPayFees.Name = "btnPayFees";
            this.btnPayFees.Size = new System.Drawing.Size(176, 45);
            this.btnPayFees.TabIndex = 3;
            this.btnPayFees.Text = "Pay Fees";
            this.btnPayFees.UseVisualStyleBackColor = false;
            this.btnPayFees.Click += new System.EventHandler(this.btnPayFees_Click);
            // 
            // pnlStudResults
            // 
            this.pnlStudResults.Controls.Add(this.DGVResults);
            this.pnlStudResults.Location = new System.Drawing.Point(32, 228);
            this.pnlStudResults.Name = "pnlStudResults";
            this.pnlStudResults.Size = new System.Drawing.Size(993, 320);
            this.pnlStudResults.TabIndex = 4;
            this.pnlStudResults.Visible = false;
            // 
            // DGVResults
            // 
            this.DGVResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVResults.Location = new System.Drawing.Point(56, 16);
            this.DGVResults.Name = "DGVResults";
            this.DGVResults.RowHeadersWidth = 51;
            this.DGVResults.RowTemplate.Height = 24;
            this.DGVResults.Size = new System.Drawing.Size(888, 292);
            this.DGVResults.TabIndex = 3;
            // 
            // StudentForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1057, 560);
            this.Controls.Add(this.pnlStudResults);
            this.Controls.Add(this.btnPayFees);
            this.Controls.Add(this.btnViewResults);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.Name = "StudentForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StudentForm";
            this.Load += new System.EventHandler(this.StudentForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlStudResults.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.DGVResults)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnViewResults;
        private System.Windows.Forms.Button btnPayFees;
        private System.Windows.Forms.Panel pnlStudResults;
        private System.Windows.Forms.DataGridView DGVResults;
    }
}