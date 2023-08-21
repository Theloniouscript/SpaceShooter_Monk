using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Object ignores damage
        /// </summary>
        [SerializeField] private bool m_Indestructible;
      //  [SerializeField] private GameObject explosionPrefab;

        //[SerializeField] private int m_Armor; // броня
        public int m_Armor { get; set; }
        public bool IsIndestructible => m_Indestructible;

        /// <summary>
        /// Starting amount of hitpoints
        /// </summary>
        [SerializeField] private int m_HitPoints;

        /// <summary>
        /// Current hitpoints
        /// </summary>
        private int m_CurrentHitpoints;
        public int HitPoints => m_CurrentHitpoints;
        #endregion

        #region Unity Events

        protected virtual void Start()
        {
            m_CurrentHitpoints = m_HitPoints;
        }
        #endregion

        #region Public API

        /// <summary>
        /// Applying damage to the gameobject + check on armor value
        /// </summary>
        /// <param name="damage">Damage inflicted to an object</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;
            m_CurrentHitpoints -= damage;

            /*if (m_Armor > 0)
            {
                m_Armor--;
                Debug.Log($"after damage = {m_Armor}");
            }
            else OnDeath();*/
            

            if (m_CurrentHitpoints <= 0)
                OnDeath();
        
        }
        #endregion

        /// <summary>
        /// Override event of object destruction
        /// </summary>
        protected virtual void OnDeath()
        {
            /*if (explosionPrefab != null)
            {
                var explosion = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
                // Destroy(explosion, 1f);
            }*/


            Destroy(gameObject);
            m_EventOnDeath?.Invoke();


            /* if (m_Armor <= 0)
             {
                 Destroy(gameObject);
                 m_EventOnDeath?.Invoke();
                 //Player.Instance.Respawn();
             }*/

        }

        private static HashSet<Destructible> m_AllDestructibles;
        public static IReadOnlyCollection<Destructible> AllDestructibles => m_AllDestructibles;

        protected virtual void OnEnable()
        {
            if (m_AllDestructibles == null)
                m_AllDestructibles = new HashSet<Destructible>();

            m_AllDestructibles.Add(this);
            Debug.Log("OnEnable");
        }

        protected virtual void OnDestroy()
        {
            m_AllDestructibles.Remove(this);
            Debug.Log("OnDestroy");
        }

        public const int TeamIdNeutral = 0;
        [SerializeField] private int m_TeamId;
        public int TeamId => m_TeamId;


        [SerializeField] private UnityEvent m_EventOnDeath;
        public UnityEvent EventOnDeath => m_EventOnDeath;


    }
}
