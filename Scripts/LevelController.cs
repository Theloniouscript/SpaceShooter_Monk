using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{

    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }
    public class LevelController : MonoBehaviour
    {
        [SerializeField] private int m_ReferenceTime;
        public int ReferenceTime => m_ReferenceTime;

        [SerializeField] private UnityEvent m_EventLevelCompleted;
        private ILevelCondition[] m_Conditions;

        private bool m_IsLevelCompleted;

        private float m_LevelTime;
        public float LevelTime => m_LevelTime;

        private void Start()
        {
            m_Conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void Update()
        {
            if(!m_IsLevelCompleted)
            {
                m_LevelTime += Time.deltaTime;
                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        {
            if (m_Conditions == null || m_Conditions.Length == 0)
                return;

            int numCompleted = 0;

            foreach(var v in m_Conditions)
            {
                if (v.IsCompleted) numCompleted++;
            }

            if(numCompleted == m_Conditions.Length)
            {
                m_IsLevelCompleted = true;
                m_EventLevelCompleted?.Invoke();

<<<<<<< HEAD
                LevelSequenceController.Instance?.FinishCurrentLevel(true);
=======
                //LevelSequenceController.Instance?.FinishCurrentLevel(true);
>>>>>>> SS_20.6.4
            }
        }
    }
}
