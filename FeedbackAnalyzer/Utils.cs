using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackAnalyzer
{
   
    class Utils
    {

        private static String[] pronouns = { "HE", "SHE", "HIS", "HIM", "HER" }; 

        private static String[] adverbs = { "STILL", "THEN", "FINALLY", "OTHERWISE", "INSTEAD" };

        private static String[] prepositions = { "OF", "FROM", "WAS", "IT", "AT", "AMONG", "TO", "IS", "FOR", "ON", "IN", "BUT","LIKE", "THE", "NOW" };

        // for above we can use NLP apis to reomve adverbs,preposition etc

        public static string removeCommonArtilces(String text)
        {
            foreach(String s in pronouns)
            {
                string textToFind = string.Format(@"\b{0}\b", s);
                text =  Regex.Replace(text, textToFind, "");
            }

            foreach (String s in adverbs)
            {
                string textToFind = string.Format(@"\b{0}\b", s);
                text = Regex.Replace(text, textToFind, "");
            }

            foreach (String s in prepositions)
            {
                string textToFind = string.Format(@"\b{0}\b", s); // finding the exact match
                text = Regex.Replace(text, textToFind, "");
            }
            text = Regex.Replace(text, @"( |\r?\n)\1+", "$1");  // removing all extra spaces
          //  Console.WriteLine("======================================================");
          //  Console.WriteLine(text);
            return text;

        }
  
        public static Dictionary<string, int> getMostCommonOccurences(String text, int count)
        {
           string words = removeCommonArtilces(text);

            var orderedWords = words
              .Split(' ')
              .GroupBy(x => x)
              .Select(x => new {
                  KeyField = x.Key,
                  Count = x.Count()
              })
             .OrderByDescending(x => x.Count)
             .Take(count);

            Dictionary<string, int> wordDic = orderedWords.ToDictionary(x => x.KeyField, x => x.Count);

            return wordDic;
        }
    }
}
