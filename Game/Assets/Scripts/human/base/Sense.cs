using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HitJoy
{
    public class Sense : MonoBehaviour
    {
        public float heightOffset = 0.0f;

        public float senseDetectRadius = 10;
        [Range(0.0f, 360.0f)]
        public float senseDetectAngle = 270;

        public float attackDetectRadius = 1.8f;
        [Range(0.0f, 360.0f)]
        public float attackDetectAngle = 160;

        [Range(0.0f, 10.0f)]
        public float nearByDetectRadius = 3;

        public LayerMask viewBlockerLayerMask;

        void Awake()
        {
            viewBlockerLayerMask = LayerMask.GetMask(LayerNames.Obstacle, LayerNames.Wall);
        }

        // Update is called once per frame
        void Update()
        {
        }


        public virtual Transform FindTarget()
        {
            return null;
        }

        public bool DetectTargetInArea(Transform target, float detectAngle, float detectRadius)
        {
            if (target == null)
            {
                return false;
            }

            Transform detector = transform;

            Vector3 eyePos = detector.position + Vector3.up * heightOffset;
            Vector3 toPlayer = target.position - eyePos;
            Vector3 toPlayerTop = target.position + Vector3.up * 1.5f - eyePos;

            Vector3 toPlayerFlat = toPlayer;
            toPlayerFlat.y = 0;

            if (toPlayerFlat.sqrMagnitude <= detectRadius * detectRadius)
            {
                if (Vector3.Dot(toPlayerFlat.normalized, detector.forward) >
                    Mathf.Cos(detectAngle * 0.5f * Mathf.Deg2Rad))
                {

                    bool canSee = false;

                    Debug.DrawRay(eyePos, toPlayer, Color.blue);
                    Debug.DrawRay(eyePos, toPlayerTop, Color.blue);

                    canSee |= !Physics.Raycast(eyePos, toPlayer.normalized, detectRadius,
                        viewBlockerLayerMask, QueryTriggerInteraction.Ignore);

                    canSee |= !Physics.Raycast(eyePos, toPlayerTop.normalized, toPlayerTop.magnitude,
                        viewBlockerLayerMask, QueryTriggerInteraction.Ignore);

                    if (canSee)
                        return true;
                }
            }

            return false;
        }

        public virtual bool DetectTargetInSenseView(Transform target)
        {
            return DetectTargetInArea(target, senseDetectAngle, senseDetectRadius);
        }

        public virtual bool DetectTargetInAttackField(Transform target)
        {
            return DetectTargetInArea(target, attackDetectAngle, attackDetectRadius);
        }

        public virtual bool DetectTargetNearBy(Transform target)
        {
            return DetectTargetInArea(target, attackDetectAngle, nearByDetectRadius);
        }

        void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            Color originHandleColor = UnityEditor.Handles.color;

            Color c = new Color(0, 0, 0.7f, 0.4f);
            UnityEditor.Handles.color = c;
            Vector3 rotatedForward = Quaternion.Euler(0, -senseDetectAngle * 0.5f, 0) * transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, senseDetectAngle, senseDetectRadius);

            c = new Color(1.0f, 0, 0.0f, 0.8f);
            UnityEditor.Handles.color = c;
            rotatedForward = Quaternion.Euler(0, -attackDetectAngle * 0.5f, 0) * transform.forward;
            UnityEditor.Handles.DrawSolidArc(transform.position, Vector3.up, rotatedForward, attackDetectAngle, attackDetectRadius);

            UnityEditor.Handles.color = originHandleColor;
#endif
        }
    }

}