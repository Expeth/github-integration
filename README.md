# Github Integration MS https://travis-ci.org/Expeth/github-integration.svg?branch=dev
[![Build Status](https://travis-ci.org/Expeth/github-integration.svg?branch=dev)](https://travis-ci.org/Expeth/github-integration)

This WebAPI is used for integration with GithubAPI, providing a lightweight endpoint for getting the repositories list. It pulls data from Github every 10 minutes (interval can be configured via appsettings) and returns data from a cache, so the response time is pretty well. The application isn't really useful in real life and was created only for education purposes.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes.

### Prerequisites

What things you need to install the software and how to install them

```
1. .Net 5.0 SDK - should be downloaded from the official Microsoft website.
2. Github OAuth Token - can be generated via Github Settings -> Developer Settings -> Personal access tokens
```

### Build and run

Firstly, clone the repository to any folder you wish:

```
git clone https://github.com/Expeth/github-integration.git
```

Then, adjust appsettings.json file to use your OAuth token.

The project has Docker support, so there are two ways on how to run it locally. Here is how to run in Kestrel:

```
dotnet build -c release -o out
cd out
dotnet GithubIntegration.Host.dll --Environment Development
```

And how to build and run it with Docker:

```
docker build -t github-integration .
docker run -d -p 50001:80 github-integration --Environment Development
```

## Built With

* [Swagger](https://swagger.io/) - The web UI for API endpoints calls
* [.Net 5.0](https://dotnet.microsoft.com/download/dotnet/5.0) - Platform
* [Serilog](https://serilog.net/) - Used for logging
* [MediatR](https://github.com/jbogard/MediatR/wiki) - Used for requests handling

## Authors

* **Kovetskiy Valeriy** - *Initial work* - [telegram](https://t.me/kovetskiy)

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
