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
    public void ShouldReturnCorrectLanguageCode(string header, string expected)
    {
      var core = new Sprache.Core.Core();

      var result = core.GetLanguageCode(header);

      Assert.That(result,Is.EqualTo(expected));
    }
  }
}
