[![Home](https://codeproject.freetls.fastly.net/App_Themes/CodeProject/Img/logo250x135.gif "CodeProject")](https://www.codeproject.com/)

[Articles](https://www.codeproject.com/script/Content/SiteMap.aspx) / [Desktop Programming](https://www.codeproject.com/script/Content/Tag.aspx?tags=desktop) / [Windows Forms](https://www.codeproject.com/script/Content/Tag.aspx?tags=WinForms)

# Sort Windows on the Windows Taskbar or Minimize them to System Tray

[StehtimSchilf](https://www.codeproject.com/script/Membership/View.aspx?mid=3316814)
12 Feb 2011
[CPOL](http://www.codeproject.com/info/cpol10.aspx "The Code Project Open License (CPOL)")8 min read 

`TaskbarSorterXP` is a small utility which allows the user to sort the windows on the Windows Taskbar. Additionally windows can be minimized to system tray

-   [Download demo - 22.14 KB](https://www.codeproject.com/KB/cs/TaskbarSorterXP/TaskBarSorterXP_demo.zip)
-   [Download source - 42.57 KB](https://www.codeproject.com/KB/cs/TaskbarSorterXP/TaskBarSorterXP_src.zip)

## Table of Contents

1.  Introduction
    -   What this project will demonstrate
    -   Summary
2.  Background
3.  Using the code
    -   Retrieving all relevant windows
    -   Hiding and Showing windows
    -   Sorting the windows
    -   Set transparency of a window
    -   Retrieve icon(s) from an executable
    -   Minimize a window to system tray
4.  Make a window stay always on top
5.  Points of Interest
6.  History

## Introduction

### What this Project will Demonstrate

-   Calling unmanaged code (P/Invoke, API call)
-   Showing/Hiding/Activating application windows with unmanaged code
-   Set transparency of a window with unmanaged code
-   Minimize a window to System Tray with `NotifyIcon`
-   Retrieve the application icon(s) from an executable
-   Use of `Delegate`s to realize callback functions
-   Moving `ListViewItems` within a `ListView`
-   Raised `System.Windows.Forms.Panel` with P-Invocation
-   Saving/Retrieving User Settings
-   `ContextMenuStrip` bound to a `ListView`

### Summary

`TaskbarSorterXP` is a small utility which allows the user to sort the windows on the Windows Taskbar.

**Windows XP** does not allow to sort the windows on the taskbar like it's possible in Windows 7. Personally, I open the applications always in the same order to have them "sorted" on the taskbar (e.g. Outlook, then Explorer, then Browser, ...). But sometimes you have to close and reopen them - so the taskbar is inevitably unsorted.

**Windows 7** however supports reordering the windows. But I was too lazy to do this manually then and when.

What I was looking for, was a one-click solution to sort the windows on the taskbar in my preferred order. This was the reason to start this project.

The solution of sorting the windows on the taskbar is simple:

1.  Hide all windows
2.  Show the windows in the sorted order

First, I was messing around with:

```csharp
System.Diagnostics.Process.GetProcesses()
```

But `Process` does not expose a property to set the visible state of a window. This is obviously logic because a `Process` does not always represent a form/window.

Luckily, I've done hiding/showing windows forms already with VBA and Win32 API calls. That's why this solution is interoperating with unmanaged code like:

```csharp
// <summary>
// Used to show, hide, minimize or maximize a window
// </summary>
// <param name="hWnd">handle of window to manipulate</param>
// <param name="nCmdShow">see consts below</param>
// <returns></returns>
[DllImport("user32.dll", EntryPoint="ShowWindowAsync")]
public static extern Boolean ApiShowWindowAsync(IntPtr hWnd, int nCmdShow);
```

Sorting the windows is done by a simple GUI. Starting the application shows a Form with a `ListView` containing all currently visible windows. The user has two possibilities to sort the windows:

1.  Sorting the `ListViewItem`s by the context menu or the `Button`s
2.  Apply a predefined sort order from user settings

![TaskbarSorterXP1.png](https://www.codeproject.com/KB/cs/TaskbarSorterXP/TaskbarSorterXP1.png)

The main GUI

![TaskbarSorterXP2.png](https://www.codeproject.com/KB/cs/TaskbarSorterXP/TaskbarSorterXP2.png)

The preferences GUI with a raised Panel

All you have to specify is the executable's name.

## Background

Why should one need sorting the windows on the taskbar? Well, I like to have done my computer work efficiently. So why waste time to search the Windows Explorer on the taskbar? Or the browser window? Of course, it takes only a second to check the taskbar - but how many times do you activate a window by clicking it on the taskbar?

Why should one need sorting the windows on the taskbar in ages of Windows Vista/7? Well, my personal notebook is still running Windows XP. Fast, stable, reliably.

With v1.1.0, all my needs were fulfilled. But again, it was too painful (for me) to run `TaskBarSorterXP` from program start menu again and again. So I've added a `NofifyIcon` and was now able to run `TaskBarSorterXP` from the System Tray. But why just only minimize `TaskBarSorterXP` to the System Tray? E.g. a playing Media Player is an application you don't need staying on the taskbar. The mail client as well. That's why I've added in v1.2.0 the possibility to **minimize any window** to system tray.

![TaskbarSorterXP3.png](https://www.codeproject.com/KB/cs/TaskbarSorterXP/TaskbarSorterXP3.png)

Minimized windows in the system tray

## Using the Code

This solution consists of six small classes:

|     |     |
| --- | --- |
| `TaskBarSorterGUI` | The main GUI. Handles user interaction. |
| `TaskBarSorterHelpers` | Some helper functions for the GUI, Saving/Loading Settings, ... |
| `TaskBarSorterPreferences` | GUI to define preferred sort order of application windows. |
| `Unmanaged` | Declarations of the unmanaged API calls. |
| `WindowItem` | This class represents a window |
| `WindowsList` | This class holds all (visible) windows (handles). |
| `ListViewHelpers` | Helper functions for `ListView` concerns. |

### Retrieving All Relevant Windows

To retrieve (all of) the windows, I use managed code (s. `Unmanaged`):

```csharp
// <summary>
// Unmanaged function to enumerate all top-level windows on the screen.
// Returns the window of every opened window to the callback function
// </summary>
// <param name="lpEnumFunc">Pointer (callback) to a function which is 
// called for every opened window</param>
// <param name="lParam">application defined. In this context always 0</param>
// <returns></returns>
[DllImport("user32.Dll", EntryPoint = "EnumWindows")]
public static extern int ApiEnumWindows(WindowsList.WinCallBack lpEnumFunc, int lParam);
```

The first parameter requires a pointer to a callback function. This function is called for every opened window and passes the window handle to the callback function. Callback functions in .NET are realized by `Delegate`s. So the next step was to implement a `Delegate` (s. `WindowList`) ...

```csharp
// <summary>
// Delegate for the Unmanaged.ApiEnumWindows() function.
// In .NET callback functions can be realized by delegates.
// </summary>
// <param name="hwnd">window handle passed by unmanaged function call</param>
// <param name="lParam">unused, but needed to match callback function signature</param>
// <returns></returns>
public delegate Boolean WinCallBack(int hwnd, int lParam);
```

... with the related function call (s. `WindowList.init()`) ...

```csharp
// retrieve the windows(-handles)
// fill property by call back function
Unmanaged.ApiEnumWindows(new WinCallBack(EnumWindowCallBack), 0);
```

This expression calls for every opened window a function named `EnumWindowCallBack` and passes the window handle. What we need now is to implement this callback function which receives a window handle. What we do with this window handle afterwards is up to us. There are plenty of other handy unmanaged functions which require a window handle (s. Unmanaged).

```csharp
// <summary>
// called by delegate function (s. above).
// Callback function for Unmanaged.ApiEnumWindows() which is called for
// every opened window.
// </summary>
// <param name="hwnd">window handle passed by unmanaged function call</param>
// <param name="lParam">unused, but needed to match callback function signature</param>
// <returns></returns>
// <remarks>
// v1.1.0 : use of WINDOWPLACEMENT
// </remarks>
private bool EnumWindowCallBack(int hwnd, int lParam) {
   IntPtr windowHandle = (IntPtr)hwnd;

   StringBuilder sbWindowTitle = new StringBuilder(1024);

   // get window title text
   Unmanaged.ApiGetWindowText((int)windowHandle, sbWindowTitle,
                  sbWindowTitle.Capacity);

   // handle only processes with a title
   if (sbWindowTitle.Length > 0) {

      // get the process class (don't handle 'Progman'
      StringBuilder sbProcessClass = new StringBuilder(256);
      Unmanaged.ApiGetClassName
          (hwnd, sbProcessClass, sbProcessClass.Capacity);
      String processClass = sbProcessClass.ToString();

      // is the window visible?
      Boolean isVisible = Unmanaged.ApiIsWindowVisible(windowHandle);

      // only relevant windows?
      Boolean isRelevant = false;
      if (this.ReturnOnlyRelevantWindows) {
         isRelevant = (isVisible && !processClass.Equals
          ("Progman", StringComparison.CurrentCultureIgnoreCase));
      } else {
         isRelevant = true;
      }

      if (isRelevant) {
         // determine window size and position (just because)
         Unmanaged.RECT r = new Unmanaged.RECT();
         Unmanaged.ApiGetWindowRect(windowHandle, ref r);

         // determine window's appearance
         Unmanaged.WINDOWPLACEMENT windowPlacement =
                  new Unmanaged.WINDOWPLACEMENT();
         windowPlacement.length =
            System.Runtime.InteropServices.Marshal.SizeOf(windowPlacement);
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
```

What my callback function does is simple:

1.  I get (with unmanaged function call) the window title text, because only windows with a window text are relevant (I believe...)
2.  I get (with unmanaged function call) the class name because '`Progman`' is not relevant (I believe...)
3.  I check (with unmanaged function call) if the window is visible
4.  I check if the window is relevant or not. A relevant window is:
    -   visible and
    -   process class is not '`Progman`'
5.  If the window is relevant:
    1.  Get window's position and size (just because) with unmanaged function call
    2.  Get window's appearance (Maximized, Position, ...). With this information, a window can be restored correctly.
    3.  Create a new `WindowItem`
    4.  Add the `WindowItem` to the collection

Finally, we have a collection of 'relevant' `WindowItem`s.

My first run of my project was without the test for 'relevant' windows. So, I simply **hid all handles** and then **displayed all handles** again. Well this was funny, because there were handles which did not belong to windows. I had to restart the my notebook because I had hundred thousand million thousand objects on my desktop ...

### Hiding and Showing Windows

Hiding and/or showing a window is simply done again by a unmanaged function call (s. `Unmanaged`:

```csharp
// <summary>
// Used to show, hide, minimize or maximize a window
// </summary>
// <param name="hWnd">handle of window to manipulate</param>
// <param name="nCmdShow">see consts below</param>
// <returns></returns>
[DllImport("user32.dll", EntryPoint="ShowWindowAsync")]
public static extern Boolean ApiShowWindowAsync(IntPtr hWnd, int nCmdShow);

public const int SW_HIDE = 0;
public const int SW_SHOWNORMAL = 1;
public const int SW_SHOWMINIMIZED = 2;
public const int SW_SHOWMAXIMIZED = 3;
public const int SW_SHOWNOACTIVATE = 4;
public const int SW_RESTORE = 9;
public const int SW_SHOWDEFAULT = 10;
```

Now we have a list of window handles (in our `WindowItem` collection) and a function to show or hide a window. So we are prepared to implement our sort logic (first hide all, then show them in sorted order).

### Sorting the Windows

To sort the windows, we simply need a function which takes a collection of window handles, hides them and shows them again. Because we also need previous window placement, it's obvious our function will treat a collection of `WindowItem`s (s. `WindowsList.SortWindowsByWindowItemList()`):

```csharp
// <summary>
// Sorts the windows on the Taskbar.
//
// In order to restore the windows on the previous place,
// WindowItem has a property WindowPlacement which is set in EnumWindowCallBack()
// </summary>
// <param name="hwndOrdered"></param>
// <see cref="WindowItem">
// <see cref="EnumWindowCallBack">
public static void SortWindowsByWindowItemList(List<windowitem> hwndOrdered) {
   // STEP 1: hide all
   foreach (WindowItem wItem in hwndOrdered) {
      // hide
      Unmanaged.ApiShowWindowAsync(wItem.WindowHandle, Unmanaged.SW_HIDE);
   }
   System.Threading.Thread.Sleep(200);

   // STEP 2: show all windows one after another
   foreach (WindowItem wItem in hwndOrdered) {
      Unmanaged.ApiSetWindowPlacement(wItem.WindowHandle.ToInt32(),
                      wItem.WindowPlacement);

      if (wItem.WindowPlacement.showCmd == Unmanaged.SW_SHOWNORMAL) {
         Unmanaged.ApiShowWindowAsync
          (wItem.WindowHandle, Unmanaged.SW_SHOWNORMAL);
      } else if (wItem.WindowPlacement.showCmd ==
              Unmanaged.SW_SHOWMINIMIZED) {
         Unmanaged.ApiShowWindowAsync
              (wItem.WindowHandle, Unmanaged.SW_SHOWMINIMIZED);
      } else if (wItem.WindowPlacement.showCmd ==
              Unmanaged.SW_SHOWMAXIMIZED) {
         Unmanaged.ApiShowWindowAsync(wItem.WindowHandle,
              Unmanaged.SW_SHOWMAXIMIZED);
      } else {
         Unmanaged.ApiShowWindowAsync
          (wItem.WindowHandle, Unmanaged.SW_SHOWNORMAL);
      }

      // give some time to display the window
      System.Threading.Thread.Sleep(200);
   }
}
```

If the user sorts the `ListView`, the `WindowItem`s are taken from the `ListViewItem.Tag` and passed to the `SortWindowsByWindowItemList()`.

```csharp
// <summary>
// sorts the windows on the Windows Taskbar by ListView sort order
// </summary>
private void btnSortByListView_Click(object sender, EventArgs e) {
   List<windowitem> windowsOrdered = new List<windowitem>();

   // iterate through list view items and get attached WindowItems
   foreach (ListViewItem lvItem in this.lv_Windows.Items) {
      WindowItem wItem = (WindowItem)lvItem.Tag;
      windowsOrdered.Add(wItem);
   }

   // reorder windows by a list of WindowItem
   WindowsList.SortWindowsByWindowItemList(windowsOrdered);
}
```

### Set Transparency of a Window

The `ListView` context menu has an entry to set the transparency of the selected `WindowItem`. Setting the transparency is again done by P/Invoke (s. `WindowItem.SetTransparency()` and `Unmanaged`):

```csharp
// <summary>
// Sets the window transparency
// </summary>
// <param name="Alpha">255 = opaque, 0 = transparent</param>
public void SetTransparency(byte Alpha) {
   // Retrieve the extended window style.
   int extStyle = Unmanaged.ApiGetWindowLong
      (this.WindowHandle, Unmanaged.GWL_EXSTYLE);

   // Change the attribute of the specified window
   Unmanaged.ApiSetWindowLong
   (this.WindowHandle, Unmanaged.GWL_EXSTYLE, extStyle | Unmanaged.WS_EX_LAYERED);

   // Sets the opacity and transparency color key of a layered window.
   Unmanaged.ApiSetLayeredWindowAttributes
      (this.WindowHandle, 0, Alpha, Unmanaged.LWA_ALPHA);
}
```

### Retrieve Icon(s) from an Executable

Before we can minimize a window to the system tray, we need an `Icon` for our `NotifyIcon`. The system tray icon should be identical to the window icon. Again, there are API calls to retrieve the icon from an executable:

```csharp
// get only 1 small and 1 large icon
[DllImport("shell32.dll", EntryPoint = "ExtractIconEx", CharSet=CharSet.Auto)]
public static extern int ApiExtractIconExSingle
  (string stExeFileName, int nIconIndex, ref IntPtr phiconLarge,
  ref IntPtr phiconSmall, int nIcons);

// get all small and all large icons
[DllImport("shell32.dll", EntryPoint= "ExtractIconEx", CharSet=CharSet.Auto)]
public static extern int ApiExtractIconExMulti
  (string stExeFileName, int nIconIndex, IntPtr[] phiconLarge,
  IntPtr[] phiconSmall, int nIcons);
```

The `System.Drawing.Icon` class has a `public` method `ExtractAssociatedIcon()` which also allows to extract an Icon - but only one. To keep this project simple, I use this method but it will not always return the same icon as window has (e.g., explorer icon). (see `TaskBarSorterHelpers.GetIconFromFile()`, `TaskBarSorterGUI.refreshListView()`).


```csharp
// <summary>
// retrieves 1 icon from a file.
// if the file does not contain an icon or an error occurs
// the default icon will be used
// </summary>
internal static System.Drawing.Icon GetIconFromFile
  (String fileName, System.Drawing.Icon defaultIcon) {
   System.Drawing.Icon result = null;
   try {
      result = System.Drawing.Icon.ExtractAssociatedIcon(fileName);
   } catch (Exception ex) {
      result = defaultIcon;
      MessageBox.Show(ex.Message + "\r\n" +
          ex.StackTrace, "GetIconFromFile()");
   }
   if ((result.Height == 0) || (result.Width == 0)) {
      // empty icon, use default icon
      result = defaultIcon;
   }
   return result;
}
```

### Minimize a Window to System Tray

Minimizing a window to system tray can easily be done with a `NotifyIcon` (see `TaskBarSorterGUI.newNotifyIcon()`):

```csharp
// <summary>
// Minimizes the window to the system tray and
// adds a reference to a list so it can be restored on application exit
// </summary>
// <param name="wItem">WindowItem to minimize to System Tray</param>
private void newNotificationIcon(WindowItem wItem) {

   // create a notification icon
   NotifyIcon icon = new NotifyIcon();
   // get the icon from associated executable
   icon.Icon = TaskBarSorterHelpers.GetIconFromFile
          (wItem.ModulePath, this._DefaultIcon);
   // get window title for tool tip.
   icon.Text = StringHelpers.Left(wItem.WindowTitle, 50);
   icon.Visible = true;

   // add WindowItem data to NotifyIcon (see showMinimizedWindow())
   icon.Tag = wItem;

   // all NotifyIcons use the same event handler
   icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler
              (this.showMinimizedWindow);

   // add to minimized WindowItems collection
   // see Form_Closing()
   this._minimizedWindowItems.Add(wItem);

   // hide window
   wItem.Hide();
}
```

1.  Create a new `NotifyIcon`.
2.  Add the `WindowItem` to the `Tag` property. This information is needed to restore the window again.
3.  Add a double click event handler.
4.  Add the `WindowItem` to collection of minimized windows. On application closing, we have to restore all windows in this collection again.
5.  Finally we hide the window.

Restoring a `NotifyIcon` is as simple as that:

```csharp
// <summary>
// Double click event handler for our minimized NotifyIcons
// </summary>
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
```

### Make a Window Stay Always on Top

To make a window to stay always on top, I achieved again with P/Invocation (see `Unmanaged<apisetwindowpos() />, <code>WindowItem.StayOnTop()`:

```csharp
// used to set a window on top
[DllImport("user32.dll", EntryPoint = "SetWindowPos")]
public static extern bool ApiSetWindowPos
  (IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
```

**To keep this article tight, I'll do not post the function body (you can browse the code online, see above) of the following implementations.**

### Moving ListViewItems within a ListView

With the `Button`s and/or the `ContextMenu` of the `ListView`, a user may move up and down a selected `ListViewItem` within the `ListView`.

-   `ListViewHelpers.SwapListViewItems()`
-   `ListViewHelpers.MoveSelectedListViewItemDown()`
-   `ListViewHelpers.MoveSelectedListViewItemUp()`

### Raised System.Windows.Forms.Panel with P-Invocation

The `ListView` from the `TaskBarSorterPreferences` is placed in a raised `Panel` (just because):

-   `TaskBarSorterPreferences.initGUI()`
-   `Unmanaged`, see region GUI related APIs

### Saving/Retrieving User Settings

Saving/Retrieving User Settings is done with:

-   `TaskBarSorterHelpers.GetApplicationSortOrder()`
-   `TaskBarSorterHelpers.SetApplicationSortOrder()`

### ContextMenuStrip Bound to a ListView

Within the `ContextMenuStrip Opening Event`, one can enable/disable `ToolStripMenuItem` depending on the needs:

-   `TaskBarSorterGUI.lvContextMenu_Opening()`

## Points of Interest

-   `WindowsList` could inherit `List<>`
-   Windows which do not have a window title get omitted from the sort process
-   Transparency does not work on Windows 7

<br/>

## Comments and Discussions

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 💡 | Great App - but please add List Order Save/Load. | 👤 | [Member 15710172](https://www.codeproject.com/script/Membership/View.aspx?mid=15710172) | 18-Jul-22 12:46 |

Very useful App , but please will you add ability to saved the List Order once it is arranged the way the User  
wants it , and also the ability to load that that saved List Order.  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| My vote of 5 | 👤 | [GicuPiticu](https://www.codeproject.com/script/Membership/View.aspx?mid=649908) | 14-Nov-21 14:58 |

Awesome  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| ![Question](https://www.codeproject.com/script/Forums/Images/msg_question.gif "Question") | Fantastic!......request for 1 added feature... | 👤 | [Member 9442749](https://www.codeproject.com/script/Membership/View.aspx?mid=9442749) | 19-Sep-12 19:35 |

I know that you can save the list of Applications, but do you think you could make it possible to save the list order of "Window Titles"? The user could sort the list of opened Windows, then save that list. If the users sorts by the saved list, then any Windows in the list will be sorted into that order.  
  
Thanks.  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| My vote of 5 | 👤 | [Member 4481407](https://www.codeproject.com/script/Membership/View.aspx?mid=4481407) | 17-Feb-11 20:41 |

Very useful!  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| My vote of 3 | 👤 | [Theingi Win](https://www.codeproject.com/script/Membership/View.aspx?mid=5376935) | 9-Feb-11 19:51 |

Nice! I get new knowledge related following about  
  
# Calling unmanaged code (P/Invoke, API call)  
# Showing/Hiding/Activating application windows  
# Use of Delegates to realize callback functions  
  
Thanks! Stehtimschilf  
Theingi  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| My vote of 5 | 👤 | [**JF2015**](https://www.codeproject.com/script/Membership/View.aspx?mid=2685176) | 6-Feb-11 23:48 |

Good one. I like the usage of the Win API. Would be great if you could enhance the usability of this tool by providing sorting of the windows inside the form (move up or down).  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| ✅ | [Re: My vote of 5 - Sort `ListView` by `ContextMenu`](https://www.codeproject.com/Messages/3760419/Re-My-vote-of-5-Sort-ListView-by-ContextMenu) | 👤 | [StehtimSchilf](https://www.codeproject.com/script/Membership/View.aspx?mid=3316814) | 7-Feb-11 3:51 |

Hi JF2015  
  
Thx for your feedback. About sorting inside the form: The `ListView` has already a `ContextMenu` attached. I see, I didn't mentioned the `ContextMenu` in the article. I'll make good for it this evening.  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| My vote of 5 | 👤 | [Warrick Procter](https://www.codeproject.com/script/Membership/View.aspx?mid=3672333) | 6-Feb-11 17:53 |

I like it!  
  
I've been procrastinating about writing something along these lines myself but never got into gear. Your excellent piece of work has given me some impetus and at the same time saved me from a lot of research/time/effort.  
  
Thanks, Warrick  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| You got my 5! | 👤 | [luisnike19](https://www.codeproject.com/script/Membership/View.aspx?mid=1475315) | 6-Feb-11 10:25 |

Nice article.  
For some reason I have Google Chrome and when sorting the window is not maximized, just restored. But the other windows I tested is working as expected.  👍 


|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| [Re: You got my 5!](https://www.codeproject.com/Messages/3760072/Re-You-got-my-5) | 👤 | [StehtimSchilf](https://www.codeproject.com/script/Membership/View.aspx?mid=3316814) | 6-Feb-11 17:09 |

You're right. The windows get not correctly restored (happens also with Windows Explorer). Luckily there exists the API calls `GetWindowPlacement()` and `SetWindowPlacement()`. I'll extend the class `WindowItem` with a new property for the window placement as soon as possible. Thx for the pointer.  


|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
|✅| [Re: You got my 5!](https://www.codeproject.com/Messages/3763145/Re-You-got-my-5) | 👤 | [StehtimSchilf](https://www.codeproject.com/script/Membership/View.aspx?mid=3316814) | 8-Feb-11 22:07 |

I've finally fixed the issue with not maximized windows. Thx for the pointer.  


|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| [Re: You got my 5!](https://www.codeproject.com/Messages/3765626/Re-You-got-my-5) | 👤 | [luisnike19](https://www.codeproject.com/script/Membership/View.aspx?mid=1475315) | 10-Feb-11 20:23 |

Today I tested your fix, and now it's doing the opposite I found in your first version. For example I opened your zip file using WinRAR, and after opening the zip file, I maximized this WinRAR window, and then I hit "sort by list" and the WinRAR window restores.  
In other test I have the "My network places" and when doing the same the window was maximized and after that the window restores. ![WTF | :WTF:](https://codeproject.global.ssl.fastly.net/script/Forums/Images/smiley_WTF.gif) it's weird  

  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| [Re: You got my 5!](https://www.codeproject.com/Messages/3765987/Re-You-got-my-5) | 👤 | [StehtimSchilf](https://www.codeproject.com/script/Membership/View.aspx?mid=3316814) | 11-Feb-11 3:40 |


Hi luisnike19  
  
I could repeat this issue when I proceed as follow:  
  
1. open WinRAR  
2. open `TaskBarSorterXP`  
3. Maximize WinRAR  
4. sort by list  
  
-> WinRAR restores only. Same for the net work places.  
  
But try this.  
Before you sort the list and click on "sort by list", click on "refresh", then sort the list and then click the button. If you do not refresh the list, the `WindowItem` contain the `PLACEMENT` info from the program start.  
  


|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| [Re: You got my 5!](https://www.codeproject.com/Messages/3766426/Re-You-got-my-5) | 👤 | [luisnike19](https://www.codeproject.com/script/Membership/View.aspx?mid=1475315) | 11-Feb-11 7:46 |

Ah ok, I thought that, but I didn't know the Refresh button function. It's good. Maybe in case the the Replacemente info is null and I hit sort by list, you should get that placement info and then do the rest of the method. 👍  
  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| [Re: You got my 5!](https://www.codeproject.com/Messages/3767844/Re-You-got-my-5) | 👤 | [StehtimSchilf](https://www.codeproject.com/script/Membership/View.aspx?mid=3316814) | 12-Feb-11 18:00 |

Hi  
I cannot imagine a situation in which the `WindowPlacement` property should be `null`? Every time the `ListView` is refreshed (or initially displayed) all `WindowPlacements` are retrieved. Did you get any exceptions?  
  

|     |     |     |     |     |
| ---: | :--- | --- | :--- | ---: |
| 📄| [Re: You got my 5!](https://www.codeproject.com/Messages/3768234/Re-You-got-my-5) | 👤 | [luisnike19](https://www.codeproject.com/script/Membership/View.aspx?mid=1475315) | 13-Feb-11 14:58 |


No, I didn't get any exceptions  
