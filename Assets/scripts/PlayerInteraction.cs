using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Interaction Settings")]
    [SerializeField] private float interactionRange = 5f;

    private Camera cam;
    private IInteractable currentInteractable;

    private void Start()
    {
        cam = GetComponentInChildren<Camera>();
        if (cam == null)
        {
            cam = Camera.main;
        }
    }

    private void Update()
    {
        CheckForInteractable();

        // 💡 E키를 누를 때 작동하는 구간
        if (Input.GetKeyDown(KeyCode.E) && currentInteractable != null)
        {
            PlayerController playerController = GetComponent<PlayerController>();
            currentInteractable.Interact(playerController);

            // 👇 여기에 UIManager를 통해 "Great"을 띄우는 코드를 넣습니다!
            UIManager.instance.ShowHint("Great");

            currentInteractable = null;
        }
    }

    private void CheckForInteractable()
    {
        if (cam == null) return;

        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                if (currentInteractable != interactable)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.HideHint();
                    }
                    currentInteractable = interactable;

                    // ❌ 바라볼 때 자동으로 ShowHint()가 실행되던 이 줄은 지워줍니다!
                    // currentInteractable.ShowHint(); 
                }
                return;
            }
        }

        if (currentInteractable != null)
        {
            currentInteractable.HideHint();
            currentInteractable = null;
        }
    }
}