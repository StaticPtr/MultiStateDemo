using System;
using Runtime.Player;
using UnityEngine;

namespace Runtime.UI
{
    public class DebugPowerStateButton : MonoBehaviour
    {
        public PlayerState? PlayerState;
        public Player.Player? Player;

        public void ChangeState()
        {
            if (Player is null || PlayerState is null)
                throw new ArgumentNullException();

            Player.Model.PowerStateMachine.ChangeState(PlayerState);
        }
    }
}