namespace Gameplay.Pickupables
{
    public interface IPickupable
    {
        PickUpItemType ItemType { get; }
    }

    public enum PickUpItemType
    {
        Healthpack,
        Booster
    }
}

