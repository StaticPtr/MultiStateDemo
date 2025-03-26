using Game.FSM;
using Runtime.Player.InputStates;
using UnityEngine;

namespace Runtime.Player.PowerStates
{
    [CreateAssetMenu(menuName = "Game/Player Power FSM/Normal", fileName = "Normal Player Power State")]
    public class NormalPlayerPowerState : PlayerState
    {
        public KeyCode AttackKeyCode = KeyCode.Mouse0;
        public float AttackCooldownSeconds = 0.2f;

        public override void OnUpdate(float deltaTimeSeconds)
        {
            base.OnUpdate(deltaTimeSeconds);

            if (Player is null)
                return;
            
            if (Player.Model.InputStateMachine.CurrentState is not NormalPlayerInputState)
                return;

            if (!Input.GetKeyDown(AttackKeyCode))
                return;
            
            Debug.Log("Bang");
            Player.Model.InputStateMachine.ChangeState<CooldownPlayerInputState>(
                new CooldownPlayerInputState.Message(AttackCooldownSeconds)
            );
        }
    }
}