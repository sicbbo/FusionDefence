using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDTitle : FDSceneBase
{
    public void GoLobby()
    {
        FDSceneManager.instance.LoadScene((int)FDSystem.Scene.Lobby);
    }
}
