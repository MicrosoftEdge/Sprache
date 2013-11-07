using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Sprache.Core.Services
{
  /// <summary>
  /// Enumeration for cache priorities
  /// </summary>
  public enum CachePriority
  {
    /// <summary>
    /// The default cache priority
    /// </summary>
    Default,
    /// <summary>
    /// Marks cache item as not removable
    /// </summary>
    NotRemovable
  }

  /// <summary>
  /// Wrapper for interacting with the runtime MemoryCache
  /// </summary>
  public class CachingService
  {
    // Gets a reference to the default MemoryCache instance. 
    private static readonly ObjectCache Cache = MemoryCache.Default;
    private CacheEntryRemovedCallback _callback;
    private CacheItemPolicy _policy;

    /// <summary>
    /// Add an item to the cache with the designated cache priority
    /// </summary>
    /// <param name="cacheKeyName"></param>
    /// <param name="cacheItem"></param>
    /// <param name="cacheItemPriority"></param>
    /// <param name="filePath"></param>
    public void AddToCache(String cacheKeyName, Object cacheItem, CachePriority cacheItemPriority, List<String> filePath)
    {
      _callback = CachedItemRemovedCallback;
      _policy = new CacheItemPolicy
      {
        Priority = (cacheItemPriority == CachePriority.Default) ? CacheItemPriority.Default : CacheItemPriority.NotRemovable,
        AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(10.00),
        RemovedCallback = _callback
      };

      _policy.ChangeMonitors.Add(new HostFileChangeMonitor(filePath));

      // Add inside cache 
      Cache.Set(cacheKeyName, cacheItem, _policy);
    }

    /// <summary>
    /// Retrieve an object from the cache
    /// </summary>
    /// <param name="cacheKeyName"></param>
    /// <returns></returns>
    public Object GetCachedItem(String cacheKeyName)
    {
      return Cache[cacheKeyName];
    }

    /// <summary>
    /// Remove an item from the cache
    /// </summary>
    /// <param name="cacheKeyName"></param>
    public void RemoveCachedItem(String cacheKeyName)
    {
      if (Cache.Contains(cacheKeyName))
      {
        Cache.Remove(cacheKeyName);
      }
    }

    private static void CachedItemRemovedCallback(CacheEntryRemovedArguments arguments)
    {
      // Log these values from arguments list 
      var strLog = String.Concat("Reason: ", arguments.RemovedReason.ToString(), "| Key - Name: ", arguments.CacheItem.Key, " | Value - Object:",
      arguments.CacheItem.Value.ToString())
      ;
    }
  }
}