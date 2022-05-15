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
    public class ControllerBaseDT : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public IConfiguration Configuration { get; set; }
        protected readonly AppDbContext _db;

        public ControllerBaseDT(AppDbContext db, IConfiguration configuration)
        {
            _configuration = configuration;
            _db = db;
        }
    }
}
