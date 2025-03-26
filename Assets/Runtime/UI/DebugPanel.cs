using System;
using Game.FSM;
using TMPro;
using UnityEngine;

namespace Runtime.UI
{
    public class DebugPanel : MonoBehaviour
    {
        public Player.Player? Player;
        public TextMeshProUGUI HealthLabel = null!;
        public TextMeshProUGUI ScoreLabel = null!;
        public TextMeshProUGUI PowerStateLabel = null!;

        private void Start()
        {
            if (!Player)
                return;

            Player.Model.Health.OnValueChanged += OnHealthChanged;
            OnHealthChanged(0, Player.Model.Health.Value);
            
            Player.Model.Score.OnValueChanged += OnScoreChanged;
            OnScoreChanged(0, Player.Model.Score.Value);

            Player.Model.PowerStateMachine.OnStateChanged += OnPowerStateChanged;
            OnPowerStateChanged(Player.Model.PowerStateMachine);
        }

        private void OnDestroy()
        {
            if (Player is null)
                return;
            
            Player.Model.Health.OnValueChanged -= OnHealthChanged;
            Player.Model.Score.OnValueChanged -= OnScoreChanged;
            Player.Model.PowerStateMachine.OnStateChanged -= OnPowerStateChanged;
        }

        private void OnHealthChanged(double _, double newValue)
        {
            HealthLabel.text = $"<b>Player Health:</b> {newValue:0}";
        }
        
        private void OnScoreChanged(double _, double newValue)
        {
            ScoreLabel.text = $"<b>Score:</b> {newValue:0}";
        }

        private void OnPowerStateChanged(StateMachine stateMachine)
        {
            if (stateMachine.CurrentState is not { } state)
                return;

            PowerStateLabel.text = $"<b>State:</b> {state.name}";
        }
    }
}