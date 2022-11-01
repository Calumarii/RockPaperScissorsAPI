# RestAPI for Rock Paper Scissors in C#

Start the API directly from Visual Studio 2022.

Testing can be carried out through Swagger, or through any program, such as Postman (https://www.postman.com/), that allows sending of HTTP-requests to access the API.


The following rest calls can be made:

	  POST /api/games

	  POST /api/games/{id}/join

	  POST /api/games/{id}/move

	  GET /api/games/{id}
  
A successful request returns Status Code 200 with following information in String format.

## Using the API
### New Game 

`POST /api/games`

Post a player name as a query parameter. Example URL with name "player1":

https://localhost:7195/api/games?name=player1

Successful creation yields a GUID game id.

### Join game

`POST /api/games/{id}/join`

Join a created game using the received game id, and new name as query parameters. Names must be distinct, with 2 players only per game. 
Example URL with GUID 37695ef7-6892-437f-8018-8423b987378b, and name "player2":

https://localhost:7195/api/games/37695ef7-6892-437f-8018-8423b987378b/join?name=player2

### Make a move

`POST api/games/{id}/move`

Choose a move using player name, game id and chosen move as query parameters. Accepted moves are "Rock", "Paper", or "Scissors", and are not case-sensitive. 
Example URL with GUID 37695ef7-6892-437f-8018-8423b987378b, name "player1", and move "Rock":

https://localhost:7195/api/games/37695ef7-6892-437f-8018-8423b987378b/move?name=player1&move=Rock

### Check results

`GET /api/games/{id}`

Returns a given game's outcome. If all move have not been made, returns Status code 404 "Awaiting moves".
Example URL with GUID 37695ef7-6892-437f-8018-8423b987378b, and name "player2":

https://localhost:7195/api/games/37695ef7-6892-437f-8018-8423b987378b?name=player2

##Improvements to be made
	
Use JSON request body to input data.
Create a model for Player with properties Name and Move, for a clearer and more focused structure.
Create a resource file to hold the text that is shown to the user, making it possible to create different languages etc.
