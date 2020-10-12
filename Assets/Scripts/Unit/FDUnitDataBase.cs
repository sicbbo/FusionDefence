using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUnitDataBase : FDSingletonBase<FDUnitDataBase>
{
    public AnimationCurve curve;
    public FDUnitDataBase_Status[] statuses;

    public FDUnitDataBase_Status GetStatus(int _unitTypeID, int _unitGrade)
    {
        for (int i = 0; i < statuses.Length; i++)
        {
            if (statuses[i].UnitTypeID.Equals(_unitTypeID) && statuses[i].UnitGrade.Equals(_unitGrade))
                return statuses[i];
        }

        return null;
    }
}

[System.Serializable]
public class FDUnitDataBase_Status
{
    public int UnitTypeID;
    public int UnitGrade;
    public string UnitName;
    public int AttackDamage;
    public float AttackDelay;
    public float AttackRange;
}