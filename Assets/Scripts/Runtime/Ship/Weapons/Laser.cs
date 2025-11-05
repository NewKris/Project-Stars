using System;
using UnityEngine;

namespace NewKris.Runtime.Ship.Weapons {
    public class Laser : Weapon {
        public float minChargeTime;
        public float maxChargeTime;
        public float laserDuration = 2;
        public AudioClip chargeSound;
        public AudioClip shootSound;
        public GameObject laser;

        private bool _firing;
        private float _chargeStart;
        private float _shootStart;
        private AudioSource _audio;

        private bool FinishedCharging => (Time.time - _chargeStart) >= minChargeTime;
        private bool FinishedFiring => (Time.time - _shootStart) >= laserDuration;
        private float Now => Time.time;
        
        public override void BeginFire() {
            _chargeStart = Now;
            
            if (_firing) {
                return;
            }
            
            _audio.PlayOneShot(chargeSound);
        }
        
        public override void EndFire() {
            if (_firing) {
                return;
            }
            
            _audio.Stop();

            if (!FinishedCharging) {
                return;
            }
            
            _firing = true;
            _shootStart = Now;
            _audio.PlayOneShot(shootSound);
            laser.SetActive(true);
        }

        private void Awake() {
            _audio = GetComponent<AudioSource>();
        }

        private void Update() {
            if (_firing && FinishedFiring) {
                _firing = false;
                laser.SetActive(false);
            }
        }
    }
}
