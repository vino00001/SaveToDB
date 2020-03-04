using System;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
using Dapper;
using MySql.Data.MySqlClient;

namespace SaveToDB
{
    class Data
    {
        public static List<Item> GetData()
        {
            var Items = new List<Item>();
            var path = @"../../../price_detail.csv";

            using (var csvParser = new TextFieldParser(path))
            {
                csvParser.SetDelimiters(new string[] { "\t" }); //set delimeter as tab

                csvParser.ReadLine();   //skips first line

                while (!csvParser.EndOfData)    //creates one item in Items for every row in the .csv
                {
                    string[] fields = csvParser.ReadFields();
                    int priceValueId = int.Parse(fields[0]);
                    DateTime created = DateTime.Parse(fields[1]);
                    DateTime modified = DateTime.Parse(fields[2]);
                    string catalogEntryCode = fields[3];
                    string marketId = fields[4];
                    string currencyCode = fields[5];
                    DateTime validFrom = DateTime.Parse(fields[6]);
                    DateTime? validUntil = null;    //defaults to null
                    if (fields[7] != "NULL")    //if the value isn't null, asign it to date
                        validUntil = DateTime.Parse(fields[7]);
                    string unitPrice = fields[8];

                    Items.Add(new Item
                    {
                        PriceValueId = priceValueId,
                        Created = created,
                        Modified = modified,
                        CatalogEntryCode = catalogEntryCode,
                        MarketId = marketId,
                        CurrencyCode = currencyCode,
                        ValidFrom = validFrom,
                        ValidUntil = validUntil,
                        UnitPrice = unitPrice
                    });
                }
            }
            return Items;
        }

        public static void CreateDBTable()
        {
            using (var connection = new MySqlConnection(Helper.ConnectionValue("csvDB")))
            {
                string sql = @"CREATE TABLE IF NOT EXISTS `csv` (
                                `PriceValueId` INT(11) NOT NULL,
                                `Created` DATETIME NOT NULL ,
                                `Modified` DATETIME NOT NULL ,
                                `CatalogEntryCode` VARCHAR(8) NOT NULL ,
                                `MarketId` VARCHAR(2) NOT NULL ,
                                `CurrencyCode` VARCHAR(3) NOT NULL ,
                                `ValidFrom` DATETIME NOT NULL ,
                                `ValidUntil` DATETIME NULL ,
                                `UnitPrice` DECIMAL(30,9) NOT NULL ,
                                PRIMARY KEY (`PriceValueId`)
                               );";
                connection.Execute(sql);
            }
        }

        public static void ClearDB()
        {
            using (var connection = new MySqlConnection(Helper.ConnectionValue("csvDB")))
            {
                string sql = "DELETE FROM csv";
                connection.Execute(sql);
            }
        }

        public static void SaveData(List<Item> Items)
        {
            using (var connection = new MySqlConnection(Helper.ConnectionValue("csvDB")))
            {
                foreach (var Item in Items)
                {
                    string sql = @"INSERT INTO csv VALUES(@PriceValueId, @Created, @Modified, @CatalogEntryCode, @MarketId, @CurrencyCode, @ValidFrom, @ValidUntil, @UnitPrice)";
                    connection.Execute(sql, Item);
                }
            }
        }
    }
}
