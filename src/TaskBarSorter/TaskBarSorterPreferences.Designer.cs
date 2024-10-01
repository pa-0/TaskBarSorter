namespace StehtimSchilf.TaskBarSorterXP {
   partial class TaskBarSorterPreferences {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskBarSorterPreferences));
         this.panel1 = new System.Windows.Forms.Panel();
         this.lvApplicationOrder = new System.Windows.Forms.ListView();
         this.ApplicationBinary = new System.Windows.Forms.ColumnHeader();
         this.btnCancel = new System.Windows.Forms.Button();
         this.btnSave = new System.Windows.Forms.Button();
         this.btnMoveUp = new System.Windows.Forms.Button();
         this.btnMoveDown = new System.Windows.Forms.Button();
         this.btnAddApplicationBinaryName = new System.Windows.Forms.Button();
         this.tbAddApplicationBinaryName = new System.Windows.Forms.TextBox();
         this.btnRemove = new System.Windows.Forms.Button();
         this.panel1.SuspendLayout();
         this.SuspendLayout();
         // 
         // panel1
         // 
         this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
         this.panel1.Controls.Add(this.lvApplicationOrder);
         this.panel1.Location = new System.Drawing.Point(12, 12);
         this.panel1.Name = "panel1";
         this.panel1.Size = new System.Drawing.Size(425, 222);
         this.panel1.TabIndex = 0;
         // 
         // lvApplicationOrder
         // 
         this.lvApplicationOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ApplicationBinary});
         this.lvApplicationOrder.FullRowSelect = true;
         this.lvApplicationOrder.HideSelection = false;
         this.lvApplicationOrder.Location = new System.Drawing.Point(4, 4);
         this.lvApplicationOrder.MultiSelect = false;
         this.lvApplicationOrder.Name = "lvApplicationOrder";
         this.lvApplicationOrder.Size = new System.Drawing.Size(414, 211);
         this.lvApplicationOrder.TabIndex = 0;
         this.lvApplicationOrder.UseCompatibleStateImageBehavior = false;
         this.lvApplicationOrder.View = System.Windows.Forms.View.Details;
         // 
         // ApplicationBinary
         // 
         this.ApplicationBinary.Text = "Application Binary Name";
         this.ApplicationBinary.Width = 397;
         // 
         // btnCancel
         // 
         this.btnCancel.Location = new System.Drawing.Point(362, 270);
         this.btnCancel.Name = "btnCancel";
         this.btnCancel.Size = new System.Drawing.Size(75, 23);
         this.btnCancel.TabIndex = 1;
         this.btnCancel.Text = "Cancel";
         this.btnCancel.UseVisualStyleBackColor = true;
         this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
         // 
         // btnSave
         // 
         this.btnSave.Location = new System.Drawing.Point(281, 270);
         this.btnSave.Name = "btnSave";
         this.btnSave.Size = new System.Drawing.Size(75, 23);
         this.btnSave.TabIndex = 2;
         this.btnSave.Text = "Save";
         this.btnSave.UseVisualStyleBackColor = true;
         this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
         // 
         // btnMoveUp
         // 
         this.btnMoveUp.Location = new System.Drawing.Point(12, 270);
         this.btnMoveUp.Name = "btnMoveUp";
         this.btnMoveUp.Size = new System.Drawing.Size(75, 23);
         this.btnMoveUp.TabIndex = 3;
         this.btnMoveUp.Text = "Move Up";
         this.btnMoveUp.UseVisualStyleBackColor = true;
         this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
         // 
         // btnMoveDown
         // 
         this.btnMoveDown.Location = new System.Drawing.Point(93, 270);
         this.btnMoveDown.Name = "btnMoveDown";
         this.btnMoveDown.Size = new System.Drawing.Size(75, 23);
         this.btnMoveDown.TabIndex = 4;
         this.btnMoveDown.Text = "Move Down";
         this.btnMoveDown.UseVisualStyleBackColor = true;
         this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
         // 
         // btnAddApplicationBinaryName
         // 
         this.btnAddApplicationBinaryName.Location = new System.Drawing.Point(281, 240);
         this.btnAddApplicationBinaryName.Name = "btnAddApplicationBinaryName";
         this.btnAddApplicationBinaryName.Size = new System.Drawing.Size(75, 23);
         this.btnAddApplicationBinaryName.TabIndex = 5;
         this.btnAddApplicationBinaryName.Text = "Add";
         this.btnAddApplicationBinaryName.UseVisualStyleBackColor = true;
         this.btnAddApplicationBinaryName.Click += new System.EventHandler(this.btnAddApplicationBinaryName_Click);
         // 
         // tbAddApplicationBinaryName
         // 
         this.tbAddApplicationBinaryName.Location = new System.Drawing.Point(12, 242);
         this.tbAddApplicationBinaryName.Name = "tbAddApplicationBinaryName";
         this.tbAddApplicationBinaryName.Size = new System.Drawing.Size(263, 20);
         this.tbAddApplicationBinaryName.TabIndex = 6;
         // 
         // btnRemove
         // 
         this.btnRemove.Location = new System.Drawing.Point(174, 270);
         this.btnRemove.Name = "btnRemove";
         this.btnRemove.Size = new System.Drawing.Size(75, 23);
         this.btnRemove.TabIndex = 7;
         this.btnRemove.Text = "Remove";
         this.btnRemove.UseVisualStyleBackColor = true;
         this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
         // 
         // TaskBarSorterPreferences
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(449, 305);
         this.Controls.Add(this.btnRemove);
         this.Controls.Add(this.tbAddApplicationBinaryName);
         this.Controls.Add(this.btnAddApplicationBinaryName);
         this.Controls.Add(this.btnMoveDown);
         this.Controls.Add(this.btnMoveUp);
         this.Controls.Add(this.btnSave);
         this.Controls.Add(this.btnCancel);
         this.Controls.Add(this.panel1);
         this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.MaximizeBox = false;
         this.MinimizeBox = false;
         this.Name = "TaskBarSorterPreferences";
         this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
         this.Text = "Preferences";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskBarSorterPreferences_FormClosing);
         this.panel1.ResumeLayout(false);
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Panel panel1;
      private System.Windows.Forms.Button btnCancel;
      private System.Windows.Forms.Button btnSave;
      private System.Windows.Forms.ListView lvApplicationOrder;
      private System.Windows.Forms.ColumnHeader ApplicationBinary;
      private System.Windows.Forms.Button btnMoveUp;
      private System.Windows.Forms.Button btnMoveDown;
      private System.Windows.Forms.Button btnAddApplicationBinaryName;
      private System.Windows.Forms.TextBox tbAddApplicationBinaryName;
      private System.Windows.Forms.Button btnRemove;
   }
}