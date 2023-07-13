using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Script
{
    public class Shape : MonoBehaviour
    {
        
        public GameHandler GameHandler;
        public Wall Wall;
        public Box Box;
        
        protected Vector2 Position;
        private readonly List<RectTransform> _rectTransforms = new();
        protected RectTransform RectTransform;
        protected readonly List<Vector2> ShapeOfShape = new();
        

        
        // Start is called before the first frame update
        void Start()
        {
            Position = new Vector2(0, 0);
            RectTransform = GetComponent<RectTransform>();
            ShapeOfShape.Clear();
            ShapeOfShape.Add(new Vector2(0, 0));
            ShapeOfShape.Add(new Vector2(0, 1));
            ShapeOfShape.Add(new Vector2(0, -1));
            Initialized();
        }

        public void SetBox(Box box)
        {
            Box = box;
        }

        protected void Initialized()
        {
            foreach (var vector2 in ShapeOfShape)
            {
                var transform1 = Wall.transform;
                var wall = Instantiate(Wall, transform1.position, transform1.rotation);
                
                var rectTransform = wall.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(GameHandler.GridSize, GameHandler.GridSize);
                
                wall.transform.SetParent(transform, false);
                rectTransform.anchoredPosition = new Vector3(vector2.x * GameHandler.GridSize, GameHandler.GridSize * vector2.y, 0f);
                
                _rectTransforms.Add(rectTransform);
            }
            UpdateHashTable(true);
        }
        
        public void UpdateHashTable(bool value)
        {
            foreach (var recTransform in _rectTransforms)
            {
                var position = recTransform.anchoredPosition + RectTransform.anchoredPosition;
                var key = ((int)(position.x / GameHandler.GridSize), (int)(position.y / GameHandler.GridSize));
                GameHandler.Board.HashTable[key] = value;
            }
        }

        public bool IsValidShape()
        {
            foreach (var recTransform in _rectTransforms)
            {
                var position = recTransform.anchoredPosition + RectTransform.anchoredPosition;
                
                var key = ((int)(position.x / GameHandler.GridSize), (int)(position.y / GameHandler.GridSize));
                
                try
                {
                    if (GameHandler.Board.HashTable[key]) return false;
                }
                catch (Exception)
                {
                    //ignore
                }
                
                for (var i = 0; i < GameHandler.Snake.GetLength(); i++)
                {
                    var snakePos = GameHandler.Snake.GetBody(i);

                    if (Vector2.Distance(position, snakePos) <= GameHandler.GridSize * 2)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}