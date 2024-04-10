using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using www1.Models;

namespace www1.Controllers;

public class Q1Controller : Controller
{
    // GET
    public IActionResult Index()
    {
        var rq = HttpContext.Request;
        dynamic r = new
        {
            now = DateTime.Now
        };
        if (rq.HasFormContentType)
        {
            var q1 = rq.Form["qx"];
            if (q1 != "q1") return new JsonResult(new {error = "Invalid request qx != q1"});
            var query = rq.Form["qx"];
            r.query = query;

            {
                var wc = new WebClient();
                string x = wc.DownloadString("http://sample.de:8888/query");
                var y = JsonConvert.DeserializeObject<Api8888>(x);
                var z = wc.DownloadString($"http://{y.rip}:8000/v1/models");
                var m = JsonConvert.DeserializeObject<V1ModelsResponse>(z);
            }
        }

        return new JsonResult(r);
    }
}