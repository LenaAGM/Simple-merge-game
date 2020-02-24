using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace simplemergegame
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "IntVariable", order = 51)]
    public class IntVariable : ScriptableObject
    {
        public int Value;
    }
}