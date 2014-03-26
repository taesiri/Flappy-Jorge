using System;
using System.IO;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameScore : MonoBehaviour
    {
        private int _score;
        public GUISkin MenuSkin;
        public GameManager GManger;

        public GUILocationHelper Location = new GUILocationHelper();

        public void Start()
        {
            if (!GManger)
            {
                Debug.LogError("Game Manager Not Found");
            }

            Location.PointLocation = GUILocationHelper.Point.Center;
            Location.UpdateLocation();
        }

        public void ScoreUp()
        {
            _score++;
        }

        public int GetScore()
        {
            return _score;
        }

        private void OnGUI()
        {
            if (GManger.GameState == GameStateEnum.Running)
                GUI.Label(new Rect(Location.Offset.x-125, 25, 250, 50), String.Format("{0}", _score), MenuSkin.label);
        }
    }
}