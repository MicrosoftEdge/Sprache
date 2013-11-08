using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Sprache.Core.Services;

namespace Sprache.Tests.Services
{
  [TestFixture]
  public class CachingServiceTests
  {
    [TestCase]
    public void ShouldRemoveItemFromCache()
    {
      var cache = new CachingService();

      cache.AddToCache("test","test",CachePriority.Default,null);

      cache.RemoveCachedItem("test");

      Assert.That(cache.GetCachedItem("test"),Is.Null);
    }
  }
}
