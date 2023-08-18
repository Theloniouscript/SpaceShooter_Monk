using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class BG : MonoBehaviour
    {
        
        void Update()
        {
            MeshRenderer mr = GetComponent<MeshRenderer>();
            Material mat = mr.material;

            Vector2 offset = mat.mainTextureOffset;
            //offset.x += Time.deltaTime;

            offset.x = transform.position.x / transform.localScale.x;
            offset.y = transform.position.y / transform.localScale.y;



            mat.mainTextureOffset = offset;

        
        }
    }
}
