using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class DodgeState : ActorState
    {
        public float dodgeStartSpeed = GlobalDef.ACTOR_DODGE_SPEED;
        private float dodgeSpeed = 0;
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            dodgeSpeed = dodgeStartSpeed;
            Blackboard blackboard = GetBlackboard(animator);
            blackboard.actorState = actor_action_state.actor_action_state_dodge;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Blackboard blackboard = GetBlackboard(animator);
            if (blackboard)
            {
                float deltaTime = Time.deltaTime;
                //apply gravity
                blackboard.ApplyGravityForActorSpeed(Time.deltaTime);

                //set forward dir
                blackboard.ApplyForwardDirForActor();

                if (dodgeSpeed > 0)
                {
                    Vector3 actorSpeed = blackboard.actorSpeed;
                    actorSpeed += blackboard.character.transform.forward * dodgeSpeed * deltaTime;
                    dodgeSpeed -= deltaTime * GlobalDef.ACTOR_JUMP_SPEED_ACCEL;

                    blackboard.actorSpeed = actorSpeed;
                }

                //move character
                if (blackboard.characterController.enabled)
                {
                    blackboard.characterController.Move(blackboard.actorSpeed);
                }
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}

