using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIBoardBase : FDMonoBase
{
    protected FDBoardEvent boardEvent;
    protected IFDBoardData boardData;

    public virtual void Build(FDBoardEvent _boardEvent, IFDBoardData _boardData)
    {
        boardEvent = _boardEvent;
        boardData = _boardData;

        AddBoardEvent();
    }

    public virtual void Clear()
    {
        DelBoardEvent();
    }

    public virtual void AddBoardEvent()
    {

    }

    public virtual void DelBoardEvent()
    {

    }
}
