using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class HiddenWord
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public HiddenWord() { }
        public HiddenWord(int id, string word)
        {
            Id = id;
            Word = word;
        }
    }
}
