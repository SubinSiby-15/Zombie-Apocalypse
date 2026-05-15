using UnityEngine;

public class Camera : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Settings")]
    public float distance = 4f;
    public float height = 1.8f;
    public float mouseSensitivity = 200f;

    [Header("Rotation Limits")]
    public float minPitch = -30f;
    public float maxPitch = 60f;

    [Header("Smooth")]
    public float smoothSpeed = 10f;

    private float yaw = 0f;
    private float pitch = 20f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        // Mouse Input
        yaw += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        pitch -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Clamp up/down look
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Rotation
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);

        // Camera Position
        Vector3 targetPosition = target.position + Vector3.up * height;
        Vector3 cameraPosition = targetPosition - rotation * Vector3.forward * distance;

        // Smooth Follow
        transform.position = Vector3.Lerp(
            transform.position,
            cameraPosition,
            smoothSpeed * Time.deltaTime
        );

        // Look at Player
        transform.LookAt(targetPosition);
    }
}