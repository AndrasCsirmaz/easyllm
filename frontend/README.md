# Frontend

## Warning

Just a very, very dirty sample how to use the backend llm!

The two important files are

- Controllers/LlmController.cs
- Views/Home/Index.cshtml

## /opt/wwwtest/www1/appsettings.json

```
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*", "Urls": "http://*:80"

}

```

## Install as Service

/etc/systemd/system/www1.service
```
[Unit]
Description=www1

[Service]
WorkingDirectory=/opt/wwwtest/www1
ExecStart=/opt/wwwtest/www1/www1
SyslogIdentifier=www1

User=root

[Install]
WantedBy=multi-user.target
```

