using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameStateEnum GameState;
        public GUISkin MenuSkin;
        public GameScore GameScoreCounter;

        private void Start()
        {
            if (!GameScoreCounter)
            {
                Debug.LogError("Game Score Not Found");
            }
        }

        public void ScoreUp()
        {
            GameScoreCounter.ScoreUp();
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


        private void OnGUI()
        {
            if (GameState == GameStateEnum.StartScreen)
            {
            }

            if (GameState == GameStateEnum.GameOver)
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