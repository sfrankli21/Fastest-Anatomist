
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraPositionManager : MonoBehaviour
{
    [Header("Cameras")]
    public Camera OrbitCamera;
    public Camera Sagittal;
    public Camera Coronal;
    public Camera Transverse;

    [Header("Orbit")]
    public Transform OrbitPoint;
    public float OrbitSpeed = 0.2f;

    private float yaw;
    private float pitch;
    private float distance;

    private void Start()
    {
        Vector3 offset = OrbitCamera.transform.position - OrbitPoint.position;

        distance = offset.magnitude;

        Vector3 angles = OrbitCamera.transform.eulerAngles;
        yaw = angles.y;
        pitch = angles.x;

        UpdateOrbitCamera();
    }

    private void Update()
    {
        Mouse mouse = Mouse.current;

        if (mouse.rightButton.isPressed)
        {
            Vector2 mouseDelta = mouse.delta.ReadValue();

            yaw += mouseDelta.x * OrbitSpeed;
            pitch -= mouseDelta.y * OrbitSpeed;

            pitch = Mathf.Clamp(pitch, -89f, 89f);
        }

        UpdateOrbitCamera();
    }

    private void UpdateOrbitCamera()
    {
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 offset = rotation * new Vector3(0f, 0f, -distance);

        OrbitCamera.transform.position = OrbitPoint.position + offset;
        OrbitCamera.transform.rotation = rotation;
    }
}

