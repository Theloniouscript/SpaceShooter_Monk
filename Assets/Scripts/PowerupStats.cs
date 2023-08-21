using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class PowerupStats : Powerup
    {
        //public AudioSource audioPlayer;

        public enum EffectType
        {
            AddAmmo, 
            AddEnergy,
            AddArmor
        }

        [SerializeField] private EffectType m_EffectType;
        [SerializeField] private float m_Value;
        
        

        protected override void OnPickedUp(SpaceShip ship)
        {
            if (m_EffectType == EffectType.AddEnergy)
                ship.AddEnergy((int)m_Value);               
                
            if (m_EffectType == EffectType.AddAmmo)
                ship.AddAmmo((int)m_Value);

            if (m_EffectType == EffectType.AddArmor)            
                ship.AddArmor((int)m_Value);           
               
        }

       

       /* private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.gameObject.tag == "CollisionTag")
            {
                audioPlayer.Play();
                Debug.Log("collision!!!");
            }
        }*/
    }
}
