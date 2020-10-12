using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDEnemyMoveToTargetState : FDState
{
    private FDEnemyData enemyData { get { return data as FDEnemyData; } }

    private IList<Vector3> wayPointPosList = null;
    private int curWayPointIdx;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override bool StartState(params object[] _args)
    {
        wayPointPosList = (IList<Vector3>)_args[0];
        curWayPointIdx = 0;

        return true;
    }

    public override bool UpdateState(float _deltaTime)
    {
        if (!base.UpdateState(_deltaTime))
            return false;

        MoveToTarget(_deltaTime);
        
        return true;
    }

    private void MoveToTarget(float _deltaTime)
    {
        Vector3 curPos = actorTrans.position;
        Vector3 targetPos = wayPointPosList[curWayPointIdx];

        float step = enemyData.dynamicData.speed * _deltaTime;
        actorTrans.position = Vector3.MoveTowards(curPos, targetPos, step);
        actorTrans.LookAt(targetPos, Vector3.up);

        if (Vector3.Distance(curPos, targetPos) < 0.001f)
        {
            curWayPointIdx++;
            if (curWayPointIdx >= wayPointPosList.Count)
                curWayPointIdx = 0;
        }
    }

    public override void StopState()
    {
        base.StopState();
    }
}
