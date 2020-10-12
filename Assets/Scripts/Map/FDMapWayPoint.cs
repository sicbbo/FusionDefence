using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDMapWayPoint
{
    private List<Transform> wayPointList = new List<Transform>();

    private List<Vector3> WayPointPosList = new List<Vector3>();
    public IList<Vector3> wayPointPosList { get { return WayPointPosList; } }

    public void Build(GameObject _rootObj)
    {
        Transform trans = _rootObj.transform;

        for (int i = 0; i < trans.childCount; i++)
        {
            Transform child = trans.GetChild(i);
            wayPointList.Add(child);
            WayPointPosList.Add(child.position);
        }
    }
}
