using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFDBoardData
{
    IFDBoardStaticData GetStaticData();
    IFDBoardDynamicData GetDynamicData();
}

public class FDBoardData : FDData, IFDBoardData
{
    private FDBoardStaticData StaticData;
    private FDBoardDynamicData DynamicData;

    public FDBoardStaticData staticData { get { return StaticData; } }
    public FDBoardDynamicData dynamicData { get { return DynamicData; } }

    public IFDBoardStaticData GetStaticData() { return StaticData; }
    public IFDBoardDynamicData GetDynamicData() { return DynamicData; }

    public override void BuildData(int _actorID, int _grade)
    {
        base.BuildData(_actorID, _grade);

        StaticData = new FDBoardStaticData();
        DynamicData = new FDBoardDynamicData();

        StaticData.BuildData(_actorID, _grade);
        DynamicData.BuildData(StaticData);
    }

    public FDWaveDataBase GetWaveDataBase(int _waveIndex)
    {
        return StaticData.GetWaveDataBase(_waveIndex - 1);
    }
}

public interface IFDBoardStaticData
{

}

public class FDBoardStaticData : FDStaticData, IFDBoardStaticData
{
    private int MaxWaveCount;
    public int maxWaveCount { get { return MaxWaveCount; } }

    private int InitSummonCount;
    public int initSummonCount { get { return InitSummonCount; } }

    private int WaveSummonCount;
    public int waveSummonCount { get { return WaveSummonCount; } }

    private List<FDWaveDataBase> waveDataList = new List<FDWaveDataBase>();

    public override void BuildData(int _actorID, int _grade)
    {
        base.BuildData(_actorID, _grade);

        MaxWaveCount = FDBoardDataBase.instance.waveDataBase.Length;
        InitSummonCount = FDBoardDataBase.instance.initSummonCount;
        WaveSummonCount = FDBoardDataBase.instance.waveSummonCount;

        waveDataList.AddRange(FDBoardDataBase.instance.waveDataBase);
    }

    public FDWaveDataBase GetWaveDataBase(int _waveIndex)
    {
        return waveDataList[_waveIndex];
    }
}

public interface IFDBoardDynamicData
{
    bool GetIsReady();
    float GetRemainReadyTime();
    float GetRemainWaveTime();
    int GetWaveCount();
    float GetEnemyCount();
    int GetGold();
    void GainGold(int _amount);
    int GetSummonCount();
    void SetSummonCount(int _count);
}

public class FDBoardDynamicData : FDDynamicData, IFDBoardDynamicData
{
    //-------------------------------------------------------------------
    private int WaveCount;
    public int waveCount { get { return WaveCount; } }
    public int GetWaveCount() { return WaveCount; }
    public void WaveCountIncrease() { WaveCount++; }
    //-------------------------------------------------------------------
    public bool IsReady;
    public bool isReady { get { return IsReady; } }
    public bool GetIsReady() { return isReady; }
    //-------------------------------------------------------------------
    public float RemainReadyTime;
    public float remainReadyTime { get { return RemainReadyTime; } }
    public float GetRemainReadyTime() { return remainReadyTime; }
    //-------------------------------------------------------------------
    public float RemainWaveTime;
    public float remainWaveTime { get { return RemainWaveTime; } }
    public float GetRemainWaveTime() { return remainWaveTime; }
    //-------------------------------------------------------------------
    public float EnemyCount;
    public float enemyCount { get { return EnemyCount; } }
    public float GetEnemyCount() { return EnemyCount; }
    //-------------------------------------------------------------------
    private int Gold;
    public int gold { get { return Gold; } }
    public int GetGold() { return Gold; }
    //-------------------------------------------------------------------
    private int SummonCount;
    public int summonCount { get { return SummonCount; } }
    public int GetSummonCount() { return SummonCount; }
    //-------------------------------------------------------------------

    public override void BuildData(FDStaticData _staticData)
    {
        base.BuildData(_staticData);

        WaveCount = 1;
        IsReady = true;
        EnemyCount = 0;
        Gold = 0;
        SummonCount = 0;
        RemainReadyTime = 0f;
        RemainWaveTime = 0f;
    }

    public void GainGold(int _amount)
    {
        Gold += _amount;
    }

    public bool PayGold(int _amount)
    {
        if (Gold < _amount)
            return false;

        Gold -= _amount;

        return true;
    }

    public void SetSummonCount(int _count)
    {
        SummonCount = _count;
    }
}