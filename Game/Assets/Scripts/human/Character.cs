using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Weapon weapon = null;
    public ActorBlood actorBlood = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        weapon.Shoot();
        if (GlobalDef.ENABLE_STICKJOY)
        {
            //if (GameWorld.Instance.aimstick)
            //{

            //}
        }
        else
        {
            //if (Input.GetMouseButton(0))
            //{
            //    weapon.Shoot();
            //}
        }
    }
}
