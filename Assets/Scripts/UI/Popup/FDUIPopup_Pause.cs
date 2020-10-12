using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIPopup_Pause : FDUIPopupBase
{
    public override void Initialize()
    {
        base.Initialize();
    }

    public override void OpenPopup()
    {
        base.OpenPopup();

        FDGlobalInterface.instance.iGameManager.GetObjectManager().Pause(true);
        FDGlobalInterface.instance.boardEvent.CallBack_Pause(true);
    }

    public override void ClosePopup()
    {
        base.ClosePopup();

        FDGlobalInterface.instance.iGameManager.GetObjectManager().Pause(false);
        FDGlobalInterface.instance.boardEvent.CallBack_Pause(false);
    }

    public void GameReturn()
    {
        FDUIPopupSystem.instance.ClosePopup(FDUI.PopupUI.Popup_Pause);
    }

    public void GoLobby()
    {
        FDSceneManager.instance.LoadScene((int)FDSystem.Scene.Lobby);
    }
}
