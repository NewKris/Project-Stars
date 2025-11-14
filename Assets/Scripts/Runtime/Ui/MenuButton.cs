using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Werehorse.Runtime.Ui {
    public class MenuButton : MonoBehaviour {
        private void OnEnable() {
            GetComponent<EventTrigger>().enabled = GetComponent<Button>().interactable;
        }
    }
}
