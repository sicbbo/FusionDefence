using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDEnemyData : FDData
{
    private FDEnemyStaticData StaticData = new FDEnemyStaticData();
    private FDEnemyDynamicData DynamicData = new FDEnemyDynamicData();

    public FDEnemyStaticData staticData { get { return StaticData; } }
    public FDEnemyDynamicData dynamicData { get { return DynamicData; } }

    public override void BuildData(int _typeID, int _grade)
    {
        base.BuildData(_typeID, _grade);

        StaticData.BuildData(_typeID, _grade);
        DynamicData.BuildData(StaticData);
    }
}

public interface IFDEnemyStaticData
{
    float GetOriginalSpeed();
    float GetHitPoint();
    int GetGainGold();
}

public class FDEnemyStaticData : FDStaticData, IFDEnemyStaticData
{
    private float OriginalSpeed;
    public float GetOriginalSpeed() { return OriginalSpeed; }

    private float HitPoint;
    public float GetHitPoint() { return HitPoint; }

    private int gainGold;
    public int GetGainGold() { return gainGold; }

    public override void BuildData(int _typeID, int _grade)
    {
        base.BuildData(_typeID, _grade);

        FDEnemyDataBase_Status status = FDEnemyDataBase.instance.GetEnemyData(_typeID);
        OriginalSpeed = status.originSpeed;
        HitPoint = status.HitPoint;
        gainGold = status.gainGold;
    }
}

public class FDEnemyDynamicData : FDDynamicData
{
    public float speed;
    public float hitPoint;

    public override void BuildData(FDStaticData _staticData)
    {
        base.BuildData(_staticData);

        IFDEnemyStaticData staticData = _staticData as IFDEnemyStaticData;
        speed = staticData.GetOriginalSpeed();
        hitPoint = staticData.GetHitPoint();
    }
}