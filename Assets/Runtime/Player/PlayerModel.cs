using Game.FSM;

namespace Runtime.Player
{
    public class PlayerModel
    {
        public double Health { get; set; }
        public double Score { get; set; }

        public StateMachine InputStateMachine { get; } = new();
        public StateMachine PowerStateMachine { get; } = new();
    }
}