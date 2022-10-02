using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace HitJoy
{
    public class Blackboard : MonoBehaviour
    {
        public Animator animator;

        [HideInInspector]
        public Character character;

        [HideInInspector]
        public CharacterAim characterAim;

        [HideInInspector]
        public Brain actorBrain;

        [HideInInspector]
        public CharacterController characterController;

        public actor_action_state actorState = actor_action_state.actor_action_state_locomotion;
        public Vector3 actorSpeed = Vector3.zero;

        private Vector3 moveDir = Vector3.zero;
        private Vector3 shootDir = Vector3.zero;
        private Vector3 autoAimDir = Vector3.zero;

        public Vector3 MoveDir
        {
            get { return moveDir; }
        }


        private void Awake()
        {
            character = GetComponent<Character>();
            characterAim = GetComponent<CharacterAim>();
            actorBrain = GetComponent<Brain>();
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            ApplyForwardDirForActor();
        }

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
            if (shootDir.sqrMagnitude > 0)
            {
                character.LookTo(shootDir);
            }
            else if (autoAimDir.sqrMagnitude > 0)
            {
                character.LookTo(autoAimDir);
            }
            else if (moveDir.sqrMagnitude > 0)
            {
                character.LookTo(moveDir);
            }
        }

        public void ChangeMoveDir(Vector3 dir)
        {
            moveDir = dir;
        }

        public void ChangeShootDir(Vector3 dir)
        {
            shootDir = dir;
        }

        public void ChangeAutoAimDir(Vector3 dir)
        {
            autoAimDir = dir;
        }
    }

}