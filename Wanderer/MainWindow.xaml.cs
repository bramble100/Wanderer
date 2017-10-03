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
        Area area;

        public MainWindow()
        {
            InitializeComponent();
            FoxDraw = new FoxDraw(canvas);
            Game = new Game();
            RefreshGameArea();
        }

        private void RefreshGameArea()
        {
            DisplayDungeon();
            DisplayHero();
        }

        private void DisplayHero()
        {
            area = Game.Area;
            int XPos = area.movingObjects.hero.XPosition* area.TILE_SIZE;
            int YPos = area.movingObjects.hero.YPosition * area.TILE_SIZE;
            string hero = "./hero-down.png";

            FoxDraw.AddImage(hero, XPos, YPos);
        }

        private void DisplayDungeon()
        {
            area = Game.Area;
            string floor = "./floor.png";
            string wall = "./wall.png";

            for (int i = 0; i < area.Count; i++)
            {
                FoxDraw.AddImage(area[i].IsWalkable ? floor : wall, 
                    area.TILE_SIZE * area.XPosition(i),
                    area.TILE_SIZE * area.YPosition(i));
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