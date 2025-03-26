using System;
using Game.FSM;
using Runtime.Player.InputStates;
using UnityEngine;

namespace Runtime.Player.PowerStates
{
    [CreateAssetMenu(menuName = "Game/Player Power FSM/Normal", fileName = "Normal Player Power State")]
    public class NormalPlayerPowerState : PlayerState
    {
        public KeyCode AttackKeyCode;
        public float AttackCooldownSeconds;
        public float PlayerSpeed;

        private float _previousMoveSpeed;

        public override void OnEnter(object? message)
        {
            base.OnEnter(message);
            
            if (Player is null)
                return;
            
            _previousMoveSpeed = Player.ThirdPersonController.MoveSpeed;
            Player.ThirdPersonController.MoveSpeed = PlayerSpeed;
        }

        public override void OnLeaving()
        {
            base.OnLeaving();
            
            if (Player is null)
                return;

            Player.ThirdPersonController.MoveSpeed = _previousMoveSpeed;
        }

        public override void OnUpdate(float deltaTimeSeconds)
        {
            base.OnUpdate(deltaTimeSeconds);

            if (Player is null)
                return;
            
            if (Player.Model.InputStateMachine.CurrentState is not NormalPlayerInputState)
                return;

            if (!Input.GetKey(AttackKeyCode))
                return;

            Shoot();
        }

        private void Shoot()
        {
            if (Player is null)
                throw new ArgumentNullException();

            Projectile projectile = Player.ProjectilePool.Get();
            projectile.transform.SetParent(Player.ProjectileSpawnPoint, false);
            projectile.transform.localPosition = Vector3.zero;
            projectile.transform.localRotation = Quaternion.identity;
            projectile.transform.SetParent(null, true);
            
            Player.Model.InputStateMachine.ChangeState<CooldownPlayerInputState>(
                new CooldownPlayerInputState.Message(AttackCooldownSeconds)
            );
        }
    }
}