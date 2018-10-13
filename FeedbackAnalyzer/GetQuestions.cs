using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FeedbackAnalyzer
{

    public class Questions
    {
        public string Id { get; set; }
        public string DisplayType { get; set; }
    }

    class GetQuestions
    {

        public ArrayList getQuestions()
        {
            ApiClient api = new ApiClient(ApiConstants.QUESTIONS_API, "GET", "", false, true);
            string apiResponse = api.GetResponse();
            if (string.IsNullOrEmpty(apiResponse))
                throw new ApiException("No Data Available");

            var questions = JsonConvert.DeserializeObject<List<Questions>>(apiResponse);
            ArrayList qList = new ArrayList();

            foreach (var q in questions)
            {
                string id = q.Id.ToString();
                String type = q.DisplayType.ToString().Trim().ToUpper();
                if (type.Equals("MULTILINETEXT"))
                    qList.Add(id);
            }
            bool flag = qList.Contains("59f5d6a7c5b1931290b89913");

            return qList;


        }
    }
}
