using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TopDown : MonoBehaviour
{
    // Variables

    public float wasdSpeed = 5f;
    public Rigidbody2D PlayerBody;
    public Weapon Gun;//getting things from other classes
    public SpecialWeapon SpecialGun;
    public Transform gunPivot;
    private SpriteRenderer playerSprite;
    Animator anim;

    [SerializeField] bool canShootSpecial = false;
    bool isIdling = false;


    //movement
    Vector2 MoveControl;
    Vector2 MouseControl;

    Vector2 move;
    //

    //dash variables
    public float DashDistance = 15f;
    bool isDashing;
    float DoubleTap;
    KeyCode LastKeycode;
    //


    private void OnEnable()
    {
        EnergyBarEvents.OnEnergyFull += EnableSpecialGun;
        EnergyBarEvents.OnSpecialShot += EnableNormalGun;
    }

    private void OnDisable()
    {
        EnergyBarEvents.OnEnergyFull -= EnableSpecialGun;
        EnergyBarEvents.OnSpecialShot -= EnableNormalGun;
    }

    private void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    void EnableNormalGun()
    {
        Gun.gameObject.SetActive(true);
        SpecialGun.gameObject.SetActive(false);
        canShootSpecial = false;
    }

    void EnableSpecialGun()
    {
        Gun.gameObject.SetActive(false);
        SpecialGun.gameObject.SetActive(true);
        canShootSpecial = true;
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!canShootSpecial)
            {
                Gun.Fire();//getting fire method from weapon script
            }
            else
            {
                SpecialGun.Fire();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (!isDashing)
                StartCoroutine(Dash(move));
        }

    }

    //enumerator for dashing 
    public IEnumerator Dash(Vector2 _direction)
    {
        isDashing = true;
        PlayerBody.AddForce(_direction.normalized * DashDistance, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.4f);
        isDashing = false;

    }
    //





    private void FixedUpdate()
    {
        //WASD MOVEMENT
        move = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (move.magnitude > 0.05 || move.magnitude < 0.05)
        {
            if (isIdling)
            {
                isIdling = false;
                anim.Play("Walk");
            }
            
        }
        else
        {
            if(!isIdling)
            {
                isIdling = true;
                anim.Play("Idle");
            }
            
        }

        //
        //shooting mechanic 


        MoveControl = move.normalized;
        MouseControl = Camera.main.ScreenToWorldPoint(Input.mousePosition);




        if (!isDashing)
        {
            PlayerBody.velocity = new Vector2(MoveControl.x * wasdSpeed, MoveControl.y * wasdSpeed);//moving player

            Vector2 aimDirection = MouseControl - PlayerBody.position;

            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            //PlayerBody.rotation = aimAngle;
            Quaternion _currentRotation = new();
            _currentRotation.eulerAngles = new Vector3(0, 0, aimAngle + 90);
            gunPivot.rotation = _currentRotation;

            if (MouseControl.x < transform.position.x)
            {
                playerSprite.flipX = true;
                gunPivot.transform.localScale = new Vector3(1, -1, 1);
            }
            else
            {
                gunPivot.transform.localScale = new Vector3(1, 1, 1);
                playerSprite.flipX = false;
            }
        }


        //
    }
}
