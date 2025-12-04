# OPUC Lifeline Application

This project is a Blazor Server web application for the **Oregon Public Utility Commission (OPUC)** Lifeline Online Application and Undeliverable Mail Automation project.

## Prerequisites

Before running the app, make sure you have the following installed:

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [Sass](https://sass-lang.com/install) – used to compile `.scss` to `.css`

## Running the Project

In two terminals, run the following commands:

### 1️⃣ Start the Blazor Server app

```bash
dotnet watch run
```

This will launch the app in watch mode. It rebuilds automatically when code changes.

### 2️⃣ Start the Blazor Server app

```bash
sass --watch wwwroot/scss/main.scss:wwwroot/css/styles.css --style=expanded

```

This continuously watches your SCSS files and recompiles them to styles.css whenever you make changes.

## Notes

- The compiled CSS file should not be manually edited - always modify .scss files.
- If you encounter issues with the Sass command, ensure sass is in your system path:

```bash
sass --version
```

- You can also install Sass via npm if needed:

```bash
npm install -g sass
```

## TODO as of (11/29/2025):

- Check that all inputs are valid before allowing applicant to progress in each step.
- Remove unused code
- Add address validation via USPS API (https://developers.usps.com/addressesv3)
- Desktop design optimization
- Accessibility Testing (WAVE, Lighthouse, etc)
