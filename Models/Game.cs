
namespace RockPaperScissorsAPI.Models

{
    public enum MoveAction { None, Rock, Paper, Scissors}
    public class Game
    {
        public System.Guid Id { get; set; }
        public string? P1Name { get; set; }
        public string? P2Name { get; set; }
        public MoveAction P1Move = MoveAction.None;
        public MoveAction P2Move = MoveAction.None;
        public string? P1Outcome { get; set; }
        public string? P2Outcome { get; set; }
    }
}
