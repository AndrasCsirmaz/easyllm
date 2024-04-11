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
