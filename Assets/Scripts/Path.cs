using UnityEngine;

namespace SpaceShooter
{
    public class Path : MonoBehaviour
    {
       /* [SerializeField] private CircleArea startArea;
        public CircleArea StartArea { get { return startArea; }  }*/

        [SerializeField] private AIPointPatrol[] points;
        public int Length { get => points.Length; } 
        public AIPointPatrol this[int i] { get => points[i]; }



        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            foreach(var point in points)
            {
                Gizmos.DrawSphere(point.transform.position, point.Radius);
            }
        }
    }
}