using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace HitJoy
{
    public class LocomotionState : ActorState
    {
        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Blackboard blackboard =  GetBlackboard(animator);
            blackboard.actorState = actor_action_state.actor_action_state_locomotion;
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Blackboard blackboard = GetBlackboard(animator);
            if (blackboard)
            {
                //apply gravity
                blackboard.ApplyGravityForActorSpeed(Time.deltaTime);

                ////set forward dir, navMesh will do
                blackboard.ApplyForwardDirForActor();

                //update animation
                //float moveSpeed = Mathf.Min(blackboard.moveDir.magnitude, GlobalDef.ACTOR_MAX_FOWARD_SPEED);
                Vector3 moveDir = blackboard.moveDir;
                Vector3 forwardDir = blackboard.character.transform.forward;
                Vector3 rightDir = blackboard.character.transform.right;

                Vector3 forwardProject = Vector3.Project(moveDir, forwardDir);
                Vector3 rightProject = Vector3.Project(moveDir, rightDir);

                float ySpeed = forwardProject.magnitude;
                float xSpeed = rightProject.magnitude;

                if (Vector3.Dot(forwardProject, forwardDir) < 0)
                    ySpeed = -ySpeed;

                if (Vector3.Dot(rightProject, rightDir) < 0)
                    xSpeed = -xSpeed;

                animator.SetFloat(AnimatorParameter.YSpeed, ySpeed);
                animator.SetFloat(AnimatorParameter.XSpeed, xSpeed);

                //move character
                if (blackboard.characterController.enabled)
                {
                    blackboard.characterController.Move(blackboard.actorSpeed);
                    if (Mathf.Abs(xSpeed) > 0 ||
                        Mathf.Abs(ySpeed) > 0)
                    {
                        blackboard.character.EnterRunState();
                    }
                    else
                    {
                        blackboard.character.ExitRunState();
                    }
                }
            }
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {

        }
    }
}

