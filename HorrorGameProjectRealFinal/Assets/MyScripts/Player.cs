using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [Header("stats")]
    public int currentHP;
    public int maxHP;

    [Header("Movement")]
    public float moveSpeed;
    public int jumpForce;

    [Header("Camera")]
    public float lookSensitivity;//how fast the camera rotates around
    public float maxLookX;//for limiting the camera look
    public float minLookX;
    private float rotX; //current x rotation of the camera

    private Camera cam;
    private Rigidbody rig;
    private Weapon weapon;
    public CharacterController controller;


    private void Awake()
    {
        //get the components
        cam = Camera.main;
        rig = GetComponent<Rigidbody>();
        weapon = GetComponent<Weapon>();

        //disable cursor
        Cursor.lockState = CursorLockMode.Locked;

    }

    private void Start()
    {
        //initialize the UI
        UIManager.instance.UpdateHealthBar(currentHP, maxHP);
        UIManager.instance.UpdateScoreText(0);
        UIManager.instance.UpddateAmmoText(weapon.currentAmmo, weapon.maxAmmo);

    }

    private void Update()
    {
        //dont do anything if the game is paused
        if (GameManager.instance.gamePaused == true)
            return;

        Move();
        if(Input.GetButtonDown("Jump"))
            TryJump();
       
        if(Input.GetButton("Fire1"))
        {
            if (weapon.CanShoot())
                weapon.Shoot();
        }

        if(Cursor.lockState==CursorLockMode.Locked)
       CameraLook();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal") * moveSpeed;
        float z = Input.GetAxis("Vertical") * moveSpeed;

        Vector3 dir = transform.right * x + transform.forward * z;
        dir.Normalize();
        dir *= moveSpeed * Time.deltaTime;

        controller.Move(dir);
        dir.y = rig.velocity.y;
        rig.velocity = dir;
       
    }

    void CameraLook()
    {
        float y = Input.GetAxis("Mouse X") * lookSensitivity;
        rotX += Input.GetAxis("Mouse Y") * lookSensitivity;
        rotX = Mathf.Clamp(rotX, minLookX, maxLookX);
        /*rotX: the floating point value to restrict inside the range defined by the 
        min and max values

        minLookX: the minimum floating point value to compare against.
        maxLookX: the max floating point value to compare against.
        Clamps the given value between the minmum float and maximum float values.
        Returns the given value if it is within the min and max range.
         */

        cam.transform.localRotation = Quaternion.Euler(-rotX, 0, 0);
        /*Quaternion.Euler returns a rotation that rotates z degrees around
        the z axis, x degrees around the x axis, and the y axis, applied in that order. 
        */
        transform.eulerAngles += Vector3.up * y;
        //represents rotation in world space (transform.eulerAngles)

    }

    void TryJump()
    {
        
        Ray ray = new Ray(transform.position, Vector3.down);
        if(Physics.Raycast(ray,1.1f)) 
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
           
        }
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        UIManager.instance.UpdateHealthBar(currentHP, maxHP);
        if (currentHP <= 0)
            Die();

    }

    void Die()
    {
        GameManager.instance.LoseGame();
        
    }

    public void GiveHealth(int amountToGive)
    {
        currentHP = Mathf.Clamp(currentHP + amountToGive, 0, maxHP);
        UIManager.instance.UpdateHealthBar(currentHP, maxHP);
    }

    public void GiveAmmo(int amountToGive)
    {
        weapon.currentAmmo= Mathf.Clamp(weapon.currentAmmo + amountToGive, 0, weapon.maxAmmo);

        UIManager.instance.UpddateAmmoText(weapon.currentAmmo, weapon.maxAmmo);
    }
}
