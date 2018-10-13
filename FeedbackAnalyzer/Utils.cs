using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackAnalyzer
{
   
    class Utils
    {

        private static String[] pronouns = { "he", "she", "his", "him", "her" }; 

        private static String[] adverbs = { "still", "then", "finally", "otherwise", "instead" };

        private static String[] prepositions = { "of", "from", "at", "among", "to", "for", "on", "in", "but","like", "except", "now" };

        // for above we can use NLP apis to reomve adverbs,preposition etc

        public static string removeCommonArtilces(String text)
        {
            foreach(String s in pronouns)
            {
                text.Replace(s, " ");
            }

            foreach (String s in adverbs)
            {
                text.Replace(s, " ");
            }

            foreach (String s in prepositions)
            {
                text.Replace(s, " ");

            }

            return text;
        }

        public static Dictionary<string, int> getMostCommonOccurences(String text, int count)
        {
           text = removeCommonArtilces(text);

            var orderedWords = text
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
