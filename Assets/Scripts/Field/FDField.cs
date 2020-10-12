using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDField : FDActor
{
    private bool _isEmpty = true;
    public bool isEmpty { get { return _isEmpty; } }

    [System.NonSerialized]
    public int curUnitID = int.MaxValue;

    public override void Initialize(FDSystem.ObjectID _objectType, int _typeID, int _grade, int _actorID,Vector3 _initPos, Vector3 _initRot, FDData _data, FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        base.Initialize(_objectType, _typeID, _grade, _actorID, _initPos, _initRot, _data, _boardData, _boardEvnet);

        _isEmpty = true;
    }

    public void DeployUnit(int _unitID)
    {
        curUnitID = _unitID;
        _isEmpty = false;
    }

    public void SetEmpty()
    {
        curUnitID = int.MaxValue;
        _isEmpty = true;
    }
}
