using System;
using System.Data.SQLite;
using System.IO;

namespace emailQuotes
{

    class sqlite
    {
        #region variables

        SQLiteConnection sqLiteconnection = new SQLiteConnection("data source = C:/yourdatasource/yourdatafile.db3"); // < replace with your database path/file 
        SQLiteConnection mConnect;
        private string mTable;
        private string mEmailAddress;
        private string mFirstName;
        private string mLastName;
        private string mCategory;
        private string mDateAdd;
        private string mQuote;
        private string mCountDate;

        #endregion

        #region properties

        public string Table
        {
            get
            {
                return mTable;
            }

            set
            {
                mTable = value;

            }
        }
        public string EmailAddress
        {
            get { return mEmailAddress; }
            set { mEmailAddress = value; }
        }
        public string FirstName
        {
            get
            {
                return mFirstName;
            }

            set
            {
                mFirstName = value;

            }
        }
        public string LastName
        {
            get
            { return mLastName; }
            set
            { mLastName = value; }
        }
        public string Category
        {
            get
            { return mCategory; }
            set
            { mCategory = value; }
        }
        public string DateAdd
        {
            get
            { return mDateAdd; }
            set
            { mDateAdd = value; }
        }
        public string Quote
        {
            get
            { return mQuote; }
            set
            { mQuote = value; }
        }
        #endregion

        // DB Check 
        public void dbFileCheck()
        {
            FileInfo dbtest = new FileInfo(@"C:/yourdatasource/yourdatafile.db3"); // check file path. I need to change this to a variable or config setting

            if (dbtest.Exists == false)
            {
                Console.WriteLine("No data exists Buck Rogers. Let me try to download it from the server"); 
                Console.ReadLine();
                SQLiteConnection.CreateFile(@"C:/yourdatasource/yourdatafile.db3"); // create file
            }

            Console.WriteLine("file Exists");
            Console.ReadLine();


        }

        // add a single quote
        public string AddQuote(string mCategory, string mQuote, string mDateAdd)
        {

            try
            {
                // using these for now, need to switch to SQLite to LINQ. 
                SQLiteCommand sqLitecommand = new SQLiteCommand(sqLiteconnection);
                string insertQuote = "INSERT INTO quotes(category, quote, dateAdded) values (?,?,?)";

                sqLiteconnection.Open();
                SQLiteParameter pCategory = sqLitecommand.CreateParameter();
                SQLiteParameter pQuote = sqLitecommand.CreateParameter();
                SQLiteParameter pDateAdd = sqLitecommand.CreateParameter();
                sqLitecommand.Parameters.Add(pCategory);
                sqLitecommand.Parameters.Add(pQuote);
                sqLitecommand.Parameters.Add(pDateAdd);
                sqLitecommand.CommandText = insertQuote;

                using (SQLiteTransaction trans = sqLiteconnection.BeginTransaction())
                {
                    pCategory.Value = mCategory;
                    pQuote.Value = mQuote;
                    pDateAdd.Value = mDateAdd;

                    sqLitecommand.ExecuteNonQuery();

                    trans.Commit();
                }

                sqLiteconnection.Close();
                mTable = "success!";
                return Table;

            }
            catch
            {
                //mTable = "update failed dickface";
                SQLiteException ex = new SQLiteException();
                return ex.ResultCode.ToString(); ;
            }

        }

        // add a file of quotes
        public string AddQuoteFile(string file)
        {
            try
            {
                // using these for now, need to switch to SQLite to LINQ. 
                SQLiteCommand sqLitecommand = new SQLiteCommand(sqLiteconnection);
                string insertQuote = "INSERT INTO quotes(category, quote, dateAdded) values (?,?,?)";

                sqLiteconnection.Open();
                sqLitecommand.CommandText = insertQuote;

                var linePicker = File.ReadAllLines(file);

                using (SQLiteTransaction trans = sqLiteconnection.BeginTransaction())
                {
                    var counter = linePicker.Length;
                    for (int i = 0; i < counter; i++)
                    {
                        SQLiteParameter pCategory = sqLitecommand.CreateParameter();
                        SQLiteParameter pQuote = sqLitecommand.CreateParameter();
                        SQLiteParameter pDateAdd = sqLitecommand.CreateParameter();
                        sqLitecommand.Parameters.Add(pCategory);
                        sqLitecommand.Parameters.Add(pQuote);
                        sqLitecommand.Parameters.Add(pDateAdd);
                        pCategory.Value = "wisdom"; // adding one file at a time for now. 
                        pQuote.Value = linePicker[i].ToString();
                        pDateAdd.Value = DateTime.Now.ToString();
                        sqLitecommand.ExecuteNonQuery();
                        sqLitecommand.Parameters.Clear();
                    }
                    trans.Commit();
                }

                sqLiteconnection.Close();
                mTable = "success!";
                return Table;

            }
            catch (SQLiteException ex)
            {
                mTable = ex.ToString();

                return mTable;
            }
        }

    }
}
