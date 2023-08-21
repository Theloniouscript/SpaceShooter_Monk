﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private Text m_Kills;
        [SerializeField] private Text m_Score;
        [SerializeField] private Text m_Time;
        [SerializeField] private Text m_Result;
        [SerializeField] private Text m_ButtonNextText;

        private bool m_Success; // поведение кнопки

        private void Start()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Panel and button text depending on player results
        /// </summary>
        /// <param name="levelResults"></param>
        /// <param name="success"></param>
        public void ShowResults(PlayerStatistics levelResults, bool success)
        {
            m_Success = success;
            m_Result.text = success ? "Win" : "Lose";
            m_ButtonNextText.text = success ? "Next" : "Restart";

            m_Kills.text = "Kills: " + levelResults.numKills.ToString();
            m_Score.text = "Score: " + levelResults.score.ToString();
            m_Time.text = "Time: " + levelResults.time.ToString();


            Time.timeScale = 0; // stops all movement on scene
        }

        public void OnButtonNextAction()
        {
            gameObject.SetActive(false);
            Time.timeScale = 1;

            if(m_Success)
            {
                LevelSequenceController.Instance.AdvanceLevel(); // if level is won
            }
            else
            {
                LevelSequenceController.Instance.RestartLevel(); // if level is lost
            }
        }
    }
}
