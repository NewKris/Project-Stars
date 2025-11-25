using System;
using UnityEngine;

namespace Werehorse.Runtime.Hangar {
    public class HangarController : MonoBehaviour {
        private void Awake() {
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
