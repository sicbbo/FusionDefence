using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDEnemy : FDActor
{
    private FDEnemyData enemyData = null;

    public override void Initialize(FDSystem.ObjectID _objectType, int _typeID, int _grade, int _actorID, Vector3 _initPos, Vector3 _initRot, FDData _data, FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        base.Initialize(_objectType, _typeID, _grade, _actorID, _initPos, _initRot, _data, _boardData, _boardEvnet);

        enemyData = _data as FDEnemyData;
    }

    public void SetInfo(IList<Vector3> _wayPointPosList)
    {
        SendState(FDSystem.State.MoveToTarget, _wayPointPosList);
    }
}
