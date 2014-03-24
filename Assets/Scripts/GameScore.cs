using System;
using UnityEngine;

namespace Assets.Scripts
{
    public class GameScore : MonoBehaviour
    {
        public int Score;
        public GUISkin MenuSkin;
        public GameManager GManger;

        public void Start()
        {
            if (!GManger)
            {
                Debug.LogError("Game Manager Not Found");
            }
        }

        public void ScoreUp()
        {
            Score++;
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 250, 50), String.Format("Score : {0}", Score), MenuSkin.label);
        }
    }
}