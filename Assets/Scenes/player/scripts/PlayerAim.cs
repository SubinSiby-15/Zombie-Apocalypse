using UnityEngine;

public class AimCamera : MonoBehaviour
{
    public GameObject normalCamera;
    public GameObject aimCamera;
   
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            normalCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
        else
        {
            normalCamera.SetActive(true);
            aimCamera.SetActive(false);
        }
    }
}