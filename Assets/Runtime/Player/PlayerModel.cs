using System;
using Game.FSM;

namespace Runtime.Player
{
    public class PlayerModel
    {
        public Reactive<double> Health { get; private set; } = new(100);
        public Reactive<double> Score { get; private set; } = new(0);

        public StateMachine InputStateMachine { get; } = new();
        public StateMachine PowerStateMachine { get; } = new();
    }
}