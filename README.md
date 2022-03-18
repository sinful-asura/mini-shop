# Mini Web Shop
This is a web project for university, deployed on github pages.
Running locally is possible by opening `index.html`.

This project uses a mini SPA framework with routing to eliminate code clutter and make things easier to manage.

Adding new routes is fairly easy as developer would just define a template inside `routes` folder and add that route inside `constants.js` in the `helpers` folder.

Router will do everything else.

Important note: `404.html` exists to handle github pages routes that don't exist and redirect back to `index.html`.

Considering this framework is very very very shallow since it is custom-made by me, it isn't possible to intercept the routes and routing can only be achieved from within the application.

That means, starting point will always be homepage, after which using links inside the app will re-route to the corresponding locations.

# Prerequisites

To run this project locally, you need to have `.NET Core 6` installed, and also `SQLLocalDB` for database.

To create a new database, use `sqllocaldb create MiniShop` command.

