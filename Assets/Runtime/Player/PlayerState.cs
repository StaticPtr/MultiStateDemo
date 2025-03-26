using Game.FSM;

namespace Runtime.Player
{
    public abstract class PlayerState : State
    {
        public Player? Player { get; internal set; }
    }
}