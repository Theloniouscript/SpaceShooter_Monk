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

        private void Start()
        {
            m_Ship.EventOnDeath.AddListener(OnShipDeath);
            Debug.Log($"start m_NumLives = {m_NumLives}");
            m_NumLives++;
        }

        private void OnShipDeath()
        {
            if(m_Ship.EventOnDeath != null)
                m_NumLives--;
                Debug.Log($"m_NumLives = {m_NumLives}");

            if(m_NumLives > 0)
                Respawn();

                //Invoke("Respawn", 2);
            



        }

        public void Respawn()
        {
            var newPlayerShip = Instantiate(m_PlayerShipPrefab);
            m_Ship = newPlayerShip.GetComponent<SpaceShip>();

            m_CameraController.SetTarget(m_Ship.transform);
            m_MovementController.SetTargetShip(m_Ship);

            m_Ship.EventOnDeath.AddListener(OnShipDeath);
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
