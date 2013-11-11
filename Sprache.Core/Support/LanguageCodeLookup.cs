using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using Sprache.Core.Models;
using Sprache.Core.Services;

namespace Sprache.Core.Support
{

  /// <summary>
  /// Uses an xml document to perform language code lookups to translate languages like en to en-us, or whatever 
  /// configuration you need
  /// </summary>
  public class LanguageCodeLookup
  {
    /// <summary>
    /// Read-only property for accesing LanguageLookup information
    /// </summary>
    public LanguageLookup LanguageLookup
    {
      get
      {
        var cache = new CachingService();

        var languageLookup = cache.GetCachedItem("languageLookup") as LanguageLookup;

        if (languageLookup != null) return languageLookup;

        // Load up the languages into the cache
        languageLookup = LoadLanguageList();

        var languageLookupPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,GetLanguageFilePath());

        languageLookupPath = Path.GetFullPath(languageLookupPath);

        var listOfFiles = new List<String>
        {
          languageLookupPath
        };

        cache.AddToCache("languageLookup", languageLookup, CachePriority.NotRemovable, listOfFiles);

        return languageLookup;
      }
    }

    /// <summary>
    /// Loads in the language lookup that is specified in the configuration, then looks
    /// up the code based on the configuration
    /// </summary>
    /// <param name="aLanguageCode">Language code to check against</param>
    /// <returns>The appropriate language code</returns>
    public String LookupLanguage(String aLanguageCode)
    {
      var languageCode = aLanguageCode.ToLower();

      return CheckLookupMatch(LanguageLookup, languageCode);
    }

    private static String CheckLookupMatch(LanguageLookup languageLookup, String languageCode)
    {
      if (languageLookup.RootLanguages.Any(ll => ll.MappedLanguages.Any(ml => ml.LanguageCode.Equals(languageCode))))
      {
        return
          languageLookup.RootLanguages.Single(ll => ll.MappedLanguages.Any(ml => ml.LanguageCode.Equals(languageCode)))
            .LanguageCode;
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

      return String.Empty;
    }

    private static LanguageLookup LoadLanguageList()
    {
      var languageLookupFile = GetLanguageFilePath();

      // Load in the document from some source, specified by the configuration manager
      return JsonConvert.DeserializeObject<LanguageLookup>(File.ReadAllText(languageLookupFile));
    }

    private static String GetLanguageFilePath()
    {
      // Get the location of the language preference file
      var sprecheConfig = (SpracheConfiguration) ConfigurationManager.GetSection("sprache");

      return sprecheConfig.LanguageLookupSource;
    }
  }
}
