using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDResultBoard : FDBoardBase
{
    public FDResultBoard(FDBoardEvent _boardEvent, FDGameManager gameManager, FDBoardData _boardData)
        : base(_boardEvent, gameManager, _boardData)
    {
        enumBoard = FDSystem.GameBoard.Result;
    }
    public override void Build()
    {
        base.Build();

        gameManager.objectMgr.Pause(true);

        isSuccess = true;
        //: 로비로 버튼 클릭후 로비로 씬 이동
    }

    public override void Clear()
    {
        base.Clear();

        isSuccess = false;
    }
}
