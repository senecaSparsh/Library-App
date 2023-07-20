using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Library_Sparsh {
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class BooksList : ContentPage {

      private List<Books> books = new List<Books>();
      int index = 0;
      int count = 0;
      string bName = string.Empty;
      string author = string.Empty;
      string PersonName = string.Empty;
      bool isAdded = false;

      public BooksList(string name) {
         InitializeComponent();
         ToolbarItems.Clear();
         AddBook();


         books.Add(new Books("Verity", "Colleen Hoover"));
         books.Add(new Books("Dreamland", "Nicholas Sparks"));
         books.Add(new Books("The Golden Enclaves", "Naomi Novik"));
         books.Add(new Books("Lucy By The Sea", "Elizabeth Strout"));
         xBooks.ItemsSource = books;

         PersonName = name;
         lblName.Text = $"Welcome {name}!";
      }

      async void xBooks_ItemTapped(object sender, ItemTappedEventArgs e) {

         count++;
         index = e.ItemIndex;
         Console.WriteLine("iteam tapped");
         Console.WriteLine(e.Item);
         author = e.Item.ToString();

         if (author == "Colleen Hoover") {
            bName = "Verity";
         }
         else if (author == "Nicholas Sparks") {
            bName = "Dreamland";
         }
         else if (author == "Naomi Novik") {
            bName = "The Golden Enclaves";
         }
         else if (author == "Elizabeth Strout") {
            bName = "Lucy By The Sea";
         }



         Books iteamfromDB = null;
         try {
            iteamfromDB = await App.MyDb.GetItemsByID(index);


         }
         catch (Exception ex) {
            xMessage.Text = $"ERROR: Cannot find item with index: {index}";
            xMessage.IsVisible = true;
            return;
         }


         if (iteamfromDB.Borrowing) {
            if (iteamfromDB.Borrowed == PersonName) {

               xMessage.Text = "You have this book checked out!";
               if (!xMessage.IsVisible)
                  xMessage.IsVisible = true;
            }
            else {
               if(iteamfromDB.Borrowed == "marry")
               xMessage.Text = $"Sorry, {bName} is already checked out by marry";

               if (iteamfromDB.Borrowed == "peter")
                  xMessage.Text = $"Sorry, {bName} is already checked out by peter";

               if (!xMessage.IsVisible)
                  xMessage.IsVisible = true;
            }

         }
         else {
            xMessage.Text = $"{bName} is available.";
            if (!xMessage.IsVisible)
               xMessage.IsVisible = true;

         }

         if (e.Item != null && count == 2) {

            if (!ToolbarItems.Contains(xCheckout))
               ToolbarItems.Add(xCheckout);

            if (!ToolbarItems.Contains(xReturn))
               ToolbarItems.Add(xReturn);


            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Black;
         }
         else {

            if (ToolbarItems.Contains(xCheckout))
               ToolbarItems.Remove(xCheckout);

            if (ToolbarItems.Contains(xReturn))
               ToolbarItems.Remove(xReturn);

            ((NavigationPage)Application.Current.MainPage).BarBackgroundColor = Color.Default;


         }

      }

      async void AddBook() {

         Books iteamToAdd = new Books("Verity", "Colleen Hoover");
         Books iteamToAdd1 = new Books("Dreamland", "Nicholas Sparks");
         Books iteamToAdd2 = new Books("The Golden Enclaves", "Naomi Novik");
         Books iteamToAdd3 = new Books("Lucy By The Sea", "Elizabeth Strout");

         int results = await App.MyDb.AddItem(iteamToAdd);
         if (results == 0) {
            Console.WriteLine("++++ ERROR: Item could not be created");
         }
         else {
            Console.WriteLine("++++ Item added!");
         }
         int results1 = await App.MyDb.AddItem(iteamToAdd1);
         if (results1 == 0) {
            Console.WriteLine("++++ ERROR: Item could not be created");
         }
         else {
            Console.WriteLine("++++ Item added!");
         }
         int results2 = await App.MyDb.AddItem(iteamToAdd2);
         if (results2 == 0) {
            Console.WriteLine("++++ ERROR: Item could not be created");
         }
         else {
            Console.WriteLine("++++ Item added!");
         }
         int results3 = await App.MyDb.AddItem(iteamToAdd3);
         if (results3 == 0) {
            Console.WriteLine("++++ ERROR: Item could not be created");
         }
         else {
            Console.WriteLine("++++ Item added!");
         }
      }

      void xBooks_ItemSelected(object sender, SelectedItemChangedEventArgs e) {
         count = 0;
         xMessage.IsVisible = false;
         Console.WriteLine("iteam selected");

      }

      async void Checkout_Clicked(object sender, EventArgs e) {
         // 1. get item information 
         string title = bName;
         string author1 = author;


         // find the iteam
         Books iteamfromDB = null;
         try {
            iteamfromDB = await App.MyDb.GetItemsByID(index);
         }
         catch (Exception) {
            xMessage.Text = $"ERROR: Cannot find item with index: {index}";
            return;
         }


         // get the updated value 
         string BorrowedName = PersonName;
         bool borrowing = true;

         // set the iteams new value
         if (iteamfromDB.Borrowed == string.Empty) {
            iteamfromDB.xBookName = title;
            iteamfromDB.xBookDetail = author1;
            iteamfromDB.Borrowing = borrowing;
            iteamfromDB.Borrowed = BorrowedName;


            // save the changes
            int rowsUpdated = await App.MyDb.UpdateItems(iteamfromDB);

            if (rowsUpdated == 0) {
               xMessage.Text = $"Error: Update for book {title} failed";
            }
            else {
               xMessage.IsVisible = false;
               await DisplayAlert("Success", "Done!", "OK");
            }
         }
         else if (iteamfromDB.Borrowed == BorrowedName) {
            await DisplayAlert("ERROR", "You have this book checked out!", "OK");
            xMessage.Text = $"You have this book checked out!";
         }
         else if (iteamfromDB.Borrowed != string.Empty) {

            if (iteamfromDB.Borrowed == "marry")
               await DisplayAlert("ERROR", $"This book is already checked out by marry", "OK");

            if (iteamfromDB.Borrowed == "peter")
               await DisplayAlert("ERROR", $"This book is already checked out by peter", "OK");

         }

      }

      async void Return_Clicked(object sender, EventArgs e) {

         string title = bName;
         string author1 = author;

         string BorrowedName = string.Empty;
         bool borrowing = false;
         Books iteamfromDB = null;


         try {
            iteamfromDB = await App.MyDb.GetItemsByID(index);
         }
         catch (Exception ex) {
            xMessage.Text = $"ERROR: Cannot find item with index: {index}";
            return;
         }

         if (iteamfromDB.Borrowing && iteamfromDB.Borrowed == PersonName) {

            // set the iteams new value
            iteamfromDB.Borrowing = borrowing;
            iteamfromDB.Borrowed = BorrowedName;

            // save the changes
            int rowsUpdated = await App.MyDb.UpdateItems(iteamfromDB);
            if (rowsUpdated == 0) {
               xMessage.IsVisible = false;
               await DisplayAlert("Error", "This book cannot be returned", "OK");
            }
            else {
               xMessage.IsVisible = false;
               await DisplayAlert("Success", "Done!", "OK");
            }
         }
         else {
            await DisplayAlert("Error", "This book cannot be returned", "OK");
         }


      }

      void BookList_Appearing(object sender, EventArgs e) {
         ToolbarItems.Clear();

      }
   }
}