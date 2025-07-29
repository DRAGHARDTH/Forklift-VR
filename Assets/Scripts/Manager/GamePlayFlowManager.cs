using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayFlowManager : StateMachine
{
    public static GamePlayFlowManager instance;

    public override void Start()
    {
        base.Start();
        instance = this;
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
       
    }

    public void ChangeStateto<T>() where T : State
    {
        ChangeState(typeof(T));
    }

    #endregion State Machine Implementation
}
