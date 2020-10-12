using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDReadyBoard : FDBoardBase
{
    public FDReadyBoard(FDBoardEvent _boardEvent, FDGameManager _gameManager, FDBoardData _boardData)
        : base(_boardEvent, _gameManager, _boardData)
    {
        enumBoard = FDSystem.GameBoard.Ready;

        boardData.dynamicData.SetSummonCount(boardData.staticData.initSummonCount);
    }

    public override void Build()
    {
        base.Build();

        isSuccess = true;
    }

    public override void Clear()
    {
        base.Clear();

        isSuccess = false;
    }
}
