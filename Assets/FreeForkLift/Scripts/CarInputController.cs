using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


[RequireComponent(typeof(CarMovementController))]
public class CarInputController : MonoBehaviour
{
    // === DRIVE CONTROL SYSTEM ===
    [Header("Drive Control System")]
    public XRKnob driveLever;
    public XRKnob steeringWheel;

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

        // === Brake Input ===
        float brakeInput = 0f;


        float leverValue = driveLever.value;
        float steerValue = steeringWheel.value;


        if (isLeverGrabbed)
        {
            // Map lever value (0–1) to -1 to 1
            driveInput = (leverValue - 0.5f) * 2f;
        }
        else
        {
            driveLever.value = Mathf.MoveTowards(driveLever.value, 0.5f, Time.fixedDeltaTime * 1.5f);
            driveInput = 0f;
            brakeInput = 1.5f; 
        }

        if (isSteerGrabbed)
        {
            // Map steering wheel value (0–1) to -1 to 1
            steerInput = (steerValue - 0.5f) * 2f;
        }
        else
        {
            steeringWheel.value = Mathf.MoveTowards(steeringWheel.value, 0.5f, Time.fixedDeltaTime * 1.5f);
            steerInput = 0f;
        }

        carController.Move(steerInput, driveInput, driveInput, brakeInput);

    }
}