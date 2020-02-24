using System;
using UnityEngine;

namespace simplemergegame
{

    public class DragCardEndEventArgs
    {
        public int oldX;
        public int oldY;

        public int x;
        public int y;
    }

    public class InputController : MonoBehaviour
    {

        private const string LAYER_NAME_CARD = "Card";
        private const string LAYER_NAME_CURRENT_CARD = "CurrentCard";

        private Transform objectHit = null;

        private float oldX;
        private float oldY;

        public event EventHandler<EventArgs> OnClickedButtonAddElement = (sender, e) => { };
        public event EventHandler<DragCardEndEventArgs> OnDragCardEnd = (sender, e) => { };

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

                    if (hit.collider != null)
                    {
                        oldX = hit.collider.transform.position.x;
                        oldY = hit.collider.transform.position.y;
                        objectHit = hit.transform;
                        objectHit.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = LAYER_NAME_CURRENT_CARD;
                    }
                }
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    if (objectHit != null)
                    {
                        objectHit.position = (Vector2) Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                    }
                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended || Input.GetTouch(0).phase == TouchPhase.Canceled)
                {
                    if (objectHit != null)
                    {
                        objectHit.gameObject.GetComponent<SpriteRenderer>().sortingLayerName = LAYER_NAME_CARD;
                        Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                        int x = Mathf.RoundToInt(touchPos.x);
                        int y = Mathf.RoundToInt(touchPos.y);
                        DragCardEndEventArgs dragCardEndEventArgs = new DragCardEndEventArgs();
                        dragCardEndEventArgs.oldX = Mathf.RoundToInt(oldX);
                        dragCardEndEventArgs.oldY = Mathf.RoundToInt(oldY);
                        dragCardEndEventArgs.x = x;
                        dragCardEndEventArgs.y = y;
                        objectHit = null;
                        OnDragCardEnd(this, dragCardEndEventArgs);
                    }
                }
            }
        }

        public void ButtonAddElement()
        {
            OnClickedButtonAddElement(this, new EventArgs());
        }
    }
}