using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/*--------------------------------------------
 * 
 -------------------------------------------*/
public class FDSceneManager : FDSingletonBase<FDSceneManager>
{
    public void LoadScene(int sceneNum)
    {
        StartCoroutine(_LoadScene(sceneNum));
    }

    private IEnumerator _LoadScene(int sceneNum)
    {
        AsyncOperation async = SceneManager.LoadSceneAsync(sceneNum);
        async.allowSceneActivation = false;

        System.GC.Collect();

        float percentage = 0f;
        while (!async.isDone)
        {
            if (percentage < 0.9f)
            {
                percentage = async.progress * 100;
                FDDebugLog.Log(string.Format("{0}%", (int)percentage));
            }
            else
            {
                percentage = (async.progress / 0.9f) * 100;
                FDDebugLog.Log(string.Format("{0}%", (int)percentage));
                async.allowSceneActivation = true;
            }

            yield return null;
        }

        yield return async;
    }
}
