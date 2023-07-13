
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Script
{
    public class Box : MonoBehaviour
    {
        public GameHandler GameHandler;

        private RectTransform _rectTransform;
        public bool IsHavingShape;
        // Start is called before the first frame update
        void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        
        // Update is called once per frame
        void Update()
        {
            if (IsHavingShape) return;

            var randomNumber = Random.Range(0, 5);
    
            switch (randomNumber)
            {
                case 0:
                    var transform1 = GameHandler.ShapeManager.Shape.transform;
                    var shape = Instantiate(GameHandler.ShapeManager.Shape, transform1.position, transform1.rotation);
                    
                    shape.transform.SetParent(GameHandler.Canvas.transform, false);
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    var rectTransform = shape.GetComponent<RectTransform>();
                    var position = _rectTransform.anchoredPosition;
                    rectTransform.anchoredPosition = new Vector3(position.x, position.y, 0f);
                    
                    shape.SetBox(this);
                    break;
                case 1: 
                    transform1 = GameHandler.ShapeManager.LShape.transform;
                    shape = Instantiate(GameHandler.ShapeManager.LShape, transform1.position, transform1.rotation);
                    
                    shape.transform.SetParent(GameHandler.Canvas.transform, false);
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    rectTransform = shape.GetComponent<RectTransform>();
                    position = _rectTransform.anchoredPosition;
                    rectTransform.anchoredPosition = new Vector3(position.x, position.y, 0f);
                    
                    shape.SetBox(this);
                    break;
                case 2: 
                    transform1 = GameHandler.ShapeManager.LlShape.transform;
                    shape = Instantiate(GameHandler.ShapeManager.LlShape, transform1.position, transform1.rotation);
                    
                    shape.transform.SetParent(GameHandler.Canvas.transform, false);
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    rectTransform = shape.GetComponent<RectTransform>();
                    position = _rectTransform.anchoredPosition;
                    rectTransform.anchoredPosition = new Vector3(position.x, position.y, 0f);
                    
                    shape.SetBox(this);
                    break;
                case 3: 
                    transform1 = GameHandler.ShapeManager.HorizontalShape.transform;
                    shape = Instantiate(GameHandler.ShapeManager.HorizontalShape, transform1.position, transform1.rotation);
                    
                    shape.transform.SetParent(GameHandler.Canvas.transform, false);
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    rectTransform = shape.GetComponent<RectTransform>();
                    position = _rectTransform.anchoredPosition;
                    rectTransform.anchoredPosition = new Vector3(position.x, position.y, 0f);
                    
                    shape.SetBox(this);
                    break;
                case 4: 
                    transform1 = GameHandler.ShapeManager.PointShape.transform;
                    shape = Instantiate(GameHandler.ShapeManager.PointShape, transform1.position, transform1.rotation);
                    
                    shape.transform.SetParent(GameHandler.Canvas.transform, false);
                    // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                    rectTransform = shape.GetComponent<RectTransform>();
                    position = _rectTransform.anchoredPosition;
                    rectTransform.anchoredPosition = new Vector3(position.x, position.y, 0f);
                    
                    shape.SetBox(this);
                    break;
            }

            IsHavingShape = true;


        }

    }
}