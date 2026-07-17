using UnityEngine;

public class Aim : MonoBehaviour
{
    public Transform target;

    [Header("Camera Settings")]
    public float distance;
    public float mouseSensitivity = 200f;

    [Header("Height Settings")]
    public float height;
    public float shoulderOffset;

    [Header("Smooth")]
    public float smoothSpeed = 10f;

    private float yaw;
    private float pitch = 15f;

    public Transform aimTarget;
    public bool isAiming;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        pitch = Mathf.Clamp(pitch, -30f, 60f);

        isAiming = Input.GetMouseButton(0);

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
            smoothSpeed * Time.deltaTime);

        if (isAiming && aimTarget != null)
        {
            transform.LookAt(aimTarget.position);
        }
        else
        {
            transform.LookAt(target.position + Vector3.up * height);
        }
    }
}
   