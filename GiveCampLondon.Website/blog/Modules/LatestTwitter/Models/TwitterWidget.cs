using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Records;
using System.ComponentModel;

namespace LatestTwitter.Models
{
    public class TwitterWidgetRecord : ContentPartRecord
    {
        public virtual string Username { get; set; }
        public virtual int Count { get; set; }
        public virtual int CacheMinutes { get; set; }
        public virtual bool ShowAvatars { get; set; }
        public virtual bool ShowTimestamps { get; set; }
        public virtual bool ShowMentionsAsLinks { get; set; }
        public virtual bool ShowHashtagsAsLinks { get; set; }
    }

    public class TwitterWidgetPart : ContentPart<TwitterWidgetRecord>
    {
        [Required]
        public string Username
        {
            get { return Record.Username; }
            set { Record.Username = value; }
        }

        [Required]
        [DefaultValue("5")]
        [DisplayName("Number of Tweets to display")]
        public int Count
        {
            get { return Record.Count; }
            set { Record.Count = value; }
        }

        [Required]
        [DefaultValue("5")]
        [DisplayName("Time to cache Tweets (in minutes)")]
        public int CacheMinutes
        {
            get { return Record.CacheMinutes; }
            set { Record.CacheMinutes = value; }
        }

        public bool ShowAvatars
        {
            get { return Record.ShowAvatars; }
            set { Record.ShowAvatars = value; }
        }

        public bool ShowTimestamps
        {
            get { return Record.ShowTimestamps; }
            set { Record.ShowTimestamps = value; }
        }

        public bool ShowMentionsAsLinks
        {
            get { return Record.ShowMentionsAsLinks; }
            set { Record.ShowMentionsAsLinks = value; }
        }

        public bool ShowHashtagsAsLinks
        {
            get { return Record.ShowHashtagsAsLinks; }
            set { Record.ShowHashtagsAsLinks = value; }
        }
    }
}