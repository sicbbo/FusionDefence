using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIUnit : FDUIActor
{
    public GameObject attackRangeObj;

    private FDUnitDynamicData unitDynamicData = null;
    private bool isUpdate = false;

    public override void Initialize(FDActor _actorObj)
    {
        base.Initialize(_actorObj);

        unitDynamicData = actorObj.GetData<FDUnitData>().dynamicData;
        attackRangeObj.SetActive(unitDynamicData.isSelect);
    }

    public override void UpdateUI(float _deltaTime)
    {
        base.UpdateUI(_deltaTime);

        if (isUpdate != unitDynamicData.isSelect)
        {
            Vector3 scale = new Vector3(unitDynamicData.attackRange * 2f, unitDynamicData.attackRange * 2f, 1f);
            attackRangeObj.transform.localScale = scale;
            attackRangeObj.SetActive(unitDynamicData.isSelect);

            isUpdate = unitDynamicData.isSelect;
        }
    }

    public override void LateUpdateUI(float _deltaTime)
    {
        base.LateUpdateUI(_deltaTime);
    }
}
