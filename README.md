# RestAPI for Rock Paper Scissor in C#

Start the API directly from Visual Studio 2022.

Testing can be carried out through Swagger, or through any program that allows sending of HTTP-requests to access the API.


The following rest calls can be made:

	  POST /api/games

	  POST /api/games/{id}/join

	  POST /api/games/{id}/move

	  GET /api/games/{id}
  
All POST requests are to be made as JSON.
A sucessful request returns Status Code 200 with following information in String format.

## Using the API
### New Game 

`POST /api/games`

Give a player name in the request body:

{
"name":"player1"
}

Sucessful creation yeilds a GUID game id.

### Join game

`POST /api/games/{id}/join`

Join a created game using the received game id and new player name in the request-body:

{
"id": "game id", 
"name": "player2"
}

### Make a move

`POST api/games/{id}/move`

Choose a move using player name, game id and chosen move in the request-body. Accepted moves are "Rock", "Paper", or "Scissors"; case sensitive:

{
"id": "game id", 
"name": "player1", 
"move": "Rock"
}

### Check results

`GET /api/games/{id}`

Returns a given game's outcome. If all move have not been made, returns Status code 404 "Awaiting moves".
