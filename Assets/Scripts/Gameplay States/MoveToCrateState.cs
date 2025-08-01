using UnityEngine;

public class MoveToCrateState : State
{

    public override void Enter()
    {
        base.Enter();

        // Enable forklift movement

        // Show hologram highlight
        Global.Instance.go_CrateHighlight.SetActive(true);
        Global.Instance.go_CrateHighlight.transform.SetParent(Global.Instance.go_TargetCrate.transform);
        Global.Instance.go_CrateHighlight.transform.localPosition = new Vector3(0f, 0.66522f, 0f);


        // Show tooltip above crate
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(true);
        UIReferences.Instance.txt_ObjectToolTip.text =
            "Step 6: Move to the target crate\n\nDrive the forklift so the forks are positioned under the crate.";
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(Global.Instance.go_IgnitionButton.transform, false);
        UIReferences.Instance.canvas_ObjectTooltip.transform.localPosition = new Vector3(0f, .25f, 0f);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        // Check if forks are underneath the crate
        if (AreForksUnderCrate())
        {
            Debug.Log("Forks under crate — ready to lift!");
            GamePlayFlowManager.Instance.ChangeState<DepositCrateState>();
        }
    }

    public override void Exit()
    {
        // Hide hologram and tooltip
        Global.Instance.go_CrateHighlight.SetActive(false);
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(false);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(null);
        base.Exit();
    }

    private bool AreForksUnderCrate()
    {
        // Example method: checks horizontal distance and fork height
        Transform forks = Global.Instance.go_ForkLiftForks.transform;
        Transform crate = Global.Instance.go_TargetCrate.transform;

        float horizontalDistance = Vector3.Distance(
            new Vector3(forks.position.x, 0, forks.position.z),
            new Vector3(crate.position.x, 0, crate.position.z)
        );

     
        bool distanceOK = horizontalDistance < 0.75f; // Adjust tolerance


        return distanceOK;
    }
}