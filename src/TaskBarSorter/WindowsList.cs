using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StehtimSchilf.TaskBarSorterXP {
   /// <summary>
   /// Class to retrieve all window handles.
   /// Uses unmanaged api calls.
   /// <see cref="Unmanaged"/>
   /// </summary>
   /// <remarks>
   /// StehtimSchilf's TaskBarSorter XP.
   /// This code was initially posted on codeproject.com
   /// 
   /// 09.02.2011 v1.1.0 - SortWindowsByWindowItemList(), EnumWindowCallBack() modified
   /// </remarks>
   public class WindowsList {

      // holds the windows
      // filled by call back function (s. below)
      public List<WindowItem> Windows { get; set; }

      /// <summary>
      /// Delegate for the Unmanaged.ApiEnumWindows() function.
      /// In .NET callback functions can be realized by delegates.
      /// </summary>
      /// <param name="hwnd">window handle passed by unmanaged function call</param>
      /// <param name="lParam">unused, but needed to match callback function signature</param>
      /// <returns></returns>
      public delegate Boolean WinCallBack(int hwnd, int lParam);


      /// <summary>
      /// if set to true, it returns only relevant window handles:
      /// - only visible windows
      /// - does not include 'progman'
      /// </summary>
      private Boolean ReturnOnlyRelevantWindows = false;

      private WindowsList() {
      }

      /// <summary>
      /// Create a list of (all) window handles.
      /// </summary>
      /// <param name="ReturnOnlyRelevantWindows">set to true, will return only visible windows</param>
      public WindowsList(Boolean ReturnOnlyRelevantWindows) {
         this.Windows = new List<WindowItem>();
         this.ReturnOnlyRelevantWindows = ReturnOnlyRelevantWindows;
         this.init();
      }


      /// <summary>
      /// 
      /// </summary>
      /// <seealso cref="EnumWindowCallBack"/>
      private void init() {
         // retrieve the windows(-handles)
         // fill property by call back function
         Unmanaged.ApiEnumWindows(new WinCallBack(EnumWindowCallBack), 0);

         // fill additional properties (could also be done in EnumWindowCallBack())
         foreach (WindowItem wi in this.Windows) {
            // get process id by window handle
            IntPtr processId = IntPtr.Zero;
            Unmanaged.ApiGetWindowThreadProcessId(wi.WindowHandle, out processId);
            wi.ProcessId = processId;

            // get Module Infos
            wi.ModulePath = Process.GetProcessById(processId.ToInt32()).MainModule.FileName;
            wi.ModuleFileName = new System.IO.FileInfo(wi.ModulePath).Name;
         }
      }

      /// <summary>
      /// called by delegate function (s. above).
      /// Callback function for Unmanaged.ApiEnumWindows() which is called for
      /// every opened window.
      /// </summary>
      /// <param name="hwnd">window handle passed by unmanaged function call</param>
      /// <param name="lParam">unused, but needed to match callback function signature</param>
      /// <returns></returns>
      /// <remarks>
      /// v1.1.0 : use of WINDOWPLACEMENT
      /// </remarks>
      private bool EnumWindowCallBack(int hwnd, int lParam) {
         IntPtr windowHandle = (IntPtr)hwnd;

         StringBuilder sbWindowTitle = new StringBuilder(1024);

         // get window title text
         Unmanaged.ApiGetWindowText((int)windowHandle, sbWindowTitle, sbWindowTitle.Capacity);

         // handle only processes with a title
         if (sbWindowTitle.Length > 0) {

            // get the process class (don't handle 'Progman'
            StringBuilder sbProcessClass = new StringBuilder(256);
            Unmanaged.ApiGetClassName(hwnd, sbProcessClass, sbProcessClass.Capacity);
            String processClass = sbProcessClass.ToString();

            // is the window visible?
            Boolean isVisible = Unmanaged.ApiIsWindowVisible(windowHandle);

            // only relevant windows?
            Boolean isRelevant = false;
            if (this.ReturnOnlyRelevantWindows) {
               isRelevant = (isVisible && !processClass.Equals("Progman", StringComparison.CurrentCultureIgnoreCase));
            } else {
               isRelevant = true;
            }

            if (isRelevant) {
               // determine window size and position (just because)
               Unmanaged.RECT r = new Unmanaged.RECT();
               Unmanaged.ApiGetWindowRect(windowHandle, ref r);

               // determine window's appearence
               Unmanaged.WINDOWPLACEMENT windowPlacement = new Unmanaged.WINDOWPLACEMENT();
               windowPlacement.length = System.Runtime.InteropServices.Marshal.SizeOf(windowPlacement);
               Unmanaged.ApiGetWindowPlacement(hwnd, ref windowPlacement);

               // create new WindowItem
               WindowItem wi = new WindowItem(sbWindowTitle.ToString(),
                                              windowHandle,
                                              processClass,
                                              isVisible,
                                              new Unmanaged.POINT(r.Left, r.Top),
                                              new Unmanaged.POINT(r.Right - r.Left, r.Bottom - r.Top)
                                             );
               // set additional values
               wi.WindowPlacement = windowPlacement;
               wi.WindowRect = r;

               // add to collection
               this.Windows.Add(wi);
            } else {
               // window is not relevant
            }
         } else {
            // empty window titles are not of any interest (i believe ...)
         }
         return true;
      }

      /// <summary>
      /// Sorts the windows on the Taskbar.
      /// 
      /// In order to restore the windows on the previous place,
      /// WindowItem has a property WindowPlacement which is set in EnumWindowCallBack()
      /// </summary>
      /// <param name="hwndOrdered"></param>
      /// <see cref="WindowItem"/>
      /// <see cref="EnumWindowCallBack"/>
      public static void SortWindowsByWindowItemList(List<WindowItem> hwndOrdered) {
         // STEP 1: hide all 
         foreach (WindowItem wItem in hwndOrdered) {
            // hide
            Unmanaged.ApiShowWindowAsync(wItem.WindowHandle, Unmanaged.SW_HIDE);
         }
         System.Threading.Thread.Sleep(200);

         // STEP 2: show all windows one after another
         foreach (WindowItem wItem in hwndOrdered) {
            Unmanaged.ApiSetWindowPlacement(wItem.WindowHandle.ToInt32(), wItem.WindowPlacement);

            if (wItem.WindowPlacement.showCmd == Unmanaged.SW_SHOWNORMAL) {
               Unmanaged.ApiShowWindowAsync(wItem.WindowHandle, Unmanaged.SW_SHOWNORMAL);
            } else if (wItem.WindowPlacement.showCmd == Unmanaged.SW_SHOWMINIMIZED) {
               Unmanaged.ApiShowWindowAsync(wItem.WindowHandle, Unmanaged.SW_SHOWMINIMIZED);
            } else if (wItem.WindowPlacement.showCmd == Unmanaged.SW_SHOWMAXIMIZED) {
               Unmanaged.ApiShowWindowAsync(wItem.WindowHandle, Unmanaged.SW_SHOWMAXIMIZED);
            } else {
               Unmanaged.ApiShowWindowAsync(wItem.WindowHandle, Unmanaged.SW_SHOWNORMAL);
            }

            // give some time to display the window
            System.Threading.Thread.Sleep(200);
         }
      }
   }
}
