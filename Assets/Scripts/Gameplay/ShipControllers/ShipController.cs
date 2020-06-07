using Gameplay.ShipSystems;
using Gameplay.Spaceships;
using UnityEngine;

namespace Gameplay.ShipControllers
{
    public abstract class ShipController : MonoBehaviour
    {

        private ISpaceship _spaceship;

        protected Vector3 _initialPosition;

        public virtual void Init(ISpaceship spaceship)
        {
            _spaceship = spaceship;
            _initialPosition = transform.position;
        }


        private void Update()
        {
            ProcessHandling(_spaceship.MovementSystem);
            ProcessFire(_spaceship.WeaponSystem);
        }


        protected abstract void ProcessHandling(MovementSystem movementSystem);
        protected abstract void ProcessFire(WeaponSystem fireSystem);
    }
}
