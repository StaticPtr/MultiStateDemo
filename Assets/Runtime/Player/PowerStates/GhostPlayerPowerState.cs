using Game.FSM;
using Runtime.Player.InputStates;
using UnityEngine;

namespace Runtime.Player.PowerStates
{
    [CreateAssetMenu(menuName = "Game/Player Power FSM/Ghost", fileName = "Ghost Player Power State")]
    public class GhostPlayerPowerState : NormalPlayerPowerState
    {
        public LayerMask ClippableLayers;

        private LayerMask _previousExcludedLayers;
        
        public override void OnEnter(object? message)
        {
            base.OnEnter(message);

            if (Player is null)
                return;

            if (!Player.CharacterController)
                return;

            _previousExcludedLayers = Player.CharacterController.excludeLayers;
            Player.CharacterController.excludeLayers = ClippableLayers;
        }

        public override void OnLeaving()
        {
            base.OnLeaving();
            
            if (Player is null)
                return;

            if (!Player.CharacterController)
                return;
            
            Player.CharacterController.excludeLayers = _previousExcludedLayers;
        }
    }
}