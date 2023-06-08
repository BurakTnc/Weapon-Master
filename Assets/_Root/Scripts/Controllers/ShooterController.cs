using System;
using System.Collections.Generic;
using _Root.Scripts.Objects;
using UnityEngine;

namespace _Root.Scripts.Controllers
{
    public class ShooterController : MonoBehaviour
    {
        [Header("Shooting Positions")] 
        [SerializeField] private List<Transform> shootingPositions = new List<Transform>();

        [Header("Default Values")]
        [SerializeField] private float defaultFireRate;
        [SerializeField] private float defaultRange;
        [SerializeField] private int defaultDamage;
        [SerializeField] private float bulletSpeed;

        private float _fireRate;
        private float _range;
        private int _damage;
        private int _weaponLevel = 1;
        
        private float _shootTimer;

        private void Start()
        {
            _fireRate = defaultFireRate;
            _range = defaultRange;
            _damage = defaultDamage;
        }

        private void Update()
        {
            BeginFire();
        }

        public float FireRate
        {
            get => _fireRate;
            set
            {
                _fireRate -= value;
                _fireRate = Mathf.Clamp(_fireRate, 0, 2);
            } 
        }
        public float Range
        {
            get => _range;
            set => _range += value;
        }
        public int Damage
        {
            get => _damage;
            set => _damage += value;
        }
        
        public int WeaponLevel
        {
            get => _weaponLevel;
            set => _weaponLevel += value;
        }

        private void BeginFire()
        {
            if (_shootTimer <= 0)
            {
                _shootTimer += _fireRate;
                Fire();
            }
            else
            {
                _shootTimer -= Time.deltaTime;
                _shootTimer = Mathf.Clamp(_shootTimer, 0, _fireRate);
            }
        }

        private void Fire()
        {
            for (var i = 0; i < _weaponLevel; i++)
            {
                var bullet = Instantiate(Resources.Load<GameObject>("Spawnables/Bullet")).transform;

                bullet.position = shootingPositions[i].position;
                
                if (bullet.gameObject.TryGetComponent(out Bullet script))
                {
                    script.Fire(bulletSpeed, _range, _damage,_fireRate);
                }
            }
            
            
        }
    }
}