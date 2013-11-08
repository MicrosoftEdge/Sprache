using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Sockets;
using NUnit.Framework;
using Sprache.Core.Models;
using Sprache.Core.Support;

namespace Sprache.Tests.Support
{
  [TestFixture]
  public class LanguageHeaderParserTests
  {
    [TestCase]
    public void ShouldParseOutHeader()
    {
      const string header = "en-US,en;q=0.8,de;q=0.6,de-DE;q=0.4";

      var expected = new List<Language>
      {
        new Language("en-us", 1.0d),
        new Language("en", 0.8d),
        new Language("de", 0.6d),
        new Language("de-de", 0.4d)
      };

      var parser = new LanguageHeaderParser();

      var result = parser.Parse(header);

      for (var i = 0; i < result.Count; i++)
      {
        Assert.That(new LanguageComparer().Equals(result[i], expected[i]));
      }
    }

    [TestCase]
    public void ShouldReturnNullWithNoHeader()
    {
      var parser = new LanguageHeaderParser();
      var result = parser.Parse("");

      Assert.That(result,Is.Null);
    }

    [TestCase("fooBar768889,en;q=0.8,de;q=0.6,de-DE;q=0.4")]
    [TestCase("123")]
    [TestCase(";q=0.84")]
    public void ShouldReturnNullForMalformedHeader(string header)
    {
      var parser = new LanguageHeaderParser();
      var result = parser.Parse(header);

      Assert.That(result, Is.Null);
    }

    [ExcludeFromCodeCoverage]
    private class LanguageComparer : IEqualityComparer<Language>
    {
      public bool Equals(Language x, Language y)
      {
        if (x == null && y == null)
        {
          return true;
        }

        if (x == null || y == null)
        {
          return false;
        }

        return (x.LanguageCode == y.LanguageCode) && (x.Preference == y.Preference) && (x.CultureInfo.Name == y.CultureInfo.Name);
      }

      public int GetHashCode(Language obj)
      {
        throw new NotImplementedException();
      }
    }
  }
}
