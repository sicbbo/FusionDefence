using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUnitDeployFieldState : FDState
{
    public FDUnitData unitData { get { return data as FDUnitData; } }

    public override bool StartState(params object[] _args)
    {
        FDField curField = _args[0] as FDField;
        if (curField == null)
        {
            actorObj.SendState(FDSystem.State.Select, false);
            unitData.dynamicData.isSelect = false;
            SetActive(false);
            return false;
        }

        actorTrans.position = curField.transform.position;
        actorObj.SendState(FDSystem.State.Select, false);
        unitData.dynamicData.isSelect = false;

        FDField prvField = FDGlobalInterface.instance.iGameManager.GetMapManager().GetField(unitData.dynamicData.curFieldID);
        if (prvField != null)
        {
            prvField.SetEmpty();

            FDUnit fieldInUnit = FDGlobalInterface.instance.iGameManager.GetObjectManager().TryGetUnit(curField.curUnitID);
            if (fieldInUnit != null)
            {
                fieldInUnit.SendState(FDSystem.State.DeployField, prvField);
            }
        }

        unitData.dynamicData.curFieldID = curField.actorID;
        curField.DeployUnit(actorID);

        actorObj.SendState(FDSystem.State.Targeting, true);
        actorObj.SendState(FDSystem.State.Attack, true);

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
