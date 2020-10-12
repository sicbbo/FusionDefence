using FDSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDEffectController : FDController
{
    public override void Build(ObjectID _objectType, int _modelID, int _grade, FDActor _actorObj, FDControllers _controllers, GameObject _poolingRoot)
    {
        base.Build(_objectType, _modelID, _grade, _actorObj, _controllers, _poolingRoot);
    }

    public override void UpdateController(float _deltaTime)
    {
        base.UpdateController(_deltaTime);
    }

    public override void LateUpdateController(float _deltaTime)
    {
        base.LateUpdateController(_deltaTime);
    }
}
