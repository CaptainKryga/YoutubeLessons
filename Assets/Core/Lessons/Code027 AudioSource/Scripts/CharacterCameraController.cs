using UnityEngine;

public sealed class CharacterCameraController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform targetCamera;
    [SerializeField] private CharacterController characterController;

    [Header("Rotation")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private float minPitch = -70f;
    [SerializeField] private float maxPitch = 70f;

    private float pitch;

    private void Start()
    {
        if (targetCamera == null)
        {
            Debug.LogError($"{nameof(CharacterCameraController)}: Camera reference is missing.", this);
            enabled = false;
            return;
        }

        if (characterController == null)
        {
            Debug.LogError($"{nameof(CharacterCameraController)}: CharacterController reference is missing.", this);
            enabled = false;
            return;
        }

        pitch = NormalizeAngle(targetCamera.transform.localEulerAngles.x);
    }

    private void Update()
    {
        if (!Input.GetMouseButton(0))
            return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // Вращаем CharacterController по Y
        characterController.transform.Rotate(0f, mouseX, 0f);

        // Вращаем камеру по X
        pitch -= mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        Vector3 cameraEuler = targetCamera.transform.localEulerAngles;
        cameraEuler.x = pitch;
        cameraEuler.y = 0f;
        cameraEuler.z = 0f;

        targetCamera.transform.localEulerAngles = cameraEuler;
    }

    private float NormalizeAngle(float angle)
    {
        if (angle > 180f)
            angle -= 360f;

        return angle;
    }
}