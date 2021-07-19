using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MinecraftCrafterList.Miscellaneous
{
    class ImageSet
    {
        public readonly static BitmapImage MissingImage = new BitmapImage(new Uri("../../Images/Items/Missing Image.png", UriKind.Relative));

        public static BitmapImage LoadRelativeImage(string path)
        {
            BitmapImage image;
            
            try
            {
                /*ImageDisplay.Source*/ 
                image = new BitmapImage(new Uri(path, UriKind.Relative));//
            }
            catch (Exception) //e
            {
                /*ImageDisplay.Source*/
                image = MissingImage;
            }

            return image;
        }

        public static BitmapImage LoadImage(string path)
        {
            BitmapImage image;

            try
            {
                /*ImageDisplay.Source*/
                image = new BitmapImage(new Uri(path));//
            }
            catch (Exception) //e
            {
                /*ImageDisplay.Source*/
                image = MissingImage;
            }

            return image;
        }
    }
}
