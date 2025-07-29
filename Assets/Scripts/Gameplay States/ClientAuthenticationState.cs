
using UnityEngine;
using UnityEngine.Networking;


public class ClientAuthenticationState: State
{
    public override void Enter()
    {
        base.Enter();
       AddListeners();
    }

    public override void StateUpdate()
    {
        base.StateUpdate();
    }

    public override void Exit()
    {
        RemoveListeners();
        base.Exit();
    }

    void AddListeners()
    {

    }

    void RemoveListeners()
    {
    }

}
