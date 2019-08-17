# Nolita

## Problem

Notion has really horrible share links that are:

1. Not memorizable
2. Not communicate able 

## Solution

- A link redirection thingie that works off a publicly maintained file on [gist.github.com](http://gist.github.com) written in [TOML](https://github.com/toml-lang/toml)

### Spec

Gist Syntax:

- Group using tables

        [SideProjects]

- Keys are the short links. They **must** be hypen separated (kebab case)

        [SideProjects]
        nolita = 

- Values will be the actual link itself

        [SideProjects]
        nolita = https://www.notion.so/shrayasr/notion-shrayas-com-9534bed0b89340bf9248d17f75c4eeb7

- This would mean [notes.shrayas.com/nolita](http://notes.shrayas.com/nolita) would link to [this page](https://www.notion.so/shrayasr/notion-shrayas-com-9534bed0b89340bf9248d17f75c4eeb7)
