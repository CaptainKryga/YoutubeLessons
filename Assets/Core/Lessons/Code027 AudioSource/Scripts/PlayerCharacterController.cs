using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerCharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpHeight = 1.2f;
    [SerializeField] private float gravity = -20f;

    [Header("Optional Camera Reference")]
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Vector3 verticalVelocity;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 input = new Vector3(x, 0f, z).normalized;
        Vector3 move = input;

        // Делаем движение относительно камеры, если ссылка задана
        if (cameraTransform != null && input.sqrMagnitude > 0f)
        {
            Vector3 camForward = cameraTransform.forward;
            Vector3 camRight = cameraTransform.right;

            camForward.y = 0f;
            camRight.y = 0f;

            camForward.Normalize();
            camRight.Normalize();

            move = (camForward * input.z + camRight * input.x).normalized;
        }

        // Небольшое прижатие к земле
        if (controller.isGrounded && verticalVelocity.y < 0f)
        {
            verticalVelocity.y = -2f;
        }

        // Прыжок
        if (Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Гравитация
        verticalVelocity.y += gravity * Time.deltaTime;

        // Одно итоговое движение за кадр
        Vector3 finalMotion = move * moveSpeed + Vector3.up * verticalVelocity.y;
        CollisionFlags flags = controller.Move(finalMotion * Time.deltaTime);

        // Если ударились головой — сбрасываем вертикальную скорость вверх
        if ((flags & CollisionFlags.Above) != 0 && verticalVelocity.y > 0f)
        {
            verticalVelocity.y = 0f;
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody body = hit.collider.attachedRigidbody;

        if (body == null || body.isKinematic)
            return;

        Vector3 pushDirection = new Vector3(hit.moveDirection.x, 0f, hit.moveDirection.z);
        body.AddForce(pushDirection * 3f, ForceMode.Impulse);
    }
}