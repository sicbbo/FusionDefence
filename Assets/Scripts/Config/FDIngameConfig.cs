using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolingData
{
    public FDSystem.ObjectID objectType;
    public int count;
}

public class FDIngameConfig : FDConfigBase
{
    public PoolingData[] poolingDataArray = null;

    public int GetPoolingCount(FDSystem.ObjectID _objectType)
    {
        for (int i=0; i<poolingDataArray.Length; i++)
        {
            if (poolingDataArray[i].objectType.Equals(_objectType))
                return poolingDataArray[i].count;
        }

        return 0;
    }
}
