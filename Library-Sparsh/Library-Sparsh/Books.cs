using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Sparsh {
   public class Books {

      [PrimaryKey, AutoIncrement]
      public int ISBN {
         get; set;
      }

      public string xBookName {
         get; set;
      }
      public string xBookDetail {
         get; set;
      }

      public bool Borrowing { get; set; } = false;
      public string Borrowed {
         get; set;
      } = string.Empty;

      public Books() {
      }

      public Books(string bookname, string detail) {
         this.xBookName = bookname;
         this.xBookDetail = detail;
      }

      public bool isBorrowing() {
         return Borrowing;
      }

      public override string ToString() {
         return $"{this.xBookDetail}";
      }

   }
}
