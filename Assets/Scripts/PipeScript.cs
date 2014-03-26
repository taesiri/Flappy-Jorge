using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class PipeScript : MonoBehaviour
    {
        private readonly Random _randomGenerator = new Random(DateTime.Now.Millisecond);

        public GameManager GManger;

        public float Speed = 10;
        public float Distance;
        public float ScoreUpXOffset = -0.9f;
        public int NumberOfPipes = 10;
        public float XOffset = -4;
        public PipeGenerator Mother;
        private bool _isScored = false;


        public void Start()
        {
            if (!GManger)
            {
                Debug.LogError("Game Manager Not Found");
            }
        }

        public void SetYOffset(float yOffset)
        {
            transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
        }

        public void Update()
        {
            if (GManger.GameState == GameStateEnum.Running)
            {
                transform.position += Vector3.left*Time.deltaTime*Speed;

                if (transform.position.x < -4)
                {
                    transform.position += Vector3.right*NumberOfPipes*Distance;
                    var newYOffset = GenerateYOffset(-1.8f, 3.4f);
                    transform.position = new Vector3(transform.position.x, newYOffset, transform.position.z);

                    _isScored = false;
                }

                if (transform.position.x < ScoreUpXOffset && !_isScored)
                {
                    GManger.ScoreUp();
                    _isScored = true;
                }
            }
        }

        private float GenerateYOffset(float min, float max)
        {
            var value = _randomGenerator.Next(0, (int) ((max - min)*100))/100f + min;
            if (value < max && value > min)
            {
                return value;
            }
            else
            {
                return GenerateYOffset(min, max);
            }
        }
    }
}