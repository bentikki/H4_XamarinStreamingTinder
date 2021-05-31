using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StreamingTinderWepApi.Entities;
using Dapper;
using Dapper.Contrib;
using System.Data.SqlClient;
using Dapper.Contrib.Extensions;
using StreamingTinderWepApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using StreaminTinderClassLibrary.Hashing;
using StreamingTinderClassLibrary.StreamingService;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StreamingTinderWepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StreamingPlatformsController : ControllerBase
    {
        private readonly StreaminTinderContext _context;

        public StreamingPlatformsController(StreaminTinderContext context)
        {
            this._context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StreamingPlatformEntity>>> Get()
        {
            return await _context.StreamingPlatforms.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StreamingPlatformEntity>> Get(int id)
        {
            var entity = await _context.StreamingPlatforms.FindAsync(id);

            if(entity == null)
            {
                return NotFound();
            }

            return entity;
        }

        [HttpGet("name/{value}")]
        public async Task<ActionResult<StreamingPlatformEntity>> GetByName(string value)
        {
            var entity = _context.StreamingPlatforms.Where(x => x.Name == value).FirstOrDefault();

            if (entity == null)
            {
                return NotFound();
            }

            return entity;
        }

    }
}
