using DALibrary;
using ModelsLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLibrary
{
    public class WordsBL
    {
        WordsDAL wordDA;
        public WordsBL()
        {
            wordDA = new WordsDAL();
        }
        public HiddenWord AddWord(string word)
        {
            try
            {
                int id = wordDA.AddWordToDB(word);
                return new HiddenWord(id, word);
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Console.WriteLine("Something went wrong");
            }
            return null;            
        }
        public void RemoveWord(int wordId)
        {
            try
            {
                wordDA.RemoveWordFromDB(wordId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Console.WriteLine("Something went wrong");
            }
        }
        public Queue<HiddenWord> CreateWordQueueFromDB()
        {
            try
            {
                Queue<HiddenWord> queue = wordDA.GenerateWordsQueue();
                return queue;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                Console.WriteLine("Something went wrong");
                return new Queue<HiddenWord>();
            }
        }
    }
}
