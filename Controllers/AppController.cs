using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Model;
using Xabe.FFmpeg.Streams;
using Dapper;

namespace FfMpeg.Controllers
{
    [Route("api")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        private readonly SqlConnection _dapper;

        public AppController(IHostingEnvironment hostingEnvironment, SqlConnection dapper)
        {
            _env = hostingEnvironment;
            _dapper = dapper;
        }
        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {


            return Ok(_dapper.Query(@"
               select top 10 mol_id, name from mols where is_working = 1 and name_logon is not null order by mol_id;
            "));

        }


        [HttpGet("video")]
        public async Task<ActionResult<object>> GetVideo()
        {
            var file = Path.Combine(_env.WebRootPath, "SampleAudio_0.7mb.mp3");
            var outPath = Path.Combine(_env.WebRootPath, "test");
            var inf = await MediaInfo.Get(file);

            Conversion.ExtractAudio(file, outPath);


            return Ok(file);
        }



    }
}
