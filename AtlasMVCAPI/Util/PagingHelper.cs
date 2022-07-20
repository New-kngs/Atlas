using AtlasMVCAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace AtlasMVCAPI
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper html,
            PagingInfo info, Func<int, string> pageUrl)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 1; i <= info.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();
                tag.AddCssClass("page-link");

                TagBuilder li = new TagBuilder("li");
                if (i == info.CurrentPage)
                {
                    li.AddCssClass("page-item active");
                }
                else
                {
                    li.AddCssClass("page-item");
                }

                li.InnerHtml = tag.ToString();

                sb.Append(li.ToString());
            }

            return MvcHtmlString.Create(sb.ToString());
        }
    }
}