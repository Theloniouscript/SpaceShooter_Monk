using UnityEngine;
using UnityEngine.SceneManagement;
namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string MainMenuSceneNickname = "main_menu";
<<<<<<< HEAD
=======
        public static SpaceShip PlayerShip { get; set; }
>>>>>>> SS_20.6.4

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public void StartEpisode(Episode e)
        {
            CurrentEpisode = e;
            CurrentLevel = 0;

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
<<<<<<< HEAD

=======
            if (success)
                AdvanceLevel();
>>>>>>> SS_20.6.4
        }

    }
}