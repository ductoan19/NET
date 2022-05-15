using Backend.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBaseDT
    {
        public HomeController(AppDbContext context, IConfiguration configuration): base (context, configuration)
        {

        }
    }
}
