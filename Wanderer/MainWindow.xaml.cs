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
            for (int i = 0; i < Game.Area.Count; i++)
            {
                FoxDraw.AddImage(Game.Area[i].IsWalkable ? "./floor.png" : "./wall.png",
                    Game.Area.TILE_SIZE * Game.Area.XPosition(i),
                    Game.Area.TILE_SIZE * Game.Area.YPosition(i));
            }
            labelAreaInfo.Content = Game.Area.ToString();
        }

        private void DisplayMonsters()
        {
            foreach (Monster monster in Game.Area.MovingObjects.Monsters.Where(monster => monster.IsAlive))
            {
                FoxDraw.AddImage(MonsterSprite[monster.GetType()],
                    monster.XPosition * Game.Area.TILE_SIZE,
                    monster.YPosition * Game.Area.TILE_SIZE);
            }

            if (Game.Area.ActualOpponent != null)
            {
                labelMonster.Content = Game.Area.ActualOpponent.ToString();
            }
        }

        private void DisplayHero()
        {
            FoxDraw.AddImage(HeroSprite[Game.Area.MovingObjects.Hero.LookingDirection],
                Game.Area.MovingObjects.Hero.XPosition * Game.Area.TILE_SIZE,
                Game.Area.MovingObjects.Hero.YPosition * Game.Area.TILE_SIZE);
            labelHero.Content = Game.Area.MovingObjects.Hero.ToString();
        }

        private void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (KeyBoardReaction.ContainsKey(e.Key) && Game.HeroIsAlive)
            {
                if (e.Key == Key.Space)
                {
                    Game.Area.Battle();
                }
                else
                {
                    Game.Area.TryToMoveHero(KeyBoardReaction[e.Key]);
                }
                if (Game.Area.IsOver)
                {
                    Game.GetNewArea();
                }                
                RefreshGameArea();
            }
        }
    }
}