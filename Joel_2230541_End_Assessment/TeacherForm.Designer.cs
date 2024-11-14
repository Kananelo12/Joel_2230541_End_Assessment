namespace Joel_2230541_End_Assessment
{
    partial class TeacherForm
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
            this.panel4 = new System.Windows.Forms.Panel();
            this.btnStudents = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.btnLogout = new System.Windows.Forms.Button();
            this.panel8 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnStaff = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.label11 = new System.Windows.Forms.Label();
            this.cmbFilterResults = new System.Windows.Forms.ComboBox();
            this.btnRefreshResults = new System.Windows.Forms.Button();
            this.DGVResults = new System.Windows.Forms.DataGridView();
            this.label2 = new System.Windows.Forms.Label();
            this.pnlSideMenu = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btnMenu = new System.Windows.Forms.PictureBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnClearF = new System.Windows.Forms.Button();
            this.txtSchoolYear = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCaptureResults = new System.Windows.Forms.Button();
            this.cmbTerms = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtStudMarks = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStudID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.DGVMyStudents = new System.Windows.Forms.DataGridView();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.panel4.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVResults)).BeginInit();
            this.pnlSideMenu.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMyStudents)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.btnStudents);
            this.panel4.Location = new System.Drawing.Point(3, 214);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(259, 54);
            this.panel4.TabIndex = 3;
            // 
            // btnStudents
            // 
            this.btnStudents.FlatAppearance.BorderSize = 0;
            this.btnStudents.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStudents.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStudents.ForeColor = System.Drawing.Color.White;
            this.btnStudents.Image = global::Joel_2230541_End_Assessment.Properties.Resources.student;
            this.btnStudents.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStudents.Location = new System.Drawing.Point(-25, -9);
            this.btnStudents.Name = "btnStudents";
            this.btnStudents.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnStudents.Size = new System.Drawing.Size(293, 63);
            this.btnStudents.TabIndex = 1;
            this.btnStudents.Text = "                      View Results";
            this.btnStudents.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStudents.UseVisualStyleBackColor = true;
            this.btnStudents.Click += new System.EventHandler(this.btnStudents_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.btnLogout);
            this.panel7.Controls.Add(this.panel8);
            this.panel7.Location = new System.Drawing.Point(3, 274);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(259, 293);
            this.panel7.TabIndex = 6;
            // 
            // btnLogout
            // 
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLogout.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLogout.ForeColor = System.Drawing.Color.White;
            this.btnLogout.Image = global::Joel_2230541_End_Assessment.Properties.Resources.logout;
            this.btnLogout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.Location = new System.Drawing.Point(-25, 179);
            this.btnLogout.Name = "btnLogout";
            this.btnLogout.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnLogout.Size = new System.Drawing.Size(293, 63);
            this.btnLogout.TabIndex = 1;
            this.btnLogout.Text = "                      Logout";
            this.btnLogout.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLogout.UseVisualStyleBackColor = true;
            this.btnLogout.Click += new System.EventHandler(this.btnLogout_Click);
            // 
            // panel8
            // 
            this.panel8.Location = new System.Drawing.Point(0, 119);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(259, 54);
            this.panel8.TabIndex = 6;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnStaff);
            this.panel3.Location = new System.Drawing.Point(3, 154);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(259, 54);
            this.panel3.TabIndex = 2;
            // 
            // btnStaff
            // 
            this.btnStaff.FlatAppearance.BorderSize = 0;
            this.btnStaff.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStaff.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStaff.ForeColor = System.Drawing.Color.White;
            this.btnStaff.Image = global::Joel_2230541_End_Assessment.Properties.Resources.navItem;
            this.btnStaff.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStaff.Location = new System.Drawing.Point(-25, -9);
            this.btnStaff.Name = "btnStaff";
            this.btnStaff.Padding = new System.Windows.Forms.Padding(30, 0, 0, 0);
            this.btnStaff.Size = new System.Drawing.Size(293, 63);
            this.btnStaff.TabIndex = 1;
            this.btnStaff.Text = "                      Capture Results";
            this.btnStaff.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnStaff.UseVisualStyleBackColor = true;
            this.btnStaff.Click += new System.EventHandler(this.btnStaff_Click);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.cmbFilterResults);
            this.tabPage3.Controls.Add(this.btnRefreshResults);
            this.tabPage3.Controls.Add(this.DGVResults);
            this.tabPage3.Font = new System.Drawing.Font("Ebrima", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1437, 728);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "View Captured Results";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(438, 36);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(114, 23);
            this.label11.TabIndex = 55;
            this.label11.Text = "Filter by Term";
            // 
            // cmbFilterResults
            // 
            this.cmbFilterResults.FormattingEnabled = true;
            this.cmbFilterResults.Items.AddRange(new object[] {
            "Jan - Mar",
            "Apr - Jun",
            "Jul - Sep",
            "Oct - Dec"});
            this.cmbFilterResults.Location = new System.Drawing.Point(442, 62);
            this.cmbFilterResults.Name = "cmbFilterResults";
            this.cmbFilterResults.Size = new System.Drawing.Size(218, 31);
            this.cmbFilterResults.TabIndex = 54;
            this.cmbFilterResults.Text = "Select term...";
            // 
            // btnRefreshResults
            // 
            this.btnRefreshResults.BackColor = System.Drawing.Color.DarkBlue;
            this.btnRefreshResults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRefreshResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefreshResults.Font = new System.Drawing.Font("Ebrima", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRefreshResults.ForeColor = System.Drawing.Color.White;
            this.btnRefreshResults.Location = new System.Drawing.Point(963, 44);
            this.btnRefreshResults.Name = "btnRefreshResults";
            this.btnRefreshResults.Size = new System.Drawing.Size(192, 43);
            this.btnRefreshResults.TabIndex = 53;
            this.btnRefreshResults.Text = "Refresh";
            this.btnRefreshResults.UseVisualStyleBackColor = false;
            this.btnRefreshResults.Click += new System.EventHandler(this.btnRefreshResults_Click);
            // 
            // DGVResults
            // 
            this.DGVResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVResults.Location = new System.Drawing.Point(442, 93);
            this.DGVResults.Name = "DGVResults";
            this.DGVResults.RowHeadersWidth = 51;
            this.DGVResults.RowTemplate.Height = 24;
            this.DGVResults.Size = new System.Drawing.Size(713, 292);
            this.DGVResults.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Impact", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(74, 73);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 19);
            this.label2.TabIndex = 2;
            this.label2.Text = "of Schools";
            // 
            // pnlSideMenu
            // 
            this.pnlSideMenu.BackColor = System.Drawing.Color.MidnightBlue;
            this.pnlSideMenu.Controls.Add(this.panel1);
            this.pnlSideMenu.Controls.Add(this.panel3);
            this.pnlSideMenu.Controls.Add(this.panel4);
            this.pnlSideMenu.Controls.Add(this.panel7);
            this.pnlSideMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSideMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlSideMenu.MaximumSize = new System.Drawing.Size(262, 759);
            this.pnlSideMenu.MinimumSize = new System.Drawing.Size(70, 759);
            this.pnlSideMenu.Name = "pnlSideMenu";
            this.pnlSideMenu.Size = new System.Drawing.Size(262, 759);
            this.pnlSideMenu.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.btnMenu);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 145);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(74, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Karabo Group";
            // 
            // btnMenu
            // 
            this.btnMenu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMenu.Image = global::Joel_2230541_End_Assessment.Properties.Resources.manuBar2;
            this.btnMenu.Location = new System.Drawing.Point(9, 42);
            this.btnMenu.Name = "btnMenu";
            this.btnMenu.Size = new System.Drawing.Size(50, 50);
            this.btnMenu.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.btnMenu.TabIndex = 0;
            this.btnMenu.TabStop = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnClearF);
            this.tabPage2.Controls.Add(this.txtSchoolYear);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.txtSubject);
            this.tabPage2.Controls.Add(this.label9);
            this.tabPage2.Controls.Add(this.btnCaptureResults);
            this.tabPage2.Controls.Add(this.cmbTerms);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.txtStudMarks);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.txtStudID);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.DGVMyStudents);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Font = new System.Drawing.Font("Ebrima", 10.2F);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1437, 728);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Results Management";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnClearF
            // 
            this.btnClearF.BackColor = System.Drawing.Color.RoyalBlue;
            this.btnClearF.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClearF.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClearF.Font = new System.Drawing.Font("Ebrima", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClearF.ForeColor = System.Drawing.Color.White;
            this.btnClearF.Location = new System.Drawing.Point(275, 370);
            this.btnClearF.Name = "btnClearF";
            this.btnClearF.Size = new System.Drawing.Size(212, 43);
            this.btnClearF.TabIndex = 57;
            this.btnClearF.Text = "Reset";
            this.btnClearF.UseVisualStyleBackColor = false;
            this.btnClearF.Click += new System.EventHandler(this.btnClearF_Click);
            // 
            // txtSchoolYear
            // 
            this.txtSchoolYear.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSchoolYear.Location = new System.Drawing.Point(274, 283);
            this.txtSchoolYear.Multiline = true;
            this.txtSchoolYear.Name = "txtSchoolYear";
            this.txtSchoolYear.Size = new System.Drawing.Size(211, 35);
            this.txtSchoolYear.TabIndex = 56;
            this.txtSchoolYear.Text = "2024";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Ebrima", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(271, 257);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(99, 23);
            this.label10.TabIndex = 55;
            this.label10.Text = "School Year";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(275, 111);
            this.txtSubject.Multiline = true;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(211, 35);
            this.txtSubject.TabIndex = 54;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Ebrima", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(270, 80);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(66, 23);
            this.label9.TabIndex = 53;
            this.label9.Text = "Subject";
            // 
            // btnCaptureResults
            // 
            this.btnCaptureResults.BackColor = System.Drawing.Color.DarkBlue;
            this.btnCaptureResults.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCaptureResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaptureResults.Font = new System.Drawing.Font("Ebrima", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCaptureResults.ForeColor = System.Drawing.Color.White;
            this.btnCaptureResults.Location = new System.Drawing.Point(30, 370);
            this.btnCaptureResults.Name = "btnCaptureResults";
            this.btnCaptureResults.Size = new System.Drawing.Size(212, 43);
            this.btnCaptureResults.TabIndex = 52;
            this.btnCaptureResults.Text = "Capture";
            this.btnCaptureResults.UseVisualStyleBackColor = false;
            this.btnCaptureResults.Click += new System.EventHandler(this.btnCaptureResults_Click);
            // 
            // cmbTerms
            // 
            this.cmbTerms.FormattingEnabled = true;
            this.cmbTerms.Items.AddRange(new object[] {
            "Jan - Mar",
            "Apr - Jun",
            "Jul - Sep",
            "Oct - Dec"});
            this.cmbTerms.Location = new System.Drawing.Point(32, 286);
            this.cmbTerms.Name = "cmbTerms";
            this.cmbTerms.Size = new System.Drawing.Size(210, 31);
            this.cmbTerms.TabIndex = 51;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Ebrima", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(27, 257);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(99, 23);
            this.label8.TabIndex = 50;
            this.label8.Text = "Select Term";
            // 
            // txtStudMarks
            // 
            this.txtStudMarks.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudMarks.Location = new System.Drawing.Point(32, 199);
            this.txtStudMarks.Multiline = true;
            this.txtStudMarks.Name = "txtStudMarks";
            this.txtStudMarks.Size = new System.Drawing.Size(454, 35);
            this.txtStudMarks.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Ebrima", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(27, 168);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(55, 23);
            this.label7.TabIndex = 48;
            this.label7.Text = "Marks";
            // 
            // txtStudID
            // 
            this.txtStudID.Font = new System.Drawing.Font("Ebrima", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtStudID.Location = new System.Drawing.Point(31, 111);
            this.txtStudID.Multiline = true;
            this.txtStudID.Name = "txtStudID";
            this.txtStudID.Size = new System.Drawing.Size(211, 35);
            this.txtStudID.TabIndex = 47;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Ebrima", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(26, 80);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(92, 23);
            this.label5.TabIndex = 46;
            this.label5.Text = "Student ID";
            // 
            // DGVMyStudents
            // 
            this.DGVMyStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGVMyStudents.Location = new System.Drawing.Point(518, 63);
            this.DGVMyStudents.Name = "DGVMyStudents";
            this.DGVMyStudents.RowHeadersWidth = 51;
            this.DGVMyStudents.RowTemplate.Height = 24;
            this.DGVMyStudents.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.DGVMyStudents.Size = new System.Drawing.Size(780, 248);
            this.DGVMyStudents.TabIndex = 3;
            this.DGVMyStudents.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVMyStudents_CellClick);
            this.DGVMyStudents.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DGVMyStudents_CellContentClick);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(514, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 23);
            this.label4.TabIndex = 2;
            this.label4.Text = "My Students";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Ebrima", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(26, 27);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(213, 28);
            this.label3.TabIndex = 0;
            this.label3.Text = "Capture Student Marks";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(295, 3);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1445, 757);
            this.tabControl1.TabIndex = 2;
            // 
            // TeacherForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1820, 757);
            this.Controls.Add(this.pnlSideMenu);
            this.Controls.Add(this.tabControl1);
            this.Name = "TeacherForm";
            this.Text = "TeacherForm";
            this.Load += new System.EventHandler(this.TeacherForm_Load);
            this.panel4.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVResults)).EndInit();
            this.pnlSideMenu.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnMenu)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DGVMyStudents)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btnStudents;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Button btnLogout;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btnStaff;
        private System.Windows.Forms.PictureBox btnMenu;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.FlowLayoutPanel pnlSideMenu;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DataGridView DGVResults;
        private System.Windows.Forms.TextBox txtStudID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView DGVMyStudents;
        private System.Windows.Forms.TextBox txtStudMarks;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbTerms;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCaptureResults;
        private System.Windows.Forms.TextBox txtSchoolYear;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnRefreshResults;
        private System.Windows.Forms.Button btnClearF;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cmbFilterResults;
    }
}