using UnityEngine;
using UnityEngine.InputSystem;

public class Handcontroller : MonoBehaviour
{
    private Animator animator;
    [SerializeField]
    private InputActionReference gripInput;
    [SerializeField]
    private InputActionReference triggerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent <Animator>();  
    }

    // Update is called once per frame
    void Update()
    {
        float gripValue = gripInput.action.ReadValue<float>();
        float triggerValue = triggerInput.action.ReadValue<float>();

        animator.SetFloat("Grip", gripValue);
        animator.SetFloat("Trigger", triggerValue);

    }
}
