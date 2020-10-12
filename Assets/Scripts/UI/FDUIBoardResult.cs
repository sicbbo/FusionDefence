using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIBoardResult : FDUIBoardBase
{
    public void GoResult()
    {
        FDSceneManager.instance.LoadScene((int)FDSystem.Scene.Result);
    }
}
