using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class ActorState : StateMachineBehaviour
    {
        protected Blackboard GetBlackboard(Animator animator)
        {
            return animator.gameObject.transform.parent.GetComponent<Blackboard>();
        }
    }
}
