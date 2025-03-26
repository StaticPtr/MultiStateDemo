using System;
using Runtime.Player;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private TagHandle _environmentTag;
    private TagHandle _playerTag;
    private TagHandle _enemyTag;
    
    public float Speed;
    public float LifespanSeconds;

    public float LifespanRemainingSeconds { get; private set; }
    public ProjectilePool? Pool { get; private set; }

    private void Awake()
    {
        _environmentTag = TagHandle.GetExistingTag("Environment");
        _playerTag = TagHandle.GetExistingTag("Player");
        _enemyTag = TagHandle.GetExistingTag("Enemy");
    }

    public void SetPool(ProjectilePool pool)
    {
        Pool = pool;
    }

    private void OnEnable()
    {
        LifespanRemainingSeconds = LifespanSeconds;
    }

    private void Update()
    {
        LifespanRemainingSeconds -= Time.deltaTime;

        if (LifespanRemainingSeconds <= 0.0)
        {
            ReturnToPool();
            return;
        }
        
        transform.Translate(Vector3.forward * Time.deltaTime * Speed, Space.Self);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_playerTag))
            return;

        if (other.CompareTag(_environmentTag))
        {
            ReturnToPool();
            return;
        }

        if (other.CompareTag(_enemyTag))
        {
            throw new NotImplementedException();
            ReturnToPool();
            return;
        }
    }

    private void ReturnToPool()
    {
        Pool?.Return(this);
    }
}
