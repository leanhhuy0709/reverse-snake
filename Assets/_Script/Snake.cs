using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Script
{
    public class AStarState
    {
        public Vector2 Current;
        public Vector2 Goal;
        public List<Vector2> MoveList = new();

        public void Initialization(Vector2 current, Vector2 goal, List<Vector2> moveList)
        {
            Current = current;
            Goal = goal;
            MoveList = moveList.GetRange(0, moveList.Count);
        }

        public int Value()
        {
            return (int)(MoveList.Count + Math.Abs(Goal.x - Current.x) + Math.Abs(Goal.y - Current.y));
        }
            
    }
    
    public class AStarStateComparer : IComparer<AStarState>
    {
        public int Compare(AStarState x, AStarState y)
        {
            if (y != null && x != null && x.Value() > y.Value())
                return -1;
            else if (y != null && x != null && x.Value() < y.Value())
                return 1;
            else return 0;
        }
    }
    public class Snake : MonoBehaviour
    {
        public GameHandler GameHandler;
        public Sprite Body;

        private float _gridSize;
        private readonly List<GameObject> _snakeBody = new();
        private readonly List<RectTransform> _rectTransforms = new();
        
        private float _nextUpdate;
        
        private Vector2 _direction;

        private readonly Dictionary<(int, int), bool> _hashTable = new Dictionary<(int, int), bool>();
        private readonly Dictionary<(int, int), bool> _visited = new Dictionary<(int, int), bool>();
        private List<Vector2> _moveList = new();

        public GameObject SnakeBody;
        
        // Start is called before the first frame update
        void Start()
        {
            _gridSize = GameHandler.GridSize;
            _direction = new Vector2(0, -1);
            
            
            for (var i = 0; i < 3; i++)
            {
                var transform1 = SnakeBody.transform;
                var snakeBodyPart = Instantiate(SnakeBody, transform1.position, transform1.rotation);
                
                var rectTransform = snakeBodyPart.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(GameHandler.GridSize, GameHandler.GridSize);
                
                snakeBodyPart.transform.SetParent(GameHandler.Canvas.transform, false);
                rectTransform.anchoredPosition = new Vector3(0, 0 - _gridSize * i, 0f);
                
                _rectTransforms.Add(rectTransform);
                _snakeBody.Add(snakeBodyPart);
            }
            
            var tmp = _snakeBody[0].GetComponent<Image>();
            tmp.color = Color.yellow;

            tmp.transform.SetAsLastSibling();

            _nextUpdate = Time.time;
        }

        // Update is called once per frame
        private void Update()
        {
            //HandleInput();
            
            if (!(Time.time >= _nextUpdate)) return;
            AutoFindFood();
            //Move();
            _nextUpdate = Time.time + GameHandler.Delay;
        }
    
        private void HandleInput()
        {
            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (Math.Abs(_direction.x - 1) > 0.01)
                    _direction = new Vector2(-1, 0);
            }
            else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if (Math.Abs(_direction.x - (-1)) > 0.01)
                    _direction = new Vector2(1, 0);
            }
            else if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                if (Math.Abs(_direction.y - (-1)) > 0.01)
                    _direction = new Vector2(0, 1);
            }
            else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                if (Math.Abs(_direction.y - 1) > 0.01)
                    _direction = new Vector2(0, -1);
            }
        }

        private void Move()
        {
            UpdateNear();
            var newPosition = new Vector2(_rectTransforms[0].anchoredPosition.x + _direction.x * _gridSize,
                _rectTransforms[0].anchoredPosition.y + _direction.y * _gridSize);
            try
            {
                var key = ((int)(newPosition.x / _gridSize), (int)(newPosition.y / _gridSize));
                if (GameHandler.Board.HashTable[key])
                {
                    _moveList.Clear();
                    return;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            for (var i = _snakeBody.Count - 1; i > 0; i--) _rectTransforms[i].anchoredPosition = _rectTransforms[i - 1].anchoredPosition;

            _rectTransforms[0].anchoredPosition = newPosition;
        }
        
        // ReSharper disable Unity.PerformanceAnalysis
        private void AddBody()
        {
            var transform1 = SnakeBody.transform;
            var snakeBodyPart = Instantiate(SnakeBody, transform1.position, transform1.rotation);
                
            var rectTransform = snakeBodyPart.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(GameHandler.GridSize, GameHandler.GridSize);
                
            snakeBodyPart.transform.SetParent(GameHandler.Canvas.transform, false);
            var length = _snakeBody.Count;
            rectTransform.anchoredPosition = _rectTransforms[length - 1].anchoredPosition;
                
            _rectTransforms.Add(rectTransform);
            _snakeBody.Add(snakeBodyPart);
            
            _snakeBody[0].transform.SetAsLastSibling();
        }

        private void RemoveBody()
        {
            var tmp = _snakeBody[^1];
            Destroy(tmp, 0.01f);
            
            _snakeBody.RemoveAt(_snakeBody.Count - 1);
            _rectTransforms.RemoveAt(_rectTransforms.Count - 1);
        }

        public void HandleEatFood()
        {
            AddBody();
            _moveList.Clear();
        }
        
        public void HandleEatPoison()
        {
            RemoveBody();
        }

        private void AutoFindFood()
        {
            if (_moveList.Count == 0)
            {
                AStarSearch();
            }
            else
            {
                _direction = _moveList[0];
                _moveList.RemoveAt(0);
                Move();
            }
        }

        private void UpdateNear()
        {
            
            var position = _rectTransforms[0].anchoredPosition;
            for (var i = -2; i <= 2; i++)
            {
                for (var j = -2; j <= 2; j++)
                {
                    var tmpX = i + (int)(position.x / _gridSize);
                    var tmpY = j + (int)(position.y / _gridSize);

                    try
                    {
                        _hashTable[(tmpX, tmpY)] = GameHandler.Board.HashTable[(tmpX, tmpY)];
                    }
                    catch (Exception)
                    {
                        _hashTable[(tmpX, tmpY)] = false;
                    }
                }
            }
            
            
        }

        public int GetLength()
        {
            return _snakeBody.Count;
        }
        
        public Vector3 GetPosition()
        {
            return _rectTransforms[0].anchoredPosition;
        }
        
        private void AStarSearch()
        {
            _visited.Clear();

            List<AStarState> aStarStates = new();
            var tmp = new AStarState();

            var position = GetPosition();
            var posX = (int)(position.x / _gridSize);
            var posY = (int)(position.y / _gridSize);
            
            var foodPosition = GameHandler.Food.GetPosition();
            int foodX = 0, foodY = 0;
            if (foodPosition != null)
            {
                foodX = (int)(foodPosition?.x / _gridSize);
                foodY = (int)(foodPosition?.y / _gridSize);
            }
            
            
            
            tmp.Initialization(new Vector2(posX, posY), 
                new Vector2(foodX, foodY), new List<Vector2>());

            _visited[(posX, posY)] = true;
            aStarStates.Add(tmp);
            
            var directionArray = new Vector2[4];
            directionArray[0] = new Vector2(0, 1);
            directionArray[1] = new Vector2(0, -1);
            directionArray[2] = new Vector2(1, 0);
            directionArray[3] = new Vector2(-1, 0);

            while (aStarStates.Count > 0)
            {
                aStarStates.Sort(new AStarStateComparer());
                var currentState = aStarStates[^1];
                aStarStates.RemoveAt(aStarStates.Count - 1);
               

                var currPos = currentState.Current;
                
                for (var i = 0; i < 4; i++)
                {
                    var newPos = currPos + directionArray[i];
                    
                    try
                    {
                        if (_hashTable[((int)newPos.x, (int)newPos.y)]
                           ) continue;
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                    
                    
                    try
                    {
                        if (_visited[((int)newPos.x, (int)newPos.y)]) continue;
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                    _visited[((int)newPos.x, (int)newPos.y)] = true;
                    
                    if (currentState.Goal == newPos)
                    {
                        _moveList = currentState.MoveList.GetRange(0, currentState.MoveList.Count);
                        _moveList.Add(directionArray[i]);
                        
                        return;
                    }
                    else
                    {
                        var newState = new AStarState();
                        newState.Initialization(new Vector2(newPos.x, newPos.y), 
                            new Vector2(foodX, foodY), currentState.MoveList);
                        newState.MoveList.Add(directionArray[i]);
                        aStarStates.Add(newState);
                    }
                    
                    
                }
            }
        }
        
        public bool AStarSearchChecked()
        {
            _visited.Clear();

            List<AStarState> aStarStates = new();
            var tmp = new AStarState();

            var position = GetPosition();
            var posX = (int)(position.x / _gridSize);
            var posY = (int)(position.y / _gridSize);
            
            var foodPosition = GameHandler.Food.GetPosition();
            
            int foodX = 0, foodY = 0;
            if (foodPosition != null)
            {
                foodX = (int)(foodPosition?.x / _gridSize);
                foodY = (int)(foodPosition?.y / _gridSize);
            }
            
            tmp.Initialization(new Vector2(posX, posY), 
                new Vector2(foodX, foodY), new List<Vector2>());

            _visited[(posX, posY)] = true;
            aStarStates.Add(tmp);
            
            var directionArray = new Vector2[4];
            directionArray[0] = new Vector2(0, 1);
            directionArray[1] = new Vector2(0, -1);
            directionArray[2] = new Vector2(1, 0);
            directionArray[3] = new Vector2(-1, 0);

            while (aStarStates.Count > 0)
            {
                aStarStates.Sort(new AStarStateComparer());
                var currentState = aStarStates[^1];
                aStarStates.RemoveAt(aStarStates.Count - 1);
               

                var currPos = currentState.Current;
                
                for (var i = 0; i < 4; i++)
                {
                    var newPos = currPos + directionArray[i];
                    
                    try
                    {
                        if (GameHandler.Board.HashTable[((int)newPos.x, (int)newPos.y)]
                           ) continue;
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                    
                    
                    try
                    {
                        if (_visited[((int)newPos.x, (int)newPos.y)]) continue;
                    }
                    catch (Exception)
                    {
                        //ignore
                    }
                    _visited[((int)newPos.x, (int)newPos.y)] = true;
                    
                    if (currentState.Goal == newPos)
                    {
                        return true;
                    }
                    else
                    {
                        var newState = new AStarState();
                        newState.Initialization(new Vector2(newPos.x, newPos.y), 
                            new Vector2(foodX, foodY), currentState.MoveList);
                        newState.MoveList.Add(directionArray[i]);
                        aStarStates.Add(newState);
                    }
                    
                    
                }
            }

            return false;
        }

        public Vector3 GetBody(int index)
        {
            return _rectTransforms[index].anchoredPosition;
        }
        
    }
}
