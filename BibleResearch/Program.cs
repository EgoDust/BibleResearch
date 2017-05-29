using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BibleResearch
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Bible Research Program, please enter your name and press 'Enter' ");
            var userName = Console.ReadLine();
            Console.WriteLine("Greetings " + userName + " we're going to try and get some input from our bible file");

            string filePath = @"c:\\Dev\\BibleResearch\\BibleResearch\\Bible\\bible.txt";

            //path check
            if (Path.HasExtension(filePath))
            {
                Console.WriteLine("{0} has an extension.", filePath);
            }

            //check if file exists
            if (File.Exists(filePath))
            {
                foreach (string line in File.ReadLines(@filePath))
                {
                    //if (line.Contains("Genesis") & line.Contains("God"))
                    //{
                    //    Console.WriteLine(line);
                    //}
                    Console.WriteLine(line);

                    //   lets extract the actual verse names & numbers from the actual verse
                    string sentence = "10 cats, 20 dogs, 40 fish and 1 programmer.";
                    // Get all digit sequence as strings.
                    string[] digits = Regex.Split(sentence, @"\D+");
                    // Now we have each number string.
                    foreach (string value in digits)
                    {
                        // Parse the value to get the number.
                        int number;
                        if (int.TryParse(value, out number))
                        {
                            Console.WriteLine(value);
                        }
                    }


                    //move to db call method
                    string bibleVerse = line.ToString();
                    Program p = new Program();
                    p.InsertData(bibleVerse);
                }
            }
            else
            {
                Console.WriteLine("File does not exist or cannot open!");
            }

            Console.ReadLine();
            Console.Beep();
        }

        private void InsertData(string bibleVerse)
        {
            string connectionString = "Server = LAPDOG; Database = BibleTest; Trusted_Connection = True;";
            // define INSERT query with parameters
            string query = "INSERT INTO dbo.bibleVerse (Verse) " +
                           "VALUES (@bibleVerse) ";

            // create connection and command
            using (SqlConnection cn = new SqlConnection(connectionString))
            using (SqlCommand cmd = new SqlCommand(query, cn))
            {
                // define parameters and their values
                cmd.Parameters.Add("@bibleVerse", SqlDbType.VarChar, 6000).Value = bibleVerse;

                // open connection, execute INSERT, close connection
                cn.Open();
                cmd.ExecuteNonQuery();
                cn.Close();

            }
        }
    }
}
