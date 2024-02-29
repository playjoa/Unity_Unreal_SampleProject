using Gameplay.GameModeSystem.Data;

namespace Gameplay.GameModeSystem.Utils
{
    public static class EndGameExtensions
    {
        public static string GetEndGameSubTitle(this GameEndReason gameEndReason)
        {
            switch (gameEndReason)
            {
                case GameEndReason.PlayerDied:
                    return "You Died!";
                case GameEndReason.GameModeSuccess:
                    return "You Completed the Mission!";
                case GameEndReason.TimeOver:
                    return "No Time Left!";
                default:
                    return "You Failed the Mission!";
            }
        }
    }
}