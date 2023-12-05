using UnityEngine;

namespace HackedDesign
{
    public class DeadState : IState
    {
        public bool Playing => false;

        private PlayerController player;

        public DeadState(PlayerController player)
        {
            this.player = player;
        }

        public void Begin()
        {
            Debug.Log("Dead");
        }

        public void End()
        {
            
        }

        public void FixedUpdate()
        {
            
        }

        public void Select()
        {
            
        }

        public void Start()
        {
            
        }

        public void Update()
        {
            //Game.Instance.SetPlaying();
        }
    }
}