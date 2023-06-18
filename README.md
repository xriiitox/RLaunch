# RLaunch

a simple roguelike instance manager for windows

made with .NET 7 in visual studio 2022 and switched to Rider in the middle of it

i would not recommend compiling this yourself at least for now

standalone download in releases ig?

app will auto update

im bad at markdown

## stuff that may happen eventually to this 

* version selector per-game
* variant selector for games like NetHack or DCSS (e.g. SLASHEM or bCrawl)
* quick public server selection/connection for NetHack and similar games with ssh
* actual settings menu and not just "delete installed games"
* progress bar for downloads?

## Contributing

Got a feature or game recommendation? Add an issue!

Make a PR if you want to program it directly yourself.

PLEASE READ GAME ADDITION GUIDELINES BELOW IF ADDING NEW GAME

## Game JSON Guidelines

basically copy one of the other json and just change the values lol

* make sure `Dir` points to the folder which would contain the exe

* the directory after `games` in `Dir` should also be `Name` without spaces + `Ver`

* get an example image for the game as well, put in `availableGames/exImgs` named correctly

* `Img` should be an icon for the game, image links from the Wikimedia Foundation will not work (for now) 
