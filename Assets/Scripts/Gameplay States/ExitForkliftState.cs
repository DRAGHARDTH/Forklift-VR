using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitForkliftState : State
{
    private bool movedBack = false;
    private bool forksLowered = false;

    // Distance player must reverse from the drop-off point
    private float requiredReverseDistance = 3.0f;

    public override void Enter()
    {
        base.Enter();
        AddListeners();

        UIReferences.Instance.canvas_ObjectTooltip.SetActive(true);
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(13);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(Global.Instance.go_IgnitionButton.transform);
        UIReferences.Instance.canvas_ObjectTooltip.transform.localPosition = new Vector3(0f, .25f, 0f);
    }

    public override void StateUpdate()
    {
        base.StateUpdate();

        if (!movedBack)
        {
            if (HasReversedEnough())
            {
                movedBack = true;
                ShowLowerForkPrompt();
            }
            return; 
        }

        if (!forksLowered)
        {
            if (AreForksLowered())
            {
                forksLowered = true;
                ShowEndMenu();
            }
        }
    }

    public override void Exit()
    {
        RemoveListeners();
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(false);
        UIReferences.Instance.canvas_ObjectTooltip.transform.SetParent(null);
        base.Exit();
    }

    private bool HasReversedEnough()
    {
        Vector3 forkliftPos = Global.Instance.go_ForkLift.transform.position;
        Vector3 dropOffPos = Global.Instance.go_TargetDropOffPose.transform.position;

        float distance = Vector3.Distance(
            new Vector3(forkliftPos.x, 0, forkliftPos.z),
            new Vector3(dropOffPos.x, 0, dropOffPos.z)
        );

        return distance >= requiredReverseDistance;
    }

    private void ShowLowerForkPrompt()
    {
        UIReferences.Instance.txt_ObjectToolTip.text = TooltipManager.Instance.PlayVoice(14);
    }

    private bool AreForksLowered()
    {
        float forkHeight = Global.Instance.go_ForkLiftForks.transform.position.y;
        return forkHeight < 1.0f; 
    }

    private void ShowEndMenu()
    {
        // Hide tooltip and show main menu canvas
        UIReferences.Instance.canvas_ObjectTooltip.SetActive(false);
        UIReferences.Instance.canvas_GeneralToolTip.SetActive(true);
        TrainingDataManager.Instance.LogAction("Forklift Safely shut off");


        // Hide the forklift
        Global.Instance.go_Player.transform.SetParent(null);
        Global.Instance.go_ForkLift.SetActive(false);

        // Change button labels for restart/quit
        UIReferences.Instance.txt_GeneralToolTip.text = TooltipManager.Instance.PlayVoice(15);
        UIReferences.Instance.btn_ActionButton1.gameObject.SetActive(true);
        UIReferences.Instance.btn_ActionButton2.gameObject.SetActive(true);
        UIReferences.Instance.txt_ActionButton1Text.text = "Restart Training";
        UIReferences.Instance.txt_ActionButton2Text.text = "Quit Training";
       

        
    }



    void AddListeners()
    {
        UIReferences.Instance.btn_ActionButton1.onClick.AddListener(RestartTraining);
        UIReferences.Instance.btn_ActionButton2.onClick.AddListener(Quit);
    }

    void RemoveListeners()
    {
        UIReferences.Instance.btn_ActionButton1.onClick.RemoveListener(RestartTraining);
        UIReferences.Instance.btn_ActionButton2.onClick.RemoveListener(Quit);
    }

    private void RestartTraining()
    {
        TrainingDataManager.Instance.EndSession();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    private void Quit()
    {
        TrainingDataManager.Instance.EndSession();

        Application.Quit();
    }
}
