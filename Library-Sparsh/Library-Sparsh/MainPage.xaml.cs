using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Library_Sparsh {
   public partial class MainPage : ContentPage {

      public MainPage() {
         InitializeComponent();
      }

      async void Button_Clicked(object sender, EventArgs e) {
         xError.IsVisible = false;
         Accounts A = new Accounts();
         bool flag = false;

         for (int i = 0; i < 2; i++) {
            if (xUsername.Text == "mary") {
               xError.Text = "Please use marry instead of mary";
            }
            if (A.Name[i] == xUsername.Text && A.password[i] == xPassword.Text) {
               xError.IsVisible = false;
               flag = true;
               await Navigation.PushAsync(new BooksList(xUsername.Text));
               xUsername.Text = string.Empty;
               xPassword.Text = string.Empty;

            }
            else if(!flag) {
               xError.Text = "ERROR: Wrong username or password";
               xError.IsVisible = true;
            }

         }







      }
   }
}
