using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Library_Sparsh {
   public partial class App : Application {

      private static DataBase db;
      public static DataBase MyDb {
         get {
            if (db == null) {
               db = new DataBase();
            }
            return db;
         }
      }

      public App() {
         InitializeComponent();

         MainPage = new NavigationPage(new MainPage());
      }

      protected override void OnStart() {
      }

      protected override void OnSleep() {
      }

      protected override void OnResume() {
      }
   }
}
