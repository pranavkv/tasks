using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Collections;
using Newtonsoft.Json.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace FeedbackAnalyzer
{

    public class Response
    {
        public string QuestionId { get; set; }
        public string QuestionText { get; set; }
        public string TextInput { get; set; } 
        public int NumberInput { get; set; } 
    }

    public class Answer
    {
        public string LocationId { get; set; }
        public DateTime ResponseDateTime { get; set; }
        public List<Response> Responses { get; set; }
    }

    class GetAnswers
    {

        public string getUserAnswers()
        {
           JObject obj = new JObject();
            obj["afterdate"] = "2018-04-10T07:00:00.000Z";
            obj["beforedate"] = "2018-10-10T07:00:00.000Z";
            String requestData = obj.ToString();
            requestData = ""; // NOTE: filtering api request is not working it is giving response code 500
            //tried the same json equest from API documentation

           // Console.WriteLine(obj.ToString());

            ApiClient api = new ApiClient(ApiConstants.ANSWERS_API, "POST", requestData, false, true);
            string apiResponse = api.GetResponse();
            if (string.IsNullOrEmpty(apiResponse))
                throw new ApiException("No Data Available");

            var answerStructure = new { LocationId = string.Empty, ResponseDateTime = new DateTime(), Responses = new List<Response>() };
            var answers = JsonConvert.DeserializeObject<List<Answer>>(apiResponse);

            GetQuestions qObj = new GetQuestions();
            ArrayList qlist = qObj.getQuestions();    // gettting the question ids which are multilineText

            StringBuilder sb = new StringBuilder();
            foreach (var a in answers) { 
                foreach (var r in a.Responses)
                {
                    string answer = !string.IsNullOrEmpty(r.TextInput) ? r.TextInput : r.NumberInput.ToString();
                    if (qlist.Contains(r.QuestionId)) // taking only answers which are of multilineText Questions
                    {
                        sb.Append(answer);
                        sb.Append(" ");
                    }
                }
            }
           // Console.WriteLine(sb.ToString().ToUpper());
            return sb.ToString().ToUpper();
        }
       

    }
}
