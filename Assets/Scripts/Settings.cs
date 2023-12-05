using System.Collections.Generic;
using UnityEngine;

namespace HackedDesign
{
    [CreateAssetMenu(fileName = "Settings", menuName = "State/Settings")]
    public class Settings : ScriptableObject
    {
        [Header("Game settings")]
        public bool skipIntro = false;
        public int maxLevels = 5;

        [Header("Player Settings")]
        public float moveSpeed = 10.0f;
        public float dashSpeed = 10.0f;
        public float jumpSpeed = 5f;
        public float groundDistance = 0.4f;
        public float gravityMultiplier = 2.5f;
        public float kiteMultiplier = 0.75f;
        public LayerMask groundMask;
        public float minYAngle = -85.0f;
        public float maxYAngle = 85.0f;
        public float deadY = 0.0f;

    }
}