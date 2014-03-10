using UnityEngine;
using UnityEngineInternal;

namespace Assets.Scripts
{
    public class FlyScript : MonoBehaviour
    {
        public float Gravity = 9.8f;
        public float CurrentVelocity = 0;
        public float JumpVelocity = 5.0f;

        private bool _isGameRunning = true;

        private void Update()
        {
            if (_isGameRunning)
            {
                CurrentVelocity = CalculateVelocity(CurrentVelocity, -Gravity, Time.deltaTime);
                var newPosY = CalculatePosition(transform.position.y, CurrentVelocity, -Gravity, Time.deltaTime);

                transform.position = new Vector3(transform.position.x, newPosY, transform.position.z);
            }

            if (transform.position.y <= -2.9f)
            {
                Gravity = 0;
                CurrentVelocity = 0;
                _isGameRunning = false;
            }
        }

        private void FixedUpdate()
        {
            if (_isGameRunning)
            {
#if UNITY_IPHONE || UNITY_ANDROID
                

#elif !UNITY_FLASH
                if (Input.GetMouseButtonDown(0))
                {
                    CurrentVelocity = JumpVelocity;
                }
#endif
            }
        }


        private float CalculatePosition(float y0, float v, float a, float dt)
        {
            return 0.5f*a*Mathf.Pow(dt, 2.0f) + v*dt + y0;
        }

        private float CalculateVelocity(float v0, float a, float dt)
        {
            return v0 + a*dt;
        }
    }
}