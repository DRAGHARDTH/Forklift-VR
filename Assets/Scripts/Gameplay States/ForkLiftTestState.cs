using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class ForkLiftTestState : State
{
    private bool liftedUp = false;
    private bool loweredDown = false;

    private float upHoldTime = 0f;
    private float downHoldTime = 0f;

    private float requiredHoldDuration = 1.5f;

    public override void Enter()
    {
        base.Enter();

        UIReferences.Instance.canvas_ObjectTooltip.SetActive(true);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(Global.Instance.go_ForkLiftLever.transform);
        UIReferences.Instance.canvas_ObjectTooltip.transform.localPosition = new Vector3(-0.35f, 0f, 0f);

        ShowLiftUpPrompt();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        float leverValue = Global.Instance.go_ForkLiftLever.GetComponent<XRKnob>().value;

        // Step 1: Lift forks UP
        if (!liftedUp)
        {
            if (leverValue > 0.8f) // Up threshold
            {
                upHoldTime += Time.deltaTime;
                if (upHoldTime >= requiredHoldDuration)
                {
                    liftedUp = true;
                    ShowLowerDownPrompt();
                }
            }
            else
            {
                upHoldTime = 0f; // Reset if released
            }
            return; // Wait until up is done before checking down
        }

        // Step 2: Lower forks DOWN
        if (!loweredDown)
        {
            if (leverValue < 0.2f) // Down threshold
            {
                downHoldTime += Time.deltaTime;
                if (downHoldTime >= requiredHoldDuration)
                {
                    loweredDown = true;
                    OnForkLiftTestComplete();
                }
            }
            else
            {
                downHoldTime = 0f; // Reset if released
            }
        }
    }

    private void ShowLiftUpPrompt()
    {
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(7);
    }

    private void ShowLowerDownPrompt()
    {
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(8);
    }

    private void OnForkLiftTestComplete()
    {
        Debug.Log("Forklift lift/lower test complete!");
        TrainingDataManager.Instance.LogAction("Forklift lift/lower test complete");
        GamePlayFlowManager.Instance.ChangeState<MoveToCrateState>(); // Replace with your actual next state
    }

    public override void Exit()
    {
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(false);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(null);
        base.Exit();
    }
}
