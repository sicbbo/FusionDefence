using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FDUIPopupSystem : FDSingletonBase<FDUIPopupSystem>
{
    private Transform parent = null;
    private Dictionary<FDUI.PopupUI, FDUIPopupBase> poolingDic = new Dictionary<FDUI.PopupUI, FDUIPopupBase>();

    public void OpenPopup(FDUI.PopupUI _popupType)
    {
        if (parent == null)
        {
            GameObject obj = new GameObject("UIPopupRoot");
            parent = obj.transform;
        }

        FDUIPopupBase popupObject = null;
        if (poolingDic.TryGetValue(_popupType, out popupObject))
        {
            popupObject.gameObject.SetActive(true);    
        }
        else
        {
            GameObject res = Resources.Load<GameObject>(string.Format("Prefabs/UI/Popup/{0}", _popupType.ToString()));
            GameObject obj = Instantiate<GameObject>(res, parent);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.name = _popupType.ToString();

            popupObject = obj.GetComponent<FDUIPopupBase>();
            poolingDic.Add(_popupType, popupObject);
        }

        popupObject.Initialize();
        popupObject.OpenPopup();
    }

    public void ClosePopup(FDUI.PopupUI _popupType)
    {
        FDUIPopupBase popupObject = null;
        if (poolingDic.TryGetValue(_popupType, out popupObject))
        {
            popupObject.ClosePopup();
            popupObject.gameObject.SetActive(false);
        }
    }
}
