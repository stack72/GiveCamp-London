using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Xml.Linq;
using JetBrains.Annotations;
using LatestTwitter.Contracts.Services;
using LatestTwitter.Models;
using Orchard.Caching;
using Orchard.Services;

namespace LatestTwitter.Services
{
    [UsedImplicitly]
    public class CachedTweetRetrievalService
        : ITweetRetrievalService
    {
        protected readonly string CacheKeyPrefix = "B74EDE32-86E4-4A58-850B-016E6F595CF9_";

        protected ICacheManager CacheManager { get; private set; }
        protected IClock Clock { get;  private set; }

        public CachedTweetRetrievalService(ICacheManager cacheManager, IClock clock)
        {
            this.CacheManager = cacheManager;
            this.Clock = clock;
        }

        public List<TweetModel> GetTweetsFor(TwitterWidgetPart part)
        {
            // Build cache key
            var cacheKey = CacheKeyPrefix + part.Username;

            return CacheManager.Get(cacheKey, ctx =>
            {
                ctx.Monitor(Clock.When(TimeSpan.FromMinutes(part.CacheMinutes)));
                return RetrieveTweetsFromTwitterFor(part);
            });
        }

        protected List<TweetModel> RetrieveTweetsFromTwitterFor(TwitterWidgetPart part)
        {
            // Build tweet list
            var tweetList = new List<TweetModel>();

            if (!string.IsNullOrEmpty(part.Username))
            {
                // Fetch data
                WebClient twitterClient = new WebClient();
                string result =
                    twitterClient.DownloadString("http://api.twitter.com/1/statuses/user_timeline.xml?include_entities=1&screen_name=" +
                                                 part.Username);

                XElement xmlTweets = XElement.Parse(result);
                var tweets = from tweet in xmlTweets.Descendants("status")
                             select new TweetModel()
                             {
                                 Message = tweet.Element("text").Value,
                                 Username = tweet.Element("user").Element("screen_name").Value,
                                 Avatar = tweet.Element("user").Element("profile_image_url").Value,
                                 Timestamp = ToFriendlyDate(DateTime.ParseExact(tweet.Element("created_at").Value, "ddd MMM dd HH:mm:ss %zzzz yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal))
                             };
                tweetList = tweets.Take(part.Count).ToList();
            }

            // Return
            return tweetList;
        }

        protected string ToFriendlyDate(DateTime sourcedate)
        {
            // 1.
            // Get time span elapsed since the date.
            TimeSpan s = DateTime.Now.Subtract(sourcedate);

            // 2.
            // Get total number of days elapsed.
            int dayDiff = (int)s.TotalDays;

            // 3.
            // Get total number of seconds elapsed.
            int secDiff = (int)s.TotalSeconds;

            // 4.
            // Don't allow out of range values.
            if (dayDiff < 0 || dayDiff >= 31)
            {
                return null;
            }

            // 5.
            // Handle same-day times.
            if (dayDiff == 0)
            {
                // A.
                // Less than one minute ago.
                if (secDiff < 60)
                {
                    return "just now";
                }
                // B.
                // Less than 2 minutes ago.
                if (secDiff < 120)
                {
                    return "1 minute ago";
                }
                // C.
                // Less than one hour ago.
                if (secDiff < 3600)
                {
                    return string.Format("{0} minutes ago",
                        Math.Floor((double)secDiff / 60));
                }
                // D.
                // Less than 2 hours ago.
                if (secDiff < 7200)
                {
                    return "1 hour ago";
                }
                // E.
                // Less than one day ago.
                if (secDiff < 86400)
                {
                    return string.Format("{0} hours ago",
                        Math.Floor((double)secDiff / 3600));
                }
            }
            // 6.
            // Handle previous days.
            if (dayDiff == 1)
            {
                return "yesterday";
            }
            if (dayDiff < 7)
            {
                return string.Format("{0} days ago",
                    dayDiff);
            }
            if (dayDiff < 31)
            {
                return string.Format("{0} weeks ago",
                    Math.Ceiling((double)dayDiff / 7));
            }
            return sourcedate.ToString();
        }
    }
}
