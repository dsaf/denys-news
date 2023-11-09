# Developer Coding Test

Work in progress.

```mermaid
sequenceDiagram
    http://localhost:->>https://hacker-news.firebaseio.com: GET /v0/beststories.json
    https://hacker-news.firebaseio.com-->>http://localhost:: 200 OK

    loop For each best story ID
        http://localhost:->>https://hacker-news.firebaseio.com: GET /v0/item/{id}.json
        https://hacker-news.firebaseio.com-->>http://localhost:: 200 OK
    end
```