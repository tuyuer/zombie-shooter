using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using com.ootii.Messages;

namespace HitJoy
{
    public class Brain : MonoBehaviour
    {
        protected Blackboard blackboard;
        // Start is called before the first frame update
        public void Awake()
        {
            blackboard = GetComponent<Blackboard>();
        }

        public void Start()
        {

        }

        public void OnDestroy()
        {
           
        }

        public void Update()
        {

        }

        public bool IsGrounded()
        {
            int nWalkableLayer = LayerMask.NameToLayer(LayerNames.Terrain);
            RaycastHit hitInfo;
            if (Physics.Raycast(new Ray(transform.position + Vector3.up * 0.15f, Vector3.down), out hitInfo, 0.3f, 1 << nWalkableLayer))
            {
                return true;
            }
            return false;
        }

        public bool IsFalling()
        {
            bool bRet = false;
            if (blackboard.characterController.velocity.y < 0.1f)
            {
                bRet = true;
            }
            if (IsGrounded())
            {
                bRet = false;
            }

            return bRet;
        }

        public void OnJump()
        {
            if (blackboard.actorState != actor_action_state.actor_action_state_locomotion)
            {
                return;
            }

            blackboard.animator.SetTrigger(AnimatorParameter.Jump);
        }

        public void OnDodge()
        {
            if (blackboard.actorState == actor_action_state.actor_action_state_locomotion)
            {
                blackboard.animator.SetTrigger(AnimatorParameter.Dodge);
            }
        }

        public void OnAttackO()
        {

        }

        public void OnAttackX()
        {

        }

        public void OnShowWeapon()
        {

        }

        private void OnListenAttacken(IMessage incomingMessage)
        {

        }
    }

}
