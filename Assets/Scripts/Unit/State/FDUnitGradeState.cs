using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUnitGradeState : FDState
{
    private FDUnitData unitData { get { return data as FDUnitData; } }
    private Transform cameraTrans = null;

    public override void Initialize()
    {
        base.Initialize();

        cameraTrans = Camera.main.transform;
    }

    public override bool StartState(params object[] _args)
    {
        controllers.model.DoPlay((FDSystem.ModelType)(1 + unitData.staticData.GetUnitGrade()));

        return true;
    }

    public override bool UpdateState(float _deltaTime)
    {
        if (!base.UpdateState(_deltaTime))
            return false;

        controllers.model.SetForward((FDSystem.ModelType)(1 + unitData.staticData.GetUnitGrade()), -cameraTrans.forward);

        return true;
    }

    public override void StopState()
    {
        base.StopState();
    }
}
