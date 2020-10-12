using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDBoardDataBase : FDSingletonBase<FDBoardDataBase>
{
    public int initSummonCount;
    public int waveSummonCount;
    public FDWaveDataBase[] waveDataBase;
}

[System.Serializable]
public class FDWaveDataBase
{
    public float WaveReadyTime;
    public float WaveTime;
    public float EnemyCreateDelay;
    public int MaxWaveEnemy;
    public int EnemyTypeID;
}