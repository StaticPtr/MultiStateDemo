using System;
using System.Collections.Generic;
using Game.FSM;
using Runtime.Player.InputStates;
using Runtime.Player.PowerStates;
using StarterAssets;
using UnityEngine;
using UnityEngine.Pool;

namespace Runtime.Player
{
    public class Player : MonoBehaviour
    {
        public ProjectilePool ProjectilePool = null!;
        public CharacterController CharacterController = null!;
        public Transform ProjectileSpawnPoint = null!;
        public ThirdPersonController ThirdPersonController = null!;
        
        [Header("Initial States")]
        [Tooltip("Input states for the player. The first state is the initial state. Not editable at runtime.")]
        [SerializeField] private PlayerState[] _inputStates = Array.Empty<PlayerState>();
        
        [Tooltip("Power states for the player. The first state is the initial state. Not editable at runtime.")]
        [SerializeField] private PlayerState[] _powerStates = Array.Empty<PlayerState>();
        
        public PlayerModel Model { get; } = new();

        private void Awake()
        {
            InitializeStateMachine(Model.InputStateMachine, _inputStates);
            InitializeStateMachine(Model.PowerStateMachine, _powerStates);

            void InitializeStateMachine(StateMachine stateMachine, IList<PlayerState> states)
            {
                foreach (PlayerState inputState in states)
                {
                    inputState.Player = this;
                    stateMachine.AddState(inputState);
                }   
            }
        }

        private void Start()
        {
            Model.InputStateMachine.ChangeState(_inputStates[0]);
            Model.PowerStateMachine.ChangeState(_powerStates[0]);
        }

        private void Update()
        {
            float deltaTimeSeconds = Time.deltaTime;
            Model.InputStateMachine.CurrentState?.OnUpdate(deltaTimeSeconds);
            Model.PowerStateMachine.CurrentState?.OnUpdate(deltaTimeSeconds);
            
            Debug_ChangePowerState();
        }

        private void Debug_ChangePowerState()
        {
            if (!Debug.isDebugBuild && !Application.isEditor)
                return;

            for (int i = 0; i < _powerStates.Length; i++)
            {
                if (!Input.GetKeyDown(KeyCode.Alpha1 + i))
                    continue;

                Model.PowerStateMachine.ChangeState(_powerStates[i]);
                return;
            }
        }
    }
}