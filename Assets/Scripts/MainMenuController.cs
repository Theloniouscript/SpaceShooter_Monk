using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class MainMenuController : SingletonBase<MainMenuController>
    {
        [SerializeField] private GameObject m_EpisodeSelection;
        [SerializeField] private SpaceShip m_DefaultSpaceShip;
        [SerializeField] private GameObject m_ShipSelection;

        private void Start()
        {
            LevelSequenceController.PlayerShip = m_DefaultSpaceShip; // на случай, если не будем заходить в меню и устанавливать
        }

        public void OnSelectShip()
        {
            m_ShipSelection.SetActive(true);
            gameObject.SetActive(false);
        }
        public void OnButtonStartNew()
        {
            m_EpisodeSelection.gameObject.SetActive(true); 
            gameObject.SetActive(false);
        }

        public void OnButtonExit()
        {
            Application.Quit();
        }

    }
}
