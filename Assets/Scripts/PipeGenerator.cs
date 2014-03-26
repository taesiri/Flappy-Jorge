using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class PipeGenerator : MonoBehaviour
    {
        public GameManager GManger;

        public float Distance;
        public int NumberOfPipes = 10;
        public float PipeSpeed = 2;
        public GameObject Pipe;
        private GameObject[] _pipes;
        private readonly Random _randomGenerator = new Random(DateTime.Now.Millisecond);

        private void Start()
        {
            if (!GManger)
            {
                Debug.LogError("Game Manager Not Found");
            }
        }


        public void Generate()
        {
            _pipes = new GameObject[NumberOfPipes];

            for (int i = 0; i < NumberOfPipes; i++)
            {
                var newPipe = Instantiate(Pipe, Vector3.right*5 + Vector3.right*i*Distance, Quaternion.identity) as GameObject;

                if (newPipe != null)
                {
                    var pscript = newPipe.GetComponent<PipeScript>();

                    pscript.Distance = Distance;
                    pscript.Mother = this;
                    pscript.GManger = GManger;
                    pscript.NumberOfPipes = NumberOfPipes;

                    var yoff = GenerateYOffset(-1.8f, 3.4f);

                    pscript.SetYOffset(yoff);
                    pscript.Speed = PipeSpeed;
                    _pipes[i] = newPipe;
                }
            }
        }

        private float GenerateYOffset(float min, float max)
        {
            var value = _randomGenerator.Next(0, (int)((max - min) * 100)) / 100f + min;
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