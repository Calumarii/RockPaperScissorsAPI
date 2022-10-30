using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissorsAPI.Models;
using RockPaperScissorsAPI.Data;

namespace RockPaperScissorsAPI.Controllers
{
    //TODO: Return JSON, not strings
    //TODO: try-catch
    //TODO: clean up repeating code

    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly ApiContext _context;

        public GameController(ApiContext context)
        {
            _context = context;
        }

        //Create a new game
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        public IActionResult NewGame(string name)
        {
            Game game = new Game();
            
            game.P1Name = name;
            
            _context.Games.Add(game);
            
            string guid = game.Id.ToString();
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.SaveChanges();

            return Ok("Game Id: " + guid);
        }

        //Second player joining a created game
        [HttpPost("{id}/join")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Join(string name, System.Guid id)
        {
            //TODO: Make it not possible to enter a non distict name

            var result = _context.Games.Find(id);

            if (result == null)
                return (NotFound());

            result.P2Name = name;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.SaveChanges();

            return (Ok("Game joined, make your move"));
        }

        //Posting a move
        [HttpPost("{id}/move")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Move(string name, string move, System.Guid id)
        {
            var result = _context.Games.Find(id);

            if (result == null)
                return NotFound("result == null");

            //TODO: make non-case sensitive
            if ((move == "Rock") || (move == "Paper") || (move == "Scissors"))
            {

                if (name == result.P1Name)
                    result.P1Move = move;

                else if (name == result.P2Name)
                    result.P2Move = move;

                else
                    return NotFound("Player name not found");
                
            }
            else            
                return BadRequest("Move not recognised, check spelling");
            

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.SaveChanges();

            return Ok("Move accepted");

        }

        //Checking/Showing results
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(string))]
        [ProducesResponseType(400)]
        [ProducesResponseType(404)]
        public IActionResult Get(System.Guid id, string name)
        {
            var result = _context.Games.Find(id);

            if (result == null)
                return NotFound();

            //TODO: Clean up repeated code into a method

            if (result.P1Move == null || result.P2Move == null)
            {
                return NotFound("Awaiting moves");
            }
            if (result.P1Move == result.P2Move)
            {
                result.P1Outcome = "Draw";
                result.P2Outcome = "Draw";
            }

            if (result.P1Move == "Rock" && result.P2Move == "Paper")
            {
                result.P2Outcome = "Winner";
                result.P1Outcome = "Loser";
            }

            if (result.P1Move == "Rock" && result.P2Move == "Scissors")
            {
                result.P2Outcome = "Loser";
                result.P1Outcome = "Winner";
            }

            if (result.P1Move == "Paper" && result.P2Move == "Rock")
            {
                result.P2Outcome = "Loser";
                result.P1Outcome = "Winner";
            }

            if (result.P1Move == "Paper" && result.P2Move == "Scissors")
            {
                result.P2Outcome = "Winner";
                result.P1Outcome = "Loser";
            }

            if (result.P1Move == "Scissors" && result.P2Move == "Rock")
            {
                result.P2Outcome = "Winner";
                result.P1Outcome = "Loser";
            }

            if (result.P1Move == "Scissors" && result.P2Move == "Paper")
            {
                result.P2Outcome = "Loser";
                result.P1Outcome = "Winner";
            }

            string finalResultP1 = result.P1Outcome + "! Your move: " + result.P1Move + ", " + result.P2Name + "'s move: " + result.P2Move;
            string finalResultP2 = result.P2Outcome + "! Your move: " + result.P2Move + ", " + result.P1Name + "'s move: " + result.P1Move;

            if (name == result.P1Name)
            {
                return Ok(finalResultP1);
            }
            if (name == result.P2Name)
            {
                return Ok(finalResultP2);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return NotFound();
        }
    }
}
