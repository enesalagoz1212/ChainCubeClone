using UnityEngine;

namespace ChainCube.ScriptableObjects
{
    [CreateAssetMenu(fileName ="CubeData",menuName ="Data/Cubes Data",order =1)]
    public class CubeData : ScriptableObject
    {
        public int number;
        public Color color;
    }


}