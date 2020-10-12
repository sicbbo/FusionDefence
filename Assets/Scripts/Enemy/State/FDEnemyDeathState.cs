using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDEnemyDeathState : FDState
{
    private FDEnemyData enemyData { get { return data as FDEnemyData; } }

    private float deathDelay;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override bool StartState(params object[] _args)
    {
        deathDelay = 0.1f;
        enemyData.dynamicData.speed = 0f;
        //controllers.anim.Play(FDSystem.AnimationType.Death);

        return true;
    }

    public override bool UpdateState(float _deltaTime)
    {
        if (!base.UpdateState(_deltaTime))
            return false;

        deathDelay = Mathf.Max(deathDelay - _deltaTime, 0f);
        if (deathDelay <= 0f)
        {
            FDGlobalInterface.instance.iGameManager.GetObjectManager().RemoveEnemy(actorID);
            boardData.dynamicData.GainGold(enemyData.staticData.GetGainGold());
            SetActive(false);
        }

        return true;
    }

    public override void StopState()
    {
        base.StopState();
    }
}
