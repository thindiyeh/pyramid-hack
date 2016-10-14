using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class cQuestion
{
    // If modifying these scopes, delete your previously saved credentials
    // at ~/.credentials/sheets.googleapis.com-dotnet-quickstart.json
    static string[] Scopes = { SheetsService.Scope.SpreadsheetsReadonly };
    static string ApplicationName = "Google Sheets API Pyramid Lights";

    public DateTime startDate;
    public int startHour;
    public int startMinutes;
    public DateTime endDate;
    public int endHour;
    public int endMinutes;
    public string questionText = null;
    public string answer1 = null;
    public string answer2 = null;
    public string answer3 = null;
    public string answer4 = null;
    public string correctAnswer = null;
    public string followUpMessage = null;
           
    public void getQuestion()
    {
        UserCredential credential;

        using (var stream =
            new FileStream("client_secret.json", FileMode.Open, FileAccess.Read))
        {
            string credPath = System.Environment.GetFolderPath(
                System.Environment.SpecialFolder.Personal);
            credPath = Path.Combine(credPath, ".credentials/sheets.googleapis.com-dotnet-quickstart.json");

            credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                GoogleClientSecrets.Load(stream).Secrets,
                Scopes,
                "user",
                CancellationToken.None,
                new FileDataStore(credPath, true)).Result;
            //Console.WriteLine("Credential file saved to: " + credPath);
        }

        // Create Google Sheets API service.
        var service = new SheetsService(new BaseClientService.Initializer()
        {
            HttpClientInitializer = credential,
            ApplicationName = ApplicationName,
        });

        // Define request parameters.
        /*
        String spreadsheetId = "1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms";
        String range = "Class Data!A1:k";
        */

        String spreadsheetId = "1FXcRFitIAiWQ_qEmnP0O2nLjFt3PHt_YRFOf_dlKZtg";

        //https://docs.google.com/spreadsheets/d/1FXcRFitIAiWQ_qEmnP0O2nLjFt3PHt_YRFOf_dlKZtg/edit#gid=0
        //String range = "Class Data!A2:E";
        String range = "Sheet1!A1:M";

        SpreadsheetsResource.ValuesResource.GetRequest request =
                service.Spreadsheets.Values.Get(spreadsheetId, range);

        // Prints the names and majors of students in a sample spreadsheet:
        // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
        ValueRange response = request.Execute();
        IList<IList<Object>> values = response.Values;
        if (values != null && values.Count > 0)
        {
           bool breakLoop = false;
            // Console.WriteLine("Date, Time, End Date, End Time, Question, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer, FollowupMessage");
                //public DateTime startDate;
                //public int startHour;
                //public int startMinutes;
                //public DateTime endDate;
                //public int endHour;
                //public int endMinutes;
                //public string questionText = null;
                //public string answer1 = null;
                //public string answer2 = null;
                //public string answer3 = null;
                //public string answer4 = null;
                //public string correctAnswer = null;
                //public string followUpMessage = null;
            //start at 1 to skip header row
            for (int i = 1; i< values.Count;i++)
            {
                var row = values[i];
                //// Print columns A to k, which correspond to indices 0 and 4.
                //Console.WriteLine("{0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}"
                //    , row[0], row[1], row[2], row[3], row[4], row[5], row[6], row[7], row[8], row[9], row[10]);
                if (breakLoop == false)
                {
                   
                    //public DateTime startDate;
                    //public int startHour;
                    //public int startMinutes;
                    //public DateTime endDate;
                    //public int endHour;
                    //public int endMinutes;
                    startDate = DateTime.Parse((string) row[0]);
                    startHour = Convert.ToInt32(row[1]);
                    startMinutes = Convert.ToInt32(row[2]);
                    endDate = DateTime.Parse((string)row[3]);
                    endHour = Convert.ToInt32(row[4]);
                    endMinutes = Convert.ToInt32(row[5]);
                    DateTime startDateTime = new DateTime(startDate.Year, startDate.Month, startDate.Day, startHour, startMinutes, 0);
                    DateTime endDateTime = new DateTime(endDate.Year, endDate.Month, endDate.Day, endHour, endMinutes,0);
                    if (DateTime.Now > startDateTime && DateTime.Now <= endDateTime)
                    {
                        breakLoop = true;
                        //public string questionText = null;
                        //public string answer1 = null;
                        //public string answer2 = null;
                        //public string answer3 = null;
                        //public string answer4 = null;
                        //public string correctAnswer = null;
                        //public string followUpMessage = null;
                        questionText = Convert.ToString(row[6]);
                        answer1 = Convert.ToString(row[7]);
                        answer2 = Convert.ToString(row[8]);
                        answer3 = Convert.ToString(row[9]);
                        answer4 = Convert.ToString(row[10]);
                        correctAnswer = Convert.ToString(row[11]);
                        followUpMessage = Convert.ToString(row[12]);
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("No data found.");
        }
        //Console.Read();

        //return values;
    }

    private void setQuestionValues(DateTime vStartDate, int vStartHour, int vStartMinutes, DateTime vEndDate, int vEndHour,
                                    int vEndMinutes, String vQuestionText, String vAnswer1, String vAnswer2, String vAnswer3, 
                                    String vAnswer4, String vCorrectAnswer, String vFollowUpMessage)
    {
             startDate = vStartDate;
   startHour = vStartHour;
     startMinutes = vStartMinutes;
    endDate = vEndDate;
   endHour = vEndHour;
    endMinutes = vEndMinutes;
    questionText = vQuestionText;
            answer1 = vAnswer1;
             answer2 = vAnswer2;
           answer3 = vAnswer3;
            answer4 = vAnswer4;
            correctAnswer =vCorrectAnswer;
            followUpMessage = vFollowUpMessage;
}
}


