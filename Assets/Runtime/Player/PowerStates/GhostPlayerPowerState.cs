using Game.FSM;

namespace Runtime.Player.PowerStates
{
    public class GhostPlayerPowerState : PlayerState
    {
        public GhostPlayerPowerState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Update(float deltaTimeSeconds)
        {
            base.Update(deltaTimeSeconds);
        }
    }
}