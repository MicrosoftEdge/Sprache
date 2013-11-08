using System;
using NUnit.Framework;
using Sprache.Core.Support;

namespace Sprache.Tests.Support
{
  [TestFixture]
  public class LanguageCodeLookupTests
  {
    [TestCase("en", "en-us")]
    [TestCase("DE-CH", "de-de")]
    [TestCase("de","de-de")]
    [TestCase("ar-sa","")]
    public void ShouldReturnProperLanguageCode(String original, String expected)
    {
      var lookup = new LanguageCodeLookup();

      var result = lookup.LookupLanguage(original);

      Assert.That(result, Is.EqualTo(expected));
    }

  }
}
