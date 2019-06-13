using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCatch : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

   void OnCollisionEnter2D(Collision2D col)
   {
       // if it's a charged beam it does -2 damage
       // otherwise just -1
       Debug.Log(col.gameObject.tag + " - " + col.gameObject.layer);
       if (col.gameObject.tag == "Player")
           Destroy(this.gameObject, 0.01f);
   }
}
