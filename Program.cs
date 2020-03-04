using System;
using System.Collections.Generic;
using System.Globalization;


namespace SaveToDB
{
    class Program
    {
        static void Main(string[] args)
        {
            if(String.IsNullOrEmpty(Helper.ConnectionValue("csvDB")))
            {
                Console.WriteLine("Please enter a connection string in app.config"); 
            }
            else
            {
                Console.WriteLine("Getting data from CSV...");
                var Items = Data.GetData();
                Console.WriteLine("Creating Database Table (if it does not already exist)");
                Data.CreateDBTable();
                Console.WriteLine("Clearing Database Table");
                Data.ClearDB();
                Console.WriteLine("Saving data to DB (this takes a long time!)");
                Data.SaveData(Items);
                Console.WriteLine("Done saving to Database!");
            }
        }
    }
}
