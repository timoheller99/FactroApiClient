|Stable|Beta|Alpha|
|---|---|---|
|![release-stable-ci]|![release-beta-ci]|![release-alpha-ci]|
|![unit-test-stable-ci]|![unit-test-beta-ci]|![unit-test-alpha-ci]|
|![integration-test-stable-ci]|![integration-test-beta-ci]|![integration-test-alpha-ci]|
|![nightly-test-stable-ci]|![nightly-test-beta-ci]|![nightly-test-alpha-ci]|

# FactroApiClient
The **FactroApiClient** is a simple API wrapper written in C# to interact with the [Factro REST API][factro-api-url].

**Disclaimer:**
This project is in a very early stage of development.
That means that some features are not (completely) implemented yet.

You may visit the [Milestones][milestones-url] page to get more information on the progress.
Each milestone summarizes tasks, activities and the progress of its functionality.
The [AppointmentAPI milestone](https://github.com/timoheller99/FactroApiClient/milestone/1) contains everything related to the `Appointment API`.

## General info
[Factro][factro-url] is a project management software by [Schuchert Managementberatung GmbH & Co. KG][schuchert-url].

This client implements the [functionalities of the REST API][factro-api-docs-url] to provide an easier way to interact with the API.

## Features of the API
All features are listed in the APIs [documentation][factro-api-docs-url].

## Project management
There are three [GitHub Projects][projects-url] to visualize and organize the progress of this project.
The tasks are represented as [issues][issues-url] to simplify the tracking and organizing of tasks in the [roadmap][roadmap-project-url].
* [Roadmap][roadmap-project-url] contains all issues (tasks and feature requests).
* [Development][development-project-url] contains all pull requests.
* [Bug Tracker][bug-tracker-project-url] contains all bug tickets.

### [Milestones][milestones-url]
All [issues][issues-url] are linked to their [milestone][milestones-url].
These milestones contain every issue and pull request that is related to this milestone.

### [Releases][releases-url]
The CI generates a GitHub-Release everytime a pull request is merged into `develop`(alpha release), `beta`(beta release) or `main`(stable release).

### [Packages][packages-url]
With every release there is a new GitHub-Package pushed to the [Packages][packages-url] page of this repository.
This packages can be installed via nuget or a by downloading the `.nupkg` file from a **Release**. ([Getting started](#getting-started))

### [Bug tracking][bug-tracker-project-url]
Bugs can be reported by creating an issue from the [bug report template][bug-report-issue-template-url].

### Feature requests
Features can be requestd by creating an issue from the [feature request template][feature-request-issue-template-url].

## Getting started
To get an example of how the package can be consumed, you may take a look at the [FactroApiClient.Integration](https://github.com/timoheller99/FactroApiClient/tree/develop/tests/FactroApiClient.Integration) project.

### Setup NuGet feed
To install the package, the custom feed is needed.
This feed can be added via the Nuget.config file or the .NET CLI.

**Config file**

Create a new file called `nuget.config` (this name is case insensitive).
The content of the file should have following structure:
```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
    <packageSources>
        <add key="github" value="https://nuget.pkg.github.com/timoheller99/index.json" />
    </packageSources>
    <packageSourceCredentials>
        <github>
            <add key="Username" value="GITHUB_USERNAME" />
            <add key="ClearTextPassword" value="GITHUB_TOKEN" />
        </github>
    </packageSourceCredentials>
</configuration>
```
**.NET CLI**
```bash
dotnet nuget add source "https://nuget.pkg.github.com/timoheller99/index.json" -n "github" -u "GITHUB_USERNAME" -p "GITHUB_TOKEN" --store-password-in-clear-text
```
* Replace `GITHUB_USERNAME` with your GitHub username.
* Replace `GITHUB_TOKEN` with your GitHub token.

### Installing the package
After adding the feed to your nuget sources you have access to the package.

You can now add the package to your project.

```xml
<ItemGroup>
  <PackageReference Include="FactroApiClient" Version="1.0.0" />
</ItemGroup>
```

### Use the FactroApiClient
To use the package, you don't theoretically have to use dependency injection.

Unfortunately there is currently no support for using this package without dependency injection, but it's planned.
(There are some hacky procedures and workarounds but it should be supported natively of course ;) )

#### Store the Factro API token
You have to store you Factro API token to pass it to the client.
You can store the token in a config file or via environment variables.

**Config file**
```json
{
    "FactroApiClient":
    {
        "ApiToken": "YourFactroApiToken"
    }
}
```
**Environment variable**
```bash
export FactroApiClient__ApiToken="YourFactroApiToken"
```

#### Create a ConfigurationRoot
You have to create a `IConfigurationRoot` to get the API-Token from the config file or the environment variables.
```csharp
private static IConfigurationRoot CreateConfigurationRoot()
{
    return new ConfigurationBuilder()
        .AddJsonFile("config.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();
}
```

#### Register the services
After creating the `IConfigurationRoot` it can be passed to the `services.AddFactroApiClientServices()` method to register all necessary services for the `FactroApiClient`.
You can optionally setup logging too.
```csharp
public static IServiceCollection ConfigureServices(IServiceCollection services)
{
    // Do stuff here

    var configurationRoot = CreateConfigurationRoot();
    services.AddSingleton(configurationRoot);

    services.AddFactroApiClientServices(configurationRoot);
    return services;
}
```

#### Use the services
After creating the `ServiceProvider` the services can be used by getting it from the `ServiceProvider`
```csharp
var appointmentApi = serviceProvider.GetRequiredService<IAppointmentApi>();
```






[schuchert-url]: https://www.schuchert.de/
[factro-url]: https://www.factro.de/
[factro-api-url]: https://www.factro.de/blog/factro-api/
[factro-api-docs-url]: https://cloud.factro.com/api/core/docs/

[issues-url]: https://github.com/timoheller99/FactroApiClient/issues
[milestones-url]: https://github.com/timoheller99/FactroApiClient/milestones
[releases-url]: https://github.com/timoheller99/FactroApiClient/releases
[packages-url]: https://github.com/timoheller99/FactroApiClient/packages/504267

[bug-report-issue-template-url]: https://github.com/timoheller99/FactroApiClient/issues/new?assignees=timoheller99&labels=bug&template=bug_report.md&title=
[feature-request-issue-template-url]: https://github.com/timoheller99/FactroApiClient/issues/new?assignees=timoheller99&labels=enhancement&template=feature_request.md&title=

[projects-url]: https://github.com/timoheller99/FactroApiClient/projects
[roadmap-project-url]: https://github.com/timoheller99/FactroApiClient/projects/1
[development-project-url]: https://github.com/timoheller99/FactroApiClient/projects/2
[bug-tracker-project-url]: https://github.com/timoheller99/FactroApiClient/projects/3

[release-alpha-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Release%20CI/badge.svg?branch=develop
[release-beta-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Release%20CI/badge.svg?branch=beta
[release-stable-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Release%20CI/badge.svg?branch=main

[unit-test-alpha-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Unit%20Tests/badge.svg?branch=develop
[unit-test-beta-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Unit%20Tests/badge.svg?branch=beta
[unit-test-stable-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Unit%20Tests/badge.svg?branch=main

[integration-test-alpha-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Integration%20Tests/badge.svg?branch=develop
[integration-test-beta-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Integration%20Tests/badge.svg?branch=beta
[integration-test-stable-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Integration%20Tests/badge.svg?branch=stable

[nightly-test-alpha-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Nightly%20Tests/badge.svg?branch=develop
[nightly-test-beta-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Nightly%20Tests/badge.svg?branch=beta
[nightly-test-stable-ci]: https://github.com/timoheller99/FactroApiClient/workflows/Nightly%20Tests/badge.svg?branch=main
