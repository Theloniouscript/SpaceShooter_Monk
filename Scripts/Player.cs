using SpaceShooter;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_NumLives;
        [SerializeField] private SpaceShip m_Ship;
        [SerializeField] private GameObject m_PlayerShipPrefab;

        [SerializeField] private CameraController m_CameraController;
        [SerializeField] private MovementController m_MovementController;

        public SpaceShip ActiveShip => m_Ship;

        /// <summary>
        /// Присвоить игроку выбранный корабль
        /// </summary>
        /*protected override void Awake()
        {
            base.Awake();
            if (m_Ship != null) Destroy(m_Ship.gameObject);
        }*/

        private void Start()
        {
            Respawn();
           /* m_Ship.EventOnDeath.AddListener(OnShipDeath);
            Debug.Log($"start m_NumLives = {m_NumLives}");
            m_NumLives++;*/
        }

        private void OnShipDeath()
        {
            /*if (m_Ship.EventOnDeath != null)*/
                m_NumLives--;
             Debug.Log($"m_NumLives = {m_NumLives}");

            if (m_NumLives > 0)
            {
                Respawn();
                Debug.Log("Respawn");
            }              

            else
            { 
                LevelSequenceController.Instance.FinishCurrentLevel(true); // выход в главное меню, если заканчивается уровень
                Debug.Log("End Level");
            }


            //Invoke("Respawn", 2);




        }

        public void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                //var newPlayerShip = Instantiate(m_PlayerShipPrefab);
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip);
                m_Ship = newPlayerShip.GetComponent<SpaceShip>();

                m_CameraController.SetTarget(m_Ship.transform);
                m_MovementController.SetTargetShip(m_Ship);

                m_Ship.EventOnDeath.AddListener(OnShipDeath);
            }

            
        }

        #region Score

        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }

        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion
    }
}
