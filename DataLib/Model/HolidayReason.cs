using System.Windows.Media;

namespace DataLib.Model
{
    public class HolidayReason
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public HolidayColor Color { get; set; } = new HolidayColor();
    }

    public class HolidayColor
    {
        public byte A { get; set; }
        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        
        public Color Color
        {
            get
            {
                return Color.FromArgb(A, R, G, B);
            }
            set
            {
                A = value.A;
                R = value.R;
                G = value.G;
                B = value.B;
            }
        }

        public HolidayColor()
        :this(0,0,0) { }

        public HolidayColor(Color color)
        {
            Color = color;
        }

        public HolidayColor(byte r, byte g, byte b)
        {
            A = 255;
            R = r;
            G = g;
            B = b;
        }
    }
}
