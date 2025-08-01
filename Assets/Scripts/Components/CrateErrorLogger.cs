using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CrateErrorLogger : MonoBehaviour
{

    [Header("Flip Detection Settings")]
    [Tooltip("Maximum allowed tilt angle before considered flipped (in degrees)")]
    public float maxTiltAngle = 45f;

    [Tooltip("How long the crate must stay flipped before logging (seconds)")]
    public float flipDetectionTime = 2f;

    private Rigidbody rb;
    private float flippedTimer = 0f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        DetectFlip();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Ignore collisions with warehouse or forklift
        if (collision.gameObject.CompareTag("Warehouse") || collision.gameObject.CompareTag("Vehicle") || collision.gameObject.CompareTag("TargetCrate"))
            return;

        TrainingDataManager.Instance.LogError(
            $"Crate collided with {collision.gameObject.name}"
        );
    }

    private void DetectFlip()
    {
        // Check crate's tilt relative to world up
        float tiltAngle = Vector3.Angle(transform.up, Vector3.up);

        if (tiltAngle > maxTiltAngle)
        {
            flippedTimer += Time.deltaTime;

            if (flippedTimer >= flipDetectionTime)
            {
                TrainingDataManager.Instance.LogError(
                    $"Crate flipped over"
                );
                flippedTimer = 0f;

                ResetCrate();
            }
        }
        else
        {
            flippedTimer = 0f; 
        }
    }

    private void ResetCrate()
    {
        Global.Instance.go_TargetCrate.transform.position = Global.Instance.tr_TargetCrateStartPose.position;
        Global.Instance.go_TargetCrate.transform.rotation = Global.Instance.tr_TargetCrateStartPose.rotation;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

    }
}
