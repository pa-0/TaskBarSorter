namespace StehtimSchilf.TaskBarSorterXP {
   partial class TaskBarSorterGUI {
      /// <summary>
      /// Erforderliche Designervariable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Verwendete Ressourcen bereinigen.
      /// </summary>
      /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Vom Windows Form-Designer generierter Code

      /// <summary>
      /// Erforderliche Methode für die Designerunterstützung.
      /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
      /// </summary>
      private void InitializeComponent() {
         this.components = new System.ComponentModel.Container();
         System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
         System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
         System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
         System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
         System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TaskBarSorterGUI));
         this.btn_Refresh = new System.Windows.Forms.Button();
         this.lv_Windows = new System.Windows.Forms.ListView();
         this.chWindowTitle = new System.Windows.Forms.ColumnHeader();
         this.chWindowHandle = new System.Windows.Forms.ColumnHeader();
         this.chProcessId = new System.Windows.Forms.ColumnHeader();
         this.chModulePath = new System.Windows.Forms.ColumnHeader();
         this.chModuleFileName = new System.Windows.Forms.ColumnHeader();
         this.chWindowState = new System.Windows.Forms.ColumnHeader();
         this.chMinPosition = new System.Windows.Forms.ColumnHeader();
         this.chMaxPosition = new System.Windows.Forms.ColumnHeader();
         this.chRect = new System.Windows.Forms.ColumnHeader();
         this.chIcon = new System.Windows.Forms.ColumnHeader();
         this.lvContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
         this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.hideWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.showWindowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.bringWindowToFrontToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.stayOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.removeStayOnTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.setTransparencyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.SetTS0toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.SetTS25toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.SetTS50toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.SetTS75toolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.SetTS100ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.minimizeToSystemToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
         this.btnSortWindowsByPreference = new System.Windows.Forms.Button();
         this.btnSortByListView = new System.Windows.Forms.Button();
         this.btnMoveUp = new System.Windows.Forms.Button();
         this.btnMoveDown = new System.Windows.Forms.Button();
         this.btnPreferences = new System.Windows.Forms.Button();
         this.btnMinimizeToSystemTray = new System.Windows.Forms.Button();
         this.applicationNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
         this.btnTest1 = new System.Windows.Forms.Button();
         toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
         toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
         toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
         toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
         this.lvContextMenu.SuspendLayout();
         this.SuspendLayout();
         // 
         // btn_Refresh
         // 
         this.btn_Refresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btn_Refresh.Location = new System.Drawing.Point(966, 339);
         this.btn_Refresh.Name = "btn_Refresh";
         this.btn_Refresh.Size = new System.Drawing.Size(75, 23);
         this.btn_Refresh.TabIndex = 1;
         this.btn_Refresh.Text = "Refresh";
         this.btn_Refresh.UseVisualStyleBackColor = true;
         this.btn_Refresh.Click += new System.EventHandler(this.btnRefresh_Click);
         // 
         // lv_Windows
         // 
         this.lv_Windows.AllowColumnReorder = true;
         this.lv_Windows.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.lv_Windows.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chWindowTitle,
            this.chWindowHandle,
            this.chProcessId,
            this.chModulePath,
            this.chModuleFileName,
            this.chWindowState,
            this.chMinPosition,
            this.chMaxPosition,
            this.chRect,
            this.chIcon});
         this.lv_Windows.ContextMenuStrip = this.lvContextMenu;
         this.lv_Windows.FullRowSelect = true;
         this.lv_Windows.HideSelection = false;
         this.lv_Windows.Location = new System.Drawing.Point(13, 13);
         this.lv_Windows.MultiSelect = false;
         this.lv_Windows.Name = "lv_Windows";
         this.lv_Windows.Size = new System.Drawing.Size(1028, 320);
         this.lv_Windows.TabIndex = 2;
         this.lv_Windows.UseCompatibleStateImageBehavior = false;
         this.lv_Windows.View = System.Windows.Forms.View.Details;
         this.lv_Windows.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lv_Windows_MouseDoubleClick);
         // 
         // chWindowTitle
         // 
         this.chWindowTitle.Text = "Window Title";
         this.chWindowTitle.Width = 97;
         // 
         // chWindowHandle
         // 
         this.chWindowHandle.Text = "Window Handle";
         this.chWindowHandle.Width = 94;
         // 
         // chProcessId
         // 
         this.chProcessId.Text = "Process Id";
         this.chProcessId.Width = 94;
         // 
         // chModulePath
         // 
         this.chModulePath.Text = "Path";
         // 
         // chModuleFileName
         // 
         this.chModuleFileName.Text = "Filename";
         this.chModuleFileName.Width = 82;
         // 
         // chWindowState
         // 
         this.chWindowState.Text = "WindowState";
         // 
         // chMinPosition
         // 
         this.chMinPosition.Text = "Min. Position";
         // 
         // chMaxPosition
         // 
         this.chMaxPosition.Text = "Max. Position";
         // 
         // chRect
         // 
         this.chRect.Text = "Window Size";
         // 
         // lvContextMenu
         // 
         this.lvContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem,
            toolStripSeparator1,
            this.hideWindowToolStripMenuItem,
            this.showWindowToolStripMenuItem,
            this.bringWindowToFrontToolStripMenuItem,
            toolStripSeparator2,
            this.stayOnTopToolStripMenuItem,
            this.removeStayOnTopToolStripMenuItem,
            toolStripSeparator3,
            this.setTransparencyToolStripMenuItem,
            toolStripSeparator4,
            this.minimizeToSystemToolStripMenuItem});
         this.lvContextMenu.Name = "lvContextMenu";
         this.lvContextMenu.Size = new System.Drawing.Size(203, 248);
         this.lvContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.lvContextMenu_Opening);
         // 
         // moveUpToolStripMenuItem
         // 
         this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
         this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
         this.moveUpToolStripMenuItem.Text = "Move Up";
         this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
         // 
         // moveDownToolStripMenuItem
         // 
         this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
         this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
         this.moveDownToolStripMenuItem.Text = "Move Down";
         this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
         // 
         // toolStripSeparator1
         // 
         toolStripSeparator1.Name = "toolStripSeparator1";
         toolStripSeparator1.Size = new System.Drawing.Size(199, 6);
         // 
         // hideWindowToolStripMenuItem
         // 
         this.hideWindowToolStripMenuItem.Name = "hideWindowToolStripMenuItem";
         this.hideWindowToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
         this.hideWindowToolStripMenuItem.Text = "Hide Window";
         this.hideWindowToolStripMenuItem.Click += new System.EventHandler(this.hideWindowToolStripMenuItem_Click);
         // 
         // showWindowToolStripMenuItem
         // 
         this.showWindowToolStripMenuItem.Name = "showWindowToolStripMenuItem";
         this.showWindowToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
         this.showWindowToolStripMenuItem.Text = "Show Window";
         this.showWindowToolStripMenuItem.Click += new System.EventHandler(this.showWindowToolStripMenuItem_Click);
         // 
         // bringWindowToFrontToolStripMenuItem
         // 
         this.bringWindowToFrontToolStripMenuItem.Name = "bringWindowToFrontToolStripMenuItem";
         this.bringWindowToFrontToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
         this.bringWindowToFrontToolStripMenuItem.Text = "Bring Window to front";
         this.bringWindowToFrontToolStripMenuItem.Click += new System.EventHandler(this.bringWindowToFrontToolStripMenuItem_Click);
         // 
         // toolStripSeparator2
         // 
         toolStripSeparator2.Name = "toolStripSeparator2";
         toolStripSeparator2.Size = new System.Drawing.Size(199, 6);
         // 
         // stayOnTopToolStripMenuItem
         // 
         this.stayOnTopToolStripMenuItem.Name = "stayOnTopToolStripMenuItem";
         this.stayOnTopToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
         this.stayOnTopToolStripMenuItem.Text = "Stay On Top";
         this.stayOnTopToolStripMenuItem.Click += new System.EventHandler(this.stayOnTopToolStripMenuItem_Click);
         // 
         // removeStayOnTopToolStripMenuItem
         // 
         this.removeStayOnTopToolStripMenuItem.Name = "removeStayOnTopToolStripMenuItem";
         this.removeStayOnTopToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
         this.removeStayOnTopToolStripMenuItem.Text = "Remove Stay On Top";
         this.removeStayOnTopToolStripMenuItem.Click += new System.EventHandler(this.removeStayOnTopToolStripMenuItem_Click);
         // 
         // toolStripSeparator3
         // 
         toolStripSeparator3.Name = "toolStripSeparator3";
         toolStripSeparator3.Size = new System.Drawing.Size(199, 6);
         // 
         // setTransparencyToolStripMenuItem
         // 
         this.setTransparencyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SetTS0toolStripMenuItem,
            this.SetTS25toolStripMenuItem,
            this.SetTS50toolStripMenuItem,
            this.SetTS75toolStripMenuItem,
            this.SetTS100ToolStripMenuItem});
         this.setTransparencyToolStripMenuItem.Name = "setTransparencyToolStripMenuItem";
         this.setTransparencyToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
         this.setTransparencyToolStripMenuItem.Text = "Set Transparency";
         // 
         // SetTS0toolStripMenuItem
         // 
         this.SetTS0toolStripMenuItem.Name = "SetTS0toolStripMenuItem";
         this.SetTS0toolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.SetTS0toolStripMenuItem.Text = "0% (opaque)";
         this.SetTS0toolStripMenuItem.Click += new System.EventHandler(this.SetTS0toolStripMenuItem_Click);
         // 
         // SetTS25toolStripMenuItem
         // 
         this.SetTS25toolStripMenuItem.Name = "SetTS25toolStripMenuItem";
         this.SetTS25toolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.SetTS25toolStripMenuItem.Text = "25%";
         this.SetTS25toolStripMenuItem.Click += new System.EventHandler(this.SetTS25toolStripMenuItem_Click);
         // 
         // SetTS50toolStripMenuItem
         // 
         this.SetTS50toolStripMenuItem.Name = "SetTS50toolStripMenuItem";
         this.SetTS50toolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.SetTS50toolStripMenuItem.Text = "50%";
         this.SetTS50toolStripMenuItem.Click += new System.EventHandler(this.SetTS50toolStripMenuItem_Click);
         // 
         // SetTS75toolStripMenuItem
         // 
         this.SetTS75toolStripMenuItem.Name = "SetTS75toolStripMenuItem";
         this.SetTS75toolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.SetTS75toolStripMenuItem.Text = "75%";
         this.SetTS75toolStripMenuItem.Click += new System.EventHandler(this.SetTS75toolStripMenuItem_Click);
         // 
         // SetTS100ToolStripMenuItem
         // 
         this.SetTS100ToolStripMenuItem.Name = "SetTS100ToolStripMenuItem";
         this.SetTS100ToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
         this.SetTS100ToolStripMenuItem.Text = "100% (sure?)";
         this.SetTS100ToolStripMenuItem.Click += new System.EventHandler(this.SetTS100ToolStripMenuItem_Click);
         // 
         // toolStripSeparator4
         // 
         toolStripSeparator4.Name = "toolStripSeparator4";
         toolStripSeparator4.Size = new System.Drawing.Size(199, 6);
         // 
         // minimizeToSystemToolStripMenuItem
         // 
         this.minimizeToSystemToolStripMenuItem.Name = "minimizeToSystemToolStripMenuItem";
         this.minimizeToSystemToolStripMenuItem.Size = new System.Drawing.Size(202, 22);
         this.minimizeToSystemToolStripMenuItem.Text = "Minimize To System Tray";
         this.minimizeToSystemToolStripMenuItem.Click += new System.EventHandler(this.minimizeToSystemToolStripMenuItem_Click);
         // 
         // btnSortWindowsByPreference
         // 
         this.btnSortWindowsByPreference.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnSortWindowsByPreference.Location = new System.Drawing.Point(885, 339);
         this.btnSortWindowsByPreference.Name = "btnSortWindowsByPreference";
         this.btnSortWindowsByPreference.Size = new System.Drawing.Size(75, 23);
         this.btnSortWindowsByPreference.TabIndex = 3;
         this.btnSortWindowsByPreference.Text = "sort by pref";
         this.btnSortWindowsByPreference.UseVisualStyleBackColor = true;
         this.btnSortWindowsByPreference.Click += new System.EventHandler(this.btnSortWindowsByPreference_Click);
         // 
         // btnSortByListView
         // 
         this.btnSortByListView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
         this.btnSortByListView.Location = new System.Drawing.Point(804, 339);
         this.btnSortByListView.Name = "btnSortByListView";
         this.btnSortByListView.Size = new System.Drawing.Size(75, 23);
         this.btnSortByListView.TabIndex = 4;
         this.btnSortByListView.Text = "sort by list";
         this.btnSortByListView.UseVisualStyleBackColor = true;
         this.btnSortByListView.Click += new System.EventHandler(this.btnSortByListView_Click);
         // 
         // btnMoveUp
         // 
         this.btnMoveUp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnMoveUp.Location = new System.Drawing.Point(13, 340);
         this.btnMoveUp.Name = "btnMoveUp";
         this.btnMoveUp.Size = new System.Drawing.Size(75, 23);
         this.btnMoveUp.TabIndex = 5;
         this.btnMoveUp.Text = "Move Up";
         this.btnMoveUp.UseVisualStyleBackColor = true;
         this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
         // 
         // btnMoveDown
         // 
         this.btnMoveDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnMoveDown.Location = new System.Drawing.Point(95, 339);
         this.btnMoveDown.Name = "btnMoveDown";
         this.btnMoveDown.Size = new System.Drawing.Size(75, 23);
         this.btnMoveDown.TabIndex = 6;
         this.btnMoveDown.Text = "Move Down";
         this.btnMoveDown.UseVisualStyleBackColor = true;
         this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
         // 
         // btnPreferences
         // 
         this.btnPreferences.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnPreferences.Location = new System.Drawing.Point(409, 340);
         this.btnPreferences.Name = "btnPreferences";
         this.btnPreferences.Size = new System.Drawing.Size(75, 23);
         this.btnPreferences.TabIndex = 7;
         this.btnPreferences.Text = "Prefs ...";
         this.btnPreferences.UseVisualStyleBackColor = true;
         this.btnPreferences.Click += new System.EventHandler(this.btnPreferences_Click);
         // 
         // btnMinimizeToSystemTray
         // 
         this.btnMinimizeToSystemTray.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnMinimizeToSystemTray.Location = new System.Drawing.Point(490, 340);
         this.btnMinimizeToSystemTray.Name = "btnMinimizeToSystemTray";
         this.btnMinimizeToSystemTray.Size = new System.Drawing.Size(75, 23);
         this.btnMinimizeToSystemTray.TabIndex = 8;
         this.btnMinimizeToSystemTray.Text = "To Sys Tray";
         this.btnMinimizeToSystemTray.UseVisualStyleBackColor = true;
         this.btnMinimizeToSystemTray.Click += new System.EventHandler(this.btnMinimizeToSystemTray_Click);
         // 
         // applicationNotifyIcon
         // 
         this.applicationNotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("applicationNotifyIcon.Icon")));
         this.applicationNotifyIcon.Text = "TaskBarSorter";
         this.applicationNotifyIcon.Visible = true;
         this.applicationNotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.applicationNotifyIcon_MouseDoubleClick);
         // 
         // btnTest1
         // 
         this.btnTest1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
         this.btnTest1.Location = new System.Drawing.Point(571, 340);
         this.btnTest1.Name = "btnTest1";
         this.btnTest1.Size = new System.Drawing.Size(75, 23);
         this.btnTest1.TabIndex = 9;
         this.btnTest1.Text = "test1";
         this.btnTest1.UseVisualStyleBackColor = true;
         this.btnTest1.Visible = false;
         this.btnTest1.Click += new System.EventHandler(this.btnTest1_Click);
         // 
         // TaskBarSorterGUI
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(1053, 374);
         this.Controls.Add(this.btnTest1);
         this.Controls.Add(this.btnMinimizeToSystemTray);
         this.Controls.Add(this.btnPreferences);
         this.Controls.Add(this.btnMoveDown);
         this.Controls.Add(this.btnMoveUp);
         this.Controls.Add(this.btnSortWindowsByPreference);
         this.Controls.Add(this.btnSortByListView);
         this.Controls.Add(this.lv_Windows);
         this.Controls.Add(this.btn_Refresh);
         this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
         this.Name = "TaskBarSorterGUI";
         this.Text = "TaskBarSorterXP";
         this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TaskBarSorterGUI_FormClosing);
         this.lvContextMenu.ResumeLayout(false);
         this.ResumeLayout(false);

      }

      #endregion

      private System.Windows.Forms.Button btn_Refresh;
      private System.Windows.Forms.ListView lv_Windows;
      private System.Windows.Forms.ColumnHeader chWindowTitle;
      private System.Windows.Forms.ColumnHeader chWindowHandle;
      private System.Windows.Forms.ColumnHeader chProcessId;
      private System.Windows.Forms.ColumnHeader chModulePath;
      private System.Windows.Forms.ColumnHeader chModuleFileName;
      private System.Windows.Forms.Button btnSortWindowsByPreference;
      private System.Windows.Forms.ContextMenuStrip lvContextMenu;
      private System.Windows.Forms.ToolStripMenuItem hideWindowToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem showWindowToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem bringWindowToFrontToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
      private System.Windows.Forms.Button btnSortByListView;
      private System.Windows.Forms.ColumnHeader chWindowState;
      private System.Windows.Forms.ColumnHeader chMinPosition;
      private System.Windows.Forms.ColumnHeader chMaxPosition;
      private System.Windows.Forms.ColumnHeader chRect;
      private System.Windows.Forms.Button btnMoveUp;
      private System.Windows.Forms.Button btnMoveDown;
      private System.Windows.Forms.Button btnPreferences;
      private System.Windows.Forms.Button btnMinimizeToSystemTray;
      private System.Windows.Forms.NotifyIcon applicationNotifyIcon;
      private System.Windows.Forms.Button btnTest1;
      private System.Windows.Forms.ColumnHeader chIcon;
      private System.Windows.Forms.ToolStripMenuItem minimizeToSystemToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem stayOnTopToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem removeStayOnTopToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem setTransparencyToolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem SetTS0toolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem SetTS25toolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem SetTS50toolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem SetTS75toolStripMenuItem;
      private System.Windows.Forms.ToolStripMenuItem SetTS100ToolStripMenuItem;
   }
}

