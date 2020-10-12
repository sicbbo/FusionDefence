using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public interface IFDMapManager
{
    FDField GetField(int _fieldID);
    FDField GetEmptyField();
}

public class FDMapManager : IFDMapManager
{
    private List<FDField> fieldList = new List<FDField>();
    private GameObject enemyRoad = null;
    private GameObject enemyGate = null;
    private FDMapWayPoint wayPoint = new FDMapWayPoint();

    private Transform parentMap = null;

    private int maxCol = 6;
    private int maxrow = 6;

    public FDMapManager(Transform _parentMap)
    {
        parentMap = _parentMap;
    }

    private GameObject LoadObject(string _resPath, bool _isResetTrans = true)
    {
        GameObject res = Resources.Load(_resPath) as GameObject;
        GameObject obj = _isResetTrans.Equals(true) ? Object.Instantiate(res, Vector3.zero, Quaternion.identity, parentMap) : Object.Instantiate(res, parentMap);

        return obj;
    }

    public void CreateMap(FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        CreateEnemyRoad();
        CreateFIeld(_boardData, _boardEvnet);
        CreateEnemyGate();
        CreateWayPoint();
    }

    private void CreateEnemyRoad()
    {
        enemyRoad = LoadObject("Map/EnemyRoad");
    }

    private void CreateFIeld(FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        GameObject resField = Resources.Load("Prefabs/Tile/Field") as GameObject;
        Transform resFieldTrans = resField.transform;
        
        for(int col=0; col<maxCol; col++)
        {
            for(int row = maxrow; row>0; row--)
            {
                GameObject fieldObj = Object.Instantiate(resField, parentMap);
                FDField field = fieldObj.GetComponent<FDField>();
                int id = (int)FDSystem.ObjectID.Field + fieldList.Count;
                field.Initialize(FDSystem.ObjectID.Field, 0, 0, id, new Vector3(col - 2.5f, resFieldTrans.position.y, row - 3.5f), Vector3.zero, null, _boardData, _boardEvnet);

                fieldList.Add(field);
            }
        }
    }

    private void CreateEnemyGate()
    {
        enemyGate = LoadObject("Map/EnemyGate", false);
    }

    private void CreateWayPoint()
    {
        GameObject rootObj = LoadObject("Map/WayPoint");
        wayPoint.Build(rootObj);
    }

    public Vector3 GetEnemyGatePos()
    {
        return enemyGate.transform.position;
    }

    public IList<Vector3> MapWayPointList()
    {
        return wayPoint.wayPointPosList;
    }

    public FDField GetField(int _fieldID)
    {
        for (int i=0; i<fieldList.Count; i++)
        {
            FDField field = fieldList[i];
            if (field.actorID.Equals(_fieldID))
                return field;
        }

        return null;
    }

    public FDField GetEmptyField()
    {
        for (int i=0; i<fieldList.Count; i++)
        {
            if (fieldList[i].isEmpty == true)
                return fieldList[i];
        }

        return null;
    }
}
