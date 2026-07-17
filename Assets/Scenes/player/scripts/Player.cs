using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    public float jumpHeight = 1.5f;
    public float gravity = -9.81f;

    [Header("Camera")]

    public Transform playerCamera;
    public float mouseSensitivity = 200f;
    public float maxLookAngle = 75f;
    public float cameraSmoothSpeed = 10f;

    [Header("Health")]
    public int maxHealth = 100;
    [HideInInspector]
    public int currentHealth;

    private CharacterController cc;
    private Animator anim;

    private Vector3 velocity;
    private float xRotation = 0f;
    private float currentSpeed;
    private Healthber healthBar;
    private Weapon weapon;
    private Camera cam;
    public float RotationSpeed = 60f;
    public Quaternion targetRotation;

    // public Camera aimCamera;
    Vector3 move;
    private SaveSystem SaveSystem;
    void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();

        SaveSystem = FindObjectOfType<SaveSystem>();

        currentHealth = SaveSystem.LoadHealth();
        weapon = FindObjectOfType<Weapon>();
        cam = FindObjectOfType<Camera>();

        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            SaveSystem.SaveData(currentHealth, 0);
        }

        healthBar = FindObjectOfType<Healthber>();
        healthBar.UpdateHealthBar(currentHealth, maxHealth); // ✅ UPDATE UI

        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        MouseLook();
        Movement();
        HandleAnimation();
       //TakeDamage(10); // For testing purposes, you can remove this line later
        shoot();
    }

    // 🎥 Camera
    void MouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -maxLookAngle, maxLookAngle);

        playerCamera.localRotation = Quaternion.Lerp(
            playerCamera.localRotation,
            Quaternion.Euler(xRotation, 0f, 0f),
            cameraSmoothSpeed * Time.deltaTime
        );

        playerCamera.Rotate(Vector3.up * mouseX);
    }

    // 🚶 Movement
    void Movement()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 forward = playerCamera.forward;
        Vector3 right = playerCamera.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        move = forward * v + right * h;

        // Rotate player
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);

            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }

        // Ground check
        if (cc.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

            anim.SetBool("Jump", false);

        }
        anim.SetFloat("velocity.y", velocity.y);
        Debug.Log("Velocity y: " + velocity.y);
        Debug.Log("Grounded: " + cc.isGrounded);

        // Jump
        if (Input.GetButtonDown("Jump") && cc.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            anim.SetBool("Jump", true);
        }

        // Gravity
        velocity.y += gravity * Time.deltaTime;

        // FINAL MOVE
        Vector3 finalMove = move * moveSpeed;
        finalMove.y = velocity.y;

        cc.Move(finalMove * Time.deltaTime);
    }

    // 🎬 Animation
    void HandleAnimation()
    {
        if (anim == null) return;

        float targetSpeed = move.magnitude;
        currentSpeed = Mathf.Lerp(currentSpeed, targetSpeed, 10f * Time.deltaTime);

        anim.SetBool("Running", currentSpeed > 0.1f);

    }

    // ❤️ Damage System
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        Debug.Log("Health: " + currentHealth);

        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // ✅ SAVE AFTER DAMAGE
        SaveSystem.SaveData(currentHealth, 0);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player Died");

        // ✅ Reset data properly
        SaveSystem.ClearData();

        // Reset health locally
        currentHealth = maxHealth;

        // Update UI immediately
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (anim != null)
        {
            anim.SetTrigger("Death");
        }

        enabled = false;
    }

    void OnApplicationQuit()
    {
        SaveSystem.SaveData(currentHealth, 0);
    }

    void shoot()
    {
        if (Input.GetMouseButton(0))
        {
           weapon.Shoot();
           weapon.AimAtEnemy(null); // Aim at mouse position
           anim.SetTrigger("Shoot"); // Running shoot
           anim.SetBool("Running", true); // Ensure running is true for running shoot
           anim.SetBool("Idle", true); // Ensure idle shoot is true for running shoot
         
        }
    }

}




