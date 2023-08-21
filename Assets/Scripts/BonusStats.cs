using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SpaceShooter
{
    public class BonusStats : MonoBehaviour
    {
        [SerializeField] private Text m_Text;
       
        private int m_LastBonus;

        private void Update()
        {
            UpdateBonus(); // если очки не изменились, их не надо обновлять каждый кадр
        }

        void UpdateBonus()
        {
            if (Player.Instance != null)
            {
                int currentBonus = Player.Instance.Bonus;

                if (m_LastBonus != currentBonus)
                {
                    m_LastBonus = currentBonus;
                    m_Text.text = "Bonuses: " + m_LastBonus.ToString();
                }
            }
        }
    }
}
