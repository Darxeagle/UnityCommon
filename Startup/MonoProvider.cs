using Assets.Common.Scripts.Events;
using UnityEngine;

namespace Assets.Common.Scripts
{
    public class MonoProvider : MonoBehaviour
    {
        public EventWrap UpdateEvent = new EventWrap();
        public EventWrap FixedUpdateEvent = new EventWrap();

        void Update()
        {
            UpdateEvent.Dispatch();
        }

        void FixedUpdate()
        {
            FixedUpdateEvent.Dispatch();
        }
    }
}
