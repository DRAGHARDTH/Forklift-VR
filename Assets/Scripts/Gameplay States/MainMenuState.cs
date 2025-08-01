
using UnityEngine;
using UnityEngine.Networking;


public class MainMenuState: State
{
    public override void Enter()
    {
        base.Enter();
        UIReferences.Instance.canvas_MainMenu.SetActive(true); 
        AddListeners();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void Exit()
    {
        RemoveListeners();
        UIReferences.Instance.canvas_MainMenu.SetActive(false);
        base.Exit();
    }

    void AddListeners()
    {
        UIReferences.Instance.btn_StartButton.onClick.AddListener(StartButtonClicked);
        UIReferences.Instance.btn_ExitButton.onClick.AddListener(ExitButtonClicked);
    }

    void RemoveListeners()
    {
        UIReferences.Instance.btn_StartButton.onClick.RemoveListener(StartButtonClicked);
        UIReferences.Instance.btn_ExitButton.onClick.RemoveListener(ExitButtonClicked);
    }


    void StartButtonClicked()
    {
        TrainingDataManager.Instance.StartSession();
        GamePlayFlowManager.Instance.ChangeState<EnterForkliftState>();
    }

    void ExitButtonClicked()
    {
        Application.Quit();
    }
}
