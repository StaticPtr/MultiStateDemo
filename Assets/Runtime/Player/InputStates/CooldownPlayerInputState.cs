using System;
using Game.FSM;
using UnityEngine;

namespace Runtime.Player.InputStates
{
    public class CooldownPlayerInputState : PlayerState
    {
        public static readonly Type DefaultReturnState = typeof(NormalPlayerInputState);
        
        public float TimeRemainingSeconds { get; private set; }
        private Type _returnStateType = typeof(NormalPlayerInputState);
        
        public CooldownPlayerInputState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void OnEnter(object? message)
        {
            base.OnEnter(message);
            
            if (message is not Message typedMessage)
                throw new ArgumentException(nameof(message));

            TimeRemainingSeconds = typedMessage.Duration;
            _returnStateType = typedMessage.ReturnState ?? DefaultReturnState;
        }

        public override void Update(float deltaTimeSeconds)
        {
            base.Update(deltaTimeSeconds);

            TimeRemainingSeconds -= deltaTimeSeconds;

            if (TimeRemainingSeconds <= 0)
            {
                StateMachine.ChangeState<NormalPlayerInputState>();
            }
        }

        #region Embedded Types
        public record Message(float Duration)
        {
            public Type? ReturnState { get; init; }
        }
        #endregion
    }
}