using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayFlowManager : StateMachine
{
    public static GamePlayFlowManager Instance;

    public override void Start()
    {
        base.Start();
        Instance = this;
    }
    private void OnEnable()
    {
        RegisterEventListeners();
    }
    private void OnDisable()
    {
        DeRegisterEventListeners();
    }

    void RegisterEventListeners()
    {
    }

    void DeRegisterEventListeners()
    {
    }

    #region State Machine Implementation
    public override void AddStates()
    {
        AddState<MainMenuState>();
        AddState<EnterForkliftState>();
        AddState<StartIgnitionState>();
        AddState<DriveLeverTestState>();
        AddState<SteeringTestState>();
        AddState<ForkLiftTestState>();
        AddState<MoveToCrateState>();
        AddState<DepositCrateState>();
        AddState<ExitForkliftState>();

        SetInitialState<MainMenuState>();
       
    }

    public void ChangeStateto<T>() where T : State
    {
        ChangeState(typeof(T));
    }

    #endregion State Machine Implementation
}
