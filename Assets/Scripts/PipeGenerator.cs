using System;
using UnityEngine;
using Random = System.Random;

namespace Assets.Scripts
{
    public class PipeGenerator : MonoBehaviour
    {
        public float Distance;
        public int NumberOfPipes = 10;
        public GameObject Pipe;
        private GameObject[] _pipes;
        private readonly Random _randomGenerator = new Random(DateTime.Now.Millisecond);


        private void Start()
        {
            _pipes = new GameObject[NumberOfPipes];

            for (int i = 0; i < NumberOfPipes; i++)
            {
                var newPipe = Instantiate(Pipe, Vector3.right*5 + Vector3.right*i*Distance, Quaternion.identity) as GameObject;

                if (newPipe != null)
                {
                    var pscript = newPipe.GetComponent<PipeScript>();

                    pscript.Distance = Distance;

                    pscript.NumberOfPipes = NumberOfPipes;
                    pscript.SetYOffset(_randomGenerator.Next(1, 780)/100f - 3.8f);

                    _pipes[i] = newPipe;
                }
            }
        }
    }
}