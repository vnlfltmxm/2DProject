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
    public BombBaseState CurrentState { get; set; }  // 현재 상태
    private Dictionary<BombStateName, BombBaseState> states =
    new Dictionary<BombStateName, BombBaseState>();


    public BombState(BombStateName stateName, BombBaseState state)
    {
        AddState(stateName, state);
        CurrentState = GetState(stateName);
    }

    public void AddState(BombStateName stateName, BombBaseState state)  // 상태 등록
    {
        if (!states.ContainsKey(stateName))
        {
            states.Add(stateName, state);
        }
    }

    public BombBaseState GetState(BombStateName stateName)  // 상태 꺼내오기
    {
        if (states.TryGetValue(stateName, out BombBaseState state))
            return state;
        return null;
    }

    public void DeleteState(BombStateName removeStateName)  // 상태 삭제
    {
        if (states.ContainsKey(removeStateName))
        {
            states.Remove(removeStateName);
        }
    }

    public void ChangeState(BombStateName nextStateName)    // 상태 전환
    {
        CurrentState.OnExitState();   //현재 상태를 종료하는 메소드를 실행하고,
        if (states.TryGetValue(nextStateName, out BombBaseState newState)) // 상태 전환
        {
            CurrentState = newState;
        }
        CurrentState.OnEnterState();  // 다음 상태 진입 메소드 실행
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
