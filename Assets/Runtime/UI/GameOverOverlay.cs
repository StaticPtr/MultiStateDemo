using System;
using UnityEngine;

namespace Runtime.UI
{
    public class GameOverOverlay : MonoBehaviour
    {
        public Player.Player Player = null!;
        public GameObject Root = null!;
        
        private void Start()
        {
            Player.Model.Health.OnValueChanged += OnHealthChanged;
        }

        private void OnDestroy()
        {
            Player.Model.Health.OnValueChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(double _, double newValue)
        {
            Root.SetActive(newValue <= 0);
        }
    }
}