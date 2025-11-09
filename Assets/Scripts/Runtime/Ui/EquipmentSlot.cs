using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace NewKris.Runtime.Ui {
    public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
        public Image image;
        public Sprite hoverSprite;
        public AudioClip hoverSound;

        private Sprite _defaultSprite;
        private AudioSource _audio;
        
        public void OnPointerEnter(PointerEventData eventData) {
            image.sprite = hoverSprite;
            _audio.PlayOneShot(hoverSound);
        }
        
        public void OnPointerExit(PointerEventData eventData) {
            image.sprite = _defaultSprite;
        }

        private void Awake() {
            _defaultSprite = image.sprite;
            _audio = GetComponent<AudioSource>();
        }
    }
}
