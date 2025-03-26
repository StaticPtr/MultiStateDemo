using Game.FSM;

namespace Runtime.Player.PowerStates
{
    public class EmpoweredPlayerPowerState : PlayerState
    {
        public EmpoweredPlayerPowerState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Update(float deltaTimeSeconds)
        {
            base.Update(deltaTimeSeconds);
        }
    }
}