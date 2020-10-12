using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDSingletonBase<T> : FDMonoBase where T : FDMonoBase
{
    private static T _instance;
    private static bool appIsClosing = false;

    public static T instance
    {
        get
        {
            if (appIsClosing.Equals(true))
                return null;

            if (_instance == null)
            {
                _instance = (T)FindObjectOfType(typeof(T));
                if (_instance == null)
                {
                    GameObject newSingleton = new GameObject();
                    _instance = newSingleton.AddComponent<T>();
                    newSingleton.name = typeof(T).ToString();
                }
                DontDestroyOnLoad(_instance);
            }

            return _instance;
        }
    }

    public static void BuildInstance(string _objectPath)
    {
        if (_instance == null)
        {
            GameObject newSingleton = Resources.Load(_objectPath) as GameObject;
            _instance = newSingleton.GetComponent<T>();
            newSingleton.name = typeof(T).ToString();
        }

        DontDestroyOnLoad(_instance);
    }

    private void Start()
    {
        T[] allInstances = FindObjectsOfType(typeof(T)) as T[];

        if (allInstances.Length > 1)
        {
            foreach(T instanceToCheck in allInstances)
            {
                if (instanceToCheck != instance)
                    Destroy(instanceToCheck.gameObject);
            }
        }

        DontDestroyOnLoad((T)FindObjectOfType(typeof(T)));
    }

    private void OnApplicationQuit()
    {
        appIsClosing = true;
    }
}
