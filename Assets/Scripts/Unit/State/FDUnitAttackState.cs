using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUnitAttackState : FDState
{
    private FDUnitData unitData { get { return data as FDUnitData; } }

    private float delay;

    public override void Initialize()
    {
        base.Initialize();
    }

    public override bool StartState(params object[] _args)
    {
        bool isActive = (bool)_args[0];
        SetActive(isActive);

        delay = 0f;

        return isActive;
    }

    public override bool UpdateState(float _deltaTime)
    {
        if (!base.UpdateState(_deltaTime))
            return false;

        AttackProcess(_deltaTime);

        return true;
    }

    private void AttackProcess(float _deltaTime)
    {
        delay = Mathf.Max(delay - _deltaTime, 0f);
        if (delay <= 0f)
        {
            delay = unitData.dynamicData.attackDelay;

            FDEnemy target = FDGlobalInterface.instance.iGameManager.GetObjectManager().TryGetEnemy(unitData.dynamicData.curTargetID);

            if (target == null)
                return;

            controllers.ani.Play(FDSystem.AnimationType.Attack);
            controllers.ani.SetFloat("AttackSpeed", FDUnitDataBase.instance.curve.Evaluate(unitData.dynamicData.attackDelay));
            target.SendState(FDSystem.State.Hit, unitData.dynamicData.attackDamage);
        }
    }

    public override void StopState()
    {
        base.StopState();
    }
}
