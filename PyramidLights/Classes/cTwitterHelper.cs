using Stream = Tweetinvi.Stream;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.IO;

public class cTwitterHelper
{
    private string consumerKey = "R3ek6L6RUUhaLpuRNoRZClrpr";
    private string consumerSecret = "FjBpP4H6mJS6h0QP1o6nEdApJ4LIbXtSq8GuP9sOCUdNW0v494";
    private string accessToken = "2746338399-VFPKcjtdg8WdVVcWG2NH9m4zH6TTgqkiI8zI77u";
    private string accessTokenSecret = "tTxVTfQDblDWGviHjQZhbYkdWWiyDaORTNVqP2RGY4zop";

    public long sendTweet(String vText)
    {
        Tweetinvi.Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);

        var publishedTweet = Tweetinvi.Tweet.PublishTweet(vText);
        var tweetId = publishedTweet.Id;
        System.Diagnostics.Debug.WriteLine("Tweet published with ID: " + tweetId.ToString());

        return tweetId;
    }
    //Open listening connecting to twitter account
    public void listenAndProcess(long vQuestionTweetID, cQuestion vQuestion)
    {
        BlockingCollection<cTweet> correctAnswers = new BlockingCollection<cTweet>(1000);
        cTriggerPyramid triggerPyramid = new cTriggerPyramid();
        // An action to consume the ConcurrentQueue.
        Action processQueue = () =>
        {
            System.Diagnostics.Debug.WriteLine("before extracting tweet");
            while (true)
            {
                cTweet tempAnswer = correctAnswers.Take();
                if (isTweetReply(tempAnswer, (ulong)vQuestionTweetID))
                {
                    long answerTweetID = tempAnswer.getID();
                    if (tempAnswer.getText().Contains(vQuestion.correctAnswer))
                    {
                        
                        Tweetinvi.Tweet.PublishTweetInReplyTo("@"+tempAnswer.getUserAccount() + " That's Correct! " +
                                                                vQuestion.followUpMessage, answerTweetID);
                    }
                    //trigger pyramid


                }
            }
        };

        // Start 1 concurrent consuming actions.
        Task.Run(processQueue);
        // Parallel.Invoke(action);

        Tweetinvi.Auth.SetUserCredentials(consumerKey, consumerSecret, accessToken, accessTokenSecret);
        var stream = Stream.CreateUserStream();
        stream.TweetCreatedByAnyoneButMe += (sender, args) =>
        {
            //When tweet received, evaluate to determine if it is an answer to a question
            //send return tweet
            //validate answer
            //add validated successful answer
          
            correctAnswers.TryAdd(new cTweet(args.Tweet.Id, args.Tweet.Text, args.Tweet.CreatedAt, args.Tweet.CreatedBy,
                                    args.Tweet.InReplyToStatusId));
            System.Diagnostics.Debug.WriteLine("A question reply has been found; the tweet is '" + args.Tweet + "'");
        };

        System.Diagnostics.Debug.WriteLine("before starting stream");


        stream.StartStream();
        System.Diagnostics.Debug.WriteLine("after starting stream before while");


    }

    private bool isTweetReply(cTweet vTweet, ulong vQuestionTweetID)
    {
        bool reply = false;
        ulong replyToStatusID = (ulong)vTweet.getReplyToStatusID();
        if (replyToStatusID == (ulong)vQuestionTweetID)
        {
            reply = true;
            System.Diagnostics.Debug.WriteLine("Validating tweet: " + vTweet.getID().ToString());
            System.Diagnostics.Debug.WriteLine("This tweet is a reply to: " + vTweet.getReplyToStatusID().ToString());
        }

        return reply;
    }

}