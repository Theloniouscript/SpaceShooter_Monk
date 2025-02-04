﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip))]
    public class AIController : MonoBehaviour
    {
        public enum AIBehaviour
        {
            Null, 
            Patrol
        }

        [SerializeField] private AIBehaviour m_AIBehaviour;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationLinear;
        [Range(0.0f, 1.0f)]
        [SerializeField] private float m_NavigationAngular;

        [SerializeField] private float m_RandomSelectMovePointTime;
        [SerializeField] private float m_FindNewTargetTime;
        [SerializeField] private float m_ShootDelay;
        [SerializeField] private float m_AvoidRayLength;

        /*[SerializeField] private AIPointPatrol[] m_PatrolPoints;*/
        [SerializeField] private AIPointPatrol m_PatrolPoint;

        private SpaceShip m_SpaceShip;

        /// <summary>
        /// Точка назначения, а не цель корабля (Destructible)
        /// </summary>
        private Vector3 m_MovePosition;
        /// <summary>
        /// Ссылка на объект слежения
        /// </summary>
        private Destructible m_SelectedTarget;

        /*//*for test
         * 
         * private Timer TestTimer;



          * 
          * private void Start()
         {
             TestTimer = new Timer(3);
         }

         private void Update()
         {
             TestTimer.RemoveTime(Time.deltaTime);

             if(TestTimer.IsFinished == true)
             {
                 Debug.Log("Test");
                 TestTimer.Start(3);
             }
         }*/

        /*private int m_PatrolPointCount;
        private int m_PatrolPointRange;*/
        private void Start()
        {
            m_SpaceShip= GetComponent<SpaceShip>();
            InitTimers();

           /* if(m_PatrolPoints != null)
            {
                m_PatrolPointCount = 0;
                m_MovePosition = m_PatrolPoints[0].Position;
            }*/
        }

        private void Update()
        {
            UpdateTimers();
            UpdateAI();
            
        }

        /* [SerializeField] Path m_Path;
         private int pathIndex;*/

        // [SerializeField] private CircleArea m_PatrolZone;

        /*public void SetPath(Path newPath)
        {
            m_Path = newPath;
            pathIndex = 0;
            SetPatrolBehaviour(m_Path[pathIndex]);
            Debug.Log("SetPath");
        }

        private void GetNewPoint()
        {
            //pathIndex += 1;
            Debug.Log("GetNewPoint");
            if (m_Path.Length > pathIndex)
            {
                pathIndex += 1;
                SetPatrolBehaviour(m_Path[pathIndex]);
                Debug.Log(pathIndex);

            }
            else
            {
                Debug.Log(gameObject.name);
                Destroy(gameObject);

            }


        }*/

        /// <summary>
        /// Setting behavour start parameters for Patrol Mode
        /// </summary>
        /// <param name="point"></param>
        /* public void SetPatrolBehaviour(AIPointPatrol point)
         {
             m_AIBehaviour = AIBehaviour.Patrol;

             m_PatrolPoint = point;
             Debug.Log("SetPatrolBehaviour");
         }*/

        public void SetPatrolBehaviour(AIPointPatrol point)
        {
            m_AIBehaviour = AIBehaviour.Patrol;

            m_PatrolPoint = point;
            Debug.Log("SetPatrolBehaviour");
        }

        /* public void SetPatrolBehaviour(CircleArea zone)
         {
             m_AIBehaviour = AIBehaviour.Patrol;

             m_PatrolZone = zone; ;
             Debug.Log("SetPatrolBehaviour");
         }*/

        private void UpdateAI()
        {
            if(m_AIBehaviour == AIBehaviour.Patrol)
            {
                UpdateBehaviourPatrol();
            }
        }

        private void UpdateBehaviourPatrol()
        {
            Debug.Log("UpdateBehaviourPatrol");
            //GetNewPoint();
            ActionFindNewMovePosition();
            ActionControlShip();
            ActionFindNewAttackTarget();
            ActionFire();
            ActionAvoidCollision();

        }

        

        private void ActionFire()
        {
            Debug.Log("StartActionFire");
            if (m_SelectedTarget != null)
            {
               
                if (m_FireTimer.IsFinished == true)
                {
                    m_SpaceShip.Fire(TurretMode.Primary);
                    m_FireTimer.Start(m_ShootDelay);
                    Debug.Log("ActionFire");
                }
            }
            
        }

        private void ActionFindNewAttackTarget()
        {
            Debug.Log("Start ActionFindNewAttackTarget");
            if (m_SelectedTarget == null)
            {
                Debug.Log("ActionFindNewAttackTarget_SelectedTarget");
                if (m_FindNewTargetTimer.IsFinished == true)
                {
                    m_SelectedTarget = FindNearestDestructibleTarget();
                    m_FindNewTargetTimer.Start(m_ShootDelay);
                    //m_FindNewTargetTimer.Start(m_FindNewTargetTime);
                    Debug.Log("ActionFindNewAttackTarget");
                }
            }

        }

        private Destructible FindNearestDestructibleTarget()
        {
            float maxDist = float.MaxValue;

            Destructible potentialTarget = null;

            foreach (var v in Destructible.AllDestructibles)
            {
                Debug.Log("foreach AllDestructibles");
               /* if (v.GetComponent<SpaceShip>() == m_SpaceShip)
                    continue;*/

                if (v.TeamId == Destructible.TeamIdNeutral)
                    continue;

                if (v.TeamId == m_SpaceShip.TeamId)
                    continue;


                float dist = Vector2.Distance(m_SpaceShip.transform.position, v.transform.position);
                if (dist < maxDist)
                {
                    maxDist = dist;
                    potentialTarget = v;

                }
            }
            /*Debug.Log("FindNearestDestructibleTarget");
            Debug.Log(potentialTarget.name);*/
            return potentialTarget;
           
            

        }

        private void ActionAvoidCollision()
        {
            if (Physics2D.Raycast(transform.position, transform.up, m_AvoidRayLength) == true)
            {
                m_MovePosition = transform.position + transform.right * 100.0f;
                Debug.Log("ActionAvoidCollision");
            }
        }

        /// <summary>
        /// Ship movement control threw script
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void ActionControlShip()
        {
            m_SpaceShip.ThrustControl = m_NavigationLinear;
            m_SpaceShip.TorqueControl = ComputeAlignTorqueNormalized(m_MovePosition, m_SpaceShip.transform) * m_NavigationAngular;
            Debug.Log("ActionControlShip");
        }

        private const float MAX_ANGLE = 45.0f;

        /// <summary>
        /// Movement // rotation towards target point
        /// </summary>
        private static float ComputeAlignTorqueNormalized(Vector3 targetPosition, Transform ship)
        {
            Vector2 localTargetPosition = ship.InverseTransformPoint(targetPosition); // position in local coordinates
            float angle = Vector3.SignedAngle(localTargetPosition, Vector3.up, Vector3.forward); // angle between two vectors, forward = z
            angle = Mathf.Clamp(angle, - MAX_ANGLE, MAX_ANGLE) / MAX_ANGLE;

            Debug.Log("ComputeAlignTorqueNormalized");

            return - angle;
        }

        /// <summary>
        /// Random setting for a new target inside patrol zone
        /// </summary>
        private void ActionFindNewMovePosition()
        {
            Debug.Log("ActionFindNewMovePosition");
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else
                {
                    if (m_PatrolPoint != null)
                    {
                        bool isInsidePatrolZone = (m_PatrolPoint.transform.position - transform.position).
                                    sqrMagnitude < m_PatrolPoint.Radius * m_PatrolPoint.Radius;
                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                Vector2 newPoint = UnityEngine.Random.onUnitSphere * m_PatrolPoint.Radius +
                                   m_PatrolPoint.transform.position;

                                m_MovePosition = newPoint;
                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                                Debug.Log("newpoint");
                            }

                        }
                        else
                        {
                            m_MovePosition = m_PatrolPoint.transform.position;
                        }
                    }
                }
            }
        }


        /*private void ActionFindNewMovePosition()
        {
            Debug.Log("ActionFindNewMovePosition");
            if (m_AIBehaviour == AIBehaviour.Patrol)
            {
                if (m_SelectedTarget != null)
                {
                    m_MovePosition = m_SelectedTarget.transform.position;
                }
                else
                {
                    if (m_PatrolZone != null)
                    {
                        Debug.Log("InsidePatrolZone");
                        bool isInsidePatrolZone = (m_PatrolZone.transform.position - transform.position).
                                    sqrMagnitude < m_PatrolZone.Radius * m_PatrolZone.Radius;
                        if (isInsidePatrolZone == true)
                        {
                            if (m_RandomizeDirectionTimer.IsFinished == true)
                            {
                                m_MovePosition = (Vector3)m_PatrolZone.GetRandomInsideZone();                               
                                m_RandomizeDirectionTimer.Start(m_RandomSelectMovePointTime);
                                Debug.Log("newpoint");
                            }

                        }
                        else
                        {
                            m_MovePosition = m_PatrolZone.transform.position;
                        }
                    }
                    else if(m_PatrolPoints != null)
                    {
                        Debug.Log("PatrolPointsMovement");
                        if (m_PatrolPointCount >= m_PatrolPoints.Length)
                        {
                            m_PatrolPointCount = 0;
                            Debug.Log("m_PatrolPointCount = 0");
                        }

                        if (m_PatrolPointCount <= m_PatrolPoints.Length)
                        {
                            
                            m_MovePosition = m_PatrolPoints[m_PatrolPointCount].Position;


                            Debug.Log("start " + m_PatrolPointCount);
                            m_PatrolPointCount++;
                            Debug.Log("end " + m_PatrolPointCount); 
                        }                         


                    }
                }
            }
        }*/

        #region Timers

        /// <summary>
        /// Timer for searching new target
        /// </summary>

        private Timer m_RandomizeDirectionTimer;
        private Timer m_FireTimer;
        private Timer m_FindNewTargetTimer;
        private void InitTimers()
        {
            m_RandomizeDirectionTimer = new Timer(m_RandomSelectMovePointTime);
            m_FireTimer = new Timer(m_ShootDelay);
            m_FindNewTargetTimer = new Timer(m_FindNewTargetTime);
            Debug.Log("InitTimers");
        }

        private void UpdateTimers()
        {
            m_RandomizeDirectionTimer.RemoveTime(Time.deltaTime);
            m_FireTimer.RemoveTime(Time.deltaTime);
            m_FindNewTargetTimer.RemoveTime(Time.deltaTime);
            Debug.Log("UpdateTimers");
        }

        #endregion

    }
}
