using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDEnemyDataBase : FDSingletonBase<FDEnemyDataBase>
{
    public FDEnemyDataBase_Status[] statuses;

    public FDEnemyDataBase_Status GetEnemyData(int _enemyTypeID)
    {
        FDEnemyDataBase_Status status = null;

        for (int i = 0; i < statuses.Length; i++)
        {
            if (statuses[i].TypeID.Equals(_enemyTypeID))
            {
                status = statuses[i];
                break;
            }
        }

        return status;
    }
}

[System.Serializable]
public class FDEnemyDataBase_Status
{
    public int TypeID;
    public float originSpeed;
    public int HitPoint;
    public int gainGold;
}