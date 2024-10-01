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
   /// This project demonstrates following implementations:
   /// - use of P/Invokes
   /// - show/hide windows
   /// - set transparency of a window
   /// - minimize a window to System Tray
   /// - reorder windows on the taskbar
   /// - reorder list view items
   /// 
   /// To reorder the windows on the taskbar proceed as follows:
   /// 1. get all visible windows handles.
   ///    This is achieved by the Win32-API call EnumWindows(), IsWindowVisible()
   ///    See: class Unmanaged and WindowsList.
   ///    
   /// 2. hide all visible windows retrieved in step 1.
   ///    This is achieved by the Win32-API call ShowWindowAsync()
   ///    See: class Unmanaged 
   ///    
   /// 3. show all visible windows again in the prefered order.
   ///    This is achieved by the Win32-API call ShowWindowAsync()
   ///    See: class Unmanaged 
   ///    
   /// To reorder the windows two options are available:
   /// - reorder the windows handles in a list view
   ///   see this.btnSortByListView_Click()
   ///   
   /// - reorder the windows handles by an associated application name
   ///   see this.SortWindowsByPreference()
   ///   
   /// </summary>
   /// <remarks>
   /// StehtimSchilf's TaskBarSorter XP.
   /// This code was initially posted on codeproject.com
   /// 
   /// 05.02.2011 v1.0.0 - first release
   /// 09.02.2011 v1.1.0 - refreshListView() displayes additonal columns
   ///                   - btnSortByListView_Click() based on ListView.Tag
   ///                   - Redesign with use of ListViewHelpers
   /// 11.02.2011 v1.2.0 - new context menu item to set transparency of a window
   ///                   - new context menu item to minimize window to system tray
   ///                   - new to minimize TaskBarSorter to system tray
   ///                   - ListView displays window icon
   /// </remarks>
   public partial class TaskBarSorterGUI : Form {

      #region Constructor and Properties

      // collection of all visible windows
      private WindowsList windowsList = null;

      // collection of all windows minimized to sys tray
      private List<WindowItem> _minimizedWindowItems = new List<WindowItem>();

      // use this icon, if a window does not have a 
      private Icon _DefaultIcon = TaskBarSorterHelpers.GetIconFromFile("default.ico", null);

      public TaskBarSorterGUI() {
         InitializeComponent();

         this.initApplication();
      }
      #endregion

      private void initApplication() {
         this.initGUI();

         // refresh list view
         this.refreshListView();
      }

      private void initGUI() {
         // set the form caption including version number
         this.Text = TaskBarSorterHelpers.GetCaption(1);

         // hide the notification icon
         this.applicationNotifyIcon.Visible = false;
      }

      /// <summary>
      /// restore all minimized windows again
      /// </summary>
      private void TaskBarSorterGUI_FormClosing(object sender, FormClosingEventArgs e) {
         foreach (WindowItem wItem in this._minimizedWindowItems) {
            wItem.Show();
         }
      }

      /// <summary>
      /// restore the application if minimized to System Tray
      /// </summary>
      private void applicationNotifyIcon_MouseDoubleClick(object sender, MouseEventArgs e) {
         this.Show();
         this.WindowState = FormWindowState.Normal;
         this.applicationNotifyIcon.Visible = false;
      }

      /// <summary>
      /// brings selected Window Item to front
      /// </summary>
      private void lv_Windows_MouseDoubleClick(object sender, MouseEventArgs e) {
         if (this.lv_Windows.SelectedItems.Count > 0) {
            WindowItem wItem = this.GetSelectedWindowItem();
            wItem.BringToFront();
         }
      }


      #region Button Event Handlers

      /// <summary>
      /// updates list view with all windows which are currently visible
      /// </summary>
      private void btnRefresh_Click(object sender, EventArgs e) {
         this.refreshListView();
      }

      /// <summary>
      /// sorts the windows on the Windows Taskbar by preferences
      /// </summary>
      private void btnSortWindowsByPreference_Click(object sender, EventArgs e) {
         this.SortWindowsByPreference();
      }

      // reorder list view items.
      private void btnMoveUp_Click(object sender, EventArgs e) {
         ListViewHelpers.MoveSelectedListViewItemUp(this.lv_Windows);
      }

      // reorder list view items.
      private void btnMoveDown_Click(object sender, EventArgs e) {
         ListViewHelpers.MoveSelectedListViewItemDown(this.lv_Windows);
      }

      /// <summary>
      /// sorts the windows on the Windows Taskbar by ListView sort order
      /// </summary>
      private void btnSortByListView_Click(object sender, EventArgs e) {
         List<WindowItem> windowsOrdered = new List<WindowItem>();

         // iterate through list view items and get attached WindowItems
         foreach (ListViewItem lvItem in this.lv_Windows.Items) {
            WindowItem wItem = (WindowItem)lvItem.Tag;
            windowsOrdered.Add(wItem);
         }

         // reorder windows by a list of WindowItem
         WindowsList.SortWindowsByWindowItemList(windowsOrdered);
      }

      // show the Preferences Dialog
      private void btnPreferences_Click(object sender, EventArgs e) {
         new TaskBarSorterPreferences().ShowDialog(this);
      }

      private void btnMinimizeToSystemTray_Click(object sender, EventArgs e) {
         this.applicationNotifyIcon.Visible = true;
         this.Hide();
      }

      private void btnTest1_Click(object sender, EventArgs e) {

      }

      #endregion // buttons


      #region Context Menu Event Handlers

      private void lvContextMenu_Opening(object sender, CancelEventArgs e) {
         Boolean enabled = (this.lv_Windows.SelectedItems.Count > 0);

         this.hideWindowToolStripMenuItem.Enabled = enabled;
         this.showWindowToolStripMenuItem.Enabled = enabled;
         this.bringWindowToFrontToolStripMenuItem.Enabled = enabled;
         this.moveUpToolStripMenuItem.Enabled = enabled;
         this.moveDownToolStripMenuItem.Enabled = enabled;
         this.minimizeToSystemToolStripMenuItem.Enabled = enabled;
      }

      // reorder list view items.
      private void moveUpToolStripMenuItem_Click(object sender, EventArgs e) {
         ListViewHelpers.MoveSelectedListViewItemUp(this.lv_Windows);
      }

      // reorder list view items.
      private void moveDownToolStripMenuItem_Click(object sender, EventArgs e) {
         ListViewHelpers.MoveSelectedListViewItemDown(this.lv_Windows);
      }

      // hides selected window
      private void hideWindowToolStripMenuItem_Click(object sender, EventArgs e) {
         IntPtr hwnd = GetSelectedWindowItem().WindowHandle;
         Unmanaged.ApiShowWindowAsync(hwnd, Unmanaged.SW_HIDE);
      }

      // shows selected window
      private void showWindowToolStripMenuItem_Click(object sender, EventArgs e) {
         IntPtr hwnd = GetSelectedWindowItem().WindowHandle;
         Unmanaged.ApiShowWindowAsync(hwnd, Unmanaged.SW_SHOWNORMAL);
      }

      // activates selected window
      private void bringWindowToFrontToolStripMenuItem_Click(object sender, EventArgs e) {
         IntPtr hwnd = GetSelectedWindowItem().WindowHandle;
         Unmanaged.ApiSetForegroundWindow(hwnd);
         Unmanaged.ApiShowWindowAsync(hwnd, Unmanaged.SW_RESTORE);
         System.Threading.Thread.Sleep(100);
      }

      private void stayOnTopToolStripMenuItem_Click(object sender, EventArgs e) {
         WindowItem wItem = (WindowItem)this.GetSelectedWindowItem();
         wItem.StayOnTop();
      }

      private void removeStayOnTopToolStripMenuItem_Click(object sender, EventArgs e) {
         WindowItem wItem = (WindowItem)this.GetSelectedWindowItem();
         wItem.RemoveStayOnTop();
      }

      // Transparency
      private void SetTS0toolStripMenuItem_Click(object sender, EventArgs e) {
         WindowItem wItem = (WindowItem)this.GetSelectedWindowItem();
         wItem.SetTransparency(0F);
      }
      private void SetTS25toolStripMenuItem_Click(object sender, EventArgs e) {
         WindowItem wItem = (WindowItem)this.GetSelectedWindowItem();
         wItem.SetTransparency(25F);
      }
      private void SetTS50toolStripMenuItem_Click(object sender, EventArgs e) {
         WindowItem wItem = (WindowItem)this.GetSelectedWindowItem();
         wItem.SetTransparency(50F);
      }
      private void SetTS75toolStripMenuItem_Click(object sender, EventArgs e) {
         WindowItem wItem = (WindowItem)this.GetSelectedWindowItem();
         wItem.SetTransparency(75F);
      }
      private void SetTS100ToolStripMenuItem_Click(object sender, EventArgs e) {
         WindowItem wItem = (WindowItem)this.GetSelectedWindowItem();
         wItem.SetTransparency(100F);
      }

      // minimize to System Tray
      private void minimizeToSystemToolStripMenuItem_Click(object sender, EventArgs e) {
         WindowItem wItem = GetSelectedWindowItem();
         this.newNotificationIcon(wItem);
      }


      #endregion  // Context Menu Event Handlers


      #region Private helpers

      // retrieves all windows and updates the ListView
      private void refreshListView() {

         ImageList windowIcons = new ImageList();

         // clear list view
         lv_Windows.Items.Clear();

         // get only relevant windows
         this.windowsList = new WindowsList(true);

         // add results to list view
         int imageIndex = -1;
         foreach (WindowItem wi in this.windowsList.Windows) {
            
            // image
            imageIndex++;

            Icon appIcon = TaskBarSorterHelpers.GetIconFromFile(wi.ModulePath, this._DefaultIcon);
            //List<System.Drawing.Icon> largeIcons = new List<Icon>();
            //List<System.Drawing.Icon> smallIcons = new List<Icon>();
            //Icon appIcon = TaskBarSorterHelpers.GetIconsFromFile(wi.ModulePath, ref largeIcons, ref smallIcons, this._DefaultIcon);

            windowIcons.Images.Add(appIcon);
            this.lv_Windows.SmallImageList = windowIcons;

            ListViewItem lvItem = new ListViewItem(wi.WindowTitle, imageIndex);

            // map WindowItem to a ListViewItem
            lvItem.SubItems.AddRange(new String[] {
                  wi.WindowHandle.ToString(), 
                  wi.ProcessId.ToString(),
                  wi.ModulePath,
                  wi.ModuleFileName,
                  wi.WindowPlacement.showCmd.ToString(),                         // new v111
                  Unmanaged.POINTToString(wi.WindowPlacement.ptMinPosition),     // new v111
                  Unmanaged.POINTToString(wi.WindowPlacement.ptMaxPosition),     // new v111
                  Unmanaged.RECTToString(wi.WindowPlacement.rcNormalPosition),   // new v111
            });
            lvItem.Tag = wi;
            this.lv_Windows.Items.Add(lvItem);
         }

         // resize list view columns
         this.lv_Windows.AutoResizeColumn(0, ColumnHeaderAutoResizeStyle.ColumnContent);
         this.lv_Windows.AutoResizeColumn(1, ColumnHeaderAutoResizeStyle.HeaderSize);
         this.lv_Windows.AutoResizeColumn(2, ColumnHeaderAutoResizeStyle.HeaderSize);
         this.lv_Windows.AutoResizeColumn(3, ColumnHeaderAutoResizeStyle.ColumnContent);
         this.lv_Windows.AutoResizeColumn(4, ColumnHeaderAutoResizeStyle.ColumnContent);
      }

      private WindowItem GetSelectedWindowItem() {
         WindowItem wItem = null;
         if (this.lv_Windows.SelectedItems.Count > 0) {
            ListViewItem lvItem = this.lv_Windows.SelectedItems[0];
            wItem = (WindowItem)lvItem.Tag;
         } else {
            // should/can not happen by design
         }
         return wItem;
      }


      /// <summary>
      /// sorts the windows by preferences.
      /// 1. add the WindowItems to the list which match the preferences
      /// 2. add remaining WindowItems to the list
      /// 3. reorder 
      /// </summary>
      private void SortWindowsByPreference() {
         // must refresh windows list
         this.refreshListView();

         // retrieve application name sort order from config file
         List<String> applicationNameOrder = TaskBarSorterHelpers.GetApplicationSortOrder();

         // contains sorted application hwnds
         // preceding the hwnds of the applications above
         // List<IntPtr> applicationHwndOrder = new List<IntPtr>();
         List<WindowItem> preferedWindowOrder = new List<WindowItem>();

         // first add WindowItem of the defnied applications (quick n dirty)
         foreach (String applicationName in applicationNameOrder) {
            foreach (ListViewItem lvItem in this.lv_Windows.Items) {
               String moduleFileName = lvItem.SubItems[4].Text;

               if (applicationName.Equals(moduleFileName, StringComparison.CurrentCultureIgnoreCase)) {
                  // module file name matches one of the specified application names
                  // IntPtr hwnd = new IntPtr(Int32.Parse(lvItem.SubItems[1].Text));
                  // applicationHwndOrder.Add(hwnd);
                  WindowItem wItem = (WindowItem)lvItem.Tag;
                  preferedWindowOrder.Add(wItem);
               }
            }
         }

         // add WindowItems which haven't been added yet.
         foreach (WindowItem wi in this.windowsList.Windows) {
            if (!preferedWindowOrder.Contains(wi) && wi.IsVisible && !wi.ProcessClass.Equals("Progman", StringComparison.CurrentCultureIgnoreCase)) {
               preferedWindowOrder.Add(wi);
            }
         }

         // reorder the windows by a list of WindowItems
         WindowsList.SortWindowsByWindowItemList(preferedWindowOrder);
      }

      /// <summary>
      /// Minimizes the window to the system tray and
      /// adds a reference to a list so it can be restored on application exit
      /// </summary>
      /// <param name="wItem">WindowItem to minimize to System Tray</param>
      private void newNotificationIcon(WindowItem wItem) {

         // create a notification icon
         NotifyIcon icon = new NotifyIcon();
         // get the icon from associated executable
         icon.Icon = TaskBarSorterHelpers.GetIconFromFile(wItem.ModulePath, this._DefaultIcon);
         // get window title for tool tip.
         icon.Text = StringHelpers.Left(wItem.WindowTitle, 50);
         icon.Visible = true;
         
         // add WindowItem data to NotifyIcon (see showMinimizedWindow())
         icon.Tag = wItem;
         
         // all NotifyIcons use the same event handler
         icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.showMinimizedWindow);

         // add to minimized WindowItems collection
         // see Form_Closing()
         this._minimizedWindowItems.Add(wItem);

         // hide window
         wItem.Hide();
      }

      /// <summary>
      /// Double click event handler for our minimized NotifyIcons
      /// </summary>
      private void showMinimizedWindow(object sender, MouseEventArgs e) {
         NotifyIcon icon = (NotifyIcon)sender;
         WindowItem wItem = (WindowItem)icon.Tag;

         // show the minimized window again
         wItem.Show();

         // remove icon from taskbar
         icon.Dispose();

         // remove from minimized WindowItems collection
         this._minimizedWindowItems.Remove(wItem);
      }
      #endregion
   }
}
