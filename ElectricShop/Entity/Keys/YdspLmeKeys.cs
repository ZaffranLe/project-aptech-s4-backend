
namespace ElectricShop.Entity.Keys
{
    public class YdspLmeKeys
    {
        public string FileLme { get; set; }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = FileLme.GetHashCode();
                result = (result * 397) ^ FileLme.GetHashCode();
                return result;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is YdspLmeKeys)
            {
                var item = obj as YdspLmeKeys;
                return FileLme == item.FileLme;
            }
            return false;
        }
    }
}