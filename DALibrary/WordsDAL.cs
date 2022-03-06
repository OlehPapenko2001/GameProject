using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DALibrary
{
    public class WordsDAL
    {
        SqlConnection conn;
        public WordsDAL()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        }
        public Queue<HiddenWord> GenerateWordsQueue()
        {
            SqlCommand cmdGetAllWords = new SqlCommand();
            Queue<HiddenWord> wordsQueue = new Queue<HiddenWord>();
            cmdGetAllWords.Connection = conn;
            cmdGetAllWords.CommandText = "proc_GetAllWords";
            cmdGetAllWords.CommandType = CommandType.StoredProcedure;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            try
            {
                conn.Open();
                SqlDataReader dr = cmdGetAllWords.ExecuteReader();
                while (dr.Read())
                {
                    wordsQueue.Enqueue(new HiddenWord());
                    wordsQueue.Last().Id = (int)dr[0];
                    wordsQueue.Last().Word = dr[1].ToString();
                }
                return wordsQueue;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Console.WriteLine("Sorry, program can't get words from DB");
                return new Queue<HiddenWord>();
            }
            finally { conn.Close(); }
        }
        public int AddWordToDB(string word)
        {
            int id = -1;
            SqlCommand cmdAddWord = new SqlCommand();
            cmdAddWord.Connection = conn;
            cmdAddWord.CommandText = "proc_AddWord";
            cmdAddWord.CommandType = CommandType.StoredProcedure;
            cmdAddWord.Parameters.Add("@Word", SqlDbType.VarChar, 20);
            cmdAddWord.Parameters.Add("@Id", SqlDbType.Int, 20);
            cmdAddWord.Parameters[0].Value = word;
            cmdAddWord.Parameters[1].Direction = ParameterDirection.Output;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            try
            {
                conn.Open();
                cmdAddWord.ExecuteScalar();
                id = Convert.ToInt32(cmdAddWord.Parameters[1].Value);
                return id;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Console.WriteLine("Sorry, program can't add word to db");
                return id;
            }
            finally { conn.Close(); }
        }
        public void RemoveWordFromDB(int Id)
        {
            SqlCommand cmdRemoveWord = new SqlCommand();
            cmdRemoveWord.Connection = conn;
            cmdRemoveWord.CommandText = "proc_RemoveWord";
            cmdRemoveWord.CommandType = CommandType.StoredProcedure;
            cmdRemoveWord.Parameters.Add("@Id", SqlDbType.Int);
            cmdRemoveWord.Parameters[0].Value = Id;
            if (conn.State == ConnectionState.Open)
                conn.Close();
            try
            {
                conn.Open();
                cmdRemoveWord.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Console.WriteLine("Sorry, program can't remove word from db");
            }
            finally { conn.Close(); }
        }
    }
}
