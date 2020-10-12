using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDWaveBoard : FDBoardBase
{
    private float enemyDelay;
    private int enemyCount;

    private FDWaveDataBase waveDB { get { return boardData.GetWaveDataBase(boardData.dynamicData.waveCount); } }

    public FDWaveBoard(FDBoardEvent _boardEvent, FDGameManager gameManager, FDBoardData _boardData)
        : base(_boardEvent, gameManager, _boardData)
    {
        enumBoard = FDSystem.GameBoard.Wave;
    }
    public override void Build()
    {
        base.Build();

        enemyDelay = 0f;
        enemyCount = 0;

        boardData.dynamicData.IsReady = true;
        boardData.dynamicData.RemainReadyTime = waveDB.WaveReadyTime;
        boardData.dynamicData.RemainWaveTime = waveDB.WaveTime;

        boardEvent.ActiveInputCommander(true);

        isSuccess = true;
    }

    public override void AddBoardEvent()
    {
        base.AddBoardEvent();

        boardEvent.callBack_CreateUnit += CreateUnit;
    }

    public override void DelBoardEvent()
    {
        base.DelBoardEvent();

        boardEvent.callBack_CreateUnit -= CreateUnit;
    }

    public override void BoardUpdate(float _deltaTime)
    {
        base.BoardUpdate(_deltaTime);

        WaveTimeProcess(_deltaTime);
        WaveCreateEnemyProcess(_deltaTime);
    }

    private void WaveTimeProcess(float _deltaTime)
    {
        boardData.dynamicData.RemainReadyTime = Mathf.Max(boardData.dynamicData.remainReadyTime - _deltaTime, 0f);
        if (boardData.dynamicData.remainReadyTime <= 0f)
        {
            boardData.dynamicData.IsReady = false;

            boardData.dynamicData.RemainWaveTime = Mathf.Max(boardData.dynamicData.remainWaveTime - _deltaTime, 0f);
            if (boardData.dynamicData.remainWaveTime <= 0f)
            {
                if (boardData.dynamicData.waveCount < boardData.staticData.maxWaveCount)
                {
                    boardData.dynamicData.SetSummonCount(boardData.dynamicData.summonCount + boardData.staticData.waveSummonCount);
                    boardData.dynamicData.WaveCountIncrease();
                    boardEvent.GoBoard(FDSystem.GameBoard.Wave);
                }
                else
                    boardEvent.NextBoard(enumBoard);
            }
        }
    }

    private void WaveCreateEnemyProcess(float _deltaTime)
    {
        if (boardData.dynamicData.isReady.Equals(true))
            return;

        if (enemyCount.Equals(waveDB.MaxWaveEnemy))
            return;

        enemyDelay = Mathf.Max(enemyDelay - _deltaTime, 0f);
        if (enemyDelay <= 0f)
        {
            enemyDelay = waveDB.EnemyCreateDelay;

            gameManager.objectMgr.CreateEnemy(gameManager.mapMgr.GetEnemyGatePos(), gameManager.mapMgr.MapWayPointList(), waveDB.EnemyTypeID, boardData, boardEvent);
            enemyCount++;
        }
    }

    public FDUnit CreateUnit(FDField _field, int _unitTypeID, int _unitGrade)
    {
        return gameManager.objectMgr.CreateUnit(_field, _unitTypeID, _unitGrade, boardData, boardEvent);
    }

    public override void Clear()
    {
        base.Clear();

        boardEvent.ActiveInputCommander(false);

        isSuccess = false;
    }
}
