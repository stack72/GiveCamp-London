using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using LatestTwitter.Models;
using LatestTwitter.Services;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Drivers;
using System.Net;
using System.Web.Caching;
using LatestTwitter.Contracts.Services;

namespace LatestTwitter.Drivers
{
    public class TwitterWidgetDriver 
        : ContentPartDriver<TwitterWidgetPart>
    {
        protected ITweetRetrievalService TweetRetrievalService { get; private set; }

        public TwitterWidgetDriver(ITweetRetrievalService tweetRetrievalService)
        {
            this.TweetRetrievalService = tweetRetrievalService;
        }

        // GET
        protected override DriverResult Display(
            TwitterWidgetPart part, string displayType, dynamic shapeHelper)
        {
            return ContentShape("Parts_TwitterWidget",
                () => shapeHelper.Parts_TwitterWidget(
                    Username: part.Username ?? "",
                    Tweets: TweetRetrievalService.GetTweetsFor(part),
                    ShowAvatars: part.ShowAvatars,
                    ShowTimestamps: part.ShowTimestamps,
                    ShowMentionsAsLinks: part.ShowMentionsAsLinks,
                    ShowHashtagsAsLinks: part.ShowHashtagsAsLinks));
        }

        // GET
        protected override DriverResult Editor(TwitterWidgetPart part, dynamic shapeHelper)
        {
            return ContentShape("Parts_TwitterWidget_Edit",
                () => shapeHelper.EditorTemplate(
                    TemplateName: "Parts/TwitterWidget",
                    Model: part,
                    Prefix: Prefix));
        }

        // POST
        protected override DriverResult Editor(
            TwitterWidgetPart part, IUpdateModel updater, dynamic shapeHelper)
        {
            updater.TryUpdateModel(part, Prefix, null, null);
            return Editor(part, shapeHelper);
        }
    }
}