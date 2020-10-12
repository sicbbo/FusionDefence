using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FDBoardBase
{
    public FDSystem.GameBoard enumBoard;
    public FDBoardEvent boardEvent;
    public FDGameManager gameManager;
    public FDBoardData boardData;

    protected bool isSuccess = false;

    public FDBoardBase(FDBoardEvent _boardEvent, FDGameManager _gameManager, FDBoardData _boardData)
    {
        boardEvent = _boardEvent;
        gameManager = _gameManager;
        boardData = _boardData;

        isSuccess = false;
    }

    public virtual void Build()
    {
        FDDebugLog.Log(this.GetType().Name + "Build");

        AddBoardEvent();

        boardEvent.SetActiveUIBoard(enumBoard, true);
    }

    public virtual void AddBoardEvent()
    {

    }

    public IEnumerator SomeTimeAction()
    {
        yield return null;
    }

    public virtual void BoardUpdate(float _deltaTime)
    {

    }

    public virtual void BoardLateUpdate()
    {

    }

    public virtual void Clear()
    {
        FDDebugLog.Log(this.GetType().Name + "Clear");

        DelBoardEvent();

        boardEvent.SetActiveUIBoard(enumBoard, false);
    }

    public virtual void DelBoardEvent()
    {

    }
}
