using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUISystem : FDMonoBase
{
    public FDBoardSystem boardSystem = null;

    [SerializeField]
    private List<FDUIBoardBase> uiBoardList = new List<FDUIBoardBase>();

    public void BuildUIBoard()
    {
        for (int i = 0; i < uiBoardList.Count; i++)
        {
            uiBoardList[i].Build(boardSystem.boardEvent, boardSystem.boardData);
        }
    }

    public void BuildEvent()
    {
        boardSystem.boardEvent.callBack_SetActiveUIBoard += SetActiveUIBoard;
    }

    private void SetActiveUIBoard(FDSystem.GameBoard _enumBoard, bool _active)
    {
        uiBoardList[(int)_enumBoard].gameObject.SetActive(_active);
    }
}
