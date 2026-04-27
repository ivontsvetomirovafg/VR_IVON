using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Hands.Gestures;

public class SliderController : MonoBehaviour
{
    [SerializeField]
    private InputActionReference grabLeft, grabRight;
    private Vector3 initialPos;
    [SerializeField]
    private float limiteSlider;
    [SerializeField]
    private Transform leftHand, rightHand;
    private Vector3 handPos;
    private Transform usedHand;
    private Vector3 handPose;
    private bool isGrabbed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
   
    }

    void SliderGrabbed(InputAction.CallbackContext context)
    {
        Debug.Log("Agarro");
        initialPos = transform.localPosition;
        handPos = usedHand.position;
        isGrabbed = true;
        StopAllCoroutines();
    }

    void SliderUnGrabbed(InputAction.CallbackContext context)
    {
        isGrabbed = false;
        //transform.localPosition = initialPos;
        StartCoroutine(ReturnToInitialPos());

        //grabLeft.action.canceled -= SliderUnGrabbed;
        //grabRight.action.canceled -= SliderUnGrabbed;
    }

    IEnumerator ReturnToInitialPos()
    {
        Vector3 startPos = transform.localPosition;
        float t = 0f;
        while (t <1)
        {
            t += Time.deltaTime*5;
            transform.localPosition = Vector3.Lerp(startPos, initialPos, t);
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isGrabbed == true)
        {
            Vector3 newHandPos = usedHand.position;
            Vector3 deltaPos = newHandPos - handPos;
            float distancia = Mathf.Clamp (deltaPos.magnitude, 0, limiteSlider);
            transform.localPosition = initialPos + new Vector3(0,0, distancia);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "LeftHand")
        {
            grabLeft.action.performed += SliderGrabbed;
            grabLeft.action.canceled += SliderUnGrabbed;
            usedHand = leftHand;
        }
        else if (other.gameObject.tag == "RightHand")
        {
            grabRight.action.performed += SliderGrabbed;
            grabRight.action.canceled += SliderUnGrabbed;
            usedHand = rightHand;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LeftHand")
        {
            grabLeft.action.performed -= SliderGrabbed;
        }
        else if (other.gameObject.tag == "RightHand")
        {
            grabRight.action.performed -= SliderGrabbed;
        }
    }
}
