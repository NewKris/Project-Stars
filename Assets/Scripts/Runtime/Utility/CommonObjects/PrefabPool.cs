using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NewKris.Runtime.Utility.CommonObjects {
    public class PrefabPool {
        private readonly int _capacity;
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly HashSet<GameObject> _pool;
        
        public PrefabPool(GameObject prefab, Transform parent, int capacity = 100, int initialSize = 10) {
            _prefab = prefab;
            _parent = parent;
            _capacity = capacity;
            _pool = new HashSet<GameObject>(initialSize);
        }

        public bool GetObject(out GameObject result) {
            GameObject firstAvailable = _pool.FirstOrDefault(x => x.activeSelf == false);
            
            if (firstAvailable) {
                result = firstAvailable;
                return true;
            }
            
            return CreateNewObject(out result);
        }

        private bool CreateNewObject(out GameObject result) {
            if (_pool.Count >= _capacity) {
                result = null;
                return false;
            }
            
            result = Object.Instantiate(_prefab, _parent);
            _pool.Add(result);
            
            return true;
        }
    }
}
