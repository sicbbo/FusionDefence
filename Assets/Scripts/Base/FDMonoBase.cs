using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDMonoBase : MonoBehaviour
{
    private bool IsPause = false;

    public virtual void FDStart()
    {

    }

    private void Start()
    {
        FDStart();
    }

    public virtual void Pause(bool _flag)
    {
        IsPause = _flag;
    }

    public virtual void FDUpdate(float _deltaTime)
    {

    }

    private void Update()
    {
        float deltaTime = 0f;

        if (IsPause.Equals(false))
            deltaTime = Time.deltaTime;

        FDUpdate(deltaTime);
    }

    public virtual void FDLateUpdate(float _deltaTime)
    {

    }

    private void LateUpdate()
    {
        float deltaTime = 0f;

        if (IsPause.Equals(false))
            deltaTime = Time.deltaTime;

        FDLateUpdate(deltaTime);
    }
}
