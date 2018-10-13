using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedbackAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                GetAnswers ansObj = new GetAnswers();
                string answers = ansObj.getUserAnswers();
                Dictionary<string, int> wordDic = Utils.getMostCommonOccurences(answers, 5); // filtering out the most common words from answers
                foreach (KeyValuePair<string, int> e in wordDic)
                {
                   Console.WriteLine("Word : " + e.Key + ", No. of Occurences : " + e.Value);
                   Console.ReadKey();
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.ReadKey();
            }
        }
    }
}
