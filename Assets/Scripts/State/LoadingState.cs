using UnityEngine;

namespace HackedDesign
{
    public class LoadingState : IState
    {
        public bool Playing => false;

        private Level level;

        public LoadingState(Level level)
        {
            this.level = level;
        }

        public void Begin()
        {
            level.Generate(10, 10, 0, 10);
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