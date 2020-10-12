using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ModelData
{
    [System.NonSerialized]
    public Transform originalTrans;
    [System.NonSerialized]
    public Transform attachTrans;

    public FDSystem.ModelType modelType;
    public string resourcePath;
    public string resourceName;
    public Vector3 initLocalPos = Vector3.zero;
    public Vector3 initLocalScale = Vector3.one;
}

public interface IFDModelController
{
    GameObject GetOriginalModel();
}

public class FDModelController : FDController, IFDModelController
{
    [SerializeField]
    private ModelData[] modelData = null;
    public Dictionary<FDSystem.ModelType, ModelData> modelDataDic = new Dictionary<FDSystem.ModelType, ModelData>();

    protected GameObject originalModel = null;
    public GameObject GetOriginalModel() { return originalModel; }

    public override void Build(FDSystem.ObjectID _objectType, int _typeID, int _grade, FDActor _actorObj, FDControllers _controllers, GameObject _poolingRoot)
    {
        base.Build(_objectType, _typeID, _grade, _actorObj, _controllers, _poolingRoot);

        RemoveModel();

        modelDataDic.Clear();
        for (int i=0; i<modelData.Length; i++)
        {
            ModelData data = modelData[i];
            modelDataDic.Add(data.modelType, data);
        }

        LoadMainModel(_objectType, _typeID, _grade);
        LoadModelData();
    }

    protected virtual void LoadMainModel(FDSystem.ObjectID _objectType, int _typeID, int _grade)
    {
        GameObject res = Resources.Load(string.Format("Model/{0}/Model_{0}_{1}_{2}", _objectType.ToString(), _typeID, _grade)) as GameObject;
        GameObject obj = Instantiate(res, actorObj.modelObj.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        originalModel = obj;
    }

    private void LoadModelData()
    {
        for (int i=0; i<(int)FDSystem.ModelType.MaxCount; i++)
        {
            ModelData data = null;
            modelDataDic.TryGetValue((FDSystem.ModelType)i, out data);
            if (data != null)
            {
                GameObject res = Resources.Load(string.Format("{0}/{1}", data.resourcePath, data.resourceName)) as GameObject;
                GameObject obj = Instantiate(res, poolingRoot.transform);
                data.originalTrans = obj.transform;
                data.attachTrans = actorObj.modelObj.transform;
                obj.transform.localPosition = data.initLocalPos;
                obj.transform.localScale = data.initLocalScale;
                obj.SetActive(false);
            }
        }
    }

    private void RemoveModel()
    {
        if (originalModel != null)
        {
            DestroyImmediate(originalModel);
            originalModel = null;
        }

        IEnumerator e = modelDataDic.Keys.GetEnumerator();
        while(e.MoveNext())
        {
            ModelData temp = null;
            modelDataDic.TryGetValue((FDSystem.ModelType)e.Current, out temp);
            if (temp.originalTrans != null)
            {
                DestroyImmediate(temp.originalTrans.gameObject);
                temp.originalTrans = null;
            }
        }
    }

    public void DoPlay(FDSystem.ModelType _modelType)
    {
        if (!modelDataDic.ContainsKey(_modelType))
            return;

        ModelData data = modelDataDic[_modelType];
        data.originalTrans.SetParent(data.attachTrans);
        data.originalTrans.gameObject.SetActive(true);
    }

    public void DoStop(FDSystem.ModelType _modelType)
    {
        if (!modelDataDic.ContainsKey(_modelType))
            return;

        ModelData data = modelDataDic[_modelType];
        data.originalTrans.SetParent(poolingRoot);
        data.originalTrans.gameObject.SetActive(false);
    }

    public void LookAt(Transform _target)
    {
        actorTrans.LookAt(_target, Vector3.up);
    }

    public void SetForward(FDSystem.ModelType _modelType, Vector3 _forward)
    {
        if (!modelDataDic.ContainsKey(_modelType))
            return;

        ModelData data = modelDataDic[_modelType];
        data.originalTrans.forward = _forward;
    }
}