Microsoft Developers HK Website Codebase
========================================

*Source code of [www.msdevshk.com](https://www.msdevshk.com)*

Getting Started
---------------

For development, we recommend [Visual Studio Code](https://code.visualstudio.com).

Also recommended are the following extensions:
 - C# `ms-vscode.csharp`
 - C# Extensions `jchannon.csharpextensions`
 - C# XML Documentation Comments `k--kato.docomment`

Other editors will work as well, but you'll miss out on the neat `.vscode` setup to easily start and debug the website.

If you don't have it already, install the the [.NET Core SDK](https://www.microsoft.com/net/download/core) for your
platform. We currently target .NET Core 1.1.

*The codebase is intentionally made to target ASP.NET Core, to allow development on both macOS as well as Windows. When
you contribute, ensure that the code is cross platform.*

Now you can open the project from Code by opening the root folder, and start from there.
Follow any prompts to prepare your C# tooling, and restore packages as needed.

Alternatively, you can open a terminal and execute these commands to run the website on your local development machine:

1. `dotnet restore`
2. `dotnet build`
3. Set the Development environment:
    - Windows: `set ASPNETCORE_ENVIRONMENT=Development`
    - macOS: `export ASPNETCORE_ENVIRONMENT=Development`
4. `dotnet run`

## SSL Setup

The website uses HTTPS to securely login users.

### macOS

Use the shell files provided in `Setup/macOS`:

1. `cd Setup/macOS`
1. `chmod u+x create-cert.sh` - grant the current user execute permissions.
2. `./create-cert.sh local.www.msdevshk.com PFXPASSWORD` - to create a self-signed CA certificate to be used as the
   certificate to host the website via HTTPS. *Ensure to change the __PFXPASSWORD__ parameter to a secure password.*
3. Install the SSL certificate (.crt) file in the login keychain of macOS:
    1. In Finder, open the `MSDevsHK.Website/certs/cert.crt` certificate file. Keychain Access will open showing the
       cert in the `login` keychain.
    2. Double-click the `local.www.msdevshk.com` certificate, and expand the Trust section.
    3. Set `Secure Sockets Layer (SSL)` to `Always Trust`.
    4. Quit the Keychain Access application, you will be prompted for your administrator password to save the changes.
       Enter your password and select `Update Settings`.

## Local DNS Setup

A specific local domain is used to prevent malicious access of the development SSL certificate.

### macOS

Use the bash script to manage the `/etc/hosts` file entries:

1. `cd Setup/macOS`
2. `chmod u+x manage-etc-hosts.sh` - grant the current user execute permissions.
3. `sudo ./manage-etc-hosts.sh add local.www.msdevshk.com` - add a new entry with administrator permissions.

## Application Secrets

To prevent that any sensitive information is stored in this repository, user secrets are used.
This is managed with the `dotnet user-secrets -h` tool that needs to be executed from the website project directory.

1. `cd MSDevsHK.Website`

The password to open the PFX file made in the 'SSL Setup' section is stored as a secret.

2. `dotnet user-secrets set Host:HttpsPfxPassword PFXPASSWORD` - *Ensure to change the __PFXPASSWORD__ parameter to the
    previously used secure password.*