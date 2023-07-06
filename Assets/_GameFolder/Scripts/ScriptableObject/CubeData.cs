using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChainCube.ScriptableObjects
{
    [CreateAssetMenu(fileName ="CubeData",menuName ="Data/Cubes Data",order =1)]
    public class CubeData : ScriptableObject
    {
        public int[] numbers;
        public Color[] colors;
      
    }

}
