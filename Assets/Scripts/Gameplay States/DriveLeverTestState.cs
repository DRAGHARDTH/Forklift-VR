using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class DriveLeverTestState : State
{
    private bool movedForward = false;
    private bool movedBackward = false;

    private float forwardHoldTime = 0f;
    private float backwardHoldTime = 0f;

    private float requiredHoldDuration = 1.5f; 

    public override void Enter()
    {
        base.Enter();

        UIReferences.Instance.canvas_ObjectTooltip.SetActive(true);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(Global.Instance.go_DriveLever.transform);
        UIReferences.Instance.canvas_ObjectTooltip.transform.localPosition = new Vector3(-0.35f, 0f, 0f);

        ShowForwardPrompt();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        float leverValue = Global.Instance.go_DriveLever.GetComponent<XRKnob>().value;

        // Step 1: Move Forward
        if (!movedForward)
        {
            if (leverValue > 0.8f) // Forward threshold
            {
                forwardHoldTime += Time.deltaTime;
                if (forwardHoldTime >= requiredHoldDuration)
                {
                    movedForward = true;
                    ShowBackwardPrompt();
                }
            }
            else
            {
                forwardHoldTime = 0f; // Reset if released
            }
            return; // Wait until move forward is done before checking move backward
        }

        // Step 2: Move Backward
        if (!movedBackward)
        {
            if (leverValue < 0.2f) // Backward threshold
            {
                backwardHoldTime += Time.deltaTime;
                if (backwardHoldTime >= requiredHoldDuration)
                {
                    movedBackward = true;
                    OnDriveTestComplete();
                }
            }
            else
            {
                backwardHoldTime = 0f; // Reset if released
            }
        }
    }

    private void ShowForwardPrompt()
    {
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(3);
    }

    private void ShowBackwardPrompt()
    {
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(4);
    }

    private void OnDriveTestComplete()
    {
        Debug.Log("Drive lever test complete!");
        TrainingDataManager.Instance.LogAction("Drive lever test complete");
        GamePlayFlowManager.Instance.ChangeState<SteeringTestState>();
    }

    public override void Exit()
    {
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(false);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(null);
        base.Exit();
    }
}
