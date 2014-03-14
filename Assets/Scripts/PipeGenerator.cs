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

        public GameStateEnum GameState;

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
                    pscript.Mother = this;
                    pscript.NumberOfPipes = NumberOfPipes;
                    pscript.SetYOffset(_randomGenerator.Next(1, 780)/100f - 3.8f);

                    _pipes[i] = newPipe;
                }
            }
        }

        public void ChangeGameState(GameStateEnum newState)
        {
            GameState = newState;
            UpdateState();
        }

        private void UpdateState()
        {
            switch (GameState)
            {
                case GameStateEnum.GameOver:

                    break;
            }
        }
    }
}