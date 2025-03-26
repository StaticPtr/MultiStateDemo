using System;
using Game.FSM;
using Runtime.Player.InputStates;
using Runtime.Player.PowerStates;
using UnityEngine;

namespace Runtime.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerModel Model { get; } = new();

        private void Awake()
        {
            //TODO: Should the states be ScriptableObjects?
            Model.InputStateMachine.AddState(new NormalPlayerInputState(this, Model.InputStateMachine));
            Model.InputStateMachine.AddState(new CooldownPlayerInputState(this, Model.InputStateMachine));
            
            Model.PowerStateMachine.AddState(new NormalPlayerPowerState(this, Model.PowerStateMachine));
            Model.PowerStateMachine.AddState(new GhostPlayerPowerState(this, Model.PowerStateMachine));
            Model.PowerStateMachine.AddState(new EmpoweredPlayerPowerState(this, Model.PowerStateMachine));
        }

        private void Start()
        {
            Model.InputStateMachine.ChangeState<NormalPlayerInputState>();
            Model.PowerStateMachine.ChangeState<NormalPlayerPowerState>();
        }

        private void Update()
        {
            float deltaTimeSeconds = Time.deltaTime;
            Model.InputStateMachine.CurrentState?.Update(deltaTimeSeconds);
            Model.PowerStateMachine.CurrentState?.Update(deltaTimeSeconds);
        }
    }
}