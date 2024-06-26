﻿/*
 * Author: David Beltran
 */

using MySql.Data.MySqlClient;
using System.Collections;

namespace HerBudget
{
    /// <summary>
    /// Handles entering ArrayList to MySQL database
    /// </summary>
    public class Database
    {
        private MySqlConnection? conn;
        private string? server;
        private string? database;
        private string? uid;
        private string? password;

        /// <summary>
        /// Database class constructor
        /// </summary>
        public Database()
        {
            Initialize();
        }

        /// <summary>
        /// Instantiates MySQL connection for Database constructor
        /// </summary>
        private void Initialize()
        {
            this.server = "localhost";
            this.database = "expenses";
            this.uid = "root";
            this.password = "Diska1725!";
            string connString = "SERVER=" + this.server + ";DATABASE=" + this.database +
                ";UID=" + this.uid + ";PASSWORD=" + this.password + ";";
            this.conn = new MySqlConnection(connString);
        }

        /// <summary>
        /// Opens MySQL connection
        /// Mostly created to ensure connection was made when initially designed Database class
        /// </summary>
        public void OpenConnection()
        {
            try
            {
                this.conn.Open();
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Creates and fills 'Transactions' table in MySQL database
        /// </summary>
        /// <param name="expenses">ArrayList made from pdf file</param>
        public void CreateTable(ArrayList expenses)
        {
            string sqlTable = "CREATE TABLE Transactions (" +
                "Date varchar(15)," +
                "Details text," +
                "Amount float)";
            MySqlCommand cmd = new MySqlCommand(sqlTable, this.conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex) // catches if table already exists
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (TableFull() == false)
                {
                    FillTable(expenses);
                }
                else
                {
                    Console.WriteLine("Table has already been filled.");
                }
            }
        }

        /// <summary>
        /// Checks if 'Transactions' has been filled
        /// </summary>
        /// <returns>boolian checking for empty table</returns>
        private bool TableFull()
        {
            string findCount = "use " + this.database + "; select count(*) from Transactions;";
            MySqlCommand cmd = new MySqlCommand(findCount, this.conn);
            int rowCount = int.Parse(cmd.ExecuteScalar().ToString()!);// '!' is null forgiving operator

            if (rowCount > 0)
            {
                return true;
            }
            return false;

        }

        /// <summary>
        /// Fills 'Transactions' table in MySQL database
        /// </summary>
        /// <param name="expenses">ArrayList made from pdf file</param>
        /// TODO
        /// - Find Python equivalent of ExecuteMany() method.
        /// - Research parameter declerations... May need safer design than Parameters.Clear() within for loop
        /// - Research SQL injection... Does this pass?
        private void FillTable(ArrayList expenses)
        {
            string fillTable = "INSERT INTO Transactions(Date, Details, Amount) VALUES (@Date, @Details, @Amount)";
            MySqlCommand cmd = new MySqlCommand(fillTable, this.conn);
            
            foreach (ArrayList exp in expenses)
            {
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@Date", exp[0]);
                cmd.Parameters.AddWithValue("@Details", exp[1]);
                cmd.Parameters.AddWithValue("@Amount", exp[2]);
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Close connection to MySQL database for each Database class instance
        /// </summary>
        public void CloseDatabase()
        {
            this.conn.Close();
        }
    }
}