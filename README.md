# GatheringTheMagic

I made this project as a final project in a course on advanced C#. 

You don't need an account to get the cards from the official MTG database, and if you use the attached python script you'll not overuse the API. It'll take a couple of hours to finish,
because of the added delays.

When you use the python script included, you populate a database in .\SQLEXPRESS with card data.

You can then run the main project - a blazor WASM project that shows the cards in the database, with sorting on color and (MTG) sets.

The initial commit is the project that i handed in, and got good feedback on. The plan is to expand the project, with better interface (the sorting is heinous right now),
an option to make decks (with MTG legality in mind), and some way of getting new rulings for cards.

