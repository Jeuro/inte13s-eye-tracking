using System;
using System.Drawing;


namespace eyeProject2 {
    class Items {
        private Image logo = null;

        public Items(Image img) {
            this.logo = img;
        }

        public Image getLogo() {
            return logo;
        }
    }
}
