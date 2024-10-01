using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace StehtimSchilf.TaskBarSorterXP {
   /// <summary>
   /// This class provides helper functions for System.Windows.Forms.ListView objects.
   /// </summary>
   /// <remarks>
   /// StehtimSchilf's TaskBarSorter XP.
   /// This code was initially posted on codeproject.com
   /// 
   /// 09.02.2011 v1.1.0 - new, based on v1.0.0 TaskBarSorterFunctions
   /// </remarks>
   internal static class ListViewHelpers {
      /// <summary>
      /// swaps the list view items at the specified indeces
      /// </summary>
      /// <param name="lv"></param>
      /// <param name="index1"></param>
      /// <param name="index2"></param>
      /// <returns>True, if the items have been swapped</returns>
      /// <seealso cref="MoveSelectedListViewItemDown"/>
      /// <seealso cref="MoveSelectedListViewItemUp"/>
      internal static Boolean SwapListViewItems(ListView listView, int index1, int index2) {
         Boolean result = false;
         if ((index2 < 0) || (index2 > listView.Items.Count - 1)) {
            // do not swap first or last item out of the list
            // so do nothing
         } else if ((index1 < 0) || (index1 > listView.Items.Count - 1)) {
            // do not swap first or last item out of the list
            // so do nothing
         } else {
            // swap
            ListViewItem lvItem1 = (ListViewItem)listView.Items[index1].Clone();
            ListViewItem lvItem2 = (ListViewItem)listView.Items[index2].Clone();

            listView.Items[index1] = lvItem2;
            listView.Items[index2] = lvItem1;

            // set selection again
            // works only if ListView.HideSelection is set to false
            // otherwise selection is canceled after focus lost
            listView.Items[index2].Selected = true;
            result = true;
         }
         return result;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="lv"></param>
      /// <returns>True, if the selected item has been moved up</returns>
      /// <seealso cref="SwapListViewItems"/>
      internal static Boolean MoveSelectedListViewItemUp(ListView listView) {
         Boolean result = false;
         if (listView.SelectedItems.Count > 0) {
            int index = listView.SelectedItems[0].Index;
            result = ListViewHelpers.SwapListViewItems(listView, index, index - 1);
         }
         return result;
      }

      /// <summary>
      /// 
      /// </summary>
      /// <param name="lv"></param>
      /// <returns>True, if the selected item has been moved down</returns>
      /// <seealso cref="SwapListViewItems"/>
      internal static Boolean MoveSelectedListViewItemDown(ListView listView) {
         Boolean result = false;
         if (listView.SelectedItems.Count > 0) {
            int index = listView.SelectedItems[0].Index;
            result = ListViewHelpers.SwapListViewItems(listView, index, index + 1);
         }
         return result;
      }

      /// <summary>
      /// Removes all selected ListViewItems of the specied ListView
      /// </summary>
      /// <param name="listView"></param>
      /// <returns>number of removed ListViewItems</returns>
      internal static int RemoveSelectedListViewItems(ListView listView) {
         int result = listView.SelectedItems.Count;
         foreach (ListViewItem lvItem in listView.SelectedItems) {
            listView.Items.Remove(lvItem);
         }
         return result;
      }
   }
}
