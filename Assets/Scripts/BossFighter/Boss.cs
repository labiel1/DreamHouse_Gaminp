using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour {

    public int health;
    public int damage;
    private float timeBtwDamage = 1.5f;


    public Animator camAnim;
    public Slider healthBar;
    private Animator anim;
    public bool isDead;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {

        if (health <= 25) {
            anim.SetTrigger("stageTwo");
        }

        if (health <= 0) {
            anim.SetTrigger("death");
        }

        // give the player some time to recover before taking more damage !
        if (timeBtwDamage > 0) {
            timeBtwDamage -= Time.deltaTime;
        }

        healthBar.value = health;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        // if it's a charged beam it does -2 damage
        // otherwise just -1
        // Debug.Log("Collision Enter  - Tag: " + col.gameObject.tag + " - Layer: " + col.gameObject.layer);
        if (col.gameObject.tag == "ChargedBeam")
            health -= 2;
        else if (col.gameObject.layer == 10)
        {
           // anim.SetTrigger("Damaged");
            health--;

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // deal the player damage ! 
         if (other.CompareTag("PlayerBullet") && isDead == false) {
       // if (other.gameObject.layer == 10) { 
                if (timeBtwDamage <= 0) {
               // camAnim.SetTrigger("shake");
                other.GetComponent<Player>().health -= damage;
            }
        } 
    }
}
