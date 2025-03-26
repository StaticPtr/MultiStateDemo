using System;
using Game.FSM;
using UnityEngine;

namespace Runtime.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerModel Model { get; } = new();

        private void Awake()
        {
            //TODO: Should the states be ScriptableObjects?
            Model.StateMachine.AddState<NormalPlayerState>();
            Model.StateMachine.AddState<GhostPlayerState>();
            Model.StateMachine.AddState<EmpoweredPlayerState>();
        }

        private void Start()
        {
            Model.StateMachine.ChangeState<NormalPlayerState>();
        }

        private void FixedUpdate()
        {
            Model.StateMachine.CurrentState?.Update();
        }
    }
}