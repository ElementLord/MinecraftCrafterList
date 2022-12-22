using System;
using System.Windows.Media.Imaging;

namespace MinecraftCrafterList.Miscellaneous
{
    public class ImageSet
    {
        public static readonly BitmapImage MissingImage = new BitmapImage(new Uri("../../Images/Items/Missing Image.png", UriKind.Relative));

        public static BitmapImage LoadRelativeImage(string path)
        {
            BitmapImage image;

            try
            {
                image = new BitmapImage(new Uri(path, UriKind.Relative));
            }
            catch (Exception) //e
            {
                image = MissingImage;
            }

            return image;
        }

        public static BitmapImage LoadImage(string path)
        {
            BitmapImage image;

            try
            {
                image = new BitmapImage(new Uri(path));
            }
            catch (Exception) //e
            {
                image = MissingImage;
            }

            return image;
        }
    }
}
