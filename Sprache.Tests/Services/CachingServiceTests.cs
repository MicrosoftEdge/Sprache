using NUnit.Framework;
using Sprache.Core.Services;

namespace Sprache.Tests.Services
{
  [TestFixture]
  public class CachingServiceTests
  {
    [TestCase]
    public void ShouldAddItemToCache()
    {
      var cache = new CachingService();

      cache.AddToCache("test","test",CachePriority.Default,null);

      var result = cache.GetCachedItem("test");

      Assert.That(result,Is.Not.Null);
    }

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
