using System;
using System.Collections.Generic;
using Sprache.Core.Models;
using Sprache.Core.Support;

namespace Sprache.Core
{
  /// <summary>
  /// Entry class for Sprache
  /// </summary>
  public class Core
  {
    private static readonly LanguageHeaderParser HeaderParser = new LanguageHeaderParser();
    private static readonly LanguageCodeLookup CodeLookup = new LanguageCodeLookup();
    private const string DefaultLanguage = "en-us";

    /// <summary>
    /// Pass in a language header, get a language code that complies with the language rules you specified 
    /// </summary>
    /// <param name="languageHeader">String, the language header passed in with a request</param>
    /// <returns></returns>
    public String GetLanguageCode(String languageHeader)
    {
        if (!String.IsNullOrEmpty(languageHeader))
        {
            var languages = HeaderParser.Parse(languageHeader);
            return ProcessLanguages(languages);
        }
        else
            return DefaultLanguage;
    }

    private static String ProcessLanguages(IEnumerable<Language> languages)
    {

        foreach (var language in languages)
        {
            var lookup = CodeLookup.LookupLanguage(language.LanguageCode);

            if (!String.IsNullOrEmpty(lookup)) return lookup;
        }

        return DefaultLanguage;
    }
  }
}
