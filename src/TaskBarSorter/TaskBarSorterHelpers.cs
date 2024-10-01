using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Management;
using System.Text;
using System.Windows.Forms;

namespace StehtimSchilf.TaskBarSorterXP {
   /// <summary>
   /// some helper functions.
   /// </summary>
   /// <remarks>
   /// StehtimSchilf's TaskBarSorter XP.
   /// This code was initially posted on codeproject.com
   /// 
   /// 09.02.2011 v1.1.0 - complete redesign
   /// 11.02.2011 v1.2.0 - Icon related helpers added
   /// </remarks>
   static class TaskBarSorterHelpers {

      #region GUI related
      private const String PRODUCT_NAME = "TaskBarSorterXP";
      private const String PRODUCT_VERSION = "v1.2.0";

      /// <summary>
      /// Used to set forms or dialog captions
      /// </summary>
      /// <param name="mode">
      ///    1 : returns product name + product version
      ///    else: returns product name only
      /// </param>
      /// <returns></returns>
      internal static String GetCaption(int mode) {
         String caption = "";
         switch (mode) {
            case 1:
               caption = PRODUCT_NAME + " " + PRODUCT_VERSION;
               break;
            default:
               caption = PRODUCT_NAME;
               break;
         }
         return caption;
      }
      #endregion


      #region Settings related

      /// <summary>
      /// retrieves application sort order from user settings
      /// </summary>
      /// <returns>
      /// 
      /// </returns>
      internal static List<String> GetApplicationSortOrder() {
         List<String> applicationSortOrder = new List<String>();

         try {
            //String applicationSortOrderValue = System.Configuration.ConfigurationManager.AppSettings.Get("ApplicationSortOrder");
            String applicationSortOrderValue = Properties.Settings.Default.ApplicationSortOrder;

            foreach (String applicationBinaryName in applicationSortOrderValue.Split(';')) {
               applicationSortOrder.Add(applicationBinaryName);
            }

         } catch (Exception ex) {
            System.Windows.Forms.MessageBox.Show("Failed to load application settings!\r\n\r\n" +
                        ex.Message, GetCaption(0), System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
         }

         return applicationSortOrder;
      }

      /// <summary>
      /// saves application sort order from user settings
      /// </summary>
      /// <param name="applicationSortOrder"></param>
      internal static void SetApplicationSortOrder(List<string> applicationSortOrder) {
         // convert List<String> into a semicolon seperated String
         String applicationSortOrderValue = "";
         foreach (String applicationBinaryName in applicationSortOrder) {
            applicationSortOrderValue += applicationBinaryName + ";";
         }

         // trim last ';'
         if (applicationSortOrderValue.Length > 0) {
            applicationSortOrderValue = applicationSortOrderValue.TrimEnd(';');
         }

         // save user settings
         Properties.Settings.Default.ApplicationSortOrder = applicationSortOrderValue;
         Properties.Settings.Default.Save();
      }

      #endregion


      #region Icon related

      /// <summary>
      /// retrieves 1 icon from a file.
      /// if the file does not contain an icon or an error occurs
      /// the default icon will be used
      /// </summary>
      internal static System.Drawing.Icon GetIconFromFile(String fileName, System.Drawing.Icon defaultIcon) {
         System.Drawing.Icon result = null;
         try {
            result = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
         } catch (Exception ex) {
            result = defaultIcon;
            MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "GetIconFromFile()");
         }
         if ((result.Height == 0) || (result.Width == 0)) {
            // empty icon, use default icon
            result = defaultIcon;
         }
         return result;
      }

      /// <summary>
      /// retrievs all available icons of a file
      /// but returns the first icon
      /// other icons are returned by ref
      /// </summary>
      internal static System.Drawing.Icon GetIconsFromFile(String fileName, ref List<System.Drawing.Icon> largeIcons, ref List<System.Drawing.Icon> smallIcons,  System.Drawing.Icon defaultIcon) {
         System.Drawing.Icon result = defaultIcon;
         try {
            int mnIcons = Unmanaged.ApiExtractIconExMulti(fileName, -1, null, null, 0);
            if (mnIcons == -1) {
               // no icon available
               // use default icon
            } else {
               IntPtr[] phIconsLarge = new IntPtr[mnIcons];
               IntPtr[] phIconsSmall = new IntPtr[mnIcons];
               Unmanaged.ApiExtractIconExMulti(fileName, 0, phIconsLarge, phIconsSmall, mnIcons);
               for (int i = 0; i < mnIcons; i++) {
                  System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(phIconsLarge[i]);
                  largeIcons.Add(icon);
                  icon = System.Drawing.Icon.FromHandle(phIconsSmall[i]);
                  smallIcons.Add(icon);
               }
               result = System.Drawing.Icon.FromHandle(phIconsSmall[0]);
             }
         } catch (Exception ex) {
            MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace, "GetIconsFromFile()");
         }

         if ((result.Height == 0) || (result.Width == 0)) {
            // empty icon, use default icon
         }
         return result;
      }

      #endregion GUI related


      #region Wrong Approach
      ///// <summary>
      ///// mostly you see this way to retrieve window handles.
      ///// But you got problems e.g. for windows explorer window title, ...
      ///// </summary>
      ///// <returns></returns>
      //internal static List<WindowItem> GetApplicationsByNET() {

      //   List<WindowItem> applications = new List<WindowItem>();

      //   foreach (Process p in Process.GetProcesses(".")) {
      //      try {
      //         if (p.MainWindowTitle.Length > 0) {
      //            WindowItem wi = new WindowItem(p.MainWindowTitle, p.MainWindowHandle, p.ProcessName);
      //            applications.Add(wi);
      //         }
      //      } catch { }
      //   }

      //   return applications;
      //}
      #endregion
   }
}
