using UnityEngine;
using System.Collections;

namespace Gameplay.Pickupables
{
    public class Healthpack : MonoBehaviour, IPickupable
    {
        [SerializeField]
        private float _healPower = 10.0f; // сила исцеления

        [SerializeField]
        private float _speed = 5.0f; // скорость движения по игровому полю

        private PickUpItemType _itemType = PickUpItemType.Healthpack;

        public PickUpItemType ItemType => _itemType;
        public float HealingPower => _healPower;

        // перемещаем объект
        private void Update()
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
        }

        // при столкновении
        private void OnCollisionEnter2D(Collision2D other)
        {
            var puckuper = other.gameObject.GetComponent<IPickuper>(); // IPickuper может быть только корабль игрока

            if (puckuper != null)
            {
                puckuper.PickUp(this);
                Destroy(gameObject); //уничтожение объекта после столкновения с кораблем игрока
            }
        }
    }
}

