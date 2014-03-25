using UnityEngine;

namespace Assets.Scripts
{
    public class WallMovement : MonoBehaviour
    {
        public float Speed = 1.0f;
        public float LeftLimit = -6.40f;
        public float ResetPosition = 28.83f;

        public GameManager GManager;

        private void Update()
        {
            if (GManager.GameState == GameStateEnum.Running)
            {
                transform.position += Vector3.left*Time.deltaTime*Speed;

                if (transform.position.x < LeftLimit)
                    transform.position = new Vector3(ResetPosition, transform.position.y, transform.position.z);
            }
        }
    }
}