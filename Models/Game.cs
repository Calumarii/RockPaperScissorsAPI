
namespace RockPaperScissorsAPI.Models
{
    public class Game
    {
        public System.Guid Id { get; set; }
        public string? P1Name { get; set; }
        public string? P2Name { get; set; }
        public string? P1Move { get; set; }
        public string? P2Move { get; set; }
        public string? P1Outcome { get; set; }
        public string? P2Outcome { get; set; }
    }
}
