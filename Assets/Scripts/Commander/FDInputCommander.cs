using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class FDInputCommander
{
    private FDGameManager gameManager;
    private FDBoardData boardData;

    private Ray ray;
    private RaycastHit rayHit = new RaycastHit();

    private bool isActive = false;
    private Vector2 prvInputPosition;

    public FDInputCommander(FDGameManager _gameManager, FDBoardData _boardData)
    {
        gameManager = _gameManager;
        boardData = _boardData;

        isActive = false;
    }

    public void SetActive(bool _active)
    {
        isActive = _active;
    }

    private void Interact_Object()
    {
        if (IsRayCast("Unit"))
        {
            FDUnit selectUnit = gameManager.objectMgr.GetSelectUnit();
            if (selectUnit != null)
            {
                FDUnit rayUnit = rayHit.transform.GetComponent<FDUnit>();
                selectUnit.SendState(FDSystem.State.DeployField, gameManager.mapMgr.GetField(rayUnit.GetData<FDUnitData>().dynamicData.curFieldID));
            }
            else
            if (rayHit.transform != null)
            {
                FDUnit unit = rayHit.transform.GetComponent<FDUnit>();
                unit.SendState(FDSystem.State.Select, true);
            }
        }
        else
        if (IsRayCast("Field"))
        {
            FDUnit selectUnit = gameManager.objectMgr.GetSelectUnit();
            
            if (rayHit.transform != null && selectUnit != null)
            {
                FDField field = rayHit.transform.GetComponent<FDField>();
                //if (field.isEmpty.Equals(true))
                {
                    selectUnit.SendState(FDSystem.State.DeployField, field);
                }
            }
        }
        else
        {
            FDUnit selectUnit = gameManager.objectMgr.GetSelectUnit();
            if (selectUnit != null)
            {
                selectUnit.SendState(FDSystem.State.Select, false);
            }
        }
    }

    private bool IsRayCast(string _layer)
    {
        if (Physics.Raycast(ray.origin, ray.direction, out rayHit, Mathf.Infinity, 1 << LayerMask.NameToLayer(_layer)))
        {
            return true;
        }

        return false;
    }

    public void CommanderUpdate()
    {
        if (isActive.Equals(false))
            return;
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                prvInputPosition.Set(Input.mousePosition.x, Input.mousePosition.y);
                gameManager.cameraMgr.StartMoveCamera();
            }
        }
        else
        if (Input.GetMouseButtonUp(0))
        {
            gameManager.cameraMgr.StopMoveCamera();
        }

        Vector2 mp = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        if (Input.GetMouseButtonUp(0) && prvInputPosition == mp)
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                ray = gameManager.cameraMgr.camera_3D.ScreenPointToRay(Input.mousePosition);
                Interact_Object();
            }
        }
#endif
        if (Input.touchCount > 0 && Input.touches[0].phase.Equals(TouchPhase.Began))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                gameManager.cameraMgr.StartMoveCamera();
            }
        }
        else
        if (Input.touchCount > 0 && Input.touches[0].phase.Equals(TouchPhase.Ended))
        {
            gameManager.cameraMgr.StopMoveCamera();
        }

        if (Input.touchCount > 0 && Input.touches[0].phase.Equals(TouchPhase.Ended))
        {
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                ray = gameManager.cameraMgr.camera_3D.ScreenPointToRay(Input.touches[0].position);
                Interact_Object();
            }
        }
    }
}
