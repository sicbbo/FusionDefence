using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIBoardReady : FDUIBoardBase
{
    public override void Build(FDBoardEvent _boardEvent, IFDBoardData _boardData)
    {
        base.Build(_boardEvent, _boardData);
    }

    public void OnClick_GameStart()
    {
        boardEvent.NextBoard(FDSystem.GameBoard.Ready);
    }
}
