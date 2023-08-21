using UnityEngine;

namespace SpaceShooter
{
    public class MovementController : MonoBehaviour
    {
        public AudioSource audioPlayer;
        public enum ControlMode
        {
            Keyboard,
            Mobile
        }

        [SerializeField] private SpaceShip m_TargetShip;
        [SerializeField] private VirtualJoystick m_MobileJoystick;
        [SerializeField] private ControlMode m_ControlMode;

        [SerializeField] private PointerClickHold m_MobileFirePrimary;
        [SerializeField] private PointerClickHold m_MobileFireSecondary;


        public void SetTargetShip(SpaceShip ship) => m_TargetShip = ship;

        private void Start()
        {
            if(m_ControlMode == ControlMode.Keyboard)
            {
                m_MobileJoystick.gameObject.SetActive(false);
                m_MobileFirePrimary.gameObject.SetActive(false);
                m_MobileFireSecondary.gameObject.SetActive(false);
            }
                

            else
            {
                m_MobileJoystick.gameObject.SetActive(true);
                m_MobileFirePrimary.gameObject.SetActive(true);
                m_MobileFireSecondary.gameObject.SetActive(true);
            }
        }
        private void Update()
        {
            if (m_TargetShip == null) return;
            if (m_ControlMode == ControlMode.Keyboard)
                ControlKeyboard();

            if (m_ControlMode == ControlMode.Mobile)
                ControlMobile();
        }

        private void ControlMobile()
        {
            /*Vector3 dir = m_MobileJoystick.Value;

            var dot = Vector2.Dot(dir, m_TargetShip.transform.up);
            var dot2 = Vector2.Dot(dir, m_TargetShip.transform.right);

            m_TargetShip.ThrustControl = Mathf.Max(0, dot);
            m_TargetShip.TorqueControl = -dot2;*/

            if (m_MobileFirePrimary.IsHold == true)
                m_TargetShip.Fire(TurretMode.Primary);

            if (m_MobileFireSecondary.IsHold == true)
                m_TargetShip.Fire(TurretMode.Secondary);

            var dir = m_MobileJoystick.Value;
            m_TargetShip.ThrustControl = dir.y;
            m_TargetShip.TorqueControl = - dir.x;

        }

        private void ControlKeyboard()
        {
            float thrust = 0;
            float torque = 0;

            if (Input.GetKey(KeyCode.UpArrow))
                thrust = 1.0f;

            if (Input.GetKey(KeyCode.DownArrow))
                thrust = -1.0f;

            if (Input.GetKey(KeyCode.LeftArrow))
                torque = 1.0f;

            if (Input.GetKey(KeyCode.RightArrow))
                torque = -1.0f;

            if (Input.GetKey(KeyCode.Space))
            {
                m_TargetShip.Fire(TurretMode.Primary);
            } else if (Input.GetKey(KeyCode.X))
            {
                m_TargetShip.Fire(TurretMode.Secondary);
                //Debug.Log("X pressed");
            }
            
            m_TargetShip.ThrustControl= thrust;
            m_TargetShip.TorqueControl= torque;

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.tag == "CollisionTag")
            {
                audioPlayer.Play();
                Debug.Log("collision!!!");
            }
        }

    }

}
