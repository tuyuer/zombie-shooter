using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class LandState : ActorState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Blackboard blackboard = GetBlackboard(animator);
            blackboard.actorState = actor_action_state.actor_action_state_land;
            UpdateActor(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateActor(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("LandState Exit !!!");
        }

        void UpdateActor(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
        }
    }
}

