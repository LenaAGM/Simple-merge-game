using System;
using UnityEngine;

namespace simplemergegame
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField]
        private InputController inputController;

        [SerializeField]
        private WorldManager worldManager;

        // Start is called before the first frame update
        void Start()
        {
            inputController.OnClickedButtonAddElement += HandleClickedButtonAddElement;
            inputController.OnDragCardEnd += HandleDragCardEnd;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void HandleClickedButtonAddElement(object sender, EventArgs e)
        {
            worldManager.AddElement();
        }

        private void HandleDragCardEnd(object sender, DragCardEndEventArgs e)
        {
            worldManager.UpgradeCard(e.oldX, e.oldY, e.x, e.y);
        }
    }
}