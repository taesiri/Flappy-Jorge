using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameStateEnum GameState;
        public GUISkin MenuSkin;
        public GUISkin ScoreSkin;
        public GameScore GameScoreCounter;
        public GUILocationHelper Location = new GUILocationHelper();

        private int _highScore = 0;
        private int _currentScore = 0;

        private void Start()
        {
            if (!GameScoreCounter)
            {
                Debug.LogError("Game Score Not Found");
            }
            Location.PointLocation = GUILocationHelper.Point.Center;
            Location.UpdateLocation();
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
                    _highScore = PlayerPrefs.GetInt("Highscore");
                    _currentScore = GameScoreCounter.GetScore();
                    SaveScore();
                    break;
            }
        }


        private void SaveScore()
        {
            if (_currentScore > _highScore)
            {
                Debug.Log(string.Format("New Highscore {0}", _currentScore));

                PlayerPrefs.SetInt("Highscore", _currentScore);
            }
        }

        private void OnGUI()
        {
            if (GameState == GameStateEnum.StartScreen)
            {
            }

            if (GameState == GameStateEnum.GameOver)
            {
                GUI.Label(new Rect(Location.Offset.x - 125, Location.Offset.y - 100, 250, 50), string.Format("{0,-7}{1,-5}", "Score", _currentScore), ScoreSkin.label);
                GUI.Label(new Rect(Location.Offset.x - 125, Location.Offset.y - 25, 250, 50), string.Format("{0,-7}{1,-5}", "Best", _highScore), ScoreSkin.label);


                if (GUI.Button(new Rect(Screen.width/2 - 125, Screen.height/2 + 80, 250, 120), "Start Game", MenuSkin.button))
                {
                    Application.LoadLevel(0);
                }
            }
        }
    }
}