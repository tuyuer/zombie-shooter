using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAim : MonoBehaviour
{
    public GameObject aimTarget;
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, 200, layerMask))
        {
            Vector3 target = hitInfo.point;
            target.y = transform.position.y;
            transform.forward = target - transform.position;
            target.y = 1.3f;
            aimTarget.transform.position = target;
        }
    }
}
