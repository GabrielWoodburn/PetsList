using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetList.Models
{
    public static class TagHelperExtensions
    {
        public static void AppendCssClass(this TagHelperAttributeList list,
            string newCssClasses)
        {
            string oldCssClasses = list["class"]?.Value?.ToString();
            string cssClasses = (string.IsNullOrEmpty(oldCssClasses)) ?
                newCssClasses : $"{oldCssClasses} {newCssClasses}";
            list.SetAttribute("class", cssClasses);
        }

        public static void BuildTag(this TagHelperOutput output, string tagName,
        string classNames)
        {
            output.TagName = tagName;
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.AppendCssClass(classNames);
        }

        public static void BuildLink(this TagHelperOutput output, string url,
        string classNames)
        {
            output.BuildTag("a", classNames);
            output.Attributes.SetAttribute("href", url);
        }
    }
}
