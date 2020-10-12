using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*------------------------------------------
 * 인게임 안의 모든 매니저들을 관리
------------------------------------------*/
public interface IFDGameManager
{
    IFDObjectManager GetObjectManager();
    IFDMapManager GetMapManager();
    IFDCameraManager GetCameraManager();
}

public class FDGameManager : IFDGameManager
{
    private FDObjectManager ObjectMgr;
    public FDObjectManager objectMgr { get { return ObjectMgr; } }

    private FDMapManager MapMgr;
    public FDMapManager mapMgr { get { return MapMgr; } }

    private FDCameraManager CameraMgr;
    public FDCameraManager cameraMgr { get { return CameraMgr; } }

    public void Create(Transform _parentMap, Transform _parentEnemy, Transform _parentUnit, FDCameraManager _cameraMgr, FDBoardData _boardData, FDBoardEvent _boardEvnet)
    {
        ObjectMgr = new FDObjectManager(_parentEnemy, _parentUnit, _boardData, _boardEvnet);
        MapMgr = new FDMapManager(_parentMap);
        CameraMgr = _cameraMgr;
    }

    public IFDObjectManager GetObjectManager() { return ObjectMgr; }
    public IFDMapManager GetMapManager() { return MapMgr; }
    public IFDCameraManager GetCameraManager() { return CameraMgr; }
}