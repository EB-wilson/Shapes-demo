using System;
using Shapes.Logic;
using Unity.VisualScripting;
using UnityEngine;

namespace Shapes.Components
{
    [RequireComponent(typeof(Transform))]
    public class Shooter : MonoBehaviour
    {
        public int flag;

        [NonSerialized] public Transform selfPos;

        void Start()
        {
            selfPos = transform;
        }
    }
}