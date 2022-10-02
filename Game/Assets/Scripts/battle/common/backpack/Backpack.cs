using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    private List<BackpackElementInfo> elementInfoList = new List<BackpackElementInfo>(); 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetElementCount(backpack_element_type elementType)
    {
        foreach (var item in elementInfoList)
        {
            if (item.elementType == elementType)
            {
                return item.elementCount;
            }
        }

        return 0;
    }

    public void AddElement(backpack_element_type elementType, int elementCount = 1)
    {
        bool bSuccess = false;
        foreach (var item in elementInfoList)
        {
            if (item.elementType == elementType)
            {
                item.elementCount += elementCount;
                bSuccess = true;
                break;
            }
        }

        if (!bSuccess)
        {
            BackpackElementInfo elementInfo = new BackpackElementInfo();
            elementInfo.elementCount = elementCount;
            elementInfoList.Add(elementInfo);
        }
    }

    public void RemoveElement(backpack_element_type elementType, int elementCount = 1)
    {
        foreach (var item in elementInfoList)
        {
            if (item.elementType == elementType)
            {
                item.elementCount -= elementCount;
                if (item.elementCount < 0)
                {
                    item.elementCount = 0;
                }
                break;
            }
        }
    }
}
