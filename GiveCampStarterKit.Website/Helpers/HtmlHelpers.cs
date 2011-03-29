using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace GiveCampStarterKit.Website.Helpers
{
    public static class HtmlHelpers
    {

        public static string CheckBoxList(this HtmlHelper helper, string name, IDictionary<string, string> items)
        {
            return CheckBoxList(helper, name, items, null, null);
        }

        public static string CheckBoxList(this HtmlHelper helper, string name, IDictionary<string, string> items, IDictionary<string, object> checkboxHtmlAttributes)
        {
            return CheckBoxList(helper, name, items, null, checkboxHtmlAttributes);
        }

        public static string CheckBoxList(this HtmlHelper helper, string name, IDictionary<string, string> items, IEnumerable<string> selectedValues)
        {
            return CheckBoxList(helper, name, items, selectedValues, null);
        }

        public static string CheckBoxList(this HtmlHelper helper, string name, IDictionary<string, string> items, IEnumerable<string> selectedValues, IDictionary<string, object> checkboxHtmlAttributes)
        {
            var selectListItems = from i in items
                                  select new SelectListItem
                                  {
                                      Text = i.Key,
                                      Value = i.Value,
                                      Selected = (selectedValues != null && selectedValues.Contains(i.Value))
                                  };

            return CheckBoxList(helper, name, selectListItems, checkboxHtmlAttributes);
        }

        public static string CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items)
        {
            return CheckBoxList(helper, name, items, null);
        }

        public static string CheckBoxList(this HtmlHelper helper, string name, IEnumerable<SelectListItem> items, IDictionary<string, object> checkboxHtmlAttributes)
        {
            var output = new StringBuilder();

            foreach (var item in items)
            {
                output.Append("<div class=\"fields\"><label>");
                var checkboxList = new TagBuilder("input");
                checkboxList.MergeAttribute("type", "checkbox");
                checkboxList.MergeAttribute("name", name);
                checkboxList.MergeAttribute("value", item.Value);

                // Check to see if it's checked
                if (item.Selected)
                    checkboxList.MergeAttribute("checked", "checked");

                // Add any attributes
                if (checkboxHtmlAttributes != null)
                    checkboxList.MergeAttributes(checkboxHtmlAttributes);

                checkboxList.SetInnerText(item.Text);
                output.Append(checkboxList.ToString(TagRenderMode.SelfClosing));
                output.Append("&nbsp; " + item.Text + "</label></div>");
            }

            return output.ToString();
        }

        public static IList<SelectListItem> ToSelectList<T>(this IEnumerable<T> itemsToMap, Func<T, string> textProperty, Func<T, string> valueProperty, Predicate<T> isSelected)
        {
            return itemsToMap.Select(item => new SelectListItem
                                                 {
                                                     Value = valueProperty(item), 
                                                     Text = textProperty(item), 
                                                     Selected = isSelected(item)
                                                 }).ToList();
        }

    }

}