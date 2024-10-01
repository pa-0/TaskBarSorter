using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StehtimSchilf.TaskBarSorterXP {
   /// <summary>
   /// Dummy class to hold some window/process properties
   /// </summary>
   /// <remarks>
   /// StehtimSchilf's TaskBarSorter XP.
   /// This code was initially posted on codeproject.com
   /// 
   /// 09.02.2001 v1.1.0 - Property WindowPlacement added
   /// 11.02.2001 v1.2.0 - public methods added
   /// </remarks>
   public class WindowItem {
      public String WindowTitle { get; set; }
      public IntPtr WindowHandle { get; set; }
      public IntPtr ProcessId { get; set; }
      public String ProcessName { get; set; }
      public String ProcessClass { get; set; }
      public Boolean IsVisible { get; set; }
      public Unmanaged.RECT WindowRect {get; set;}
      public Unmanaged.POINT WindowPosition { get; private set; }
      public Unmanaged.POINT WindowSize { get; private set; }
      public String ModulePath { get; set; }
      public String ModuleFileName { get; set; }
      public Unmanaged.WINDOWPLACEMENT WindowPlacement { get; set; }

      public WindowItem(String WindowTitle, IntPtr WindowHandle, String ProcessName) {
         this.WindowTitle = WindowTitle;
         this.WindowHandle = WindowHandle;
         this.ProcessName = ProcessName;
      }

      public WindowItem(String WindowTitle, IntPtr WindowHandle, String ProcessClass, Boolean IsVisible, Unmanaged.POINT WindowPosition, Unmanaged.POINT WindowSize) {
         this.WindowTitle = WindowTitle;
         this.WindowHandle = WindowHandle;
         this.ProcessClass = ProcessClass;
         this.IsVisible = IsVisible;
         this.WindowSize = WindowSize;
         this.WindowPosition = WindowPosition;
      }

      // shows the window
      public void Show() {
         Unmanaged.ApiSetWindowPlacement(this.WindowHandle.ToInt32(), this.WindowPlacement);

         if (this.WindowPlacement.showCmd == Unmanaged.SW_SHOWNORMAL) {
            Unmanaged.ApiShowWindowAsync(this.WindowHandle, Unmanaged.SW_SHOWNORMAL);
         } else if (this.WindowPlacement.showCmd == Unmanaged.SW_SHOWMINIMIZED) {
            Unmanaged.ApiShowWindowAsync(this.WindowHandle, Unmanaged.SW_SHOWMINIMIZED);
         } else if (this.WindowPlacement.showCmd == Unmanaged.SW_SHOWMAXIMIZED) {
            Unmanaged.ApiShowWindowAsync(this.WindowHandle, Unmanaged.SW_SHOWMAXIMIZED);
         } else {
            Unmanaged.ApiShowWindowAsync(this.WindowHandle, Unmanaged.SW_SHOWNORMAL);
         }
      }
      // hides the window
      public void Hide() {
         Unmanaged.ApiShowWindowAsync(this.WindowHandle, Unmanaged.SW_HIDE);
      }
      // brings the window to front
      public void BringToFront() {
         Unmanaged.ApiSetForegroundWindow(this.WindowHandle);
      }
      // sets a window to stay on top
      public void StayOnTop() {
         Unmanaged.ApiSetWindowPos(this.WindowHandle, Unmanaged.HWND_TOPMOST, this.WindowRect.Left, this.WindowRect.Top, this.WindowRect.Right, this.WindowRect.Bottom, Unmanaged.SWP_SHOWWINDOW);
      }
      // remove on top
      public void RemoveStayOnTop() {
         Unmanaged.ApiSetWindowPos(this.WindowHandle, Unmanaged.HWND_NOTOPMOST, this.WindowRect.Left, this.WindowRect.Top, this.WindowRect.Right, this.WindowRect.Bottom, Unmanaged.SWP_SHOWWINDOW);
      }
      /// <summary>
      /// Sets the window transparency
      /// </summary>
      /// <param name="Alpha">255 = opaque, 0 = transparent</param>
      public void SetTransparency(byte Alpha) {
         // Retrieve the extended window style. 
         int extStyle = Unmanaged.ApiGetWindowLong(this.WindowHandle, Unmanaged.GWL_EXSTYLE);

         // Change the attribute of the specified window
         Unmanaged.ApiSetWindowLong(this.WindowHandle, Unmanaged.GWL_EXSTYLE, extStyle | Unmanaged.WS_EX_LAYERED);

         // Sets the opacity and transparency color key of a layered window.
         Unmanaged.ApiSetLayeredWindowAttributes(this.WindowHandle, 0, Alpha, Unmanaged.LWA_ALPHA);
      }
      /// <summary>
      /// Sets the window transparency
      /// </summary>
      /// <param name="PercentTransparency">100 = transparent, 0 = opaque</param>
      public void SetTransparency(Single PercentTransparency) {
         
         // assert preconditions
         if (PercentTransparency > 100) PercentTransparency = 100;
         if (PercentTransparency < 0) PercentTransparency = 0;

         byte alpha = (byte)(255 - ((PercentTransparency / 100) * 255));
         this.SetTransparency(alpha);
      }
   }
}
