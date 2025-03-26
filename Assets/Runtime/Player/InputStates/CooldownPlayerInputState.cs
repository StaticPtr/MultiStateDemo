using System;
using Game.FSM;
using UnityEngine;

namespace Runtime.Player.InputStates
{
    [CreateAssetMenu(menuName = "Game/Player Input FSM/Cooldown", fileName = "Cooldown Player Input State")]
    public class CooldownPlayerInputState : PlayerState
    {
        public static readonly Type DefaultReturnState = typeof(NormalPlayerInputState);
        
        public float TimeRemainingSeconds { get; private set; }
        private Type _returnStateType = typeof(NormalPlayerInputState);

        public override void OnEnter(object? message)
        {
            base.OnEnter(message);
            
            if (message is not Message typedMessage)
                throw new ArgumentException(nameof(message));

            TimeRemainingSeconds = typedMessage.Duration;
            _returnStateType = typedMessage.ReturnState ?? DefaultReturnState;
        }

        public override void OnUpdate(float deltaTimeSeconds)
        {
            base.OnUpdate(deltaTimeSeconds);

            TimeRemainingSeconds -= deltaTimeSeconds;

            if (TimeRemainingSeconds <= 0)
            {
                StateMachine!.ChangeState<NormalPlayerInputState>();
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