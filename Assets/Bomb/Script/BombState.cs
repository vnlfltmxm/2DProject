using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BombStateName
{
    Normal,
    Plus,

    Last


}


public class BombState
{
    public BombBaseState CurrentState { get; set; }  // ���� ����
    private Dictionary<BombStateName, BombBaseState> states =
    new Dictionary<BombStateName, BombBaseState>();


    public BombState(BombStateName stateName, BombBaseState state)
    {
        AddState(stateName, state);
        CurrentState = GetState(stateName);
    }

    public void AddState(BombStateName stateName, BombBaseState state)  // ���� ���
    {
        if (!states.ContainsKey(stateName))
        {
            states.Add(stateName, state);
        }
    }

    public BombBaseState GetState(BombStateName stateName)  // ���� ��������
    {
        if (states.TryGetValue(stateName, out BombBaseState state))
            return state;
        return null;
    }

    public void DeleteState(BombStateName removeStateName)  // ���� ����
    {
        if (states.ContainsKey(removeStateName))
        {
            states.Remove(removeStateName);
        }
    }

    public void ChangeState(BombStateName nextStateName)    // ���� ��ȯ
    {
        CurrentState.OnExitState();   //���� ���¸� �����ϴ� �޼ҵ带 �����ϰ�,
        if (states.TryGetValue(nextStateName, out BombBaseState newState)) // ���� ��ȯ
        {
            CurrentState = newState;
        }
        CurrentState.OnEnterState();  // ���� ���� ���� �޼ҵ� ����
    }

    public void UpdateState()
    {
        CurrentState.OnUpdateState();
    }

    public void FixedUpdateState()
    {
        CurrentState.OnFixedUpdateState();
    }
}
