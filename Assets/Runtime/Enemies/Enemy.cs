using UnityEngine;

namespace Runtime.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public float ScoreValue;
        public bool IsDummy;
        public float Damage;

        public void Kill()
        {
            if (Player.Player.Instance is not null)
            {
                Player.Player.Instance.Model.Score.SetValue(Player.Player.Instance.Model.Score.Value + ScoreValue);
            }
            
            if (IsDummy)
                return;
            
            Destroy(gameObject);
        }
    }
}