using UnityEngine;

namespace _Script
{
    public class PointShape : Shape
    {
        void Start()
        {
            Position = new Vector2(0, 0);
            RectTransform = GetComponent<RectTransform>();
            ShapeOfShape.Clear();
            ShapeOfShape.Add(new Vector2(0, 0));
            
            Initialized();
        }

        
        
    }
}