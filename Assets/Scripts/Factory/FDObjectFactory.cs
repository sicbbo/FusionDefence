using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDObjectFactory
{
    private static int enemyCount;
    private static int unitCount;

    public static FDUnit CreateUnit(Transform _parentTrans, FDField _field, int _unitTypeID, int _unitGrade, FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        FDUnit unit = FDPoolingSystem.instance.PopActor<FDUnit>();
        if (unit == null)
        {
            FDDebugLog.LogWarning("Pooling Empty Unit");

            GameObject res = Resources.Load("Prefabs/Unit/Unit") as GameObject;
            GameObject obj = Object.Instantiate(res, Vector3.zero, Quaternion.identity);
            unit = obj.GetComponent<FDUnit>();
        }
        unit.transform.SetParent(_parentTrans);

        FDUnitData data = new FDUnitData();
        data.BuildData(_unitTypeID, _unitGrade);

        int unitID = (int)FDSystem.ObjectID.Unit + unitCount;
        unitCount++;

        unit.Initialize(FDSystem.ObjectID.Unit, _unitTypeID, _unitGrade, unitID, _field.transform.position, -Vector3.forward, data, _boardData, _boardEvnet);
        _field.DeployUnit(unit.actorID);
        unit.SendState(FDSystem.State.DeployField, _field);

        return unit;
    }

    public static FDEnemy CreateEnemy(Transform _parentTrans, Vector3 _initPos, IList<Vector3> _wayPointPosList, int _enemyTypeID, FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        FDEnemy enemy = FDPoolingSystem.instance.PopActor<FDEnemy>();
        if (enemy == null)
        {
            FDDebugLog.LogWarning("Pooling Empty Enemy");

            GameObject res = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
            GameObject obj = Object.Instantiate(res, Vector3.zero, Quaternion.identity);
            enemy = obj.GetComponent<FDEnemy>();
        }
        enemy.transform.SetParent(_parentTrans);

        FDEnemyData data = new FDEnemyData();
        data.BuildData(_enemyTypeID, 0);

        int enemyID = (int)FDSystem.ObjectID.Enemy + enemyCount;
        enemyCount++;

        enemy.Initialize(FDSystem.ObjectID.Enemy, _enemyTypeID, 0, enemyID, _initPos, -Vector3.forward, data, _boardData, _boardEvnet);
        enemy.SetInfo(_wayPointPosList);

        return enemy;
    }
}
