using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class StartIgnitionState : State
{
    public override void Enter()
    {
        base.Enter();
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(true);
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(2);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(Global.Instance.go_IgnitionButton.transform, false);
        UIReferences.Instance.canvas_ObjectTooltip.transform.localPosition = new Vector3(0f, 0.25f, 0f);
        AddListeners();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void Exit()
    {
        RemoveListeners();
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(false);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(null); 

        base.Exit();
    }

    void AddListeners()
    {
        Global.Instance.go_IgnitionButton.GetComponent<XRSimpleInteractable>().selectEntered.AddListener(IgnitionButtonClicked);
    }

    void RemoveListeners()
    {

        Global.Instance.go_IgnitionButton.GetComponent<XRSimpleInteractable>().selectEntered.RemoveListener(IgnitionButtonClicked);
     
    }


    void IgnitionButtonClicked(SelectEnterEventArgs arg)
    {
        TrainingDataManager.Instance.LogAction("Ignition Started");
        GamePlayFlowManager.Instance.ChangeState<DriveLeverTestState>();
    }

}
