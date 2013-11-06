using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Sprache.Core.Models;

namespace Sprache.Core.Support
{

  /// <summary>
  /// Uses an xml document to perform language code lookups to translate languages like en to en-us, or whatever 
  /// configuration you need
  /// </summary>
  public class LanguageCodeLookup
  {
    /// <summary>
    /// Loads in the language lookup that is specified in the configuration, then looks
    /// up the code based on the configuration
    /// </summary>
    /// <param name="aLanguageCode">Language code to check against</param>
    /// <returns>The appropriate language code</returns>
    public String LookupLanguage(String aLanguageCode)
    {
      var languageCode = aLanguageCode.ToLower();
      // Get the location of the language preference file
      var sprecheConfig = (SpracheConfiguration)ConfigurationManager.GetSection("sprache");

      var languageLookupFile = sprecheConfig.LanguageLookupSource;

      // Load in the document from some source, specified by the configuration manager
      var languageLookup = JsonConvert.DeserializeObject<LanguageLookup>(File.ReadAllText(languageLookupFile));

      return CheckLookupMatch(languageLookup, languageCode);
    }

    private static String CheckLookupMatch(LanguageLookup languageLookup, String languageCode)
    {
      if(languageLookup.RootLanguages.Any(ll => ll.MappedLanguages.Any(ml => ml.LanguageCode.Equals(languageCode))))
      {
        return languageLookup.RootLanguages.First(ll => ll.MappedLanguages.Any(ml => ml.LanguageCode.Equals(languageCode))).LanguageCode;
      }

      var rootLanguages =
        languageLookup.RootLanguages.Where(ll => ll.MappedLanguages.Any(ml => ml.LanguageCode.Contains("*")));

      var enumerable = rootLanguages as IList<RootLanguage> ?? rootLanguages.ToList();

      if (
        enumerable.Any(
          rl => rl.MappedLanguages.Any(ml => ml.LanguageCode.Substring(0, 2).Equals(languageCode.Substring(0, 2)))))
      {
        return enumerable.First(
          rl => rl.MappedLanguages.Any(ml => ml.LanguageCode.Substring(0, 2).Equals(languageCode.Substring(0, 2))))
          .LanguageCode;
      }

      return "en-us";
    }

  }
}
