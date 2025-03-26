using Game.FSM;
using Runtime.Player.InputStates;
using UnityEngine;

namespace Runtime.Player.PowerStates
{
    public class NormalPlayerPowerState : PlayerState
    {
        public KeyCode AttackKeyCode = KeyCode.Mouse0;
        public float AttackCooldownSeconds = 0.2f;
        
        public NormalPlayerPowerState(Player player, StateMachine stateMachine) : base(player, stateMachine)
        {
        }

        public override void Update(float deltaTimeSeconds)
        {
            base.Update(deltaTimeSeconds);

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