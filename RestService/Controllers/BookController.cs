using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModelLib;

namespace RestService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private static List<Bog> data = new List<Bog>()
        {
            new Bog("En ny måde at spise ost", "Frank Cheese", 250, "A-34567891111"),
            new Bog("Kontorstole", "Ole Jensen", 153, "B-32567821131"),
            new Bog("Leve ude i skoven", "Bear Grylls", 793, "C-35567891551")

        };
        // GET: api/Book
        [HttpGet]
        public IEnumerable<Bog> Get()
        {
            return data;
        }

        // GET: api/Book/5
        [HttpGet("{id}", Name = "Get")]
        public Bog Get(string id)
        {
            return data.Find(c => c.Isbn13 == id);
        }

        // POST: api/Book
        [HttpPost]
        public void Post([FromBody] Bog value)
        {
            data.Add(value);
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody] Bog value)
        {
            Bog bog = Get(id);
            if (bog != null)
            {
                bog.Title = value.Title;
                bog.Author = value.Author;
                bog.Pagecount = value.Pagecount;
                bog.Isbn13 = value.Isbn13;
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            Bog bog = Get(id);
            if (bog != null)
            {
                data.Remove(bog);
            }
        }
    }
}
