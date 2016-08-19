using EloBuddy;
using EloBuddy.SDK;

namespace JokerLib.Items
{
    public static class Consumable
    {
        public static Item Biscuit = new Item((int)ItemId.Total_Biscuit_of_Rejuvenation);
        public static Item Health = new Item((int)ItemId.Health_Potion);
        public static Item Refillable = new Item((int)ItemId.Refillable_Potion);
        public static Item Hunters = new Item((int)ItemId.Hunters_Potion);
        public static Item Corrupting = new Item((int)ItemId.Corrupting_Potion);
    }
}
