using UnityEngine;


namespace HackedDesign
{
    public class PlayerPreferences : MonoBehaviour
    {
        public float mouseSensitivity = 3.0f;

        public static PlayerPreferences Instance { get; private set; }

        PlayerPreferences()
        {
            Instance = this;
        }        

        //public PlayerPreferences
    }
}