using UnityEngine;
using UnityEngineInternal;

namespace Assets.Scripts
{
    public class FlyScript : MonoBehaviour
    {
        public float Gravity = 9.8f;
        public float CurrentVelocity = 0;
        public float JumpVelocity = 5.0f;
        public GUISkin MenuSkin;
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
                // Only wokrs for first finger
                if (Input.touchCount > 0)
                {
                    if (Input.touches[0].phase == TouchPhase.Began)
                    {
                        CurrentVelocity = JumpVelocity;
                    }
                }

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

        private void OnGUI()
        {
            if (!_isGameRunning)
            {
                if (GUI.Button(new Rect(Screen.width/2 - 50, Screen.height/2 + 45, 150, 65),
                    "Start Game", MenuSkin.button))
                {
                    Application.LoadLevel(0);
                }
            }
        }
    }
}