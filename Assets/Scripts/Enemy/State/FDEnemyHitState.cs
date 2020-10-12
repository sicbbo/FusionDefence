using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDEnemyHitState : FDState
{
    private FDEnemyData enemyData { get { return data as FDEnemyData; } }

    public override void Initialize()
    {
        base.Initialize();
    }

    public override bool StartState(params object[] _args)
    {
        float damage = (float)_args[0];

        enemyData.dynamicData.hitPoint = Mathf.Max(enemyData.dynamicData.hitPoint - damage, 0f);
        if (enemyData.dynamicData.hitPoint <= 0f)
        {
            actorObj.SendState(FDSystem.State.Death);
        }

        return false;
    }

    public override bool UpdateState(float _deltaTime)
    {
        if (!base.UpdateState(_deltaTime))
            return false;

        return true;
    }

    public override void StopState()
    {
        base.StopState();
    }
}
