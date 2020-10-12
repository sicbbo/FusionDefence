using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUnitSelectState : FDState
{
    private FDUnitData unitData { get { return data as FDUnitData; } }

    public override bool StartState(params object[] _args)
    {
        base.StartState(_args);

        bool value = (bool)_args[0];
        if (value.Equals(unitData.dynamicData.isSelect))
            return false;

        if (value.Equals(true))
            controllers.model.DoPlay(FDSystem.ModelType.SelectArrow);
        else
            controllers.model.DoStop(FDSystem.ModelType.SelectArrow);

        unitData.dynamicData.isSelect = value;

        boardEvent.SelectUnit(value);

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

        unitData.dynamicData.isSelect = false;
        boardEvent.SelectUnit(false);
    }
}
