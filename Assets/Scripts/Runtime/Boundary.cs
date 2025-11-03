using System;
using UnityEngine;

namespace NewKris.Runtime {
    public class Boundary : MonoBehaviour {
        public Vector3 MinCorner => transform.position - transform.localScale * 0.5f;
        public Vector3 MaxCorner => transform.position + transform.localScale * 0.5f;
        
        private void Start() {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(transform.position, transform.localScale);
        }
    }
}
