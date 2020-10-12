using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDStateManager : FDMonoBase
{
    private Dictionary<FDSystem.State, FDState> stateDic = new Dictionary<FDSystem.State, FDState>();
    private List<FDState> stateList = new List<FDState>();

    public void BuildState(int _actorID, FDActor _actorObj, FDData _data, FDControllers _controllers, FDBoardData _boardData, FDBoardEvent _boardEvent)
    {
        stateDic.Clear();
        stateList.Clear();

        this.GetComponentsInChildren<FDState>(true, stateList);
        
        for (int i=0; i<stateList.Count; i++)
        {
            stateList[i].Initialize();
            stateList[i].SetInfo(_actorID, _actorObj, _data, _controllers, _boardData, _boardEvent);
            stateList[i].SetActive(false);

            stateDic.Add(stateList[i].stateEnum, stateList[i]);
        }
    }

    public void SendState(FDSystem.State _enum, params object[] args)
    {
        FDState state;
        
        if (!stateDic.TryGetValue(_enum, out state))
        {
            FDDebugLog.LogError(string.Format("Try Get State is Unknown! : {0}", _enum.ToString()));
            return;
        }

        state.SendState(args);
    }

    public void UpdateState(float _deltaTime)
    {
        for (int i = 0; i < stateList.Count; i++)
            stateList[i].UpdateState(_deltaTime);
    }

    public void Delete()
    {
        for (int i = 0; i < stateList.Count; i++)
        {
            stateList[i].StopState();
        }
    }
}