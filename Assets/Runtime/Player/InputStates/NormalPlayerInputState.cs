using Game.FSM;

namespace Runtime.Player.InputStates
{
    public class NormalPlayerInputState : PlayerState
    {
        public NormalPlayerInputState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }
    }
}