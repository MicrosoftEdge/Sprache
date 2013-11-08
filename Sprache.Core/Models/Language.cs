using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Sprache.Core.Models
{
  public class Language
  {
    public String LanguageCode { get; set; }
    public CultureInfo CultureInfo { get; set; }
    public double Preference { get; set; }

    [ExcludeFromCodeCoverage]
    public Language() {}

    public Language(String languageCode, double preference)
    {
      LanguageCode = languageCode;
      Preference = preference;

      CultureInfo = new CultureInfo(LanguageCode);
    }

    public Language(string languageCode, string preference)
    {
      LanguageCode = languageCode;

      double preferenceDouble;

      Preference = double.TryParse(preference, out preferenceDouble) ? preferenceDouble : 0.0;

      CultureInfo = new CultureInfo(LanguageCode);
    }
  }
}
