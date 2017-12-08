using Assets.Common.Scripts.Events;
using UnityEngine;

namespace Assets.Common.Scripts.GameUtils
{
    public enum PhysicsFieldObjectShape
    {
        Rectangle,
        Circle
    }

    public class PhysicsFieldObject
    {
        public EventWrap<PhysicsFieldObject, PhysicsFieldObject> CollisionEnter = new EventWrap<PhysicsFieldObject, PhysicsFieldObject>();
        public EventWrap<PhysicsFieldObject, PhysicsFieldObject> CollisionExit = new EventWrap<PhysicsFieldObject, PhysicsFieldObject>();
        public EventWrap<PhysicsFieldObject, PhysicsFieldObject> CollisionStay = new EventWrap<PhysicsFieldObject, PhysicsFieldObject>();

        public string Id { get; set; }
        public PhysicsFieldObjectShape Shape { get; set; }
        public Vector2 Size { get; set; }
        public string Tag { get; set; }
        public string FieldId { get; set; }

        public GameObject GameObject { get { return _gameObject; } }

        private GameObject _gameObject;
        private Collider2D _collider;
        private Rigidbody2D _rigidbody;

        public void Init()
        {
            _gameObject = new GameObject();
            _gameObject.tag = Tag;

            switch (Shape)
            {
                case PhysicsFieldObjectShape.Circle:
                    _collider = _gameObject.AddComponent<CircleCollider2D>();
                    (_collider as CircleCollider2D).radius = Size.x;
                    break;
                case PhysicsFieldObjectShape.Rectangle:
                    _collider = _gameObject.AddComponent<BoxCollider2D>();
                    (_collider as BoxCollider2D).size = Size;
                    break;
            }

            _rigidbody = _gameObject.AddComponent<Rigidbody2D>();
            _rigidbody.isKinematic = true;
        }

        public Vector2 Position
        {
            get { return _rigidbody.position; }
            set { _rigidbody.MovePosition(value);}
        }

        public float Rotation
        {
            get { return _rigidbody.rotation; }
            set { _rigidbody.MoveRotation(value); }
        }

        public Vector2 Velocity
        {
            get { return _rigidbody.velocity; }
            set { _rigidbody.velocity = value; }
        }

        void OnTriggerEnter(Collider other)
        {
            CheckObjectAndDisposeEvent(other, CollisionEnter);
        }
        void OnTriggerExit(Collider other)
        {
            CheckObjectAndDisposeEvent(other, CollisionExit);
        }
        void OnTriggerStay(Collider other)
        {
            CheckObjectAndDisposeEvent(other, CollisionStay);
        }

        private void CheckObjectAndDisposeEvent(Collider other, EventWrap<PhysicsFieldObject, PhysicsFieldObject> eventWrap)
        {
            var otherObject = other.gameObject.GetComponent<PhysicsFieldObject>();
            if (otherObject != null && otherObject.FieldId == FieldId)
            {
                eventWrap.Dispatch(this, otherObject);
            }
        }
    }
}
