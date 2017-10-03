using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GreenFox;
using WandererEngine;

namespace WandererEngine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game Game;
        FoxDraw FoxDraw;

        public MainWindow()
        {
            InitializeComponent();
            FoxDraw = new FoxDraw(canvas);
            Game = new Game();
            RefreshGameArea();
        }

        private void RefreshGameArea()
        {
            Area area = Game.Area;
            int XPos;
            int YPos;
            string floor = "./floor.png";
            string wall = "./wall.png";
            for (int i = 0; i < area.Count; i++)
            {
                XPos = area.TILE_SIZE * (i % area.NUMBER_OF_TILES_X);
                YPos = area.TILE_SIZE * (i / area.NUMBER_OF_TILES_X);
                FoxDraw.AddImage(area[i].IsWalkable? floor:wall,  XPos, YPos);
                //MessageBox.Show($"{i} {XPos} {YPos}");
            }
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                MessageBox.Show("To the left!");
            }

            if (e.Key == Key.Right)
            {
                MessageBox.Show("To the right!");
            }

            if (e.Key == Key.Up)
            {
                MessageBox.Show("Up!");
            }

            if (e.Key == Key.Down)
            {
                MessageBox.Show("Down!");
            }
        }
    }
}