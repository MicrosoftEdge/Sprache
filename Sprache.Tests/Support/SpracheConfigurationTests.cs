using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sprache.Core.Support;

namespace Sprache.Tests.Support
{
  [TestFixture]
  public class SpracheConfigurationTests
  {
    [TestCase]
    public void ShouldLoadConfiguration()
    {
      var spracheConfiguration = (SpracheConfiguration)ConfigurationManager.GetSection("sprache");

      Assert.That(spracheConfiguration, Is.Not.Null);
    }

    [TestCase]
    public void ShouldHaveCorrectValueForLanguageLookupSource()
    {
      var spracheConfiguration = (SpracheConfiguration)ConfigurationManager.GetSection("sprache");

      Assert.That(spracheConfiguration.LanguageLookupSource, Is.EqualTo("exampleLookup.json"));
    }
  }
}
