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

                    var yoff = (_randomGenerator.Next(0, 500)/100f) - 1.8f;
                    pscript.SetYOffset(yoff);
                    Debug.Log(yoff);

                    pscript.Speed = PipeSpeed;

                    _pipes[i] = newPipe;
                }
            }
        }
    }
}