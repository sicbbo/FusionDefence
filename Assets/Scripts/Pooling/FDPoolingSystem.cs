using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDPoolingSystem : FDSingletonBase<FDPoolingSystem>
{
    private Transform poolingNode = null;
    private List<FDActor> actorPoolList = new List<FDActor>();

    public void DoBuild()
    {
        poolingNode = FDUtil.FindSceneObject(FDSystem.Scene.InGame, "PoolingNode").transform;

        CreatePooling();
    }

    private void CreatePooling()
    {
        actorPoolList.Clear();

        int unitCount = FDConfig.instance.inGame.GetPoolingCount(FDSystem.ObjectID.Unit);

        for (int i=0; i<unitCount; i++)
        {
            GameObject res = Resources.Load("Prefabs/Unit/Unit") as GameObject;
            GameObject obj = Object.Instantiate(res, Vector3.zero, Quaternion.identity, poolingNode);
            FDUnit unit = obj.GetComponent<FDUnit>();
            actorPoolList.Add(unit);
        }

        int enemyCount = FDConfig.instance.inGame.GetPoolingCount(FDSystem.ObjectID.Enemy);

        for (int i = 0; i < enemyCount; i++)
        {
            GameObject res = Resources.Load("Prefabs/Enemy/Enemy") as GameObject;
            GameObject obj = Object.Instantiate(res, Vector3.zero, Quaternion.identity, poolingNode);
            FDEnemy enemy = obj.GetComponent<FDEnemy>();
            actorPoolList.Add(enemy);
        }
    }

    public T PopActor<T>() where T : FDActor
    {
        for (int i=0; i<actorPoolList.Count; i++)
        {
            if (actorPoolList[i] is T)
            {
                T temp = actorPoolList[i] as T;
                actorPoolList.RemoveAt(i);
                return temp;
            }
        }

        return null;
    }

    public void PushActor(FDActor _actor)
    {
        _actor.transform.SetParent(poolingNode);
        actorPoolList.Add(_actor);
    }
}