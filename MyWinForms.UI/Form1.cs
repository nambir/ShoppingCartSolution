namespace MyWinForms.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void method1()
        {
            button1.Bounds = new Rectangle(10, 10, 50, 60);
            textBox1.Dock = DockStyle.Top;
            textBox1.Anchor = AnchorStyles.Right;
        }
    }
}
