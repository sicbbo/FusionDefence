using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDInGame : FDSceneBase
{
    public FDBoardSystem boardSystem;

    public override void SceneStart()
    {
        base.SceneStart();

        FDConfig.BuildInstance("Config/Config");
        FDConfig.instance.BuildConfig();

        FDBoardDataBase.BuildInstance("DataBase/BoardDataBase");
        FDEnemyDataBase.BuildInstance("DataBase/EnemyDataBase");
        FDUnitDataBase.BuildInstance("DataBase/UnitDataBase");

        boardSystem.Build();

    }

    public void GoResult()
    {
        FDSceneManager.instance.LoadScene((int)FDSystem.Scene.Result);
    }
}
