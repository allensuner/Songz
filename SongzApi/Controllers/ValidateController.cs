using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SongzCore;

namespace SongzApi.Controllers
{
    [Route("api/[controller]")]
    public class ValidateController : Controller
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            string artist = this.Request.Query["artist"];
            string track = this.Request.Query["track"];
            string userId = this.Request.Query["userId"];
            return new LyricApiHerokuAppImpl().GetWordSet(artist, track);
        }
    }
}
