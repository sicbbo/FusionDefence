using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDActor : FDMonoBase
{
    private int ActorID = int.MaxValue;
    public int actorID { get { return ActorID; } }

    private int TypeID = int.MaxValue;
    public int typeID { get { return TypeID; } }

    private int Grade = int.MaxValue;
    public int grade { get { return Grade; } }

    public GameObject modelObj = null;
    public FDControllers controllers = null;
    public FDStateManager stateMgr = null;
    public GameObject poolingRoot = null;

    protected FDData data = null;

    public virtual void Initialize(FDSystem.ObjectID _objectType, int _typeID, int _grade, int _actorID, Vector3 _initPos, Vector3 _initRot, FDData _data, FDBoardData _boardData, FDBoardEvent _boardEvent)
    {
        TypeID = _typeID;
        ActorID = _actorID;
        Grade = _grade;

        this.transform.position = _initPos;
        this.transform.rotation = Quaternion.Euler(_initRot);

        data = _data;
        BuildControllers(_objectType, _typeID, _grade);
        stateMgr.BuildState(_actorID, this, _data, controllers, _boardData, _boardEvent);
    }

    public void SendState(FDSystem.State _enum, params object[] args)
    {
        stateMgr.SendState(_enum, args);
    }

    private void BuildControllers(FDSystem.ObjectID _objectType, int _typeID, int _grade)
    {
        controllers.Build(_objectType, _typeID, _grade, this, poolingRoot);
    }

    public override void FDUpdate(float _deltaTime)
    {
        base.FDUpdate(_deltaTime);

        stateMgr.UpdateState(_deltaTime);
        controllers.UpdateController(_deltaTime);
    }

    public override void FDLateUpdate(float _deltaTime)
    {
        base.FDLateUpdate(_deltaTime);

        controllers.LateUpdateController(_deltaTime);
    }

    public T GetData<T>() where T : FDData
    {
        T temp = null;
        if (data is T)
            temp = data as T;

        return temp;
    }

    public virtual void Delete()
    {
        stateMgr.Delete();

        FDPoolingSystem.instance.PushActor(this);
    }
}
