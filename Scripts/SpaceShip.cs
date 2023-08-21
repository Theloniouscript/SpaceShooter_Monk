using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// Mass for automatic installation for rigid
        /// </summary>
        [Header("Space ship")]

        [SerializeField] private float m_Mass;

        /// <summary>
        /// Pushing force
        /// </summary>
        [SerializeField] private float m_Thrust;

        /// <summary>
        /// Rotating force
        /// </summary>
        [SerializeField] private float m_Mobility;

        /// <summary>
        /// Max linear speed - forward speed limiter
        /// </summary>
        [SerializeField] private float m_MaxLinearVelocity;
        public float MaxLinearVelocity => m_MaxLinearVelocity;

        /// <summary>
        /// Max angular / rotating speed - in degree/second
        /// </summary>
        [SerializeField] private float m_MaxAngularVelocity;
        public float MaxAngularVelocity => m_MaxAngularVelocity;

        /// <summary>
        /// PlayerShip Image, used for ShipSelection in MainMenuScene via PlayerShipSelection panel
        /// </summary>
        [SerializeField] private Sprite m_PreviewImage;
        public Sprite PreviewImage => m_PreviewImage;

        /// <summary>
        /// Saved link to rigid
        /// </summary>
        private Rigidbody2D m_Rigid;

        #region Public API

        /// <summary>
        /// Linear thrust control. From -1.0 to +1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Torque Control // rotating thrust control. From -1.0 to +1.0
        /// </summary>
        public float TorqueControl{ get; set; }
        #endregion


        #region UnityEvents

        protected override void Start()
        {
            base.Start();
            m_Rigid = GetComponent<Rigidbody2D>();
            m_Rigid.mass = m_Mass;
            m_Rigid.inertia = 1;
            m_Armor = m_CurrentArmor;

            //InitOffensive();
        }
        private void FixedUpdate()
        {
            UpdateRigidBody();
            UpdateEnergyRegen();
        }

        #endregion

        /// <summary>
        /// Adds force to the ship while moving
        /// </summary>
        private void UpdateRigidBody()
        {
            m_Rigid.AddForce(ThrustControl * m_Thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigid.AddForce(-m_Rigid.velocity * (m_Thrust / m_MaxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigid.AddTorque(TorqueControl * m_Mobility * Time.fixedDeltaTime, ForceMode2D.Force);
            m_Rigid.AddTorque(-m_Rigid.angularVelocity * (m_Mobility / m_MaxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

        }

        [SerializeField] private Turret[] m_Turrets;

        /// <summary>
        /// Стрельба из турелей в зависимости от типа - Primary или Secondary
        /// </summary>
        /// <param name="mode"></param>
        public void Fire(TurretMode mode)
        {
            //Debug.Log($"input mode is {mode}");
            for (int i = 0; i < m_Turrets.Length; i++)
            {
                //Debug.Log($"m_Turret[{i}] mode is {m_Turrets[i].Mode}");
                if (m_Turrets[i].Mode == mode)
                {
                    m_Turrets[i].Fire();
                }
            }


        }
        /// <summary>
        /// Определение энергии и количества патронов. 
        /// Максимальные и текущие значения.
        /// Восстановленная энергия.
        /// </summary>
        [SerializeField] private int m_MaxEnergy;
        [SerializeField] private int m_MaxAmmo;

        [SerializeField] private int m_EnergyRegenPerSecond;

        private float m_PrimaryEnergy;
        private int m_SecondaryAmmo;

        [SerializeField] private int m_MaxArmor;

        [SerializeField] private int m_CurrentArmor;
        public int Armor => m_CurrentArmor;

        /// <summary>
        /// Добавление энергии и патронов с помощью функции, аналогичной векторной интерполяции.
        /// </summary>
        /// <param name="e"></param>
        public void AddEnergy(int e)
        {
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy + e, 0, m_MaxEnergy);
            
        }

        public void AddAmmo(int ammo)
        {
            m_SecondaryAmmo = Mathf.Clamp(m_SecondaryAmmo + ammo, 0, m_MaxAmmo);
            
        }

        public void AddArmor(int armor)
        {
            
            m_CurrentArmor = Mathf.Clamp(m_CurrentArmor + armor, 0, m_MaxArmor);
            Debug.Log($"added armor = {m_CurrentArmor}");
            m_Armor = m_CurrentArmor;

        }

       /* public void DrawArmor(int count)
        {
            if (count > 0)
            {
                
                if (m_CurrentArmor >= count)
                {
                    m_CurrentArmor -= count;
                    Debug.Log($"draw armor = {m_CurrentArmor}");
                    m_Armor = m_CurrentArmor;
                }
            }
        }
*/

        /*protected override void OnDeath()
        {
            
            if (m_CurrentArmor == 0)
            {*/
            
              // Destroy(gameObject);
                //m_EventOnDeath?.Invoke();

            //}

            /*Destroy(gameObject);
            m_EventOnDeath?.Invoke();*/

            /*for(int i = 0; i < m_Armor; i ++)
            {
                m_CurrentHitpoints--;
                m_Armor--;

                if(m_Armor == 0)
                {
                    Destroy(gameObject);
                    m_EventOnDeath?.Invoke();
                }
                    
            }
        }*/

        // [SerializeField] private UnityEvent m_EventOnDeath;
        //public new UnityEvent EventOnDeath => m_EventOnDeath;

       /* public override void ApplyDamage(int damage)
        {
            Debug.Log($"damage = {damage}");
            DrawArmor(damage);  
            // m_CurrentArmor--;
            base.ApplyDamage(damage);
           // m_CurrentArmor = Mathf.Clamp(m_CurrentArmor - damage, 0, m_MaxArmor);
            Debug.Log($"after damage = {m_CurrentArmor}");


            *//*if (m_CurrentArmor <= 0)
            {
                OnDeath();
            }*//*

        }
*/




        /// <summary>
        /// Инициализация значений
        /// </summary>
        private void InitOffensive()
        {
            m_PrimaryEnergy = m_MaxEnergy;
            m_SecondaryAmmo = m_MaxAmmo;
            m_CurrentArmor = m_MaxArmor;
        }

        /// <summary>
        /// Восстановленная энергия + ограничение энергии
        /// </summary>
        private void UpdateEnergyRegen()
        {
            m_PrimaryEnergy += (float)m_EnergyRegenPerSecond * Time.fixedDeltaTime;
            m_PrimaryEnergy = Mathf.Clamp(m_PrimaryEnergy, 0, m_MaxEnergy);
        }

        /// <summary>
        /// Две функции, отнимающие патроны и энергию
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public bool DrawAmmo(int count)
        {
            if(count == 0)
            {
                return true;
            }

            if(m_SecondaryAmmo >= count)
            {
                m_SecondaryAmmo -= count;
                return true;
            }

            return false;

        }

        public bool DrawEnergy(int count)
        {
            if (count == 0)
            {
                return true;
            }

            if (m_PrimaryEnergy >= count)
            {
                m_PrimaryEnergy -= count;
                return true;
            }

            return false;

        }

        public void AssignWeapon(TurretProperties props)
        {
            for(int i = 0; i < m_Turrets.Length; i++)
            {
                m_Turrets[i].AssignLoadOut(props);
            }
        }

        
    }
}




