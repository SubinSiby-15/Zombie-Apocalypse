using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{
    public Transform target;

    [Header("Camera Settings")]
    public float distance = 3f;
    public float mouseSensitivity = 200f;

    [Header("Height Settings")]
    public float height = 1.7f;
    public float shoulderOffset = 0.6f;

    [Header("Smooth")]
    public float smoothSpeed = 10f;

    private float yaw;
    private float pitch = 15f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") *
                       mouseSensitivity *
                       Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") *
                       mouseSensitivity *
                       Time.deltaTime;

        yaw += mouseX;
        pitch -= mouseY;

        pitch = Mathf.Clamp(pitch, -30f, 60f);
    }

    void LateUpdate()
    {
        if (target == null)
            return;

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        Vector3 direction =
            rotation * new Vector3(shoulderOffset, 0, -distance);

        Vector3 targetPosition =
            target.position +
            Vector3.up * height +
            direction;

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );

        Vector3 lookPoint =
            target.position + Vector3.up * height;

        transform.LookAt(lookPoint);
    }
}