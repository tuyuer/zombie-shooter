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
        if (GlobalDef.ENABLE_STICKJOY)
        {
            Vector2 forwardDir = GameWorld.Instance.aimstick.Direction;
            if (forwardDir != Vector2.zero)
            {
                transform.forward = new Vector3(forwardDir.x, 0, forwardDir.y);
            }
        }
        else
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
}
