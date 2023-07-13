using System;
using UnityEngine;
using Random = UnityEngine.Random;


namespace _Script
{
    public class Poison : MonoBehaviour
    {
        public GameHandler GameHandler;
        
        private float _gridSize = 0;

        private RectTransform _rectTransform;

        public int PoisonPower = 2;
        private int _currentPow = 0;

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

                var oldFood = GameHandler.Food.GetPosition();

                if (oldFood != null)
                {
                    GameHandler.Food.SetPosition(position);
                    if (!GameHandler.Snake.AStarSearchChecked())
                    {
                        GameHandler.Food.SetPosition((Vector3)oldFood);
                        continue;
                    }

                    GameHandler.Food.SetPosition((Vector3)oldFood);
                }

                break;
            }
        }

        public Vector3 GetPosition()
        {
            return _rectTransform.anchoredPosition;
        }

        public bool CanRemoveBody()
        {
            _currentPow++;
            if (_currentPow < PoisonPower)
            {
                return false;
            }
            _currentPow -= PoisonPower;
            return true;
            
        }
    }
}