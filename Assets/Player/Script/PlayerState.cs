using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;


public enum StateName
{
    Move,
    ThrowReady,
}

public class PlayerState
{
        public BaseState CurrentState { get;  set; }  // ���� ����
        private Dictionary<StateName, BaseState> states =
        new Dictionary<StateName, BaseState>();


        public PlayerState(StateName stateName, BaseState state)
        {
            AddState(stateName, state);
            CurrentState = GetState(stateName);
        }

        public void AddState(StateName stateName, BaseState state)  // ���� ���
        {
            if (!states.ContainsKey(stateName))
            {
                states.Add(stateName, state);
            }
        }

        public BaseState GetState(StateName stateName)  // ���� ��������
        {
            if (states.TryGetValue(stateName, out BaseState state))
                return state;
            return null;
        }

        public void DeleteState(StateName removeStateName)  // ���� ����
        {
            if (states.ContainsKey(removeStateName))
            {
                states.Remove(removeStateName);
            }
        }

        public void ChangeState(StateName nextStateName)    // ���� ��ȯ
        {
            CurrentState.OnExitState();   //���� ���¸� �����ϴ� �޼ҵ带 �����ϰ�,
            if (states.TryGetValue(nextStateName, out BaseState newState)) // ���� ��ȯ
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
