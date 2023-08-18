using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class Explosion : MonoBehaviour
    {
        [SerializeField] private GameObject boomPrefab;

        private void Start()
        {
            Explode();
        }

        public void Explode()
        {
            if(boomPrefab != null)
            {
                
                Destroy(gameObject, 1f);
            }
        }


        /*private Camera mainCamera;

        private void Start()
        {
            mainCamera= Camera.main;
        }

        private void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
                mousePosition.z = 0;
                if(boomPrefab != null)
                {
                    var explosion = Instantiate(boomPrefab, mousePosition, Quaternion.identity);
                    Destroy(explosion, 1f);
                    

                }
               
                

            }
        }
*/


        /*private void Start()
        {
            Invoke("Explode", 4f);
        }

        void Explode()
        {
            ParticleSystem exp = GetComponent<ParticleSystem>();
            exp.Play();
            Destroy(gameObject, exp.main.duration);
        }
*/



        /* [SerializeField] private float Radius;
         [SerializeField] private float Force;

         //public bool Active;
         public GameObject ExplosionEffect; // ссылка на систему частиц

         private void Start()
         {
             Explode();
         }

         public void Explode(Vector2 position)
         {
             //Vector2 position = transform.position;
             Collider2D[] overlappedColliders = Physics2D.OverlapCircle(position, Radius);

             for (int i = 0; i < overlappedColliders.Length; i++)
             {
                 Rigidbody rigidbody = overlappedColliders[i].attachedRigidbody;
                 if (rigidbody)
                 {
                     rigidbody.AddExplosionForce(Force, transform.position, Radius);
                 }

                 Destroy(gameObject);

                 Instantiate(ExplosionEffect, transform.position, Quaternion.identity);


             }

         }

         *//*private void Update()
         {
             if(Active)
             {
                 Explode();
             }
         }*/

    }
}
