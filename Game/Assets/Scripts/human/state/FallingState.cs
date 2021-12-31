using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class FallingState : ActorState
    {
        public float animatorGrabDistance = 0.25f;
        public float animatorGrabCheckRadius = 0.2f;
        public float animatorHeight = 1.8f;

        public float gravityFactor = 1.0f;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Blackboard blackboard = GetBlackboard(animator);
            blackboard.actorState = actor_action_state.actor_action_state_falling;
            UpdateActor(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateActor(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("FallingState Exit !!!");
        }

        void UpdateActor(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            float deltaTime = Time.deltaTime;
            Blackboard blackboard = GetBlackboard(animator);

            //check falling
            bool isFalling = blackboard.actorBrain.IsFalling();
            animator.SetBool(AnimatorParameter.IsFalling, isFalling);
            blackboard.ApplyGravityForActorSpeed(deltaTime * gravityFactor);

            //set forward dir
            blackboard.ApplyForwardDirForActor();

            //move chracter
            if (blackboard.characterController.enabled)
            {
                blackboard.characterController.Move(blackboard.actorSpeed);
            }
        }
    }
}

