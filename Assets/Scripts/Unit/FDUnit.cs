using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUnit : FDActor
{
    protected FDUnitData unitData { get { return data as FDUnitData; } }

    public override void Initialize(FDSystem.ObjectID _objectType, int _typeID, int _grade, int _actorID, Vector3 _initPos, Vector3 _initRot, FDData _data, FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        base.Initialize(_objectType, _typeID, _grade, _actorID, _initPos, _initRot, _data, _boardData, _boardEvnet);

        SendState(FDSystem.State.Grade);
    }

    public override void FDUpdate(float _deltaTime)
    {
        base.FDUpdate(_deltaTime);
    }

    public bool IsSelect()
    {
        return unitData.dynamicData.isSelect;
    }

    public override void Delete()
    {
        FDField curField = FDGlobalInterface.instance.iGameManager.GetMapManager().GetField(unitData.dynamicData.curFieldID);
        if (curField != null)
            curField.SetEmpty();

        base.Delete();
    }
}
