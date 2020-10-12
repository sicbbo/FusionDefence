using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public interface IFDBoardSystem
{

}

public class FDBoardSystem : FDMonoBase, IFDBoardSystem
{
    //: UISystem
    public FDUISystem uiSystem = null;

    //: Camera Manager
    public FDCameraManager cameraMgr = null;

    //Parent Target
    public Transform parentMap = null;
    public Transform parentEnemy = null;
    public Transform parentUnit = null;

    //: Input Commander
    private FDInputCommander inputCommander = null;

    // Board
    private Dictionary<FDSystem.GameBoard, FDBoardBase> dicBoards;
    private FDBoardBase curBoard;
    private FDBoardData _boardData = new FDBoardData();
    public FDBoardData boardData { get { return _boardData; } }

    // Manager
    private FDGameManager gameManager;

    // Event
    private FDBoardEvent BoardEvent;
    public FDBoardEvent boardEvent { get { return BoardEvent; } }

    public void Build()
    {
        BuildBoardEvent();
        BuildBoard();
    }

    private void BuildBoardEvent()
    {
        BoardEvent = new FDBoardEvent();

        boardEvent.callBack_NextBoard += NextBoard;
        boardEvent.callBack_GoBoard += GoBoard;
        boardEvent.callBack_ActiveInputCommander += ActiveInputCommander;
        boardEvent.callBack_Pause += PauseBoardSystem;

        uiSystem.BuildEvent();
    }

    private void OnDestroy()
    {
        boardEvent.callBack_NextBoard -= NextBoard;
        boardEvent.callBack_GoBoard -= GoBoard;
        boardEvent.callBack_ActiveInputCommander -= ActiveInputCommander;
        boardEvent.callBack_Pause -= PauseBoardSystem;
    }

    private void BuildBoard()
    {
        _boardData.BuildData(0, 0);

        gameManager = new FDGameManager();
        gameManager.Create(parentMap, parentEnemy, parentUnit, cameraMgr, _boardData, boardEvent);

        inputCommander = new FDInputCommander(gameManager, _boardData);

        FDGlobalInterface.instance.SetInterface(gameManager, boardEvent);

        dicBoards = new Dictionary<FDSystem.GameBoard, FDBoardBase>();
        dicBoards.Add(FDSystem.GameBoard.Load,      new FDLoadBoard(boardEvent, gameManager, _boardData));
        dicBoards.Add(FDSystem.GameBoard.Ready,     new FDReadyBoard(boardEvent, gameManager, _boardData));
        //dicBoards.Add(FDSystem.GameBoard.Select,    new FDSelectBoard(boardEvent, gameManager, _boardData));
        dicBoards.Add(FDSystem.GameBoard.Wave,      new FDWaveBoard(boardEvent, gameManager, _boardData));
        dicBoards.Add(FDSystem.GameBoard.Result,    new FDResultBoard(boardEvent, gameManager, _boardData));

        uiSystem.BuildUIBoard();

        GoBoard(FDSystem.GameBoard.Load);
    }

    private void NextBoard(FDSystem.GameBoard _enumBoard)
    {
        if (curBoard != null)
            curBoard.Clear();

        dicBoards.TryGetValue(_enumBoard + 1, out curBoard);
        if (curBoard == null)
            return;

        curBoard.Build();
    }

    private void GoBoard(FDSystem.GameBoard _enumBoard)
    {
        if (curBoard != null)
            curBoard.Clear();

        dicBoards.TryGetValue(_enumBoard, out curBoard);
        if (curBoard == null)
            return;

        curBoard.Build();
    }

    private void ActiveInputCommander(bool _active)
    {
        inputCommander.SetActive(_active);
    }

    private void PauseBoardSystem(bool _flag)
    {
        Pause(_flag);
        inputCommander.SetActive(!_flag);
    }

    public override void FDUpdate(float _deltaTime)
    {
        inputCommander.CommanderUpdate();

        if (curBoard == null)
            return;

        curBoard.BoardUpdate(_deltaTime);
    }

    public override void FDLateUpdate(float _deltaTime)
    {
        if (curBoard == null)
            return;

        curBoard.BoardLateUpdate();
    }
}
