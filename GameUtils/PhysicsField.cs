using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Common.Scripts.GameUtils
{
    public class PhysicsField
    {
        private Dictionary<string, PhysicsFieldObject> _objects;
        private GameObject _container;
        private string _id;

        public void AddObject(string id, PhysicsFieldObjectShape shape, Vector2 size, string tag)
        {
            if (_objects.ContainsKey(id)) throw new Exception("Object with this id already registered");

            var gameObject = CreateObject(id, shape, size, tag);
            _objects.Add(id, gameObject);
        }

        private PhysicsFieldObject CreateObject(string id, PhysicsFieldObjectShape shape, Vector2 size, string tag)
        {
            var fieldObject = new PhysicsFieldObject();
            fieldObject.Id = id;
            fieldObject.Tag = tag;
            fieldObject.Shape = shape;
            fieldObject.Size = size;
            fieldObject.FieldId = _id;
            fieldObject.Init();

            fieldObject.GameObject.transform.SetParent(_container.transform, false);

            return fieldObject;
        }
    }
}
