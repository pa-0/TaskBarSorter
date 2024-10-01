using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace StehtimSchilf.TaskBarSorterXP {

   /// <summary>
   /// System.Diagnostics.Process.GetProcesses() does not fullfil our needs.
   /// So use good old win32 api calls.
   /// For detailed information about the P/Invokes see MSDN. 
   /// </summary>
   /// <remarks>
   /// StehtimSchilf's TaskBarSorter XP.
   /// This code was initially posted on codeproject.com
   ///
   /// 05.02.2011 v1.0.0 - first release
   /// 09.02.2011 v1.1.0 - ApiGetWindowPlacement(), ApiSetWindowPlacement() added
   ///                     struct WINDOWPLACEMENT added
   ///                     struct Point renamed to POINT
   ///                     GUI related APIs added
   ///                     Additional Helpers moved from TaskBarSorterHelpers
   /// 11.02.2011 v1.2.0 - ApiSetLayeredWindowAttributes() and related consts added
   ///                   - ApiExtractIconExSingle(), ApiExtractIconExMulti, ApiDestroyIcon added
   /// </remarks>
   public class Unmanaged {


      #region Windows related APIs, Consts, Enums

      // bring a window to front
      [DllImport("User32.dll", EntryPoint="SetForegroundWindow")]
      public static extern bool ApiSetForegroundWindow(IntPtr hWnd);

      /// <summary>
      /// used to determine size and position of a window
      /// </summary>
      /// <param name="hwnd"></param>
      /// <param name="lpRect">see struct below</param>
      /// <returns></returns>
      [DllImport("user32.dll", EntryPoint = "GetWindowRect")] 
      public static extern long ApiGetWindowRect(IntPtr hwnd, ref RECT lpRect);

      // used to determine if a window is visible
      [DllImport("user32.dll", EntryPoint = "IsWindowVisible")] 
      public static extern bool ApiIsWindowVisible(IntPtr hWnd);

      /// <summary>
      /// Unmanaged function to enumerate all top-level windows on the screen.
      /// Returns the window of every opened window to the callback function
      /// </summary>
      /// <param name="lpEnumFunc">Pointer (callback) to a function which is called for every opened window</param>
      /// <param name="lParam">application defnied. In this context always 0</param>
      /// <returns></returns>
      [DllImport("user32.Dll", EntryPoint = "EnumWindows")]
      public static extern int ApiEnumWindows(WindowsList.WinCallBack lpEnumFunc, int lParam);

      // get window title
      [DllImport("User32.Dll", EntryPoint = "GetWindowText")] 
      public static extern void ApiGetWindowText(int h, StringBuilder s, int nMaxCount);
      
      // get class name of window/control/... handle
      [DllImport("User32.Dll", EntryPoint = "GetClassName")]
      public static extern void ApiGetClassName(int h, StringBuilder s, int nMaxCount);

      // get process id by handle
      [DllImport("user32.dll", EntryPoint="GetWindowThreadProcessId")]
      public static extern IntPtr ApiGetWindowThreadProcessId(IntPtr hWnd, out IntPtr ProcessId);

      // get module of a handle (this api is not used in this project).
      [DllImport("user32.dll", EntryPoint = "GetWindowModuleFileName")]
      public static extern int ApiGetWindowModuleFileName(int hWnd, StringBuilder title, int size);

      // used to save window state
      [DllImport("user32.dll", EntryPoint = "GetWindowPlacement")]
      public static extern Boolean ApiGetWindowPlacement(int hWnd, ref WINDOWPLACEMENT lpwndpl);

      // used to save window state
      [DllImport("user32.dll", EntryPoint = "SetWindowPlacement")]
      public static extern Boolean ApiSetWindowPlacement(int hWnd, WINDOWPLACEMENT lpwndpl);

      /// <summary>
      /// Used to show, hide, minimze or maximize a window
      /// </summary>
      /// <param name="hWnd">handle of window to manipulate</param>
      /// <param name="nCmdShow">see consts below</param>
      /// <returns></returns>
      [DllImport("user32.dll", EntryPoint="ShowWindowAsync")]
      public static extern Boolean ApiShowWindowAsync(IntPtr hWnd, int nCmdShow);

      // used to set window transparency
      [DllImport("user32.dll", EntryPoint="SetLayeredWindowAttributes")]
      public static extern Boolean ApiSetLayeredWindowAttributes(IntPtr hwnd, uint crKey, byte bAlpha, uint dwFlags);
      
      [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
      public static extern int ApiGetWindowLong(IntPtr hWnd, int nIndex);
      [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
      public static extern int ApiSetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
      
      // used to set a window on top
      [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
      public static extern bool ApiSetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

      // get only 1 small and 1 large icon
      [DllImport("shell32.dll", EntryPoint = "ExtractIconEx", CharSet=CharSet.Auto)]
      public static extern int ApiExtractIconExSingle(string stExeFileName, int nIconIndex, ref IntPtr phiconLarge, ref IntPtr phiconSmall, int nIcons);

      // get all small and all large icons
      [DllImport("shell32.dll", EntryPoint= "ExtractIconEx", CharSet=CharSet.Auto)]
      public static extern int ApiExtractIconExMulti(string stExeFileName, int nIconIndex, IntPtr[] phiconLarge, IntPtr[] phiconSmall, int nIcons);

      // used to destroy the icon handle
      [DllImport("user32.dll", EntryPoint = "DestroyIcon")]
      public static extern Boolean ApiDestroyIcon(IntPtr handle);


      // for ApiSetLayeredWindowAttributes
      public const int LWA_ALPHA = 0x2;
      public const int LWA_COLORKEY = 0x1;

      // for ApiShoWindowAsync
      public const int SW_HIDE = 0;
      public const int SW_SHOWNORMAL = 1;
      public const int SW_SHOWMINIMIZED = 2;
      public const int SW_SHOWMAXIMIZED = 3;
      public const int SW_SHOWNOACTIVATE = 4;
      public const int SW_RESTORE = 9;
      public const int SW_SHOWDEFAULT = 10;

      // for ApiSetWindowPos
      // can not make const IntPtrs
      public static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
      public static readonly IntPtr HWND_NOTOPMOST = new IntPtr(-2);
      public static readonly IntPtr HWND_TOP = new IntPtr(0);
      public static readonly IntPtr HWND_BOTTOM = new IntPtr(1);
      public const int SWP_NOSIZE = 0x0001;
      public const int SWP_NOMOVE = 0x0002;
      public const int SWP_NOZORDER = 0x0004;
      public const int SWP_NOACTIVATE = 0x0010;
      public const int SWP_FRAMECHANGED = 0x0020;
      public const int SWP_SHOWWINDOW = 0x0040;
      public const int SWP_ALL = SWP_NOSIZE | SWP_NOMOVE | SWP_NOZORDER | SWP_NOACTIVATE; // not official


      // for ApiGetWindowLong and ApiSetWindowLong
      public const int GWL_EXSTYLE = -20;
      public const int WS_EX_DLGMODALFRAME = 0x00000001;
      public const int WS_EX_LAYERED = 0x80000;

      /// <summary>
      /// used by ApiGetWindowRect()
      /// </summary>
      [StructLayout(LayoutKind.Sequential)]
      public struct RECT {
         public int Left;
         public int Top;
         public int Right;
         public int Bottom;
      }

      public struct WINDOWPLACEMENT {
         public int length;
         public int flags;
         // 1 = normal, 2 = minimized, 3 = maximized (see SW_ consts)
         public int showCmd;
         public POINT ptMinPosition;
         public POINT ptMaxPosition;
         public RECT rcNormalPosition;
      }

      /// <summary>
      /// used to represent a window position or a window size
      /// </summary>
      public struct POINT {
         public int x;
         public int y;

         public POINT(int x, int y) {
            this.x = x;
            this.y = y;
         }
      }

      public static String POINTToString(Unmanaged.POINT point) {
         return String.Format("{0},{1}", point.x, point.y);
      }
      public static String RECTToString(Unmanaged.RECT rect) {
         return String.Format("{0},{1},{2},{3}", rect.Bottom, rect.Left, rect.Top, rect.Right);
      }
      #endregion // Windows related APIs, Consts


      #region Additional Helpers based on API calls

      /// <summary>
      /// 
      /// </summary>
      /// <param name="hwnd"></param>
      /// <returns></returns>
      internal static IntPtr GetProcessIdByWindowHandle(IntPtr hwnd) {
         // get process id by window handle
         IntPtr processId = IntPtr.Zero;
         Unmanaged.ApiGetWindowThreadProcessId(hwnd, out processId);
         return processId;
      }


      /// <summary>
      /// Returns the path and name of the handle's module
      /// </summary>
      /// <param name="hwnd">module's handle</param>
      /// <returns></returns>
      internal static String GetModulePathByWindowHandle(IntPtr hwnd) {

         // get process id by window handle
         IntPtr processId = GetProcessIdByWindowHandle(hwnd);

         // get Module Path
         String modulePath = Process.GetProcessById(processId.ToInt32()).MainModule.FileName;

         return modulePath;
      }
      #endregion
   }
}
