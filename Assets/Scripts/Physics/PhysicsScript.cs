using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class PhysicsScript : MonoBehaviour
    {
        public LayerMask environment;

        //protected CapsuleCollider capsuleCollider;
        //protected const float skinWidth = 0.0f;
    //protected Vector3 Velocity;
        private float deceleration = 3;
        protected Vector3 normal;
        protected const float staticFriction = 0.55f;
        protected float dynamicFriction;
        protected const float gravityConstant = 20f;



        // Start is called before the first frame update
        void Start() { 
           
        }


        public Vector3 BoxCollisionCheck(Vector3 velocity, BoxCollider collider, float skinWidth)
        {
            RaycastHit raycastHit;
            #region Raycast

            bool boxCast = Physics.BoxCast(collider.transform.position, collider.transform.localScale/2,
                velocity, out raycastHit, collider.transform.rotation, velocity.magnitude * Time.deltaTime + skinWidth, environment);
            #endregion
            if (raycastHit.collider == null)
                return velocity;
            else
            {
                #region Apply Normal Force
                normal = Normal3D(velocity, raycastHit.normal);
                velocity += normal;
                Friction(normal.magnitude, velocity);
                #endregion
                if (velocity.magnitude < skinWidth)
                {
                    velocity = Vector3.zero;
                    return velocity;
                }

                BoxCollisionCheck(velocity, collider, skinWidth);
                return velocity;
            }
        }

        public Vector3 CapsuleCollisionCheck(Vector3 velocity, CapsuleCollider collider, float skinWidth)
        {
            RaycastHit raycastHit;
            #region Raycast
            Vector3 point1 = transform.position + collider.center + Vector3.up * (collider.height / 2 - collider.radius);
            Vector3 point2 = transform.position + collider.center + Vector3.down * (collider.height / 2 - collider.radius);
            bool capsulecast = Physics.CapsuleCast(point1, point2,
                collider.radius, velocity, out raycastHit, velocity.magnitude * Time.deltaTime + skinWidth, environment);
            #endregion
            if (raycastHit.collider == null)
                return velocity;
            else
            {
                #region Apply Normal Force
                normal = Normal3D(velocity, raycastHit.normal);
                velocity += normal;
                Friction(normal.magnitude, velocity);
                #endregion
                if (velocity.magnitude < skinWidth)
                {
                    velocity = Vector3.zero;
                    return velocity;
                }

                CapsuleCollisionCheck(velocity, collider, skinWidth);
                return velocity;
            }
            
        }

        public Vector3 Normal3D(Vector3 velocity, Vector3 normal)
        {

            float dotProduct = Vector3.Dot(velocity, normal);

            if (dotProduct > 0)
            {
                dotProduct = 0f;
            }
            Vector3 projection = dotProduct * normal;
            return -projection;
        }

        private void Friction(float normalMag, Vector3 velocity)
        {
            if (velocity.magnitude < (staticFriction * normalMag))
            {
                velocity = new Vector3(0, 0, 0);
            }
            else
            {
                velocity += (dynamicFriction * normalMag) * -velocity.normalized;
            }
        }

        public Vector3 Gravity(Vector3 velocity)
        {
            Vector3 gravity = Vector3.down * gravityConstant * Time.deltaTime;
            velocity += gravity;
            return velocity;
        }

    public Vector3 Decelerate(Vector3 velocity)
    {
        Vector3 tempVel = new Vector3(velocity.x, 0, velocity.z);
        //Vector3 tempVel = new Vector3(Velocity.x, Velocity.y, Velocity.z);
        velocity -= tempVel.normalized * deceleration * Time.deltaTime;
        return velocity;
    }
}
