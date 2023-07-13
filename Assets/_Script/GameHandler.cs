using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace _Script
{
    public class GameHandler : MonoBehaviour
    {

        public Board Board;
        public Snake Snake;
        public Food Food;
        public Poison Poison;
        public WallManager WallManager;
        public Camera Camera;
        public Canvas Canvas;
        public ShapeManager ShapeManager;
        
        public float GridSize;

        public float Delay;

        private float _nextUpdate;

        private float _initTime;
        private int _score;
        public TMP_Text ScoreText;
        public TMP_Text SnakeLengthText;
        
        // Start is called before the first frame update
        void Start()
        {
            _nextUpdate = Time.time + Delay;
            _initTime = Time.time;

        }

        // Update is called once per frame
        void Update()
        {
            _score = (int)(Time.time - _initTime);
            ScoreText.text = "Score: " + _score.ToString();
            SnakeLengthText.text = "SnakeLength: " + Snake.GetLength().ToString();

            if (Snake.GetLength() > 50)
            {
                PlayerPrefs.SetInt("Score", _score);
                SceneManager.LoadScene("GameOver");
                return;
            }
            
            if (_score >= 600)
            {
                SceneManager.LoadScene("YouWin");
                return;
            }

            if (!(Time.time >= _nextUpdate)) return;
            HandleSnakeEatFood();
            HandleSnakeEatPoison();
            EvaluateDelay();
            _nextUpdate = Time.time + Delay;
        }
        
        private void HandleSnakeEatFood()
        {
            if (Snake.GetPosition() != Food.GetPosition()) return;
            Snake.HandleEatFood();
            Food.GenerateRandomPosition();
        }
        
        private void HandleSnakeEatPoison()
        {
            if (Snake.GetPosition() != Poison.GetPosition()) return;
            if (Poison.CanRemoveBody())
                Snake.HandleEatPoison();
            Poison.GenerateRandomPosition();
        }

        private void EvaluateDelay()
        {
            Delay = 0.5f - (float) Math.Log(Snake.GetLength()) / 10.0f - (float) Math.Log(_score/50.0f + 1) / 50.0f;
            if (Delay < 0.05f) Delay = 0.05f;
        }
    }
}
