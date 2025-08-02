using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global Instance;


    // === FORKLIFT COMPONENTS ===
    [Header("Forklift Components")]
    public GameObject go_ForkLift;
    public GameObject go_ForkLiftForks;
    public GameObject go_ForkLiftLever;
    public GameObject go_DriveLever;
    public GameObject go_SteeringWheel;
    public GameObject go_IgnitionButton;


    // === PLAYER SETUP ===
    [Header("Player Setup")]
    public GameObject go_PlayerPosition;
    public GameObject go_Player;
    
    
    // === TARGET CRATE SETUP ===
    [Header("Target Crate Setup")]
    public GameObject go_TargetCrate;
    public Transform tr_TargetCrateStartPose;
    public GameObject go_CrateHighlight;
    public GameObject go_TargetDropOffPose;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        if (Instance != null && Instance != this)
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
}
