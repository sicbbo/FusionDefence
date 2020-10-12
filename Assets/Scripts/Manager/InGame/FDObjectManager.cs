using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * Unit, Enemy, Gimmick 관리
 -------------------------------------------*/
public interface IFDObjectManager
{
    IList<FDUnit> GetUnitList();
    IList<FDEnemy> GetEnemyList();
    int GetEnemyCount();
    FDUnit TryGetUnit(int _actorID);
    FDEnemy TryGetEnemy(int _actorID);
    void RemoveUnit(int _actorID);
    void RemoveEnemy(int _actorID);
    void CheckUnitUpgrade(FDUnit _seletedUnit);
    FDUnit GetSelectUnit();
    void Pause(bool _flag);
    FDUnit CreateUnit(FDField _field, int _unitTypeID, int _unitGrade, FDBoardData _boardData, FDBoardEvent _boardEvnet);
}

public class FDObjectManager : IFDObjectManager
{
    // Object List
    private List<FDUnit> UnitList = new List<FDUnit>();
    private List<FDEnemy> EnemyList = new List<FDEnemy>();

    public IList<FDUnit> unitList { get { return UnitList; } }
    public IList<FDEnemy> enemyList { get { return EnemyList; } }

    //: Parent
    private Transform parentEnemy = null;
    private Transform parentUnit = null;

    private FDBoardData boardData = null;
    private FDBoardEvent boardEvnet = null;

    public FDObjectManager(Transform _parentEnemy, Transform _parentUnit, FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        parentEnemy = _parentEnemy;
        parentUnit = _parentUnit;
        boardData = _boardData;
        boardEvnet = _boardEvnet;
    }

    public FDUnit CreateUnit(FDField _field, int _unitTypeID, int _unitGrade, FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        FDUnit unit = FDObjectFactory.CreateUnit(parentUnit, _field,  _unitTypeID, _unitGrade, _boardData, _boardEvnet);
        UnitList.Add(unit);

        return unit;
    }

    public void CreateEnemy(Vector3 _initPos, IList<Vector3> _wayPointPosList, int _enemyTypeID, FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        FDEnemy enemy = FDObjectFactory.CreateEnemy(parentEnemy, _initPos, _wayPointPosList, _enemyTypeID, _boardData, _boardEvnet);
        EnemyList.Add(enemy);
    }

    public void RemoveUnit(int _actorID)
    {
        FDUnit unit = TryGetUnit(_actorID);
        if (unit == null)
            return;

        UnitList.Remove(unit);
        unit.Delete();
    }

    public void RemoveEnemy(int _actorID)
    {
        FDEnemy enemy = TryGetEnemy(_actorID);
        if (enemy == null)
            return;

        EnemyList.Remove(enemy);
        enemy.Delete();
    }

    public FDUnit GetSelectUnit()
    {
        FDUnit unit = null;
        for (int i=0; i<UnitList.Count; i++)
        {
            if (UnitList[i].IsSelect())
            {
                unit = UnitList[i];
                break;
            }
        }

        return unit;
    }

    public IList<FDUnit> GetUnitList()
    {
        return UnitList;
    }
    public IList<FDEnemy> GetEnemyList()
    {
        return EnemyList;
    }

    public int GetEnemyCount()
    {
        return EnemyList.Count;
    }

    public FDUnit TryGetUnit(int _actorID)
    {
        FDUnit unit = null;

        for (int i = 0; i < UnitList.Count; i++)
        {
            if (UnitList[i].actorID.Equals(_actorID))
            {
                unit = UnitList[i];
                break;
            }
        }

        return unit;
    }

    public FDEnemy TryGetEnemy(int _actorID)
    {
        FDEnemy enemy = null;

        for (int i = 0; i < EnemyList.Count; i++)
        {
            if (EnemyList[i].actorID.Equals(_actorID))
            {
                enemy = EnemyList[i];
                break;
            }
        }

        return enemy;
    }

    public void Pause(bool _flag)
    {
        for (int i=0; i<UnitList.Count; i++)
            UnitList[i].Pause(_flag);

        for (int i = 0; i < EnemyList.Count; i++)
            EnemyList[i].Pause(_flag);
    }

    public void CheckUnitUpgrade(FDUnit _seletedUnit)
    {
        List<FDUnit> selectedUnitList = new List<FDUnit>();
        selectedUnitList.Add(_seletedUnit);

        int seletedUnitTypeID = _seletedUnit.typeID;
        int seletedUnitActorID = _seletedUnit.actorID;
        int seletedUnitGrade = _seletedUnit.grade;

        for (int i = 0; i < UnitList.Count; i++)
        {
            FDUnit unit = UnitList[i];
            if (seletedUnitActorID != unit.actorID && seletedUnitTypeID.Equals(unit.typeID) && seletedUnitGrade.Equals(unit.grade))
            {
                selectedUnitList.Add(unit);
                if (selectedUnitList.Count.Equals(3))
                {
                    UnitUpgrade(selectedUnitList);
                    break;
                }
            }
        }
    }

    private void UnitUpgrade(List<FDUnit> _unitUpgradeList)
    {
        if (_unitUpgradeList.Count < 2)
            return;

        FDUnit baseUnit = null;
        for (int i = 0; i < _unitUpgradeList.Count; i++)
        {
            if (baseUnit == null)
            {
                baseUnit = _unitUpgradeList[i];
                continue;
            }

            if (baseUnit.GetData<FDUnitData>().dynamicData.curStorageID > _unitUpgradeList[i].GetData<FDUnitData>().dynamicData.curStorageID)
                baseUnit = _unitUpgradeList[i];
        }
        if (baseUnit.grade.Equals(3))
            return;

        for (int i = 0; i < _unitUpgradeList.Count; i++)
        {
            RemoveUnit(_unitUpgradeList[i].actorID);
        }

        FDUnitData unitData = baseUnit.GetData<FDUnitData>();
        FDField curField = FDGlobalInterface.instance.iGameManager.GetMapManager().GetField(unitData.dynamicData.curFieldID);
        FDUnit selectedUnit = CreateUnit(curField, baseUnit.typeID, baseUnit.grade + 1, boardData, boardEvnet);

        CheckUnitUpgrade(selectedUnit);
    }
}