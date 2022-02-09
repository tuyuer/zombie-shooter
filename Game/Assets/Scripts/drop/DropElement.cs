using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropElement : MonoBehaviour
{
    public backpack_element_type elementType;
    public float destroyTime = 15;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == TagDef.Player)
        {
            Destroy(gameObject);
            GameWorld.Instance.backpack.AddElement(elementType);
        }   
    }
}
