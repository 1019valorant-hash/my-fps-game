using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public HitscanWeapon activeWeapon;
    public float moveSpeed = 5f;
    public float sprintSpeed = 7.5f;
    public float mouseSensitivity = 2.5f;
    public float gravity = -20f;
    public float jumpHeight = 1.2f;
    public float health = 100f;

    private CharacterController controller;
    private float verticalVelocity;
    private float pitch;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        if (playerCamera == null) playerCamera = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Look();
        Move();
        WeaponInput();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool locked = Cursor.lockState == CursorLockMode.Locked;
            Cursor.lockState = locked ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = locked;
        }
    }

    private void Look()
    {
        if (Cursor.lockState != CursorLockMode.Locked || playerCamera == null) return;
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
        pitch = Mathf.Clamp(pitch - mouseY, -89f, 89f);
        playerCamera.transform.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");
        Vector3 input = Vector3.ClampMagnitude(new Vector3(x, 0f, z), 1f);
        float speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : moveSpeed;
        Vector3 motion = transform.TransformDirection(input) * speed;

        if (controller.isGrounded && verticalVelocity < 0f) verticalVelocity = -2f;
        if (controller.isGrounded && Input.GetButtonDown("Jump"))
            verticalVelocity = Mathf.Sqrt(jumpHeight * -2f * gravity);
        verticalVelocity += gravity * Time.deltaTime;
        motion.y = verticalVelocity;
        controller.Move(motion * Time.deltaTime);
    }

    private void WeaponInput()
    {
        if (activeWeapon == null || playerCamera == null) return;
        if (Input.GetKeyDown(KeyCode.R)) activeWeapon.StartReload();
        bool fire = activeWeapon.isAutomatic ? Input.GetMouseButton(0) : Input.GetMouseButtonDown(0);
        if (fire) activeWeapon.TryFire(playerCamera.transform.forward);
    }
}
