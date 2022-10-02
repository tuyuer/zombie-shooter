using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAim : MonoBehaviour
{
    public GameObject aimTarget;
    public LayerMask layerMask;

    private Character character;

    void Awake()
    {
        character = this.GetComponent<Character>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (GlobalDef.ENABLE_STICKJOY)
        //{
        Vector2 forwardDir = GameWorld.Instance.aimstick.Direction;
        if (forwardDir.sqrMagnitude > 0.3)
        {
            Vector3 newDir = new Vector3(forwardDir.x, 0, forwardDir.y);
            GameWorld.Instance.player.BBoard.ChangeShootDir(newDir);
        }
        else
        {
            GameWorld.Instance.player.BBoard.ChangeShootDir(Vector3.zero);
        }
        //}
        //else
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hitInfo;
        //    if (Physics.Raycast(ray, out hitInfo, 200, layerMask))
        //    {
        //        Vector3 target = hitInfo.point;
        //        target.y = transform.position.y;
        //        transform.forward = target - transform.position;
        //        target.y = 1.3f;
        //        aimTarget.transform.position = target;
        //    }
        //}
    }
}
