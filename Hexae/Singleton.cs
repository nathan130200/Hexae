using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;

namespace Hexae
{
  public static class Singleton<T>
        where T : class
  {
    static Singleton()
    {
      Singleton<HttpClient>.Instance = new HttpClient(new HttpClientHandler
      {
        AllowAutoRedirect = true,
        AutomaticDecompression = DecompressionMethods.All,
        UseCookies = true,
        ServerCertificateCustomValidationCallback = (a, b, c, d) => true
      });
    }

    static readonly Dictionary<Type, object> Pool
        = new Dictionary<Type, object>();

    public static T Get()
    {
      lock (Pool)
        if (Pool.TryGetValue(typeof(T), out var obj))
          return (T)obj;

      return default;
    }

    public static T Instance
    {
      get => Get();
      set
      {
        var remove = value is null;

        lock (Pool)
        {
          if (remove && Pool.ContainsKey(typeof(T)))
            Pool.Remove(typeof(T));
          else
            Pool[typeof(T)] = value;
        }
      }
    }
  }
}
