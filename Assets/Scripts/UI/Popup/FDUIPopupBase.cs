using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIPopupBase : FDMonoBase
{
    public Canvas canvas = null;

    public virtual void Initialize()
    {
        canvas.worldCamera = FDGlobalInterface.instance.iGameManager.GetCameraManager().Get2DCamera();
    }

    public virtual void OpenPopup()
    {

    }

    public virtual void ClosePopup()
    {

    }
}
