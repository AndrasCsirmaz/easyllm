using Newtonsoft.Json;

namespace www1.Models;

public class V1ModelsResponse
{
    [JsonProperty("object")]
    public string obj { get; set; }
    public ModelData[] data { get; set; }
}

public class ModelData
{
    public string id { get; set; }
    [JsonProperty("object")]
    public string obj { get; set; }
    public int created { get; set; }
    public string owned_by { get; set; }
    public string root { get; set; }
    public object parent { get; set; }
    public Permission[] permission { get; set; }
}

public class Permission
{
    public string id { get; set; }
    [JsonProperty("object")]
    public string obj { get; set; }
    public int created { get; set; }
    public bool allow_create_engine { get; set; }
    public bool allow_sampling { get; set; }
    public bool allow_logprobs { get; set; }
    public bool allow_search_indices { get; set; }
    public bool allow_view { get; set; }
    public bool allow_fine_tuning { get; set; }
    public string organization { get; set; }
    public object group { get; set; }
    public bool is_blocking { get; set; }
}

