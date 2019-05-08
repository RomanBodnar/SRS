using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SRS.Core.Entities;

namespace SRS.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhraseController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            return new List<string>{ "add", "new" };
        }

        [HttpPost]
        public IActionResult Post(Phrase phrase)
        {
            // todo: redo to actually composing Uri object
            return this.Created($"api/phrase/{Guid.NewGuid()}", phrase);
        }
    }
}