using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography.X509Certificates;

namespace Library_Sparsh {
   public class DataBase {

      readonly SQLiteAsyncConnection dbConnection;


      public DataBase() {
         string databasePath = Path.Combine(Xamarin.Essentials.FileSystem.AppDataDirectory, "BookDatabase.db");
         Console.WriteLine($"++++++ Database path: {databasePath}");

         dbConnection = new SQLiteAsyncConnection(databasePath);

         // Create the table (based on the TodoItem)
         dbConnection.CreateTableAsync<Books>().Wait();

         Console.WriteLine($"++++++ Database table created!");

      }

      public async Task<int> AddItem(Books book) {
         int numRowAdded = await dbConnection.InsertAsync(book);
         return numRowAdded;
      }


      // get everything from the table
      public async Task<List<Books>> GetAllItems() {
          List<Books> resultlist = await dbConnection.Table<Books>().ToListAsync();
         return resultlist;
      }


      // get a single iteam by its primary key (id)
      public async Task<Books> GetItemsByID(int id) {
         Books result = await dbConnection.GetAsync<Books>(id);
         return result;
      }


      // Update an existing iteam
      public async Task<int> UpdateItems(Books iteam) {
         return await dbConnection.UpdateAsync(iteam);
        
      }


      // delete an iteam
      public async Task<int> DeleteItemsByID(int ID) {
         return await dbConnection.DeleteAsync<Books>(ID);
         
      }
   }
}
