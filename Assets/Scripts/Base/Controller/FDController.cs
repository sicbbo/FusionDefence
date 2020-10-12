using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDController : FDMonoBase
{
    protected FDActor actorObj = null;
    protected Transform actorTrans = null;
    protected FDControllers controllers = null;
    protected Transform poolingRoot = null;

    public virtual void Build(FDSystem.ObjectID _objectType, int _modelID, int _grade, FDActor _actorObj, FDControllers _controllers, GameObject _poolingRoot)
    {
        actorObj = _actorObj;
        actorTrans = _actorObj.transform;
        controllers = _controllers;
        poolingRoot = _poolingRoot.transform;
    }

    public virtual void UpdateController(float _deltaTime)
    {

    }

    public virtual void LateUpdateController(float _deltaTime)
    {

    }
}
