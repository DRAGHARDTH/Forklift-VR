using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class SteeringTestState : State
{
    private bool turnedRight = false;
    private bool turnedLeft = false;


    public override void Enter()
    {
        base.Enter();

        UIReferences.Instance.canvas_ObjectTooltip.SetActive(true);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(Global.Instance.go_SteeringWheel.transform);
        UIReferences.Instance.canvas_ObjectTooltip.transform.localPosition = new Vector3(0f, 0f, 0.85f);

        ShowRightTurnPrompt();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        float wheelValue = Global.Instance.go_SteeringWheel.GetComponent<XRKnob>().value;

        if (!turnedRight)
        {
            if (wheelValue > 0.95f) 
            {
                turnedRight = true;
                ShowLeftTurnPrompt();
            }
            return; 
        }

        
        if (!turnedLeft)
        {
            if (wheelValue < 0.05f) 
            {
                turnedLeft = true;
                OnSteeringTestComplete();
            }
        }
    }

    private void ShowRightTurnPrompt()
    {
        UIReferences.Instance.txt_ObjectToolTip.text =
            "Step 4: Steering Test\n\nTurn the steering wheel fully to the right.";
    }

    private void ShowLeftTurnPrompt()
    {
        UIReferences.Instance.txt_ObjectToolTip.text =
            "Step 4: Steering Test\n\nNow turn the steering wheel fully to the left.";
    }

    private void OnSteeringTestComplete()
    {
        Debug.Log("Steering test complete!");
        GamePlayFlowManager.Instance.ChangeState<ForkLiftTestState>();
    }

    public override void Exit()
    {
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(false);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(null);
        base.Exit();
    }
}
