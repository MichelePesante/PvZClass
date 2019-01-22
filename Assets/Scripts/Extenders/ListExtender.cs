using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace PvZ.Helpers {

    public static class ListExtender {

        public static T GetRandomElement<T>(this List<T> _this) {
            return _this[Random.Range(0, _this.Count)];
        }

    }

}

