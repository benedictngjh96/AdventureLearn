// SAMPLE INJECTION SCRIPT

using System;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Net;
using System.Threading;
using System.Linq;
using CsvHelper;
using System.Globalization;

namespace Injections
{
    class Program
    {
        public static MySqlConnectionStringBuilder csb = new MySqlConnectionStringBuilder
        {
            Server = "35.198.238.34",
            UserID = "root",
            Password = "MpiPkr9y04xmg11h",
            Database = "AdventureLearn",
            SslMode = MySqlSslMode.None,
        };

        public static int InsertToCampaignQuestion(List<Entry> query)
        {
            int result = 0;
            StringBuilder sCommand = new StringBuilder("INSERT INTO CampaignQuestion(QuestionId, WorldId, SectionId, LevelId) VALUES ");
            MySqlConnection conn = new MySqlConnection(csb.ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL Server ...");
                conn.Open();

                List<string> Rows = new List<string>();
                foreach(Entry r in query)
                {
                    Rows.Add(string.Format("('{0}', '{1}', '{2}', '{3}')", r.Question_ID, r.World_ID, r.Section_ID, r.Level_ID));
                }
                sCommand.Append(string.Join(",", Rows));
                sCommand.Append(";");

                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), conn))
                {
                    myCmd.CommandType = System.Data.CommandType.Text;
                    myCmd.ExecuteNonQuery();
                }              
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured: " + ex.ToString());
            }

            conn.Close();
            Console.WriteLine("SQL command successfully executed");
            return result;
        }

        public static int InsertToQuestion(List<Entry> query)
        {
            int result = 0;
            StringBuilder sCommand = new StringBuilder("INSERT INTO Question(QuestionId, Option1, Option2, Option3, CorrectOption, QuestionTitle) VALUES ");
            MySqlConnection conn = new MySqlConnection(csb.ConnectionString);
            try
            {
                Console.WriteLine("Connecting to MySQL Server ...");
                conn.Open();

                Boolean hasDuplicates = false;
                Random rand = new Random();
                
                List<string> Rows = new List<string>();
                foreach (Entry r in query)
                {
                    do
                    {
                        if(Convert.ToDouble(r.Correct_Option) % 1 == 0)
                        {
                            if ((int)Convert.ToDouble(r.Correct_Option) <= 100)
                            {
                                r.Option_1 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(10)).ToString();
                                r.Option_2 = ((int)Convert.ToDouble(r.Correct_Option) - rand.Next(10)).ToString();
                                r.Option_3 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(10)).ToString();
                            }
                            else if ((int)Convert.ToDouble(r.Correct_Option) <= 1000 && (int)Convert.ToDouble(r.Correct_Option) > 100)
                            {
                                r.Option_1 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(100)).ToString();
                                r.Option_2 = ((int)Convert.ToDouble(r.Correct_Option) - rand.Next(100)).ToString();
                                r.Option_3 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(100)).ToString();
                            }
                            else if ((int)Convert.ToDouble(r.Correct_Option) <= 10000 && (int)Convert.ToDouble(r.Correct_Option) > 1000)
                            {
                                r.Option_1 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(1000)).ToString();
                                r.Option_2 = ((int)Convert.ToDouble(r.Correct_Option) - rand.Next(1000)).ToString();
                                r.Option_3 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(1000)).ToString();
                            }
                            else if ((int)Convert.ToDouble(r.Correct_Option) <= 100000 && (int)Convert.ToDouble(r.Correct_Option) > 10000)
                            {
                                r.Option_1 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(10000)).ToString();
                                r.Option_2 = ((int)Convert.ToDouble(r.Correct_Option) - rand.Next(10000)).ToString();
                                r.Option_3 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(10000)).ToString();
                            }
                            else
                            {
                                r.Option_1 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(1000)).ToString();
                                r.Option_2 = ((int)Convert.ToDouble(r.Correct_Option) - rand.Next(1000)).ToString();
                                r.Option_3 = ((int)Convert.ToDouble(r.Correct_Option) + rand.Next(1000)).ToString();
                            }
                        }
                        else
                        {
                            if (Convert.ToDouble(r.Correct_Option) <= 100)
                            {
                                r.Option_1 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 10))).ToString("N2");
                                r.Option_2 = ((Convert.ToDouble(r.Correct_Option) - (rand.NextDouble() * 10))).ToString("N2");
                                r.Option_3 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 10))).ToString("N2");
                            }
                            else if (Convert.ToDouble(r.Correct_Option) <= 1000 && Convert.ToDouble(r.Correct_Option) > 100)
                            {
                                r.Option_1 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 100))).ToString("N2");
                                r.Option_2 = ((Convert.ToDouble(r.Correct_Option) - (rand.NextDouble() * 100))).ToString("N2");
                                r.Option_3 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 100))).ToString("N2");
                            }
                            else if (Convert.ToDouble(r.Correct_Option) <= 10000 && Convert.ToDouble(r.Correct_Option) > 1000)
                            {
                                r.Option_1 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 1000))).ToString("N2");
                                r.Option_2 = ((Convert.ToDouble(r.Correct_Option) - (rand.NextDouble() * 1000))).ToString("N2");
                                r.Option_3 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 1000))).ToString("N2");
                            }
                            else if (Convert.ToDouble(r.Correct_Option) <= 100000 && Convert.ToDouble(r.Correct_Option) > 10000)
                            {
                                r.Option_1 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 10000))).ToString("N2");
                                r.Option_2 = ((Convert.ToDouble(r.Correct_Option) - (rand.NextDouble() * 10000))).ToString("N2");
                                r.Option_3 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 10000))).ToString("N2");
                            }
                            else
                            {
                                r.Option_1 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 1000))).ToString("N2");
                                r.Option_2 = ((Convert.ToDouble(r.Correct_Option) - (rand.NextDouble() * 1000))).ToString("N2");
                                r.Option_3 = ((Convert.ToDouble(r.Correct_Option) + (rand.NextDouble() * 1000))).ToString("N2");
                            }
                            // Console.WriteLine("The new double options are: " + r.Option_1 + " " + r.Option_2 + " " + r.Option_3);
                        }                       

                        hasDuplicates = (r.Option_1.Equals(r.Option_2) || r.Option_1.Equals(r.Option_3) || r.Option_2.Equals(r.Option_3) ||
                                         r.Correct_Option.Equals(r.Option_1) || r.Correct_Option.Equals(r.Option_2) || r.Correct_Option.Equals(r.Option_3));
                    } while (hasDuplicates);                   

                    Rows.Add(string.Format("('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')", r.Question_ID, r.Option_1, r.Option_2, r.Option_3, r.Correct_Option, r.Question_Title));
                }
                sCommand.Append(string.Join(",", Rows));
                sCommand.Append(";");


                using (MySqlCommand myCmd = new MySqlCommand(sCommand.ToString(), conn))
                {
                    myCmd.CommandType = System.Data.CommandType.Text;
                    myCmd.ExecuteNonQuery();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error occured: " + ex.ToString());
            }

            conn.Close();
            Console.WriteLine("SQL command successfully executed");
            return result;
        }


        static void Main(string[] args)
        {
            string dir = Directory.GetCurrentDirectory();

            using (var reader = new StreamReader(dir + @"\questions.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Entry>().ToList();
                               
                InsertToQuestion(records);
                Thread.Sleep(30000);
                InsertToCampaignQuestion(records);               
            }

            string temp = Console.ReadLine();
        }

        public class Entry
        {
            public string Question_ID { get; set; }
            public string World_ID { get; set; }
            public string Section_ID { get; set; }
            public string Level_ID { get; set; }
            public string Question_Title { get; set; }
            public string Option_1 { get; set; }
            public string Option_2 { get; set; }
            public string Option_3 { get; set; }
            public string Correct_Option { get; set; }
        }

        
    }
}
