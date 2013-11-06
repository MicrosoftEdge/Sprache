using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Sprache.Core.Models;

namespace Sprache.Core.Support
{
  public class LanguageHeaderParser
  {
    public List<Language> Parse(String languageHeader)
    {
      Debug.WriteLine(String.Format("Current Culture: {0}", Thread.CurrentThread.CurrentCulture.Name));
      Debug.WriteLine(String.Format("Request Accepts Language: {0}", languageHeader));

      if (!String.IsNullOrEmpty(languageHeader))
      {
        var languageArray = languageHeader.Split(',').ToList();
        languageArray = languageArray.Select(la => la.ToLowerInvariant()).ToList();
        var languageRegex = new Regex(@"([a-z]{1,8}(-[a-z]{1,8})?)\s*(;\s*q\s*=\s*(1|0\.[0-9]+))?");

        var languagePreferences = new List<Language>();

        foreach (var language in languageArray)
        {
          var languageMatches = languageRegex.Match(language).Groups;
          if (String.IsNullOrEmpty(languageMatches[1].Value)) continue;

          var languagePreference = !String.IsNullOrEmpty(languageMatches[4].Value) ? new Language(languageMatches[1].Value, languageMatches[4].Value) : new Language(languageMatches[1].Value, 1.0d);

          if (languagePreference.LanguageCode.Equals("de", StringComparison.CurrentCultureIgnoreCase))
          {
            languagePreference.LanguageCode = "de-de";
          }

          languagePreferences.Add(languagePreference);
        }

        Debug.WriteLine(String.Format("Found language preferences: {0}", String.Join(", ", languagePreferences.Select(lp => String.Format("{0} : {1}", lp.LanguageCode, lp.Preference)))));

        Thread.CurrentThread.CurrentCulture = new CultureInfo(languagePreferences[0].LanguageCode);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(languagePreferences[0].LanguageCode);
      }

      return null;
    } 
  }
}
