using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StehtimSchilf.TaskBarSorterXP {
   /// <summary>
   /// Saves and retrieves user settings
   /// </summary>
   /// <remarks>
   /// StehtimSchilf's TaskBarSorter XP.
   /// This code was initially posted on codeproject.com
   /// 
   /// 09.02.2001 v1.1.0 - new added
   /// </remarks>
   public partial class TaskBarSorterPreferences : Form {

      private Boolean _hasUnsavedChanges = false;

      public TaskBarSorterPreferences() {
         InitializeComponent();
         this.initGUI();
         this.settingsLoad();
      }

      private void initGUI() {

         // hide the "second column"
         // don't know another workaround yet
         this.lvApplicationOrder.Columns[0].Width = this.lvApplicationOrder.Width - 5;
         
         // raise the panel
         // works only if Panel.BorderStyle = Fixed3D
         IntPtr hWnd = this.panel1.Handle;
         int extStyle = Unmanaged.ApiGetWindowLong(hWnd, Unmanaged.GWL_EXSTYLE);
         Unmanaged.ApiSetWindowLong(hWnd, Unmanaged.GWL_EXSTYLE, (extStyle | Unmanaged.WS_EX_DLGMODALFRAME));
         Unmanaged.ApiSetWindowPos(hWnd, IntPtr.Zero, 0, 0, 0, 0, Unmanaged.SWP_ALL);
      }

      private void settingsLoad() {
         // get the sort order from user settings
         List<String> applicationSortOrder = TaskBarSorterHelpers.GetApplicationSortOrder();

         // fill the ListView
         foreach (String applicationBinaryName in applicationSortOrder) {
            ListViewItem lvItem = new ListViewItem(applicationBinaryName);
            this.lvApplicationOrder.Items.Add(lvItem);
         }
      }
      private void settingsSave() {
         List<String> applicationSortOrder = new List<string>();

         // fill ListViewItems into List<String>
         foreach (ListViewItem lvItem in this.lvApplicationOrder.Items) {
            applicationSortOrder.Add(lvItem.Text);
         }

         // save to user settings
         TaskBarSorterHelpers.SetApplicationSortOrder(applicationSortOrder);
         this._hasUnsavedChanges = false;
      }

      private void TaskBarSorterPreferences_FormClosing(object sender, FormClosingEventArgs e) {
         if (this._hasUnsavedChanges) {
            if (MessageBox.Show("You have unsaved changes.\r\n\r\n" +
                            "Are you sure to discard your changes?", "Quit()",
                            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
               // discard settings
            } else {
               // abort
               e.Cancel = true;
            }
         } else {
            // close
         }
      }

      /// <summary>
      /// I prefer on exit
      /// </summary>
      private void formClose() {
         this.Close();
      }

      #region Button Event Handlers

      /// <summary>
      /// saves settings and closes the form
      /// </summary>
      private void btnSave_Click(object sender, EventArgs e) {
         this.settingsSave();
         MessageBox.Show("Settings successfully saved!", "Preferences()", MessageBoxButtons.OK, MessageBoxIcon.Information);
         this.formClose();
      }

      /// <summary>
      /// bye
      /// </summary>
      private void btnCancel_Click(object sender, EventArgs e) {
         this.formClose();
      }

      /// <summary>
      /// add the text of the TextBox to to ListView
      /// </summary>
      private void btnAddApplicationBinaryName_Click(object sender, EventArgs e) {
         if (tbAddApplicationBinaryName.Text.Length == 0) {
            // missing application binary name
            MessageBox.Show("Please specify an application binary name!\r\n\r\n" + 
                            "Examples:\r\n" + 
                            "explorer.exe\r\n" + 
                            "explorer.exe;devenv.exe", "Preferences()", MessageBoxButtons.OK, MessageBoxIcon.Information);
         } else {

            if (this.tbAddApplicationBinaryName.Text.IndexOf(';') != -1) {
               // multiple application binary names specified
               foreach (String applicationBinaryName in this.tbAddApplicationBinaryName.Text.Split(new char[]{';'}, StringSplitOptions.RemoveEmptyEntries)) {
                  // add new application binary name
                  this.lvApplicationOrder.Items.Add(new ListViewItem(applicationBinaryName));
               }
            } else {
               // add new application binary name
               this.lvApplicationOrder.Items.Add(new ListViewItem(this.tbAddApplicationBinaryName.Text));
            }
            
            // select the previously added item
            // so it can be moved by the buttons afterwards
            this.lvApplicationOrder.Items[this.lvApplicationOrder.Items.Count - 1].Selected = true;
            this._hasUnsavedChanges = true;
         }
      }

      // move up selected ListViewItem
      private void btnMoveUp_Click(object sender, EventArgs e) {
         if (ListViewHelpers.MoveSelectedListViewItemUp(this.lvApplicationOrder)) {
            this._hasUnsavedChanges = true;
         }
      }

      // move down selected ListViewItem
      private void btnMoveDown_Click(object sender, EventArgs e) {
         if (ListViewHelpers.MoveSelectedListViewItemDown(this.lvApplicationOrder)) {
            this._hasUnsavedChanges = true;
         }
      }

      // remove selected ListViewItem
      private void btnRemove_Click(object sender, EventArgs e) {
         ListViewHelpers.RemoveSelectedListViewItems(this.lvApplicationOrder);
      }
      #endregion
   }
}
