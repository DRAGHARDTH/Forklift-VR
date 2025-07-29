using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


[RequireComponent(typeof(CarMovementController))]
public class CarInputController : MonoBehaviour
{
    // === DRIVE CONTROL SYSTEM ===
    [Header("Drive Lever Setup")]
    [Tooltip("The lever GameObject used to control driving.")]
    public XRKnob driveLever;

    public XRKnob steeringWheel;

    [Header("Steering")]
    [Tooltip("Use Input Axis for horizontal steering (can be replaced by a VR steering wheel script).")]
    public bool useKeyboardSteering = true;

    // === RUNTIME ===
    private CarMovementController carController;
    private bool isLeverGrabbed = false;
    private bool isSteerGrabbed = false;

    private void Awake()
    {
        carController = GetComponent<CarMovementController>();
        if (driveLever != null)
        {
            driveLever.selectEntered.AddListener(OnLeverGrab);
            driveLever.selectExited.AddListener(OnLeverRelease);
        }

        if (steeringWheel != null)
        {
            steeringWheel.selectEntered.AddListener(OnSteerGrab);
            steeringWheel.selectExited.AddListener(OnSteerRelease);
        }

    }

    private void OnDestroy()
    {
        if (driveLever != null)
        {
            driveLever.selectEntered.RemoveListener(OnLeverGrab);
            driveLever.selectExited.RemoveListener(OnLeverRelease);
        }

        if (steeringWheel != null)
        {
            steeringWheel.selectEntered.RemoveListener(OnSteerGrab);
            steeringWheel.selectExited.RemoveListener(OnSteerRelease);
        }
    }

    private void OnLeverGrab(SelectEnterEventArgs args) => isLeverGrabbed = true;
    private void OnLeverRelease(SelectExitEventArgs args) => isLeverGrabbed = false;


    private void OnSteerGrab(SelectEnterEventArgs args) => isSteerGrabbed = true;
    private void OnSteerRelease(SelectExitEventArgs args) => isSteerGrabbed = false;

    private void FixedUpdate()
    {
        // === Steering ===
        float steerInput = 0f;

        // === Drive Input via Lever ===
        float driveInput = 0f;


        float leverValue = driveLever.value;
        float steerValue = steeringWheel.value;

       
        if (isLeverGrabbed)
        {
            if (leverValue > 0.65f)
            {
                driveInput = -1f;
            }
            else if (leverValue < 0.35f)
            {

                driveInput = 1f;
            }
        }

        if (isSteerGrabbed)
        {
            if (steerValue > 0.65f)
            {
                steerInput = 1f;
            }
            else if (steerValue < 0.35f)
            {

                steerInput = -1f;
            }
        }

        carController.Move(steerInput, driveInput, driveInput, 0f);

    }
}