using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIActor : FDMonoBase
{
    protected FDActor actorObj = null;

    public virtual void Initialize(FDActor _actorObj)
    {
        actorObj = _actorObj;
    }

    public virtual void UpdateUI(float _deltaTime)
    {

    }

    public virtual void LateUpdateUI(float _deltaTime)
    {

    }
}
