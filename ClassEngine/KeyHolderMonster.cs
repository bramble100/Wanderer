namespace WandererEngine
{
    public class KeyHolderMonster : Monster
    {
        // only to register which monster has the key
        public KeyHolderMonster(int areaLevel, Dice dice) : base(areaLevel, dice)
        {
            InitalizeLevel(areaLevel);
            System.Console.WriteLine($"{GetType().Name} level: {Level}");
            InitalizePoints();
        }
    }
}