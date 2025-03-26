using System;
using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Runtime.Player
{
    public sealed class ProjectilePool : MonoBehaviour
    {
        [SerializeField] private Projectile _prefab = null!;
        
        private ObjectPool<Projectile> _pool = null!;
        
        private void Awake()
        {
            _pool = new(CreateProjectile, OnProjectilePop, OnProjectilePush);
        }

        private Projectile CreateProjectile()
        {
            Projectile result = Object.Instantiate(_prefab);
            result.SetPool(this);
            return result;
        }

        private void OnProjectilePop(Projectile projectile)
        {
            projectile.gameObject.SetActive(true);
            projectile.gameObject.hideFlags = HideFlags.None;
        }
        
        private void OnProjectilePush(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
            projectile.gameObject.transform.SetParent(transform);
            projectile.gameObject.hideFlags = HideFlags.HideInHierarchy;
        }

        private void OnDestroy()
        {
            _pool.Dispose();
        }

        public Projectile Get()
        {
            return _pool.Get();
        }

        public void Return(Projectile projectile)
        {
            _pool.Release(projectile);
        }
    }
}