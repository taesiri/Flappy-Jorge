using System.Runtime.InteropServices;
using UnityEngine;

namespace Assets.Scripts
{
    public class GUILocationHelper
    {
        public enum Point
        {
            TopLeft,
            TopRigjt,
            BottomLeft,
            BottomRight,
            Center
        }


        public Point PointLocation = Point.TopLeft;
        public Vector2 Offset;

        public void UpdateLocation()
        {
            switch (PointLocation)
            {
                case Point.TopLeft:
                    Offset = new Vector2(0, 0);
                    break;
                case Point.TopRigjt:
                    Offset = new Vector2(Screen.width, 0);
                    break;
                case Point.BottomLeft:
                    Offset = new Vector2(0, Screen.height);
                    break;
                case Point.BottomRight:
                    Offset = new Vector2(Screen.width, Screen.height);
                    break;
                case Point.Center:
                    Offset = new Vector2(Screen.width/2f, Screen.height/2f);
                    break;
            }
        }
    }
}