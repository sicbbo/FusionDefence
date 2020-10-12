using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDLoadBoard : FDBoardBase
{
    public FDLoadBoard(FDBoardEvent _boardEvent, FDGameManager _gameManager, FDBoardData _boardData)
        : base(_boardEvent, _gameManager, _boardData)
    {
        enumBoard = FDSystem.GameBoard.Load;
    }
    public override void Build()
    {
        base.Build();

        //: Field Load
        gameManager.mapMgr.CreateMap(boardData, boardEvent);

        //: Pooling
        FDPoolingSystem.instance.DoBuild();

        boardEvent.NextBoard(enumBoard);

        isSuccess = true;
    }

    public override void Clear()
    {
        base.Clear();

        isSuccess = false;
    }
}
