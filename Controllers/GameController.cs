using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RockPaperScissorsAPI.Models;
using System.Globalization;
using System.Collections;
using System;
using System.Security.Cryptography.X509Certificates;

namespace RockPaperScissorsAPI.Controllers
{

    [Route("api/games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private static List<Game> games = new List<Game>();

       /// <summary>
       /// Create a game
       /// </summary>
       /// <param name="name"></param>
       /// <returns></returns>
        [HttpPost]
        public string NewGame(string name)
        {

            Game newGame = new Game();

            if (!ModelState.IsValid)
                return String.Format("Bad Request: {0}", ModelState);

            newGame.P1Name = name;
            
            games.Add(newGame);

            newGame.Id = Guid.NewGuid();
            
            string guid = newGame.Id.ToString();
            

            return String.Format("Game Id: {0}", guid);
        }

        /// <summary>
        /// Join a created game
        /// </summary>
        /// <param name="name"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/join")]
        public string Join(string name, System.Guid id)
        {
            var result = games.Where(x => x.Id == id).FirstOrDefault();

            if (!ModelState.IsValid)
                return String.Format("Bad Request: {0}", ModelState);


            if (result == null)
                return ("Game not found");

            if (result.P1Name == name)
                return ("Name already taken");

            result.P2Name = name;

            return ("Game joined, make your move");
        }

        /// <summary>
        /// Mave a move
        /// </summary>
        /// <param name="name"></param>
        /// <param name="move">
        /// The desired move. Options are as follows:
        ///
        ///     1 - Rock
        ///     2 - Paper
        ///     3 - Scissors
        /// </param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("{id}/move")]
        public string Move(string name, MoveAction move, System.Guid id)
        {
            var result = games.Where(x => x.Id == id).FirstOrDefault();

            if (result == null)
                return ("Game not found");

            if (!ModelState.IsValid)
                return String.Format("Bad Request: {0}", ModelState);

            if (move == MoveAction.None)
            {
                return ("Move not recognised");
            }
            if (name == result.P1Name)
            {

                result.P1Move = move;
            }

            else if (name == result.P2Name)
            {

                result.P2Move = move;
            }

            else
            {
                return ("Player name not found");

            }

            return ("Move accepted");
        }

        /// <summary>
        /// Checking/Showing results
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public String Get(System.Guid id, string name)
        {
            var result = games.Where(x => x.Id == id).FirstOrDefault();

            if (result == null)
                return ("Game not found");

            if (!ModelState.IsValid)
                return String.Format("Bad Request: {0}", ModelState);

            if (result.P1Move == MoveAction.None || result.P2Move == MoveAction.None)
            {
                return ("Awaiting moves");
            }

            switch (result.P1Move - result.P2Move)
            {
                case -2: 
                case 1:
                    result.P1Outcome = "Winner";
                    result.P2Outcome = "Loser";
                    break;

                case -1: 
                case 2:
                    result.P1Outcome = "Loser";
                    result.P2Outcome = "Winner";
                    break;

                    default:
                    result.P1Outcome = "Draw";
                    result.P2Outcome = "Draw"; 
                    break;
            }

            string finalResultP1 = String.Format("{0}! Your move: {1}, {2}'s move: {3}", result.P1Outcome, result.P1Move, result.P2Name, result.P2Move);
            string finalResultP2 = String.Format("{0}! Your move: {1}, {2}'s move: {3}", result.P2Outcome, result.P2Move, result.P1Name, result.P1Move);

            if (name == result.P1Name)
            {
                return (finalResultP1);
            }
            if (name == result.P2Name)
            {
                return (finalResultP2);
            }

            return ("Game not found");
        }

    }
}
