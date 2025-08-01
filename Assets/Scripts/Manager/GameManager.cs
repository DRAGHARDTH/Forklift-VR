using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }
        Instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void Teleport(TeleportationAnchor teleportationAnchor)
    {
        TeleportationProvider teleportationProvider = Global.Instance.go_Player.GetComponentInChildren<TeleportationProvider>();
        if (teleportationProvider != null && teleportationAnchor != null)
        {
            // Get the anchor's teleportation destination
            Transform teleportDestination = teleportationAnchor.transform;

            if (teleportDestination != null)
            {
                // Create a teleport request with the destination position and rotation
                TeleportRequest teleportRequest = new TeleportRequest
                {
                    destinationPosition = teleportDestination.position,
                    destinationRotation = teleportDestination.rotation,
                    matchOrientation = teleportationAnchor.matchOrientation,


                };


                // Queue the teleport request
                teleportationProvider.QueueTeleportRequest(teleportRequest);

            }
            else
            {
                Debug.LogWarning("TeleportAnchorTransform is not set on the TeleportationAnchor.");
            }
        }
        else
        {
            Debug.LogWarning("TeleportationProvider or TeleportationAnchor is not assigned.");
        }
    }

}
