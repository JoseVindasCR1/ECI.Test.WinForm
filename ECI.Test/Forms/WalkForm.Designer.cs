namespace ECI.Test.Forms
{
    partial class WalkForm
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
            this.grpDogEditor = new System.Windows.Forms.GroupBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.btnCancelWalk = new System.Windows.Forms.Button();
            this.btnSaveWalk = new System.Windows.Forms.Button();
            this.numDuration = new System.Windows.Forms.NumericUpDown();
            this.lblDuration = new System.Windows.Forms.Label();
            this.lblDate = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnDeleteWalk = new System.Windows.Forms.Button();
            this.btnEditWalk = new System.Windows.Forms.Button();
            this.dgvWalks = new System.Windows.Forms.DataGridView();
            this.lblClientName = new System.Windows.Forms.Label();
            this.lblDogName = new System.Windows.Forms.Label();
            this.grpDogEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalks)).BeginInit();
            this.SuspendLayout();
            // 
            // grpDogEditor
            // 
            this.grpDogEditor.Controls.Add(this.dateTimePicker1);
            this.grpDogEditor.Controls.Add(this.btnCancelWalk);
            this.grpDogEditor.Controls.Add(this.btnSaveWalk);
            this.grpDogEditor.Controls.Add(this.numDuration);
            this.grpDogEditor.Controls.Add(this.lblDuration);
            this.grpDogEditor.Controls.Add(this.lblDate);
            this.grpDogEditor.Location = new System.Drawing.Point(480, 82);
            this.grpDogEditor.Name = "grpDogEditor";
            this.grpDogEditor.Size = new System.Drawing.Size(300, 185);
            this.grpDogEditor.TabIndex = 11;
            this.grpDogEditor.TabStop = false;
            this.grpDogEditor.Text = "Dog Editor";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(15, 50);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 8;
            // 
            // btnCancelWalk
            // 
            this.btnCancelWalk.Location = new System.Drawing.Point(125, 138);
            this.btnCancelWalk.Name = "btnCancelWalk";
            this.btnCancelWalk.Size = new System.Drawing.Size(100, 30);
            this.btnCancelWalk.TabIndex = 7;
            this.btnCancelWalk.Text = "Cancel";
            this.btnCancelWalk.UseVisualStyleBackColor = true;
            this.btnCancelWalk.Click += new System.EventHandler(this.btnCancelWalk_Click);
            // 
            // btnSaveWalk
            // 
            this.btnSaveWalk.Location = new System.Drawing.Point(15, 138);
            this.btnSaveWalk.Name = "btnSaveWalk";
            this.btnSaveWalk.Size = new System.Drawing.Size(100, 30);
            this.btnSaveWalk.TabIndex = 6;
            this.btnSaveWalk.Text = "Add Walk";
            this.btnSaveWalk.UseVisualStyleBackColor = true;
            this.btnSaveWalk.Click += new System.EventHandler(this.btnSaveWalk_Click);
            // 
            // numDuration
            // 
            this.numDuration.Location = new System.Drawing.Point(15, 102);
            this.numDuration.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numDuration.Name = "numDuration";
            this.numDuration.Size = new System.Drawing.Size(120, 20);
            this.numDuration.TabIndex = 5;
            // 
            // lblDuration
            // 
            this.lblDuration.AutoSize = true;
            this.lblDuration.Location = new System.Drawing.Point(15, 82);
            this.lblDuration.Name = "lblDuration";
            this.lblDuration.Size = new System.Drawing.Size(50, 13);
            this.lblDuration.TabIndex = 4;
            this.lblDuration.Text = "Duration:";
            // 
            // lblDate
            // 
            this.lblDate.AutoSize = true;
            this.lblDate.Location = new System.Drawing.Point(15, 30);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(33, 13);
            this.lblDate.TabIndex = 0;
            this.lblDate.Text = "Date:";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(362, 392);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.TabIndex = 10;
            this.btnBack.Text = "Back to Dogs";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnDeleteWalk
            // 
            this.btnDeleteWalk.Location = new System.Drawing.Point(120, 392);
            this.btnDeleteWalk.Name = "btnDeleteWalk";
            this.btnDeleteWalk.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteWalk.TabIndex = 9;
            this.btnDeleteWalk.Text = "Delete";
            this.btnDeleteWalk.UseVisualStyleBackColor = true;
            this.btnDeleteWalk.Click += new System.EventHandler(this.btnDeleteWalk_Click);
            // 
            // btnEditWalk
            // 
            this.btnEditWalk.Location = new System.Drawing.Point(12, 392);
            this.btnEditWalk.Name = "btnEditWalk";
            this.btnEditWalk.Size = new System.Drawing.Size(100, 30);
            this.btnEditWalk.TabIndex = 8;
            this.btnEditWalk.Text = "Edit";
            this.btnEditWalk.UseVisualStyleBackColor = true;
            this.btnEditWalk.Click += new System.EventHandler(this.btnEditWalk_Click);
            // 
            // dgvWalks
            // 
            this.dgvWalks.AllowUserToAddRows = false;
            this.dgvWalks.AllowUserToDeleteRows = false;
            this.dgvWalks.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvWalks.Location = new System.Drawing.Point(12, 82);
            this.dgvWalks.MultiSelect = false;
            this.dgvWalks.Name = "dgvWalks";
            this.dgvWalks.ReadOnly = true;
            this.dgvWalks.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvWalks.Size = new System.Drawing.Size(450, 300);
            this.dgvWalks.TabIndex = 7;
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblClientName.Location = new System.Drawing.Point(12, 14);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(60, 20);
            this.lblClientName.TabIndex = 6;
            this.lblClientName.Text = "Client:";
            // 
            // lblDogName
            // 
            this.lblDogName.AutoSize = true;
            this.lblDogName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblDogName.Location = new System.Drawing.Point(12, 43);
            this.lblDogName.Name = "lblDogName";
            this.lblDogName.Size = new System.Drawing.Size(52, 20);
            this.lblDogName.TabIndex = 12;
            this.lblDogName.Text = "Dog: ";
            // 
            // WalkForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.lblDogName);
            this.Controls.Add(this.grpDogEditor);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDeleteWalk);
            this.Controls.Add(this.btnEditWalk);
            this.Controls.Add(this.dgvWalks);
            this.Controls.Add(this.lblClientName);
            this.Name = "WalkForm";
            this.Text = "WalkForm";
            this.grpDogEditor.ResumeLayout(false);
            this.grpDogEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvWalks)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpDogEditor;
        private System.Windows.Forms.Button btnCancelWalk;
        private System.Windows.Forms.Button btnSaveWalk;
        private System.Windows.Forms.NumericUpDown numDuration;
        private System.Windows.Forms.Label lblDuration;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Button btnDeleteWalk;
        private System.Windows.Forms.Button btnEditWalk;
        private System.Windows.Forms.DataGridView dgvWalks;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.Label lblDogName;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}