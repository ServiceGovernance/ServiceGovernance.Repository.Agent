# ServiceGovernance.Repository.Agent

[![Build status](https://ci.appveyor.com/api/projects/status/xpcj4pxtj6ftlxrv/branch/master?svg=true)](https://ci.appveyor.com/project/twenzel/servicegovernance-repository-agent/branch/master)
[![NuGet Version](http://img.shields.io/nuget/v/ServiceGovernance.Repository.Agent.svg?style=flat)](https://www.nuget.org/packages/ServiceGovernance.Repository.Agent/)
[![License](https://img.shields.io/badge/license-Apache-blue.svg)](LICENSE)

Provides an Agent (client) for the [ServiceRepository](https://github.com/ServiceGovernance/ServiceGovernance.Repository). This agent publishes the API documentation of your ASP.NET Core application to the repository.

## Usage

Either use the base NuGet package `ServiceGovernance.Repository.Agent` and provide an custom implementation for `IApiDescriptionProvider` or use the `ServiceGovernance.Repository.Agent.SwaggerV3` NuGet package with built-in support for Swagger.

Example configuration for using with Swagger (you only need `ServiceGovernance.Repository.Agent.SwaggerV3` package).

```CSharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
    });

    services.AddServiceRepositoryAgent(options => {
        options.Repository = new Uri("http://localhost:5005");
        options.ServiceIdentifier = "Api1";                
    }).UseSwagger("v1");
}

public void Configure(IApplicationBuilder app, IHostingEnvironment env)
{
    ...
    app.UseSwagger();    
    app.UseServiceRepositoryAgent();
}
```

## Configuration

It's also possible to provide these options via the configuration:

```CSharp
public void ConfigureServices(IServiceCollection services)
{
    ...
    services.AddServiceRepositoryAgent(options => Configuration.Bind("ServiceRepository", options));
}
```

```json
{
    "ServiceRepository": {
        "Repository": "https://myservicerepository.mycompany.com",
        "ServiceIdentifier": "Api1"
    }
}
```

## Background

This agent collects the Api Descriptions as [OpenApi document](https://github.com/Microsoft/OpenAPI.NET) and sends it to the [ServiceRepository](https://github.com/ServiceGovernance/ServiceGovernance.Repository) where it can be viewed among other Api documentations from other services.