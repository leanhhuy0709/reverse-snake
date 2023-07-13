using UnityEngine;

namespace _Script
{
    public class LShape : Shape
    {
        void Start()
        {
            Position = new Vector2(0, 0);
            RectTransform = GetComponent<RectTransform>();
            ShapeOfShape.Clear();
            ShapeOfShape.Add(new Vector2(0, 0));
            ShapeOfShape.Add(new Vector2(0, 1));
            ShapeOfShape.Add(new Vector2(1, 0));
            
            Initialized();
        }

        
        
    }
}