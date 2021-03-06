﻿using System;
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
    }
}