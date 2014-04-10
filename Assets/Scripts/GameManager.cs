using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameManager : MonoBehaviour
    {
        public GameStateEnum GameState;
        public GUISkin MenuSkin;
        public GUISkin MenuSkin1;
        public GUISkin MenuSkin2;
        public GUISkin ScoreSkin;
        public GUISkin GameOverSkin;
        public Texture2D BoxTexure;
        public GameScore GameScoreCounter;
        public GUILocationHelper Location = new GUILocationHelper();
        public PipeGenerator Generator;
        public FlyScript Bird;
        private int _highScore = 0;
        private int _currentScore = 0;

        public Texture2D HandTexture2D;

        public GUISkin EmpySkin;


        private Matrix4x4 _guiMatrix;

        private void Start()
        {
            if (!GameScoreCounter)
            {
                Debug.LogError("Game Score Not Found");
            }
            if (!Generator)
            {
                Debug.LogError("Pipe Generator Not Found");
            }
            if (!Bird)
            {
                Debug.LogError("Bird Not Found");
            }

            Location.PointLocation = GUILocationHelper.Point.Center;
            Location.UpdateLocation();

            GameState = GameStateEnum.StartScreen;


            Vector2 ratio = Location.GuiOffset;
            _guiMatrix = Matrix4x4.identity;
            _guiMatrix.SetTRS(new Vector3(1, 1, 1), Quaternion.identity, new Vector3(ratio.x, ratio.y, 1));
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

                case GameStateEnum.Running:
                    Generator.Generate();
                    break;
            }
        }


        private void SaveScore()
        {
            if (_currentScore > _highScore)
            {
                _highScore = _currentScore;
                PlayerPrefs.SetInt("Highscore", _currentScore);
            }
        }


        private void OnGUI()
        {
            GUI.matrix = _guiMatrix;

            if (GameState == GameStateEnum.StartScreen)
            {
                GUI.Label(new Rect(Location.Offset.x - 200, 45, 400, 50), "Get Ready!", MenuSkin1.label);

                GUI.DrawTexture(new Rect(Location.Offset.x - 16, Location.Offset.y + 80, 32, 32), HandTexture2D);
                GUI.Label(new Rect(Location.Offset.x - 200, Location.Offset.y + 140, 400, 50), "TAP", MenuSkin2.label);
            }

            if (GameState == GameStateEnum.GameOver)
            {
                GUI.Label(new Rect(Location.Offset.x - 200, 100, 400, 50), "GAME OVER", GameOverSkin.label);

                GUI.Box(new Rect(Location.Offset.x - 200, Location.Offset.y - 150, 400, 400), BoxTexure, EmpySkin.box);

                GUI.Label(new Rect(Location.Offset.x - 200, Location.Offset.y - 100, 400, 50), string.Format("{0,-7}{1,-5}", "Score", _currentScore), ScoreSkin.label);
                GUI.Label(new Rect(Location.Offset.x - 200, Location.Offset.y - 25, 400, 50), string.Format("{0,-7}{1,-5}", "Best", _highScore), ScoreSkin.label);


                if (GUI.Button(new Rect(Location.Offset.x - 125, Location.Offset.y + 100, 250, 65), "Start Game", MenuSkin.button))
                {
                    Application.LoadLevel(0);
                }
            }

            if (GameState == GameStateEnum.Running)
            {
                GUI.Label(new Rect(Location.Offset.x - 125, 25, 250, 50), String.Format("{0}", GameScoreCounter.GetScore()), ScoreSkin.label);
            }


            GUI.matrix = Matrix4x4.identity;
        }
    }
}