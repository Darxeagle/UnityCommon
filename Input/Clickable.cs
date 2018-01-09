using Assets.Common.Scripts.Events;
using Assets.Common.Scripts.UnityDI;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Common.Scripts.Input
{
    public class Clickable : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
    {
        [Dependency] public IInputController InputController { set; private get; }

        public readonly EventWrap Clicked = new EventWrap();
        public readonly EventWrap MouseDown = new EventWrap();
        public readonly EventWrap MouseUp = new EventWrap();
        public readonly EventWrap<string> ClickedId = new EventWrap<string>();
        public readonly EventWrap<string> MouseDownId = new EventWrap<string>();
        public readonly EventWrap<string> MouseUpId = new EventWrap<string>();

        public string Id;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (InputController.InputIsEnabled)
            {
                Clicked.Dispatch();
                ClickedId.Dispatch(Id);
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (InputController.InputIsEnabled)
            {
                MouseDown.Dispatch();
                MouseDownId.Dispatch(Id);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (InputController.InputIsEnabled)
            {
                MouseUp.Dispatch();
                MouseUpId.Dispatch(Id);
            }
        }
    }
}
