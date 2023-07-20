using System;
using System.Collections.Generic;
using System.Text;

namespace Library_Sparsh
{
    public class Accounts {
      public string[] Name = new string[2];
      public string[] password = new string[2];
      public string[] book = new string[4];

      public Accounts() {
         this.Name[0] = "peter";
         this.Name[1] = "marry";
         this.password[0] = "1234";
         this.password[1] = "0000";
      }
}


}
