# Completition


Request

```
curl http://kitwo:8000/v1/chat/completions \
  -H "Content-Type: application/json" \
  -H "Authorization: Bearer $OPENAI_API_KEY" \
  -d '{
    "model": "/opt/models/casperhansen/mixtral-instruct-awq",
    "messages": [
      {
        "role": "user",
        "content": "You are a helpful assistant."
      },
      {
        "role": "assistant",
        "content": "Hello!"
      },
      {
        "role": "user",
        "content": "Was ist ein Haus?"
      }
    ]
  }'
```


Response

```
{
    "id": "cmpl-6d75e544b9d042cbbb4c9d52cd333cb4",
    "object": "chat.completion",
    "created": 1712859019,
    "model": "/opt/models/casperhansen/mixtral-instruct-awq",
    "choices": [{
            "index": 0,
            "message": {
                "role": "assistant",
                "content": " Ein Haus ist ein Gebäude, in dem Menschen wohnen oder arbeiten können. Es kann sich auf ein einzelnes Gebäude beziehen oder auf eine Gruppe von Gebäuden, wie zum Beispiel ein Reihenhaus oder eine Wohnanlage. Ein Haus kann viele verschiedene Räume und Einrichtungen haben, wie Küchen, Badezimmer, Schlafzimmer und Wohnzimmer. Es kann auch einen Hof, eine Garage oder einen Garten haben. Das Wort \"Haus\" kann auch allgemeiner verwendet werden, um ein Zuhause oder eine Unterkunft zu bezeichnen, unabhängig von der Art des Gebäudes."
            },
            "logprobs": null,
            "finish_reason": "stop",
            "stop_reason": null
        }
    ],
    "usage": {
        "prompt_tokens": 30,
        "total_tokens": 187,
        "completion_tokens": 157
    }
}
```


LLM log

```
Apr 11 19:21:33 kitwo vllm[1356]: INFO 04-11 19:21:33 async_llm_engine.py:508] Received request cmpl-81abe7cfa0cc42a696c703368ba37734: prompt: '<s>[INST] Du bist ein Assistent und antwortest in deutscher Sprache. [/INST]Was kann ich für dich tun?</s>[INST] was ist ein haus [/INST]</s>', sampling_params: SamplingParams(n=1, best_of=1, presence_penalty=0.0, frequency_penalty=0.0, repetition_penalty=1.0, temperature=0.2, top_p=1.0, top_k=-1, min_p=0.0, seed=None, use_beam_search=False, length_penalty=1.0, early_stopping=False, stop=[], stop_token_ids=[], include_stop_str_in_output=False, ignore_eos=False, max_tokens=32723, min_tokens=0, logprobs=None, prompt_logprobs=None, skip_special_tokens=True, spaces_between_special_tokens=True), prompt_token_ids: [1, 1, 733, 16289, 28793, 6335, 287, 392, 1796, 3348, 8441, 640, 2725, 28577, 374, 297, 16765, 15946, 1344, 28723, 733, 28748, 16289, 28793, 22648, 15557, 15364, 3446, 19132, 15013, 28804, 2, 733, 16289, 28793, 403, 3103, 1796, 295, 1899, 733, 28748, 16289, 28793, 2], lora_request: None.
Apr 11 19:21:34 kitwo vllm[1356]: INFO 04-11 19:21:34 metrics.py:218] Avg prompt throughput: 9.0 tokens/s, Avg generation throughput: 2.8 tokens/s, Running: 1 reqs, Swapped: 0 reqs, Pending: 0 reqs, GPU KV cache usage: 0.1%, CPU KV cache usage: 0.0%
Apr 11 19:21:39 kitwo vllm[1356]: INFO 04-11 19:21:39 metrics.py:218] Avg prompt throughput: 0.0 tokens/s, Avg generation throughput: 18.4 tokens/s, Running: 1 reqs, Swapped: 0 reqs, Pending: 0 reqs, GPU KV cache usage: 0.1%, CPU KV cache usage: 0.0%
Apr 11 19:21:43 kitwo vllm[1356]: INFO 04-11 19:21:43 async_llm_engine.py:120] Finished request cmpl-81abe7cfa0cc42a696c703368ba37734.
Apr 11 19:21:43 kitwo vllm[1356]: INFO:     ::1:36100 - "POST /v1/chat/completions HTTP/1.1" 200 OK
```
