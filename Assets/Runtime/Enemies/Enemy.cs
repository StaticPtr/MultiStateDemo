using UnityEngine;

namespace Runtime.Enemies
{
    public class Enemy : MonoBehaviour
    {
        public float ScoreValue;
        public bool IsDummy;

        public void Kill()
        {
            if (Player.Player.Instance is not null)
            {
                Player.Player.Instance.Model.Score.ChangeValue(Player.Player.Instance.Model.Score.Value + ScoreValue);
            }
            
            if (IsDummy)
                return;
            
            Destroy(gameObject);
        }
    }
}