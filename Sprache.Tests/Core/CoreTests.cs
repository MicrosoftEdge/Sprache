using NUnit.Framework;
namespace Sprache.Tests.Core
{
  [TestFixture]
  public class CoreTests
  {
    [TestCase("en-US,en;q=0.8,de;q=0.6,de-DE;q=0.4","en-us")]
    [TestCase("de;q=0.6,de-DE;q=0.4", "de-de")]
    [TestCase("de-ch;q=0.6,de-DE;q=0.4", "de-de")]
    [TestCase("ar,ar-sa;q=0.8","en-us")]
    [TestCase("en,en-gb;q=0.8", "en-us")]
    [TestCase("", "en-us")]
    [TestCase("en-US,en;q=0.8,ru;q=0.6,es;q=0.4,id;q=0.2,cs;q=0.2,ro;q=0.2,jv;q=0.2", "en-us")]
    [TestCase("en-us, en;q=0.7, *;q=0.01", "en-us")]
    [TestCase("en-AU,geo;q=0.7,ru;q=0.3", "en-au")]
    [TestCase("en-AU", "en-au")]
    [TestCase("fi-fi,fi;q=0.8,en-us;q=0.5,en;q=0.3", "en-us")]
    [TestCase("en-au,en-securid", "en-au")]
    public void ShouldReturnCorrectLanguageCode(string header, string expected)
    {
      var core = new Sprache.Core.Core();

      var result = core.GetLanguageCode(header);

      Assert.That(result,Is.EqualTo(expected));
    }

  }
}
