﻿using System.Web.Mvc;

namespace GiveCampLondon.Website.Helpers
{
    public static class DisplayContentHelper
    {
        public static MvcHtmlString DisplayContent(this HtmlHelper helper, Content content)
        {
            if (content != null)
                return MvcHtmlString.Create(content.ToString());
            return MvcHtmlString.Empty;
        }
    }
}