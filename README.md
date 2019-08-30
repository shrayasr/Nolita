# Nolita

> Yet another link redirection program

## Problem

- Long links are hard to remember
- Bad links are hard to remember
- Links are hard to share

## Solution

- Create your own new links `¯\_(ツ)_/¯`

## Implementation

- Nolita works off using a gist file (private or public) as its repository of links
- The file is to be written in the [TOML](https://github.com/toml-lang/toml) format 
- Each link is defined as a [key value pair](https://github.com/toml-lang/toml#user-content-keyvalue-pair). Remember to quote all links
    ```toml
    pinboard = 'https://pinboard.in/u:shrayasr'
    ```
- Links can be subdivided into sections using [tables](https://github.com/toml-lang/toml#table)
    ```toml
    [Travel]
    valparai = '...'
    goa = '...'
    ```
- Visiting the domain its hosted on (say example.com) shows you the list of link redirections setup, grouped
- To force a reload, visit `example.com/reload?key=<KEY>`. The `KEY` here will be the one that is setup in the configuration as `SecretKey`
- Once the links are loaded, `example.com/<name-of-link>` will redirect you as per the definition 

## Caveats

- The file **has** to be named `links.toml` 

## Running it

- Clone the project
- Build it
- [Publish it](https://docs.microsoft.com/en-us/dotnet/core/deploying/deploy-with-cli) using any method of your choice
  - If you plan on having other dotnet core apps on the same box, I recommend a framework dependent deployable
  - If this would be your only dotnet core app, I recommend a self contained deployable
- [Host your service](https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/?view=aspnetcore-2.2)
- copy `appsettings.json` to `appsettings.Production.json` and fill up the `SecretKey` and `GistID` fields
- Set your DNS entries to point to the box where the service is hosted
