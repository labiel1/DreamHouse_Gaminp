using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public enum PlayerStates {
        idle = 0,
        left,
        right,
        jump,
        landing,
        falling,
        kill,
        resurrect,
        firingWeapon,
        isOnWall,
        Die,
        _stateCount
    };

    public static float[] stateDelayTimer = new float[(int)PlayerStates._stateCount];

    public delegate void playerStateHandler(PlayerStates newState);

    public static event playerStateHandler OnStateChange;

    private GameObject mobileInput;

    private PlayerStateController ps;

    public bool facingRight = true;     // used to check if player is facing right

    public int ENEMY_LAYER = 11;
    public int RESPAWN_LAYER = 12;
    public int COIN_LAYER = 13;

    private void Start()
    {
        ps = GetComponent<PlayerStateController>();
        if(SystemInfo.deviceType != DeviceType.Handheld)
        {
            mobileInput = GameObject.Find("MobileInput");
    //        mobileInput.SetActive(false);
        }
        
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == ENEMY_LAYER)
        {
           // if (Time.time >= stunMovementTime)
            {
                Damage();
            }
        }
        else if (col.gameObject.layer == RESPAWN_LAYER)
        {
            ps.health = 0;
        }

        if (col.gameObject.layer == COIN_LAYER)
        {

            {
                CoinColect();
            }
        }

    }


    void CoinColect()
    {
        ps.coins++;
        ps.UpdateCoinBar();
    }

    // Damage processes
    void Damage()
    {
        ps.StopAim();     //sets the damage animation
       // stunMovement = true;            //stuns player movement for a period of time
       // stunMovementTime = Time.time + 0.5f;
        ps.health--;
        ps.UpdateHealthBar();
    }

    void Update()
    {
        //if (!GameStates.gameActive) return;
        // Get device type
        if(SystemInfo.deviceType == DeviceType.Desktop)
        {
            // Input for keyboard (mostly for testing purposes)
            float horizontal = Input.GetAxisRaw("Horizontal");

            if (Input.GetMouseButton(0))
            {
                ps.Aim();
                //if (OnStateChange != null) OnStateChange(PlayerStates.firingWeapon);
            }
            else if(Input.GetMouseButtonUp(0))
            {
                ps.StopAim();
            }
               
            if ((Input.GetKeyDown(KeyCode.Space) ) || (Input.GetMouseButtonDown(1)))
            {
                if (OnStateChange != null) OnStateChange(PlayerStates.jump);
            }
            if (horizontal != 0f)
            {
                if (horizontal < 0f)
                {

                    if (OnStateChange != null) OnStateChange(PlayerStates.left);
                    facingRight = !facingRight;
                }
                else
                {
                    if (OnStateChange != null) OnStateChange(PlayerStates.right);
                    facingRight = !facingRight;
                }
            }
            else
            {
                if (OnStateChange != null) OnStateChange(PlayerStates.idle);
            }
        }
        else if(SystemInfo.deviceType == DeviceType.Handheld)
        {
            // Mobile input
            if(SimpleInput.GetButton("Attack"))
            {
                ps.Aim();
                //if (OnStateChange != null) OnStateChange(PlayerStates.firingWeapon);
            }
            else if(SimpleInput.GetButtonUp("Attack"))
            {
                ps.StopAim();
            }

            if(SimpleInput.GetButtonDown("Jump"))
            {
                if (OnStateChange != null) OnStateChange(PlayerStates.jump);
            }

            if(SimpleInput.GetButton("Left"))
            {
                if (OnStateChange != null) OnStateChange(PlayerStates.left);
            }
            else if(SimpleInput.GetButton("Right"))
            {
                if (OnStateChange != null) OnStateChange(PlayerStates.right);
            }
            else
            {
                if (OnStateChange != null) OnStateChange(PlayerStates.idle);
            }
        }
        
    }
}
