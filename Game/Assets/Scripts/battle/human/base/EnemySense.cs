using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class EnemySense : Sense
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override Transform FindTarget()
        {
            if (gameObject.tag == TagDef.Enemy)
            {
                GameObject targetObj = GameObject.FindGameObjectWithTag(TagDef.Player);
                if (targetObj != null)
                {
                    return targetObj.transform;
                }
            }
            return null;
        }
    }
}

