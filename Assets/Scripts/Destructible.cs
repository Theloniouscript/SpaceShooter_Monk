using UnityEngine;

namespace SpaceShooter
{
    public class Destructible : Entity
    {
        #region Properties

        /// <summary>
        /// Object ignores damage
        /// </summary>
        [SerializeField] private bool m_Indestructible;
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
        /// Applying damage to the gameobject
        /// </summary>
        /// <param name="damage">Damage inflicted to an object</param>
        public void ApplyDamage(int damage)
        {
            if (m_Indestructible) return;
            m_CurrentHitpoints -= damage;
            if(m_CurrentHitpoints <= 0)
                OnDeath();
        
        }
        #endregion

        /// <summary>
        /// Override event of object destruction
        /// </summary>
        protected virtual void OnDeath()
        {
            Destroy(gameObject);
        }


    }
}
