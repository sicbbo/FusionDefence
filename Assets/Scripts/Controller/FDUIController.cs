using FDSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIController : FDController
{
    public FDUIActor uiActor = null;

    public override void Build(ObjectID _objectType, int _modelID, int _grade, FDActor _actorObj, FDControllers _controllers, GameObject _poolingRoot)
    {
        base.Build(_objectType, _modelID, _grade, _actorObj, _controllers, _poolingRoot);

        uiActor.Initialize(_actorObj);
    }

    public override void UpdateController(float _deltaTime)
    {
        base.UpdateController(_deltaTime);

        uiActor.UpdateUI(_deltaTime);
    }

    public override void LateUpdateController(float _deltaTime)
    {
        base.LateUpdateController(_deltaTime);

        uiActor.LateUpdateUI(_deltaTime);
    }
}
