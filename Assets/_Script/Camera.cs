using UnityEngine;
using UnityEngine.Serialization;

namespace _Script
{

    public class Camera : MonoBehaviour
    {
        public GameHandler GameHandler;
        [FormerlySerializedAs("isFollowing")] public bool IsFollowing;
        
        private Snake _snake;
        private float _nextUpdate;
        

        // Start is called before the first frame update
        void Start()
        {
            _snake = GameHandler.Snake;
        }

        // Update is called once per frame
        void Update()
        {
            if (!IsFollowing) return;
            if (!(Time.time >= _nextUpdate)) return;
            var transform1 = transform;
            var position = transform1.position;
            position.x = _snake.transform.position.x;
            transform1.position = position;
            _nextUpdate = Time.time + GameHandler.Delay;
        }   
    }
}