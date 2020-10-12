using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDControllers : FDMonoBase
{
    public FDModelController model { get { return GetController<FDModelController>(); } }
    public FDAnimationController ani { get { return GetController<FDAnimationController>(); } }
    public FDEffectController effect { get { return GetController<FDEffectController>(); } }

    private List<FDController> controllerList = new List<FDController>();

    public void Build(FDSystem.ObjectID _objectType, int _typeID, int _grade, FDActor _actorObj, GameObject _poolingRoot)
    {
        this.GetComponentsInChildren<FDController>(controllerList);
        if (controllerList.Count <= 0)
            return;

        for (int i = 0; i < controllerList.Count; i++)
        {
            controllerList[i].Build(_objectType, _typeID, _grade, _actorObj, this, _poolingRoot);
        }
    }

    public void UpdateController(float _deltaTime)
    {
        for (int i = 0; i < controllerList.Count; i++)
        {
            controllerList[i].UpdateController(_deltaTime);
        }
    }

    public void LateUpdateController(float _deltaTime)
    {
        for (int i = 0; i < controllerList.Count; i++)
        {
            controllerList[i].LateUpdateController(_deltaTime);
        }
    }

    private T GetController<T>() where T : FDController
    {
        T controller = null;
        for (int i = 0; i < controllerList.Count; i++)
        {
            if (controllerList[i] is T)
            {
                controller = controllerList[i] as T;
                break;
            }
        }

        return controller;
    }
}