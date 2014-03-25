using UnityEngine;

namespace Assets.Scripts
{
    public class MovementController : MonoBehaviour
    {
        public float TargetForce = 10.0f;
        public float JumpVelocity = 4.0f;
        private bool _isGameRunning = true;
        private bool _isBirdCollided = false;
        public float Gravity = 9.8f;
        public float CurrentVelocity = 0;

        public Camera FollowCam;
        public GameObject Background;


        private void Update()
        {
            transform.position += Vector3.right*TargetForce*Time.deltaTime;
            FollowCam.transform.position += Vector3.right*TargetForce*Time.deltaTime;
            Background.transform.position += Vector3.right*TargetForce*Time.deltaTime;

            HanldeInput();

            HandleRotation();
        }

        private void HanldeInput()
        {
            if (_isGameRunning)
            {
                CurrentVelocity = CalculateVelocity(CurrentVelocity, -Gravity, Time.deltaTime);
                var newPosY = CalculatePosition(transform.position.y, CurrentVelocity, -Gravity, Time.deltaTime);

                transform.position = new Vector3(transform.position.x, newPosY, transform.position.z);
            }


            if (Input.GetMouseButtonDown(0))
            {
                CurrentVelocity = JumpVelocity;
            }


            if (_isGameRunning && !_isBirdCollided)
            {
                if (transform.position.y < 5.5)
                {
#if UNITY_IPHONE || UNITY_ANDROID
                    // Only works for first finger
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
        }


        private void HandleRotation()
        {
            var velY = rigidbody.velocity.y;

            if (velY < -0.5)
            {
                if (velY > -4)
                {
                    transform.rotation = Quaternion.Euler(0, 0, 12*velY);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(0, 0, -48);
                }
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 0, 4*velY);
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


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "DownGuard")
            {
                Gravity = 0;
                CurrentVelocity = 0;
                _isBirdCollided = true;
                _isGameRunning = false;
            }
            else
            {
                _isBirdCollided = true;
            }


            //GManger.ChangeGameState(GameStateEnum.GameOver);
        }
    }
}