#if UNITY_EDITOR

using System;
using UnityEngine;

namespace RuntimeUnitTestToolkit.Editor
{
    // functional declarative construction like flutter.

    internal interface IBuilder
    {
        GameObject GameObject { get; }
        T GetComponent<T>();
    }

    internal class Builder<T> : IBuilder
        where T : Component
    {
        public T Component1 { get; private set; }
        public GameObject GameObject { get; private set; }

        public Transform Transform { get { return GameObject.transform; } }
        public RectTransform RectTransform { get { return GameObject.GetComponent<RectTransform>(); } }

        public Action<GameObject> SetTarget
        {
            set
            {
                value(this.GameObject);
            }
        }


        public IBuilder Child
        {
            set
            {
                value.GameObject.transform.SetParent(GameObject.transform);
            }
        }

        public IBuilder[] Children
        {
            set
            {
                foreach (var item in value)
                {
                    item.GameObject.transform.SetParent(GameObject.transform);
                }
            }
        }

        public Builder(string name)
        {
            this.GameObject = new GameObject(name);
            this.Component1 = GameObject.AddComponent<T>();
        }

        public Builder(string name, out T referenceSelf) // out primary reference.
        {
            this.GameObject = new GameObject(name);
            this.Component1 = GameObject.AddComponent<T>();
            referenceSelf = this.Component1;
        }

        public TComponent GetComponent<TComponent>()
        {
            return this.GameObject.GetComponent<TComponent>();
        }
    }

    internal class Builder<T1, T2> : Builder<T1>
        where T1 : Component
        where T2 : Component
    {
        public T2 Component2 { get; private set; }

        public Builder(string name)
            : base(name)
        {
            this.Component2 = GameObject.AddComponent<T2>();
        }

        public Builder(string name, out T1 referenceSelf)
            : base(name, out referenceSelf)
        {
            this.Component2 = GameObject.AddComponent<T2>();
        }
    }

    internal class Builder<T1, T2, T3> : Builder<T1, T2>
        where T1 : Component
        where T2 : Component
        where T3 : Component
    {
        public T3 Component3 { get; private set; }

        public Builder(string name)
            : base(name)
        {
            this.Component3 = GameObject.AddComponent<T3>();
        }

        public Builder(string name, out T1 referenceSelf)
            : base(name, out referenceSelf)
        {
            this.Component3 = GameObject.AddComponent<T3>();
        }
    }

    internal class Builder<T1, T2, T3, T4> : Builder<T1, T2, T3>
        where T1 : Component
        where T2 : Component
        where T3 : Component
        where T4 : Component
    {
        public T4 Component4 { get; private set; }

        public Builder(string name)
            : base(name)
        {
            this.Component4 = GameObject.AddComponent<T4>();
        }

        public Builder(string name, out T1 referenceSelf)
            : base(name, out referenceSelf)
        {
            this.Component4 = GameObject.AddComponent<T4>();
        }
    }
}

#endif