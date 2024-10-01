using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StehtimSchilf.TaskBarSorterXP {
   static class StringHelpers {
      // returns the left [len] chars
      // corrects [len] to the length of [s] if longer
      public static String Left(String s, int len) {
         if (len < 0) return null;
         if (s == null) return null;
         if (len > s.Length) len = s.Length;
         return s.Substring(0, len);
      }
   }
}
