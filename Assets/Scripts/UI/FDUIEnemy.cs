using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIEnemy : FDUIActor
{
    public Healthbar hpBar = null;
    private Transform hpBarTrans = null;

    private float maxHP;
    private Transform cameraTrans = null;

    public override void Initialize(FDActor _actorObj)
    {
        base.Initialize(_actorObj);

        hpBarTrans = hpBar.transform;

        FDEnemyData enemyData = actorObj.GetData<FDEnemyData>();
        maxHP = enemyData.dynamicData.hitPoint;
        hpBar.health = 100f;

        cameraTrans = Camera.main.transform;
    }

    public override void UpdateUI(float _deltaTime)
    {
        base.UpdateUI(_deltaTime);
    }

    public override void LateUpdateUI(float _deltaTime)
    {
        base.LateUpdateUI(_deltaTime);

        UpdateHPBar();
    }

    private void UpdateHPBar()
    {
        hpBarTrans.forward = -cameraTrans.forward;

        FDEnemyData enemyData = actorObj.GetData<FDEnemyData>();
        hpBar.SetHealth((enemyData.dynamicData.hitPoint / maxHP) * 100f);
    }
}
