using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class JumpUpState : ActorState
    {
        public float jumpStartSpeed = GlobalDef.ACTOR_JUMP_SPEED;
        private float jumpSpeed = 0;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Blackboard blackboard = GetBlackboard(animator);
            blackboard.actorState = actor_action_state.actor_action_state_jump;
            jumpSpeed = jumpStartSpeed;
            UpdateActor(animator, stateInfo, layerIndex);
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            UpdateActor(animator, stateInfo, layerIndex);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Debug.Log("JumpUpState Exit !!!");
        }

        void UpdateActor(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Blackboard blackboard = animator.GetComponentInParent<Blackboard>();
            if (blackboard)
            {
                float deltaTime = Time.deltaTime;
                //check falling
                bool isFalling = blackboard.actorBrain.IsFalling();
                animator.SetBool(AnimatorParameter.IsFalling, isFalling);
                blackboard.ApplyGravityForActorSpeed(deltaTime);

                if (jumpSpeed > 0)
                {
                    Vector3 actorSpeed = blackboard.actorSpeed;
                    actorSpeed += blackboard.character.up * jumpSpeed * deltaTime;
                    jumpSpeed -= deltaTime * GlobalDef.ACTOR_JUMP_SPEED_ACCEL;

                    blackboard.actorSpeed = actorSpeed;
                }

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
}

