using System;
using System.Drawing;


namespace eyeProject2
{
    class Items
    {
        private Image logo = null;

        // Set icon to menu item

        public Items(Image img)
        {
            this.logo = img;
        }

        // Return icon for menu item

        public Image getLogo()
        {
            return logo;
        }
    }
}