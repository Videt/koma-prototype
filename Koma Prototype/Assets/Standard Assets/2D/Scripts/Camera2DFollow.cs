using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;
        public float lookDistance;
        public LayerMask enemyLayerMask;
        public float speed;
        public Vector3 offcet;


        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        private Collider2D m_DetectedEnemy;
        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = target.position;
            m_OffsetZ = (transform.position - target.position).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            m_DetectedEnemy = Physics2D.OverlapCircle(target.position, lookDistance, enemyLayerMask); //однаружение врага

            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (target.position - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = target.position + m_LookAheadPos + Vector3.forward * m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            if (!m_DetectedEnemy)
            {
                transform.position = newPos;
            }
            else
            {
                seeEnemy();
            }

            m_LastTargetPosition = target.position;
        }
        void seeEnemy()
        {
            // Vector2 newPos = Vector2.Slerp(target.position, m_DetectedEnemy.transform.position, .5f);
            Vector3 pos = Vector3.Slerp(target.position, m_DetectedEnemy.transform.position, .5f);
            Vector3 newPos = Vector3.MoveTowards(transform.position, pos, speed);
            transform.position = new Vector3(newPos.x, newPos.y, (transform.position.z + offcet.z));
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(target.position, lookDistance);
        }
    }
}
