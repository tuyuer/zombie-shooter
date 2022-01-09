using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HitJoy
{
    public class Blackboard : MonoBehaviour
    {
        public Animator animator;
        public Character character;
        public CharacterAim characterAim;

        public Brain actorBrain;
        public CharacterController characterController;

        public actor_action_state actorState = actor_action_state.actor_action_state_locomotion;
        public Vector3 moveDir = Vector3.zero;
        public Vector3 actorSpeed = Vector3.zero;


        public void ApplyGravityForActorSpeed(float deltaTime)
        {
            //input direction
            Vector3 newActorSpeed = moveDir * GlobalDef.ACTOR_MOVE_SPEED * deltaTime;

            //apply gravity
            newActorSpeed.y -= GlobalDef.WORLD_GRAVITY * deltaTime;

            //set actorSpeed to blackboard
            actorSpeed = newActorSpeed;
        }

        public void ApplyForwardDirForActor()
        {
            if (moveDir.sqrMagnitude > 0)
            {
                character.transform.forward = moveDir;
            }
        }
    }

}