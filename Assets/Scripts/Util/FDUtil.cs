using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUtil
{
    public static GameObject FindSceneObject(FDSystem.Scene _scene, string _name)
    {
        GameObject root = GameObject.Find(string.Format("{0}Root", _scene.ToString()));
        if (root == null)
            return null;

        Transform[] trans = root.GetComponentsInChildren<Transform>(true);
        for (int i=0; i<trans.Length; i++)
        {
            if (trans[i].name.Equals(_name))
                return trans[i].gameObject;
        }

        return null;
    }
}
