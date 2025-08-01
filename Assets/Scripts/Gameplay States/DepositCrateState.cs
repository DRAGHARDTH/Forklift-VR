using UnityEngine;

public class DepositCrateState : State
{
    private bool crateLifted = false;
    private bool crateAtDropOff = false;

    public override void Enter()
    {
        base.Enter();


        // Show tooltip to lift the crate
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(true);
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(10);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(Global.Instance.go_IgnitionButton.transform);
        UIReferences.Instance.canvas_ObjectTooltip.transform.localPosition = new Vector3(0f, .25f, 0f);

    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (!crateLifted)
        {
            if (IsCrateLifted())
            {
                crateLifted = true;
                ShowDropOffPrompt();
            }
        }
        
        if (!crateAtDropOff)
        {
            if (IsCrateAtDropOff())
            {
                crateAtDropOff = true;
                ShowLowerForksPrompt();
            }
            return;
        }

        if (crateAtDropOff)
        {
            if (IsCrateLowered())
            {
                Debug.Log("Crate deposited successfully!");
                TrainingDataManager.Instance.LogAction("Crate deposited successfully");
                GamePlayFlowManager.Instance.ChangeState<ExitForkliftState>();
            }
        }
    }

    public override void Exit()
    {
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(false);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(null);
        Global.Instance.go_TargetDropOffPose.SetActive(false);
        base.Exit();
    }

    private bool IsCrateLifted()
    {
        float forkHeight = Global.Instance.go_ForkLiftForks.transform.position.y;
        return forkHeight > 2.3f;
    }

    private void ShowDropOffPrompt()
    {
        // Update tooltip
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(11);

        // Show drop-off highlight
        Global.Instance.go_CrateHighlight.SetActive(true);
        Global.Instance.go_CrateHighlight.transform.SetParent(Global.Instance.go_TargetDropOffPose.transform);
        Global.Instance.go_CrateHighlight.transform.localPosition = new Vector3(0f, 0.66522f, 0f);

    }

    private void ShowLowerForksPrompt()
    {
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(12);
    }

    private bool IsCrateAtDropOff()
    {
        Vector3 cratePos = Global.Instance.go_TargetCrate.transform.position;
        Vector3 dropOffPos = Global.Instance.go_TargetDropOffPose.transform.position;
        float distance = Vector3.Distance(cratePos, dropOffPos);
        return distance < 0.5f; // Tweak as needed
    }

    private bool IsCrateLowered()
    {
        float forkHeight = Global.Instance.go_ForkLiftForks.transform.position.y;
        return forkHeight < 2.15f;
    }
}