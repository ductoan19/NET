using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public readonly IMemoryCache _cache;
        public readonly IConfiguration _configuration;
        public readonly HttpClient _http;
        public BaseController(IConfiguration configuration, IMemoryCache cache)
        {
            _cache = cache;
            MemoryCacheEntryOptions cacheOptions = new MemoryCacheEntryOptions();
            cacheOptions.AbsoluteExpiration = DateTime.Now.AddDays(5);
            cacheOptions.SlidingExpiration = TimeSpan.FromDays(5);
            cache.Set<string>("timestamp", DateTime.Now.ToString(), cacheOptions);

            _configuration = configuration;
            _http = new HttpClient();
        }
    }
}
