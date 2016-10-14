using System;
using System.Collections.Generic;


    class PyramidLightsMain
    {
        static void Main(string[] args)
        {
            //TelemetryConfiguration.Active.DisableTelemetry = true;
            cTwitterHelper twitterHelper = new cTwitterHelper();
            cQuestion question = new cQuestion();
       
            //get question from googledocs
            question.getQuestion();

            //tweet question
         
            long questionTweetID = twitterHelper.sendTweet(question.questionText + " " + question.answer1 +
                                                            ", " + question.answer2 + ", " + question.answer3 +
                                                            ", or " + question.answer4 + "?" + " " + DateTime.Now.ToString());
            //listen for responses 
            twitterHelper.listenAndProcess(questionTweetID, question);

        }
    }

