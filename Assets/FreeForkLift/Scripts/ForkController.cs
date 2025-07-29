using System.Collections;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ForkController : MonoBehaviour
{
    // === FORK MOVEMENT SYSTEM ===
    [Header("Fork Settings")]
    [Tooltip("Transform of the fork that moves up and down.")]
    public Transform fork;

    [Tooltip("Speed at which the fork moves up or down.")]
    public float forkLiftSpeed = 1f;

    [Tooltip("Local position the fork moves to when fully raised.")]
    public Vector3 forkMaxHeight;

    [Tooltip("Local position the fork moves to when fully lowered.")]
    public Vector3 forkMinHeight;

    // === LEVER SETUP ===
    [Header("Fork Lever Setup")]
    [Tooltip("The lever GameObject used to control the fork.")]
    public XRKnob forkLever;


    [Header("Lever Behavior")]
   
    [Tooltip("Speed at which the lever returns to neutral when released.")]
    public float leverReturnSpeed = 100f;
    [Tooltip("Angle threshold (in degrees) before fork begins to move.")]
    public float forkActivationThreshold = 10f;

    // === RUNTIME STATE ===
    private bool isLeverGrabbed = false;

    void Start()
    {
        if (forkLever != null)
        {
            forkLever.selectEntered.AddListener(OnGrab);
            forkLever.selectExited.AddListener(OnRelease);
        }
    }

    void OnDestroy()
    {
        if (forkLever != null)
        {
            forkLever.selectEntered.RemoveListener(OnGrab);
            forkLever.selectExited.RemoveListener(OnRelease);
        }
    }

    void OnGrab(SelectEnterEventArgs args) => isLeverGrabbed = true;
    void OnRelease(SelectExitEventArgs args) => isLeverGrabbed = false;

    void FixedUpdate()
    {
        if (isLeverGrabbed)
        {

            float leverValue = forkLever.value;

            if (leverValue > 0.65f)
            {
                // Move fork down
                fork.localPosition = Vector3.MoveTowards(fork.localPosition, forkMinHeight, forkLiftSpeed * Time.deltaTime);
            }
            else if (leverValue < 0.35f)
            { // Move fork up
                fork.localPosition = Vector3.MoveTowards(fork.localPosition, forkMaxHeight, forkLiftSpeed * Time.deltaTime);

            }

        }

    }
}
