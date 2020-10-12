using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUnitTargetingState : FDState
{
    private FDUnitData unitData { get { return data as FDUnitData; } }
    private Transform targetEnemyTrans = null;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override bool StartState(params object[] _args)
    {
        bool isActive = (bool)_args[0];
        SetActive(isActive);

        return isActive;
    }

    public override bool UpdateState(float _deltaTime)
    {
        if (!base.UpdateState(_deltaTime))
            return false;

        SearchTarget();
        ActionTarget();

        return true;
    }

    private void SearchTarget()
    {
        IList<FDEnemy> enemyList = FDGlobalInterface.instance.iGameManager.GetObjectManager().GetEnemyList();

        for (int i = 0; i < enemyList.Count; i++)
        {
            float minDis = float.MaxValue;
            if (targetEnemyTrans != null)
                minDis = Vector3.Distance(actorTrans.position, targetEnemyTrans.position);
            float distance = Vector3.Distance(actorTrans.position, enemyList[i].transform.position);

            if (distance <= unitData.dynamicData.attackRange && minDis > distance)
            {
                unitData.dynamicData.curTargetID = enemyList[i].actorID;
                targetEnemyTrans = enemyList[i].transform;
            }
        }
    }

    private void ActionTarget()
    {
        if (targetEnemyTrans == null)
            return;

        controllers.model.LookAt(targetEnemyTrans);
    }

    public override void StopState()
    {
        base.StopState();

        targetEnemyTrans = null;
    }
}
