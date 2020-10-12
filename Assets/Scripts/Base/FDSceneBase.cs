using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDSceneBase : FDMonoBase
{
    public virtual void SceneAwake()
    {
        FDDebugLog.Log(this.GetType().Name + "Awake");
    }

    public virtual void SceneStart()
    {
        FDDebugLog.Log(this.GetType().Name + "Start");
    }
    
    void Awake()
    {
        SceneAwake();
    }

    void Start()
    {
        SceneStart();
    }
}
