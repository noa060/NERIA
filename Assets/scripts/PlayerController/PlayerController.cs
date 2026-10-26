using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private float gravity = -9.81f; // 💡 중력 추가

    [Header("Camera")]
    [SerializeField] private Transform cameraTarget;
    [SerializeField] private float cameraSensitivity = 2f;
    [SerializeField] private float minVertical = -30f;
    [SerializeField] private float maxVertical = 60f;

    [Header("Interaction")]
    [SerializeField] private float interactionRange = 2f;

    private CharacterController controller;
    private Animator animator;
    private float verticalRotation = 0f;
    private float verticalVelocity = 0f; // 💡 낙하/착지 속도 변수 추가

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        // 마우스 커서 숨김
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        HandleMovement();
        HandleCamera();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // 카메라 기준으로 이동 방향 계산
        Vector3 moveDirection = transform.forward * verticalInput + transform.right * horizontalInput;
        Vector3 velocity = moveDirection.normalized * moveSpeed;

        // 💡 [핵심] 바닥에 닿아있을 때 수직 속도를 아래로 살짝 눌러주어 지면에 고정시킵니다.
        if (controller.isGrounded)
        {
            verticalVelocity = -2f;
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime; // 공중에 뜨면 중력 적용
        }

        velocity.y = verticalVelocity;
        controller.Move(velocity * Time.deltaTime);

        // 애니메이션
        float speed = new Vector3(horizontalInput, 0, verticalInput).magnitude;
        animator.SetFloat("Speed", speed);
    }

    private void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * cameraSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * cameraSensitivity;

        // 수평 회전 (캐릭터 몸)
        transform.Rotate(Vector3.up * mouseX);

        // 수직 회전 (카메라)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, minVertical, maxVertical);

        if (cameraTarget != null)
        {
            cameraTarget.localRotation = Quaternion.Euler(verticalRotation, 0, 0);
        }
    }

    private void HandleInteraction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, interactionRange))
            {
                IInteractable interactable = hit.collider.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactable.Interact(this);
                }
            }
        }
    }
}