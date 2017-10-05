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
        int StepsUntilMonstersMove=2;

        public Dictionary<Action, string> HeroSprite = new Dictionary<Action, string>
        {
            { Action.Up, "./hero-up.png" },
            { Action.Down, "./hero-down.png" },
            { Action.Left, "./hero-left.png" },
            { Action.Right, "./hero-right.png" }
        };

        public Dictionary<Key, Action> KeyBoardReaction = new Dictionary<Key, Action>
        {
            {Key.Up, Action.Up },
            {Key.Down, Action.Down },
            {Key.Left, Action.Left },
            {Key.Right, Action.Right },
            {Key.Space, Action.Battle }
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
            for (int i = 0; i < Game.Map.Count; i++)
            {
                FoxDraw.AddImage(Game.Map[i].IsWalkable ? "./floor.png" : "./wall.png",
                    Game.Map.TILE_SIZE * Game.Map.XPosition(i),
                    Game.Map.TILE_SIZE * Game.Map.YPosition(i));
            }
            labelAreaInfo.Content = Game.Map.ToString();
        }

        private void DisplayMonsters()
        {
            foreach (Monster monster in Game.Map.MovingObjects.Monsters.Where(monster => monster.IsAlive))
            {
                FoxDraw.AddImage(MonsterSprite[monster.GetType()],
                    monster.XPosition * Game.Map.TILE_SIZE,
                    monster.YPosition * Game.Map.TILE_SIZE);
            }

            if (Game.Map.ActualOpponent != null)
            {
                labelMonster.Content = Game.Map.ActualOpponent.ToString();
            }
        }

        private void DisplayHero()
        {
            FoxDraw.AddImage(HeroSprite[Game.Map.MovingObjects.Hero.LookingDirection],
                Game.Map.MovingObjects.Hero.XPosition * Game.Map.TILE_SIZE,
                Game.Map.MovingObjects.Hero.YPosition * Game.Map.TILE_SIZE);
            labelHero.Content = Game.Map.MovingObjects.Hero.ToString();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (KeyBoardReaction.ContainsKey(e.Key) && Game.HeroIsAlive)
            {
                if (e.Key == Key.Space)
                {
                    Game.Map.Battle();
                }
                else
                {
                    Game.Map.TryToMoveHero(KeyBoardReaction[e.Key]);
                }
                if (Game.Map.IsOver)
                {
                    Game.GetNewArea();
                }
                if (--StepsUntilMonstersMove == 0)
                {
                    Game.Map.MoveMonstersRandomly();
                    StepsUntilMonstersMove = 2;
                }
                RefreshGameArea();
            }
        }
    }
}