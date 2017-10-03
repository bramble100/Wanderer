﻿using System;
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

        public Dictionary<Direction, string> HeroSprite = new Dictionary<Direction, string>
        {
            { Direction.Up, "./hero-up.png" },
            { Direction.Down, "./hero-down.png" },
            { Direction.Left, "./hero-left.png" },
            { Direction.Right, "./hero-right.png" }
        };

        public Dictionary<Key, Direction> KeyBoardReaction = new Dictionary<Key, Direction>
        {
            {Key.Up, Direction.Up },
            {Key.Down, Direction.Down },
            {Key.Left, Direction.Left},
            {Key.Right, Direction.Right }
        };

        public Dictionary<Type, String> MonsterSprite = new Dictionary<Type, String>
        {
            {typeof(MonsterBoss), "./boss.png"},
            {typeof(KeyHolderMonster), "./skeleton.png"},
            {typeof(Monster), "./skeleton.png"}
        };
        
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
            DisplayMonsters();
            DisplayHero();
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

        private void DisplayMonsters()
        {
            //MessageBox.Show($"Monsters: {area.movingObjects.Monsters.Count}");
            foreach (Monster monster in area.movingObjects.Monsters.Where(monster => monster.IsAlive))
            {
                int XPos = monster.XPosition * area.TILE_SIZE;
                int YPos = monster.YPosition * area.TILE_SIZE;

                FoxDraw.AddImage(MonsterSprite[monster.GetType()], XPos, YPos);
            }
        }

        private void DisplayHero() => FoxDraw.AddImage(HeroSprite[Game.Area.movingObjects.Hero.LookingDirection],
                Game.Area.movingObjects.Hero.XPosition * Game.Area.TILE_SIZE,
                Game.Area.movingObjects.Hero.YPosition * Game.Area.TILE_SIZE);

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (KeyBoardReaction.ContainsKey(e.Key))
            {
                area.TryToMoveHero(KeyBoardReaction[e.Key]);
                RefreshGameArea();
            }
        }
    }
}