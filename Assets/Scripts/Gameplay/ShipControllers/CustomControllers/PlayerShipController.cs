using Gameplay.ShipSystems;
using Gameplay.Helpers;
using UnityEngine;
using Gameplay.Spaceships;

namespace Gameplay.ShipControllers.CustomControllers
{
    public class PlayerShipController : ShipController
    {

        [SerializeField]
        private SpriteRenderer _sprite;

        private bool _isBoosted = false;

        public override void Init(ISpaceship spaceship)
        {
            base.Init(spaceship);
            PlayerSpaceship.Boosted += OnBoosted;
        }

        private void OnDisable()
        {
            PlayerSpaceship.Boosted -= OnBoosted;
        }

        protected override void ProcessHandling(MovementSystem movementSystem)
        {
            var direction = Input.GetAxis("Horizontal");
            if (GameAreaHelper.IsPlayerAllowedToMove(transform, _sprite.bounds, direction)) // проверка на возможность двигаться по оси X
            {
                movementSystem.LateralMovement(direction * Time.deltaTime);
            }
        }

        protected override void ProcessFire(WeaponSystem fireSystem)
        {
            fireSystem.SetBoosted(_isBoosted); // обновляем состояние флага буста для всех орудий
            if (Input.GetKey(KeyCode.Space))
            {
                fireSystem.TriggerFire();
            }
        }

        private void OnBoosted(bool value)
        {
            _isBoosted = value;
        }
    }
}
