using System;
using UnityEngine;

namespace Shapes.Components
{
    public class Health : MonoBehaviour
    {
        public Hittable deathTrigger;
        public Health binding;
        public bool bound;

        public float maxHealth = 100;
        public float armor;

        public float health;
        public float shield;

        private bool hasTrigger;

        // Start is called before the first frame update
        void Start()
        {
            bound = binding != null;
            health = maxHealth;
            if (deathTrigger == null)
            {
                deathTrigger = GetComponent<Hittable>();
            }

            hasTrigger = deathTrigger != null;
        }

        // Update is called once per frame
        void Update()
        {
            syncBinding();

            if (health <= 0)
            {
                doDestroy();
            }
        }

        public void bind(Health other)
        {
            binding = other;
            bound = other != null;
            syncBinding();
        }

        public void unbind()
        {
            binding = null;
            bound = false;
        }

        public bool isDamaged()
        {
            syncBinding();
            return health < maxHealth;
        }

        public void damage(float damage)
        {
            damage -= armor;

            var shieldDestroy = damage >= shield;
            var shieldDelta = Mathf.Min(shield, damage);
            shield -= shieldDelta;
            damage -= shieldDelta;

            syncBinding();

            if (shieldDestroy)
            {
                shieldDestroyed();
            }

            health -= damage;
            if (health <= 0)
            {
                doDestroy();
            }
        }

        public void heal(float heal)
        {
            health = Math.Min(health + heal, maxHealth);

            syncBinding();
        }

        public void kill()
        {
            health = 0;
            shield = 0;

            syncBinding();
            doDestroy();
        }

        private void syncBinding()
        {
            if (!bound) return;

            health = binding.health;
            shield = binding.shield;
        }

        protected virtual void shieldDestroyed(){}

        protected virtual void doDestroy()
        {
            if (hasTrigger)
            {
                deathTrigger.onDeath();
            }

            Destroy(gameObject);
        }
    }
}

