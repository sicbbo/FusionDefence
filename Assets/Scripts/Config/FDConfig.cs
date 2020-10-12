using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDConfig : FDSingletonBase<FDConfig>
{
    public FDIngameConfig inGame { get { return GetConfig<FDIngameConfig>(); } }

    private List<FDConfigBase> configBaseList = new List<FDConfigBase>();

    public void BuildConfig()
    {
        GetComponents<FDConfigBase>(configBaseList);
    }

    private T GetConfig<T>() where T : FDConfigBase
    {
        for (int i=0; i<configBaseList.Count; i++)
        {
            if (configBaseList[i] is T)
            {
                return configBaseList[i] as T;
            }
        }

        return default(T);
    }
}