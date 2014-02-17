using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Sprache.Core.Models;

namespace Sprache.Core.Support
{
  public class LanguageHeaderParser
  {

      /// <summary>
      /// Determines if a culture is supported by System.Globalization
      /// </summary>
      /// <param name="cultureCode"></param>
      /// <returns></returns>
      private bool CultureExists(string cultureCode)
      {
          try
          {
              CultureInfo.GetCultureInfo(cultureCode);
          }
          catch (Exception)
          {
              return false;
          }

          return true;
      }

      /// <summary>
      /// Parses a language header and returns supported languages
      /// </summary>
      /// <param name="languageHeader"></param>
      /// <returns></returns>
      public List<Language> Parse(String languageHeader)
    {
      if (String.IsNullOrEmpty(languageHeader)) return null;

      var languageArray = languageHeader.Split(',').ToList();
      languageArray = languageArray.Select(la => la.ToLowerInvariant()).ToList();
      var languageRegex = new Regex(@"([a-z]{1,8}(-[a-z]{1,8})?)[,geo]?\s*(;\s*q\s*=\s*(1|0\.[0-9]+))?");

      var languagePreferences = new List<Language>();

      foreach (var language in languageArray)
      {
        if(!languageRegex.Match(language).Success) continue;

        var languageMatches = languageRegex.Match(language).Groups;

          if (CultureExists(languageMatches[1].Value))
          {
              var languagePreference = !String.IsNullOrEmpty(languageMatches[4].Value) ? new Language(languageMatches[1].Value.ToLower(), languageMatches[4].Value) : new Language(languageMatches[1].Value, 1.0d);

              languagePreferences.Add(languagePreference);          
          }

      }

      return languagePreferences.Any() ? languagePreferences.OrderByDescending(lp => lp.Preference).ToList() : null;
    } 
  }
}
