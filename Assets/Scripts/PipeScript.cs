using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class PipeScript : MonoBehaviour
    {
        private readonly Random _randomGenerator = new Random(DateTime.Now.Millisecond);

        public float Speed = 10;

        public float Distance;
        public int NumberOfPipes = 10;

        public float XOffset = -4;

        public PipeGenerator Mother;

        public void SetYOffset(float yOffset)
        {
            transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        }

        public void RandomYOffset()
        {
            var y = _randomGenerator.Next(1, 780)/100f - 3.8f;
            transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }

        public void Update()
        {
            if (Mother.GameState == GameStateEnum.Running)
            {
                transform.position += Vector3.left*Time.deltaTime*Speed;

                if (transform.position.x < -4)
                {
                    transform.position += Vector3.right*NumberOfPipes*Distance;
                    RandomYOffset();
                }
            }
        }
    }
}