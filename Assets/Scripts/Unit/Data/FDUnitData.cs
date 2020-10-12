using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUnitData : FDData
{
    private FDUnitStaticData StaticData = new FDUnitStaticData();
    private FDUnitDynamicData DynamicData = new FDUnitDynamicData();

    public IFDUnitStaticData staticData { get { return StaticData; } }
    public FDUnitDynamicData dynamicData { get { return DynamicData; } }

    public override void BuildData(int _unitTypeID, int _unitGrade)
    {
        base.BuildData(_unitTypeID, _unitGrade);

        StaticData.BuildData(_unitTypeID, _unitGrade);
        DynamicData.BuildData(StaticData);
    }
}

public interface IFDUnitStaticData
{
    int GetUnitTypeID();
    int GetUnitGrade();
    int GetAttackDamage();
    float GetAttackDelay();
    float GetAttackRange();
}

public class FDUnitStaticData : FDStaticData, IFDUnitStaticData
{
    private int UnitTypeID;
    public int GetUnitTypeID() { return UnitTypeID; }

    private int UnitGrade;
    public int GetUnitGrade() { return UnitGrade; }

    private int AttackDamage;
    public int GetAttackDamage() { return AttackDamage; }

    private float AttackDelay;
    public float GetAttackDelay() { return AttackDelay; }

    private float AttackRange;
    public float GetAttackRange() { return AttackRange; }

    public override void BuildData(int _unitTypeID, int _unitGrade)
    {
        base.BuildData(_unitTypeID, _unitGrade);

        FDUnitDataBase_Status status = FDUnitDataBase.instance.GetStatus(_unitTypeID, _unitGrade);

        UnitTypeID = status.UnitTypeID;
        UnitGrade = status.UnitGrade;
        AttackDamage = status.AttackDamage;
        AttackDelay = status.AttackDelay;
        AttackRange = status.AttackRange;
    }
}

public class FDUnitDynamicData : FDDynamicData
{
    public bool isSelect;
    public int curFieldID;
    public int curStorageID;
    public int curTargetID;
    public float attackDelay;
    public float attackDamage;
    public float attackRange;

    public override void BuildData(FDStaticData _staticData)
    {
        base.BuildData(_staticData);

        IFDUnitStaticData staticData = _staticData as IFDUnitStaticData;
        attackDelay = staticData.GetAttackDelay();
        attackDamage = staticData.GetAttackDamage();
        attackRange = staticData.GetAttackRange();

        isSelect = false;
        curFieldID = int.MaxValue;
        curStorageID = int.MaxValue;
        curTargetID = int.MaxValue;
    }
}