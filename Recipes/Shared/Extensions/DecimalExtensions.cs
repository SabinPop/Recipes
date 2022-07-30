namespace Recipes.Shared.Extensions
{
    public static class DecimalExtensions
    {
        public static bool IsLessThan(this decimal left, decimal right)
        {
            if(left.CompareTo(right) < 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsEqualTo(this decimal left, decimal right)
        {
            if (left.CompareTo(right) == 0)
            {
                return true;
            }
            return false;
        }

        public static bool IsGreaterThan(this decimal left, decimal right)
        {
            if (left.CompareTo(right) > 0)
            {
                return true;
            }
            return false;
        }
    }
}
