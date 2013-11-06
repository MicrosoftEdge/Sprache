using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Sprache.Core.Models;

namespace Spreche.Utility
{
  class Program
  {
    static void Main(string[] args)
    {
      var exampleLanguageLookup = new LanguageLookup
      {
        RootLanguages = new List<RootLanguage>
        {
          new RootLanguage
          {
            LanguageCode = "en-us",
            MappedLanguages = new List<MappedLanguage>
            {
              new MappedLanguage {LanguageCode = "en" },
              new MappedLanguage {LanguageCode = "en-gb" },
              new MappedLanguage {LanguageCode = "en-ca" }
            }
          }
        }
      };

      var json = JsonConvert.SerializeObject(exampleLanguageLookup, Formatting.Indented);

      File.WriteAllText(String.Format("{0}\\{1}", Directory.GetCurrentDirectory(), "exampleLookup.json"), json);
    }
  }
}
