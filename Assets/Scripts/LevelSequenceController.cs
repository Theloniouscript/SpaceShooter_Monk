using System;
using UnityEngine;
using UnityEngine.SceneManagement;
namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_menu";
        public static SpaceShip PlayerShip { get; set; }        

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        public PlayerStatistics LevelStatistics { get; private set; }

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

            // сбрасываем статы перед началом эпизода

            LevelStatistics= new PlayerStatistics();
            LevelStatistics.Reset();

            SceneManager.LoadScene(e.Levels[CurrentLevel]);
        }

        /// <summary>
        /// Force restart
        /// </summary>
        public void RestartLevel()
        {
            SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
        }

        /// <summary>
        /// Next level or main menu exit if there are no more levels
        /// </summary>
        public void AdvanceLevel()
        {
            LevelStatistics.Reset();
            CurrentLevel++;

            if(CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(MainMenuSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }

        
        public void FinishCurrentLevel(bool success)
        {
            LastLevelResult = success;
            
            
            CalculateLevelStatistics();
            ResultPanelController.Instance.ShowResults(LevelStatistics, success);

            /*if (success)
                AdvanceLevel();*/
        }

        /// <summary>
        /// Counting statistics of the previous level
        /// </summary>
        private void CalculateLevelStatistics()
        {
            LevelStatistics.score = Player.Instance.Score;
            LevelStatistics.numKills= Player.Instance.NumKills;
            LevelStatistics.time = (int)LevelController.Instance.LevelTime;
            LevelStatistics.bonus = Player.Instance.Bonus;
            
        }
    }
}