using FDSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AnimationData
{
    public FDSystem.AnimationType type;
    public string parameterName;
}

public class FDAnimationController : FDController
{
    private Animator animator = null;

    [SerializeField]
    private AnimationData[] animationData = null;
    private Dictionary<FDSystem.AnimationType, AnimationData> animationDic = new Dictionary<AnimationType, AnimationData>();

    public override void Build(ObjectID _objectType, int _modelID, int _grade, FDActor _actorObj, FDControllers _controllers, GameObject _poolingRoot)
    {
        base.Build(_objectType, _modelID, _grade, _actorObj, _controllers, _poolingRoot);

        animator = controllers.model.GetOriginalModel().GetComponent<Animator>();

        animationDic.Clear();
        for (int i = 0; i < animationData.Length; i++)
        {
            animationDic.Add(animationData[i].type, animationData[i]);
        }
    }

    public void Play(FDSystem.AnimationType _type)
    {
        if (!animationDic.ContainsKey(_type))
            return;
        AnimationData data = null;
        animationDic.TryGetValue(_type, out data);
        if (data == null)
            return;

        animator.Play(data.parameterName);
    }

    public void SetFloat(string _paramName, float _value)
    {
        animator.SetFloat(_paramName, _value);
    }
}