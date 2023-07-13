using System;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Script
{
    public class Food : MonoBehaviour
    {
        public GameHandler GameHandler;
        
        private float _gridSize = 0;

        private RectTransform _rectTransform;

        // Start is called before the first frame update
        void Start()
        {
            
            _gridSize = GameHandler.GridSize;
            
            _rectTransform = GetComponent<RectTransform>();
            _rectTransform.sizeDelta = new Vector2(GameHandler.GridSize, GameHandler.GridSize);

            
            GenerateRandomPosition();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void GenerateRandomPosition()
        {
            while (true)
            {
                var randomX = Random.Range(GameHandler.Board.WidthSt, GameHandler.Board.WidthEd + 1) * GameHandler.GridSize;
                var randomY = Random.Range(GameHandler.Board.HeightSt, GameHandler.Board.HeightEd + 1) * GameHandler.GridSize;


                var position = new Vector3(randomX, randomY, 0f);
                _rectTransform.anchoredPosition = position;


                var key = ((int)(position.x / _gridSize), (int)(position.y / _gridSize));
                try
                {
                    if (GameHandler.Board.HashTable[key]) continue;
                }
                catch (Exception)
                {
                    //ignore
                }

                if (!GameHandler.Snake.AStarSearchChecked()) continue;

                break;
            }
        }

        public Vector3? GetPosition()
        {
            if (_rectTransform)
                return _rectTransform.anchoredPosition;
            return null;
        }
        
        public void SetPosition(Vector3 newPos)
        {
            _rectTransform.anchoredPosition = newPos;
        }
    }
}
