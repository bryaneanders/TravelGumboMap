/* This class contains methods for creating and reading from a
 * database to create the menus and images in the Map */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;


namespace TravelGumboMap
{
    class DatabaseManager
    {
        // objects to manage the SQLite database
        private SQLiteConnection sqlite_conn;
        private SQLiteCommand sqlite_cmd;
        private SQLiteDataReader sqlite_datareader;

        // the window
        TGMapWindow window;

        // sql filename
        string sqlFilename;

        // log file stream
        StreamWriter logFile;

        public DatabaseManager(TGMapWindow w, StreamWriter log)
        {
            this.window = w;
            this.logFile = log;

            // this is kinda sketchy, but this is still only in proof-of-concept land
            sqlFilename = "TGMap.sql";

            try
            {
                // create a new database connection:
                sqlite_conn = new SQLiteConnection("Data Source=database.db");

                // open the connection:
                sqlite_conn.Open();

                // create a new SQL command:
                sqlite_cmd = sqlite_conn.CreateCommand();

                if (new FileInfo("sqliteDb.db").Length == 0 )
                {
                    BuildDatabase();
                    CloseDatabase();
                }
            }
            catch (SQLiteException e)
            {
                // if I can't open the database
                logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + e.ToString() + "\n");
            }
        }

        // state menu creation query
        // grabs each states name and adds it to a list to be returned
        public List<string[]> BuildStates()
        {
            Regex alphaNum, tgUrl, file, date;

            alphaNum = new Regex("[a-zA-Z]*");
            tgUrl = new Regex(@"http://www.travelgumbo.com/|blank");
            file = new Regex("[a-zA-Z0-9]+\\.?[a-zA-Z0-9]+");
            date = new Regex("[a-zA-Z] [0-9]{1,2}, [0-9]{4}");

            try
            {
                string query = "SELECT * FROM states;";

                this.sqlite_cmd.CommandText = query;    
                this.sqlite_datareader = sqlite_cmd.ExecuteReader();

                List<string[]> stateProvList = new List<string[]>();

                string[] stateInfo;
                while (sqlite_datareader.Read())
                {
                    stateInfo = new string[5];
                    // perform rudimentery checks on each of the strings
                    stateInfo[0] = sqlite_datareader["country"].ToString();
                    if (!alphaNum.IsMatch(stateInfo[0]))
                    {
                        this.window.DisplayMessageBox(stateInfo[0] + " invalid country");
                        continue;
                    } stateInfo[1] = sqlite_datareader["stpv"].ToString();
                    if (!alphaNum.IsMatch(stateInfo[1]))
                    {
                        this.window.DisplayMessageBox(stateInfo[1] + " invalid stpv");
                        continue;
                    }

                    stateInfo[2] = sqlite_datareader["flagid"].ToString();
                    if (!file.IsMatch(stateInfo[2]))
                    {
                        this.window.DisplayMessageBox(stateInfo[2] + " invalid flagid");
                        continue;
                    }

                    stateInfo[3] = sqlite_datareader["statehoodyr"].ToString();
                    if (!date.IsMatch(stateInfo[3]))
                    {
                        this.window.DisplayMessageBox(stateInfo[3] + " invalid statehoodyr");
                        continue;
                    }

                    stateInfo[4] = sqlite_datareader["tgblogs"].ToString();
                    if (!tgUrl.IsMatch(stateInfo[4]))
                    {
                        this.window.DisplayMessageBox(stateInfo[4] + " invalid tgblogs");
                        continue;
                    }

                    stateInfo[1] = sqlite_datareader["stpv"].ToString();
                    stateInfo[2] = sqlite_datareader["flagid"].ToString();
                    stateInfo[3] = sqlite_datareader["statehoodyr"].ToString();
                    stateInfo[4] = sqlite_datareader["tgblogs"].ToString();
                    stateProvList.Add(stateInfo);
                }
                sqlite_datareader.Close();
                
                return stateProvList;
            }
            catch(SQLiteException e)
            {
                logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + e.ToString() + "\n");
                return new List<string[]>();
            }
        }

        // builds a list with information about countries
        public List<string[]> BuildCountries()
        {
            Regex alphaNum, tgUrl, file, date;

            alphaNum = new Regex("[a-zA-Z]*");
            tgUrl = new Regex(@"http://www.travelgumbo.com/");
            file = new Regex("[a-zA-Z0-9]+\\.?[a-zA-Z0-9]+");
            date = new Regex("[a-zA-Z] [0-9]{1,2}, [0-9]{4}");

            try
            {
                string query = "SELECT * FROM countries;";

                this.sqlite_cmd.CommandText = query;
                this.sqlite_datareader = sqlite_cmd.ExecuteReader();

                List<string[]> stateProvList = new List<string[]>();

                string[] countryInfo;
                while (sqlite_datareader.Read())
                {
                    countryInfo = new string[5];

                    // check the contents of each of the strings
                    countryInfo[0] = sqlite_datareader["region"].ToString();
                    if (!alphaNum.IsMatch(countryInfo[0]))
                    {
                        this.window.DisplayMessageBox(countryInfo[0] + " invalid");
                        continue;
                    }

                    countryInfo[1] = sqlite_datareader["country"].ToString();
                    if (!alphaNum.IsMatch(countryInfo[1]))
                    {
                        this.window.DisplayMessageBox(countryInfo[1] + " invalid");
                        continue;
                    }

                    countryInfo[2] = sqlite_datareader["flagid"].ToString();
                    if (!file.IsMatch(countryInfo[2]))
                    {
                        this.window.DisplayMessageBox(countryInfo[2] + " invalid");
                        continue;
                    }

                    countryInfo[3] = sqlite_datareader["founding_date"].ToString();
                    if (!date.IsMatch(countryInfo[3])) {
                        this.window.DisplayMessageBox(countryInfo[3] + " invalid");
                        continue;
                    }

                    countryInfo[4] = sqlite_datareader["tgblogs"].ToString();
                    if (!tgUrl.IsMatch(countryInfo[4]))
                    {
                        this.window.DisplayMessageBox(countryInfo[4] + " invalid");
                        continue;
                    }

                    stateProvList.Add(countryInfo);
                }
                sqlite_datareader.Close();

                return stateProvList;
            }
            catch (SQLiteException e)
            {
                logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + e.ToString() + "\n");
                return new List<string[]>();
            }
        }

                
        // Execute Insert and Create Commands when building the table
        private void ExecuteNonQuery(string command)
        {
            try
            {
                // load up a command
                sqlite_cmd.CommandText = command;
                // execute it
                sqlite_cmd.ExecuteNonQuery();
            }
            catch(SQLiteException e)
            {
                logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + e.ToString() + "\n");
            }
        }

        // build database
        private void BuildDatabase()
        {
            try
            {
                string[] sqlTextLines = File.ReadAllLines(sqlFilename);

                // Check the sql statement type
                Regex createOrInsert = new Regex("^(INSERT|CREATE)");

                for (int i = 0; i < sqlTextLines.Length; i++)
                {
                    ExecuteNonQuery(sqlTextLines[i]);
                    window.DisplayMessageBox("i");

                    // if a line is an insert or create, execture it
                    if (createOrInsert.IsMatch(sqlTextLines[i]))
                    {
                       /* ExecuteNonQuery(sqlTextLines[i]);
                        window.DisplayMessageBox("i");*/
                    }
                    else
                    {
                        window.DisplayMessageBox("line " + i + ": " + sqlTextLines[i]);
                    }

                }
            }
            catch(SQLiteException sqle)
            {
                logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + sqle.ToString() + "\n");
            }
            catch(Exception e)
            {
                logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + e.ToString() + "\n");
            }
        }

        // close the database
        public void CloseDatabase()
        {
            this.sqlite_conn.Close();
        }
        
        /* get the regional map for a country, not in use
        public string GetMapUrl(string region)
        {
            try
            {

                string query = string.Format("SELECT flagid FROM countries " +
                                             "WHERE country={0}", region);

                this.sqlite_cmd.CommandText = query;
                this.sqlite_datareader = sqlite_cmd.ExecuteReader();

                sqlite_datareader.Read();

                string mapQueryResults = sqlite_datareader["country"].ToString();

                sqlite_datareader.Close();

                return mapQueryResults;
            }
            catch(SQLiteException e)
            {
                logFile.WriteLine(Stopwatch.GetTimestamp().ToString() + ": " + e.ToString() + "\n");
                return "";
            }
        }*/
    }

}
