using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDState : FDMonoBase
{
    public FDSystem.State stateEnum;

    private bool isActive = false;

    protected int actorID = int.MaxValue;
    protected FDActor actorObj = null;
    protected Transform actorTrans = null;
    protected FDData data = null;
    protected FDControllers controllers = null;
    protected FDBoardData boardData = null;
    protected FDBoardEvent boardEvent = null;

    public virtual void Initialize()
    {
        isActive = false;
        this.gameObject.SetActive(false);
    }

    public void SetInfo(int _actorID, FDActor _actorObj, FDData _data, FDControllers _controllers, FDBoardData _boardData, FDBoardEvent _boardEvent)
    {
        actorID = _actorID;
        actorObj = _actorObj;
        actorTrans = _actorObj.transform;
        data = _data;
        controllers = _controllers;
        boardData = _boardData;
        boardEvent = _boardEvent;
    }

    public void SendState(params object[] args)
    {
        SetActive(StartState(args));
    }

    public void SetActive(bool _active)
    {
        if (isActive.Equals(_active))
            return;

        isActive = _active;

        if (isActive.Equals(true))
            this.gameObject.SetActive(true);
        else
            this.gameObject.SetActive(false);
    }

    public virtual bool StartState(params object[] _args)
    {
        return false;
    }

    public virtual bool UpdateState(float _deltaTime)
    {
        if (isActive.Equals(false))
            return false;

        return true;
    }

    public virtual void StopState()
    {
        
    }
}
