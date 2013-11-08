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
      if (String.IsNullOrEmpty(languageHeader)) return null;

      var languageArray = languageHeader.Split(',').ToList();
      languageArray = languageArray.Select(la => la.ToLowerInvariant()).ToList();
      var languageRegex = new Regex(@"([a-z]{1,8}(-[a-z]{1,8})?)\s*(;\s*q\s*=\s*(1|0\.[0-9]+))?");

      var languagePreferences = new List<Language>();

      foreach (var language in languageArray)
      {
        if(!languageRegex.Match(language).Success) continue;

        var languageMatches = languageRegex.Match(language).Groups;

        try
        {
          CultureInfo.GetCultureInfo(languageMatches[1].Value);
        }
        catch (Exception)
        {
          return null;
        }

        var languagePreference = !String.IsNullOrEmpty(languageMatches[4].Value) ? new Language(languageMatches[1].Value.ToLower(), languageMatches[4].Value) : new Language(languageMatches[1].Value, 1.0d);

        languagePreferences.Add(languagePreference);
      }

      return languagePreferences.Any() ? languagePreferences.OrderByDescending(lp => lp.Preference).ToList() : null;
    } 
  }
}
