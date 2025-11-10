using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NewKris.Runtime.Ui {
    public class MenuButton : MonoBehaviour {
        private void OnEnable() {
            GetComponent<EventTrigger>().enabled = GetComponent<Button>().interactable;
        }
    }
}
