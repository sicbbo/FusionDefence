using FDSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUnitModelController : FDModelController
{
    protected override void LoadMainModel(ObjectID _objectType, int _typeID, int _grade)
    {
        GameObject res = Resources.Load(string.Format("Model/{0}/Model_{0}_{1}_{2}", _objectType.ToString(), _typeID, _grade)) as GameObject;
        GameObject obj = Instantiate(res, actorObj.modelObj.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        originalModel = obj;
    }
}
