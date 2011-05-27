using System.Collections.Generic;
using LatestTwitter.Models;
using Orchard;

namespace LatestTwitter.Contracts.Services
{
    public interface ITweetRetrievalService
        : IDependency
    {
        List<TweetModel> GetTweetsFor(TwitterWidgetPart part);
    }
}