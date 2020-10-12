using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDLobby : FDSceneBase
{
    public void GoInGame()
    {
        FDSceneManager.instance.LoadScene((int)FDSystem.Scene.InGame);
    }
}
