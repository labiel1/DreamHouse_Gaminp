using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class planeControll : MonoBehaviour {
	public float planeSpeed;
    public uiManager ui;
	Vector3 position;
    private static Animator anim;
    public int health = 8;
    public Text ValorVida;
    // Use this for initialization
    void Start () {
		position = transform.position;
        anim = GetComponent<Animator>();
        ValorVida.text = health.ToString();
    }

    public void UpdateHealthBar()
    {
        if (health < 0)
        {
            health = 0;
        }
        else
        {
            ValorVida.text = health.ToString();
        }
    }
    // Update is called once per frame
    void Update () {
        
		position.y += Input.GetAxis ("Vertical")*planeSpeed*Time.deltaTime;
		position.y = Mathf.Clamp (position.y, -4.3f, 4.3f);
        Debug.Log(Input.GetAxis("Vertical"));
        if (Input.GetAxis("Vertical") > 0)
        {
            anim.SetTrigger("Up");
           anim.SetBool("Up", true);
           anim.SetBool("Down", false);
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            anim.SetTrigger("Down");
            anim.SetBool("Up", false);
            anim.SetBool("Down", true);
        } else
        {
            anim.SetTrigger("Idle");
            anim.SetBool("Up", false);
            anim.SetBool("Down", false);
        }
		position.x += Input.GetAxis ("Horizontal")*planeSpeed*Time.deltaTime;
		position.x = Mathf.Clamp (position.x, -6.8f, 4f);
		transform.position = position;
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "ENEMY")
        {
            ui.gameOverActivated();
            Destroy(gameObject);
        }
    }
}
