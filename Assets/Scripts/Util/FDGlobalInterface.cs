using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDGlobalInterface : FDSingletonBase<FDGlobalInterface>
{
    public IFDGameManager iGameManager;
    public FDBoardEvent boardEvent;

    public void SetInterface(IFDGameManager _iGameManager, FDBoardEvent _boardEvent)
    {
        iGameManager = _iGameManager;
        boardEvent = _boardEvent;
    }
}
