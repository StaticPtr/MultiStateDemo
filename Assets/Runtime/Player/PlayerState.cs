using Game.FSM;

namespace Runtime.Player
{
    public abstract class PlayerState : State
    {
        public readonly Player Player;
        
        protected PlayerState(Player player, StateMachine stateMachine) : base(stateMachine)
        {
            Player = player;
        }
    }
}