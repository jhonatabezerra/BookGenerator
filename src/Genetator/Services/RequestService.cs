﻿using HtmlAgilityPack;
using System.Text.RegularExpressions;
using System.Web;

namespace Generator.Services
{
    /// <summary>Represents the Request services (GET, POST...).</summary>
    public class RequestService
    {
        /// <summary>Request page.</summary>
        /// <param name="url">Used to get the HTML body.</param>
        /// <returns>String of page.</returns>
        public static string GetPageString(string url, string xPath, List<string>? tags = null)
        {
            string decodedText = string.Empty;

            try
            {
                var web = new HtmlWeb();
                var doc = web.Load(url);
                var text = doc.DocumentNode.SelectSingleNode(xPath)?.InnerText.Trim();
                if (string.IsNullOrEmpty(text)) return decodedText;

                decodedText = HttpUtility.HtmlDecode(text);
                decodedText = RemoveTag(decodedText, tags);
            }
            catch (Exception exeption)
            {
                Console.WriteLine($"Error - {url}.");
                Console.WriteLine(exeption.Message);
            }

            return decodedText;
        }

        private static string RemoveTag(string decodedText, List<string>? tags)
        {
            if (tags == null || !tags.Any()) return decodedText;
            foreach (var tag in tags)
            {
                string pattern = $"\\s*{tag}";
                decodedText = Regex.Replace(decodedText, pattern, "");
            }

            return decodedText;
        }
    }
}