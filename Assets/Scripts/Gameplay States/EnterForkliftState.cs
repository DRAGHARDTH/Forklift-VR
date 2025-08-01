using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

public class EnterForkliftState : State
{
    public override void Enter()
    {
        base.Enter();
        UIReferences.Instance.canvas_GeneralToolTip.SetActive(true);
        Global.Instance.go_ForkLift.GetComponent<SplineAnimate>().Play();
        Global.Instance.StartCoroutine(ShowInitialTooltipSequence());
        AddListeners();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void Exit()
    {
        RemoveListeners();
        UIReferences.Instance.canvas_GeneralToolTip.SetActive(false);
        base.Exit();
    }

    void AddListeners()
    {
        UIReferences.Instance.btn_ActionButton1.onClick.AddListener(ActionButtonClicked);
    }

    void RemoveListeners()
    {
        UIReferences.Instance.btn_ActionButton1.onClick.RemoveListener(ActionButtonClicked);
    }


    void ActionButtonClicked()
    {
        GameManager.Instance.Teleport(Global.Instance.go_PlayerPosition.GetComponent<TeleportationAnchor>());
        Global.Instance.go_Player.transform.SetParent(Global.Instance.go_PlayerPosition.transform);
        TrainingDataManager.Instance.LogAction("Entered Forklift");
        GamePlayFlowManager.Instance.ChangeState<StartIgnitionState>();
    }



    private IEnumerator ShowInitialTooltipSequence()
    {
        // Show initial message
        UIReferences.Instance.txt_GeneralToolTip.text = TooltipManager.Instance.PlayVoice(0);
        UIReferences.Instance.btn_ActionButton1.gameObject.SetActive(false);

        yield return new WaitForSeconds(4.5f);

        // Update to 'Enter Forklift' prompt
        UIReferences.Instance.txt_GeneralToolTip.text = TooltipManager.Instance.PlayVoice(1);
        UIReferences.Instance.btn_ActionButton1.gameObject.SetActive(true);
        UIReferences.Instance.txt_ActionButton1Text.text = "Enter ForkLift";

    }
}
