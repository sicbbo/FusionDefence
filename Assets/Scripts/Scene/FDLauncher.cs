using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDLauncher : FDSceneBase
{
    public override void SceneStart()
    {
        base.SceneStart();

        Application.targetFrameRate = 60;

        FDSceneManager.instance.LoadScene((int)FDSystem.Scene.Title);
    }
}
