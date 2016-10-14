using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tweetinvi;


    public class cTweet
    {
        private long id = -1;
        private string text = null;
        private DateTime createdAt;
        private Tweetinvi.Models.IUser createdBy = null;
        private long? replyToStatusID = -1;

        public cTweet(long vId, string vText, DateTime vCreatedAt, Tweetinvi.Models.IUser vCreatedBy, long? vReplyToStatusID)
        {
            id = vId;
            text = vText;
            createdAt = vCreatedAt;
            createdBy = vCreatedBy;
            replyToStatusID = vReplyToStatusID;
        }

    public long getID()
    {
        return id;
    }

    public long? getReplyToStatusID()
    {
        return replyToStatusID;
    }

    public string getText()
    {
        return text;
    }

    public string getUserAccount()
    {
        return createdBy.ScreenName;
    }

    }
