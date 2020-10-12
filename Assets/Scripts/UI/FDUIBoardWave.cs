using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FDUIBoardWave : FDUIBoardBase
{
    public Text remainTime;
    public Text remainEnemyCount;
    public Text waveCount;
    public Text gold;
    public Text summonText;
    public GameObject unitDeleteObj;

    public override void Build(FDBoardEvent _boardEvent, IFDBoardData _boardData)
    {
        base.Build(_boardEvent, _boardData);

        unitDeleteObj.SetActive(false);
    }

    public override void AddBoardEvent()
    {
        base.AddBoardEvent();

        boardEvent.callBack_SelectUnit += SelectUnit;
    }

    public override void DelBoardEvent()
    {
        base.DelBoardEvent();

        boardEvent.callBack_SelectUnit -= SelectUnit;
    }

    public override void FDUpdate(float _deltaTime)
    {
        base.FDUpdate(_deltaTime);

        if (boardData.GetDynamicData().GetIsReady().Equals(true))
            remainTime.text = string.Format("준비 남은 시간 : {0:0.00}", boardData.GetDynamicData().GetRemainReadyTime());
        else
            remainTime.text = string.Format("웨이브 남은 시간 : {0:0.00}", boardData.GetDynamicData().GetRemainWaveTime());

        remainEnemyCount.text = string.Format("남은 적 : {0}", FDGlobalInterface.instance.iGameManager.GetObjectManager().GetEnemyCount());
        waveCount.text = string.Format("웨이브 : {0}", boardData.GetDynamicData().GetWaveCount());
        gold.text = string.Format("골드 : {0}", boardData.GetDynamicData().GetGold());
        summonText.text = string.Format("소환 X {0}", boardData.GetDynamicData().GetSummonCount());
    }

    public void SelectUnit(bool _isSelect)
    {
        unitDeleteObj.SetActive(_isSelect);
    }

    public void SellUnit()
    {
        IFDObjectManager IObjectMgr = FDGlobalInterface.instance.iGameManager.GetObjectManager();
        FDUnit selectUnit = IObjectMgr.GetSelectUnit();
        if (selectUnit == null)
            return;

        FDUnitDataBase_Status status = FDUnitDataBase.instance.GetStatus(selectUnit.typeID, selectUnit.grade);
        //: 유닛 판매시 등급에따라 소환카운트
        IObjectMgr.RemoveUnit(selectUnit.actorID);
    }

    public void OnPauseButton()
    {
        FDUIPopupSystem.instance.OpenPopup(FDUI.PopupUI.Popup_Pause);
    }

    public void OnClick_SummonButton()
    {
        if (boardData.GetDynamicData().GetSummonCount() <= 0)
            return;

        IFDGameManager IGameMgr = FDGlobalInterface.instance.iGameManager;
        IFDObjectManager IObjectMgr = IGameMgr.GetObjectManager();
        IFDMapManager IMapMgr = IGameMgr.GetMapManager();

        FDField field = IMapMgr.GetEmptyField();
        if (field == null)
        {
            //SUB: 필드가 가득찬 경우라서 경고 메시지를 띄워준다.
            return;
        }

        int ran = Random.Range(0, 12);
        FDUnit unit =  boardEvent.CreateUnit(field, ran, 1);
        IObjectMgr.CheckUnitUpgrade(unit);
        boardData.GetDynamicData().SetSummonCount(boardData.GetDynamicData().GetSummonCount() - 1);
    }
}
