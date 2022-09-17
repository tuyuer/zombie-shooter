using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class ActorBrain : Brain
    {
        private Camera mainCamera;
        private InputComponent inputComponent;
        private Character actor;
        // Start is called before the first frame update
        void Awake()
        {
            base.Awake();
            mainCamera = Camera.main;
            inputComponent = GetComponent<InputComponent>();
            actor = GetComponent<Character>();
        }

        void OnEnable()
        {
            if (inputComponent != null)
            {
                inputComponent.onInputEvent += OnInputEvent;
                inputComponent.onDirectionEvent += OnDirectionEvent;
            }
        }

        void OnDisable()
        {
            if (inputComponent != null)
            {
                inputComponent.onInputEvent -= OnInputEvent;
                inputComponent.onDirectionEvent -= OnDirectionEvent;
            }
        }

        void OnInputEvent(string action, input_action_state actionState)
        {
            if (action == InputActionNames.JUMP && actionState == input_action_state.press)
            {
                OnJump();
            }
            else if (action == InputActionNames.DODGE && actionState == input_action_state.press)
            {
                OnDodge();
            }
            else if (action == InputActionNames.O && actionState == input_action_state.press)
            {
                OnAttackO();
            }
            else if (action == InputActionNames.X && actionState == input_action_state.hold)
            {
                Debug.Log("input event!!");
                actor.Shoot();
            }
            else if (action == InputActionNames.SHOWSWEAPON && actionState == input_action_state.press)
            {
                OnShowWeapon();
            }
            else if (action == InputActionNames.SWORD_ATTACK_UP && actionState == input_action_state.press)
            {

            }
        }

        void OnDirectionEvent(Vector2 dir, Vector2 dirRaw, input_action_state inputState)
        {
            var newDir = CalculateMoveDir(dir, inputState);
            blackboard.ChangeMoveDir(newDir);
        }

        Vector3 CalculateMoveDir(Vector2 dir, input_action_state inputState)
        {
            if (inputState == input_action_state.release)
            {
                return Vector3.zero;
            }

            //得到投影向量 为vector到以planeNormal为法向量的平面上。
            Vector3 forward = Vector3.ProjectOnPlane(mainCamera.transform.forward, Vector3.up).normalized;
            Vector3 right = Vector3.ProjectOnPlane(mainCamera.transform.right, Vector3.up).normalized;

            Vector3 forwardDir = forward * dir.y;
            Vector3 rightDir = right * dir.x;
            return (forwardDir + rightDir).normalized;
        }
    }
}