using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace _Script
{

    public class WallManager : MonoBehaviour
    {
        public GameHandler GameHandler;
        public Sprite Sprite;

        public Wall Wall;
        
        private List<Wall> _walls = new();
        private readonly List<SpriteRenderer> _wallsSpriteRenderer = new();
        private float _gridSize = 0;

        
        
        // Start is called before the first frame update
        private void Start()
        {
            _gridSize = GameHandler.GridSize;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}