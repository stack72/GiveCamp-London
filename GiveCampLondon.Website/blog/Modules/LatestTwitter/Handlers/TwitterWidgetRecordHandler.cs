using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Orchard.ContentManagement.Handlers;
using Orchard.Data;
using LatestTwitter.Models;

namespace LatestTwitter.Handlers
{
    public class TwitterWidgetRecordHandler : ContentHandler
    {
        public TwitterWidgetRecordHandler(IRepository<TwitterWidgetRecord> repository)
        {
            Filters.Add(StorageFilter.For(repository));
        }
    }
}