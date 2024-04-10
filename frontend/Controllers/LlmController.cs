using System.Net;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OpenAI;
using OpenAI.Chat;
using OpenAI.Models;
using www1.Components;
using www1.Models;

namespace www1.Controllers;

public class LlmController : Controller
{
    [HttpPost]
    public IActionResult Index()
    {
        var ts0 = DateTime.Now;
        Q1Response r = new Q1Response();
        var syncIOFeature = HttpContext.Features.Get<IHttpBodyControlFeature>();
        if (syncIOFeature != null)
        {
            syncIOFeature.AllowSynchronousIO = true;
        }
        var TR = new StreamReader(Request.Body).ReadToEnd();
        var qp = JsonConvert.DeserializeObject<Q1Params>(TR);
        var kiport = 8000;
        {
            var wc = new WebClient();
            Api8888 y = new Api8888
            {
                rip = "localhost"
            };
            
            var z = wc.DownloadString($"http://{y.rip}:{kiport}/v1/models");
            var m = JsonConvert.DeserializeObject<V1ModelsResponse>(z);
            var htmlquestion = new List<Message>
            {
                new Message(Role.User, "Du bist ein Assistent und antwortest in deutscher Sprache."),
                new Message(Role.Assistant, "Was kann ich für dich tun?"),
                new Message(Role.User, qp.query),
                new Message(Role.Assistant, ""),
            };
            var chatRequest = new ChatRequest(htmlquestion,
                new Model(m.data[0].id),
                //maxTokens: 64, 
                temperature: 0.2);
            
            r.ts1 = DateTime.Now;
            OpenAIAuthentication auth = new OpenAIAuthentication("Not need");
            OpenAIClientSettings settings = new OpenAIClientSettings($"{y.rip}:{kiport}");
            HttpClient myhttpclient = new HttpClient();
            using var api = new OpenAIClient(auth, settings, myhttpclient);
            var T = api.ChatEndpoint.GetCompletionAsync(chatRequest);
            T.Wait();
            var llmresponse = T.Result;
            r.llmid = llmresponse.Model;
            r.llmmessage = llmresponse.FirstChoice.Message.ToString();
        }
        r.ts2 = DateTime.Now;
        //(DbInstance.Create()).LogLlm(Request.HttpContext.Session.Id, ts0, qp.query, r);
        return new JsonResult(new { r });
    }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary>
///
///
/// 
/// </summary>
class Q1Params
{
    public string query { get; set; }
    public bool kione { get; set; }
    public bool kitwo { get; set; }
}

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// <summary>
///
///
/// 
/// </summary>
public class Q1Response
{
    public string llmid { get; set; }
    public string llmmessage { get; set; }
    public DateTime ts1 { get; set; }
    public DateTime ts2 { get; set; }
}