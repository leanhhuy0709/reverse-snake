using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Script
{
    public class Board : MonoBehaviour
    {
        public GameHandler GameHandler;
        public int GridWidth;
        public int GridHeight;

        private float _gridSize;

        public Wall Wall;
        
        public Sprite Sprite;

        public int HeightSt;
        public int HeightEd;
        public int WidthSt;
        public int WidthEd;
        
        public readonly Dictionary<(int, int), bool> HashTable = new Dictionary<(int, int), bool>();
        
        
        
        // Start is called before the first frame update
        private void Start()
        {
            _gridSize = GameHandler.GridSize;

            HeightSt = (-GridHeight / 2);
            HeightEd = GridHeight + HeightSt - 1;
            WidthSt = -GridWidth/2;
            WidthEd = GridWidth + WidthSt - 1;
            
            for (var row = HeightSt - 1; row <= HeightEd; row++)
            {
                var transform1 = Wall.transform;
                var wall = Instantiate(Wall, transform1.position, transform1.rotation);

                var rectTransform = wall.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(GameHandler.GridSize, GameHandler.GridSize);
                
                wall.transform.SetParent(GameHandler.Canvas.transform, false);

                rectTransform.anchoredPosition = new Vector3((WidthSt - 1) * _gridSize, row * _gridSize, 0f);
                
                HashTable[(WidthSt - 1, row)] = true;
            }
            for (var row = HeightSt - 1; row <= HeightEd; row++)
            {
                var transform1 = Wall.transform;
                var wall = Instantiate(Wall, transform1.position, transform1.rotation);

                var rectTransform = wall.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(GameHandler.GridSize, GameHandler.GridSize);
                
                wall.transform.SetParent(GameHandler.Canvas.transform, false);

                rectTransform.anchoredPosition = new Vector3((WidthEd + 1) * _gridSize, row * _gridSize, 0f);
                
                HashTable[(WidthEd + 1, row)] = true;
            }
            for (var col = WidthSt; col <= WidthEd; col++)
            {
                var transform1 = Wall.transform;
                var wall = Instantiate(Wall, transform1.position, transform1.rotation);

                var rectTransform = wall.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(GameHandler.GridSize, GameHandler.GridSize);
                
                wall.transform.SetParent(GameHandler.Canvas.transform, false);

                rectTransform.anchoredPosition = new Vector3(col * _gridSize, (HeightSt - 1) * _gridSize, 0f);
                
                HashTable[(col, HeightSt - 1)] = true;
            }
            for (var col = WidthSt - 1; col <= WidthEd + 1; col++)
            {
                var transform1 = Wall.transform;
                var wall = Instantiate(Wall, transform1.position, transform1.rotation);

                var rectTransform = wall.GetComponent<RectTransform>();
                rectTransform.sizeDelta = new Vector2(GameHandler.GridSize, GameHandler.GridSize);
                
                wall.transform.SetParent(GameHandler.Canvas.transform, false);
                rectTransform.anchoredPosition = new Vector3(col * _gridSize, (HeightEd + 1) * _gridSize, 0f);
                
                HashTable[(col, HeightEd + 1)] = true;
            }
            

            
        }

        // Update is called once per frame
        void Update()
        {
            //DrawGrid();
        }

        private void DrawGrid()
        {
            foreach (var key in HashTable.Keys)
            {
                var (x, y) = key;
                if (y < HeightSt || y > HeightEd || x < WidthSt || x > WidthEd) continue;
                if (HashTable[key])
                {
                    var transform1 = Wall.transform;
                    var wall = Instantiate(Wall, transform1.position, transform1.rotation);

                    var tmp = wall.GetComponent<Image>();
                    
                    tmp.color = Color.blue;

                    var rectTransform = wall.GetComponent<RectTransform>();
                    rectTransform.sizeDelta = new Vector2(GameHandler.GridSize, GameHandler.GridSize);
                
                    wall.transform.SetParent(GameHandler.Canvas.transform, false);
                    rectTransform.anchoredPosition = new Vector3((x + 1) * _gridSize, y * _gridSize, 0f);
                }
            }
        }
    }
}
