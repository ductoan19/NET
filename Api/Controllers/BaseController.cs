using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public readonly IConfiguration _configuration;
        public readonly HttpClient _http;
        public BaseController(IConfiguration configuration)
        {
            _configuration = configuration;
            _http = new HttpClient();
        }
    }
}
