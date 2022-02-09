using HitJoy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropManager : MonoBehaviour
{
    public List<DropElement> dropElementList = new List<DropElement>();

    private static DropManager _instance = null;

    public static DropManager Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }

    public void DropElement(backpack_element_type elementType, int nCount, Vector3 dropPos)
    {
        DropElement dropElement = null;
        for (int i = 0; i < dropElementList.Count; i++)
        {
            if (dropElementList[i].elementType == elementType)
            {
                dropElement = dropElementList[i];
                break;
            }
        }

        if (dropElement != null)
        {
            GameObject clonedObj = Instantiate(dropElement.gameObject);
            clonedObj.transform.position = dropPos;
        }        
    }
}
