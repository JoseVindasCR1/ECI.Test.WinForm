namespace ECI.Test.Forms
{
    partial class DetailForm
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.Label lblClientName;
        private System.Windows.Forms.DataGridView dgvDogs;
        private System.Windows.Forms.GroupBox grpDogEditor;
        private System.Windows.Forms.TextBox txtDogName;
        private System.Windows.Forms.TextBox txtBreed;
        private System.Windows.Forms.NumericUpDown numAge;
        private System.Windows.Forms.Label lblDogName;
        private System.Windows.Forms.Label lblBreed;
        private System.Windows.Forms.Label lblAge;
        private System.Windows.Forms.Button btnSaveDog;
        private System.Windows.Forms.Button btnCancelDog;
        private System.Windows.Forms.Button btnEditDog;
        private System.Windows.Forms.Button btnDeleteDog;
        private System.Windows.Forms.Button btnBack;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblClientName = new System.Windows.Forms.Label();
            this.dgvDogs = new System.Windows.Forms.DataGridView();
            this.grpDogEditor = new System.Windows.Forms.GroupBox();
            this.btnCancelDog = new System.Windows.Forms.Button();
            this.btnSaveDog = new System.Windows.Forms.Button();
            this.numAge = new System.Windows.Forms.NumericUpDown();
            this.lblAge = new System.Windows.Forms.Label();
            this.txtBreed = new System.Windows.Forms.TextBox();
            this.lblBreed = new System.Windows.Forms.Label();
            this.txtDogName = new System.Windows.Forms.TextBox();
            this.lblDogName = new System.Windows.Forms.Label();
            this.btnEditDog = new System.Windows.Forms.Button();
            this.btnDeleteDog = new System.Windows.Forms.Button();
            this.btnBack = new System.Windows.Forms.Button();
            this.btnViewWalks = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDogs)).BeginInit();
            this.grpDogEditor.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAge)).BeginInit();
            this.SuspendLayout();
            // 
            // lblClientName
            // 
            this.lblClientName.AutoSize = true;
            this.lblClientName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblClientName.Location = new System.Drawing.Point(12, 15);
            this.lblClientName.Name = "lblClientName";
            this.lblClientName.Size = new System.Drawing.Size(94, 20);
            this.lblClientName.TabIndex = 0;
            this.lblClientName.Text = "Client: N/A";
            // 
            // dgvDogs
            // 
            this.dgvDogs.AllowUserToAddRows = false;
            this.dgvDogs.AllowUserToDeleteRows = false;
            this.dgvDogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDogs.Location = new System.Drawing.Point(12, 50);
            this.dgvDogs.MultiSelect = false;
            this.dgvDogs.Name = "dgvDogs";
            this.dgvDogs.ReadOnly = true;
            this.dgvDogs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDogs.Size = new System.Drawing.Size(450, 300);
            this.dgvDogs.TabIndex = 1;
            // 
            // grpDogEditor
            // 
            this.grpDogEditor.Controls.Add(this.btnCancelDog);
            this.grpDogEditor.Controls.Add(this.btnSaveDog);
            this.grpDogEditor.Controls.Add(this.numAge);
            this.grpDogEditor.Controls.Add(this.lblAge);
            this.grpDogEditor.Controls.Add(this.txtBreed);
            this.grpDogEditor.Controls.Add(this.lblBreed);
            this.grpDogEditor.Controls.Add(this.txtDogName);
            this.grpDogEditor.Controls.Add(this.lblDogName);
            this.grpDogEditor.Location = new System.Drawing.Point(480, 50);
            this.grpDogEditor.Name = "grpDogEditor";
            this.grpDogEditor.Size = new System.Drawing.Size(300, 250);
            this.grpDogEditor.TabIndex = 5;
            this.grpDogEditor.TabStop = false;
            this.grpDogEditor.Text = "Dog Editor";
            // 
            // btnCancelDog
            // 
            this.btnCancelDog.Location = new System.Drawing.Point(125, 200);
            this.btnCancelDog.Name = "btnCancelDog";
            this.btnCancelDog.Size = new System.Drawing.Size(100, 30);
            this.btnCancelDog.TabIndex = 7;
            this.btnCancelDog.Text = "Cancel";
            this.btnCancelDog.UseVisualStyleBackColor = true;
            this.btnCancelDog.Click += new System.EventHandler(this.btnCancelDog_Click);
            // 
            // btnSaveDog
            // 
            this.btnSaveDog.Location = new System.Drawing.Point(15, 200);
            this.btnSaveDog.Name = "btnSaveDog";
            this.btnSaveDog.Size = new System.Drawing.Size(100, 30);
            this.btnSaveDog.TabIndex = 6;
            this.btnSaveDog.Text = "Add Dog";
            this.btnSaveDog.UseVisualStyleBackColor = true;
            this.btnSaveDog.Click += new System.EventHandler(this.btnSaveDog_Click);
            // 
            // numAge
            // 
            this.numAge.Location = new System.Drawing.Point(15, 150);
            this.numAge.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numAge.Name = "numAge";
            this.numAge.Size = new System.Drawing.Size(120, 20);
            this.numAge.TabIndex = 5;
            // 
            // lblAge
            // 
            this.lblAge.AutoSize = true;
            this.lblAge.Location = new System.Drawing.Point(15, 130);
            this.lblAge.Name = "lblAge";
            this.lblAge.Size = new System.Drawing.Size(29, 13);
            this.lblAge.TabIndex = 4;
            this.lblAge.Text = "Age:";
            // 
            // txtBreed
            // 
            this.txtBreed.Location = new System.Drawing.Point(15, 100);
            this.txtBreed.Name = "txtBreed";
            this.txtBreed.Size = new System.Drawing.Size(270, 20);
            this.txtBreed.TabIndex = 3;
            // 
            // lblBreed
            // 
            this.lblBreed.AutoSize = true;
            this.lblBreed.Location = new System.Drawing.Point(15, 80);
            this.lblBreed.Name = "lblBreed";
            this.lblBreed.Size = new System.Drawing.Size(38, 13);
            this.lblBreed.TabIndex = 2;
            this.lblBreed.Text = "Breed:";
            // 
            // txtDogName
            // 
            this.txtDogName.Location = new System.Drawing.Point(15, 50);
            this.txtDogName.Name = "txtDogName";
            this.txtDogName.Size = new System.Drawing.Size(270, 20);
            this.txtDogName.TabIndex = 1;
            // 
            // lblDogName
            // 
            this.lblDogName.AutoSize = true;
            this.lblDogName.Location = new System.Drawing.Point(15, 30);
            this.lblDogName.Name = "lblDogName";
            this.lblDogName.Size = new System.Drawing.Size(38, 13);
            this.lblDogName.TabIndex = 0;
            this.lblDogName.Text = "Name:";
            // 
            // btnEditDog
            // 
            this.btnEditDog.Location = new System.Drawing.Point(12, 360);
            this.btnEditDog.Name = "btnEditDog";
            this.btnEditDog.Size = new System.Drawing.Size(100, 30);
            this.btnEditDog.TabIndex = 2;
            this.btnEditDog.Text = "Edit";
            this.btnEditDog.UseVisualStyleBackColor = true;
            this.btnEditDog.Click += new System.EventHandler(this.btnEditDog_Click);
            // 
            // btnDeleteDog
            // 
            this.btnDeleteDog.Location = new System.Drawing.Point(120, 360);
            this.btnDeleteDog.Name = "btnDeleteDog";
            this.btnDeleteDog.Size = new System.Drawing.Size(100, 30);
            this.btnDeleteDog.TabIndex = 3;
            this.btnDeleteDog.Text = "Delete";
            this.btnDeleteDog.UseVisualStyleBackColor = true;
            this.btnDeleteDog.Click += new System.EventHandler(this.btnDeleteDog_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(362, 360);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(100, 30);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back to Main";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // btnViewWalks
            // 
            this.btnViewWalks.Location = new System.Drawing.Point(228, 361);
            this.btnViewWalks.Name = "btnViewWalks";
            this.btnViewWalks.Size = new System.Drawing.Size(100, 30);
            this.btnViewWalks.TabIndex = 6;
            this.btnViewWalks.Text = "View Walks";
            this.btnViewWalks.UseVisualStyleBackColor = true;
            this.btnViewWalks.Click += new System.EventHandler(this.btnViewWalks_Click);
            // 
            // DetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 410);
            this.Controls.Add(this.btnViewWalks);
            this.Controls.Add(this.grpDogEditor);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.btnDeleteDog);
            this.Controls.Add(this.btnEditDog);
            this.Controls.Add(this.dgvDogs);
            this.Controls.Add(this.lblClientName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DetailForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dog Walker - Dog Management";
            ((System.ComponentModel.ISupportInitialize)(this.dgvDogs)).EndInit();
            this.grpDogEditor.ResumeLayout(false);
            this.grpDogEditor.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Button btnViewWalks;
    }
}
