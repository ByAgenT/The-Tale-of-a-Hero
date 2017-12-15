using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;
using TheTaleOfAHero.Models;


namespace TheTaleOfAHero
{
    public class GameScene : SKScene, ISKPhysicsContactDelegate
    {

        // Scene constants
        const string MAP_FILE = "Map.txt";

        // Physics constants
        const int GRAVITY = -5;

        // Key constants
        const int LEFT_KEY = 123;
        const int RIGHT_KEY = 124;
        const int UP_KEY = 126;
        const int W_KEY = 13;
        const int A_KEY = 0;
        const int D_KEY = 2;


        bool _endgame;

        Map GameMap { get; set; }

        nfloat _windowWidth, _windowHeight;



        public GameScene(IntPtr handle) : base(handle)
        {
            // Create map object from the file
            GameMap = Map.CreateMapFromFile(MAP_FILE);

            // Setup scene size to match map size
            Scene.Size = new CGSize(GameMap.Width, GameMap.Height);

            // Setup gravity
            PhysicsWorld.Gravity = new CGVector(0, GRAVITY);

            // Assign contact delegate to this object (impl. ISKPhysicsContactDelegate)
            PhysicsWorld.ContactDelegate = this;
        }

        public override void DidMoveToView(SKView view)
        {
            _windowWidth = view.Window.Frame.Width;
            _windowHeight = view.Window.Frame.Height;

            // Initialize camera
            Camera = new SKCameraNode()
            {
                Position = new CGPoint(_windowWidth / 2, _windowHeight / 2)
            };

            RenderScene();
        }

        void RenderScene() {
            
            DrawBackground();
            foreach (var platform in GameMap.Platforms)
            {
                AddChild(platform);
            }
            foreach (var enemy in GameMap.Enemies)
            {
                AddChild(enemy);
            }
            AddChild(GameMap.Hero);

        }

        /// <summary>
        /// Updates the camera.
        /// </summary>
        void UpdateCamera()
        {
            var heroPosition = GameMap.Hero.Position;
            int mapSegment = 0;
            for (var border = _windowWidth; border <= GameMap.Width; border += _windowWidth)
            {
                ++mapSegment;
                if (GameMap.Hero.Position.X < border && GameMap.Hero.Position.X > border - _windowWidth)
                    break;
            }
            Camera.Position = new CGPoint(_windowWidth * (mapSegment - 1) + _windowWidth / 2, _windowHeight / 2);
        }

        [Export("didBeginContact:")]
        public void DidBeginContact(SKPhysicsContact contact)
        {
            // Initialize variables for bodies
            SKPhysicsBody firstBody, secondBody;

            // Detect bitmask
            if (contact.BodyA.CategoryBitMask < contact.BodyB.CategoryBitMask)
            {
                firstBody = contact.BodyA;
                secondBody = contact.BodyB;
            }
            else
            {
                firstBody = contact.BodyB;
                secondBody = contact.BodyA;
            }

            // Handling collision with a hero and a platform
            if ((firstBody.CategoryBitMask & CollisionCategory.Hero) != 0 && 
                (secondBody.CategoryBitMask & CollisionCategory.Platform) != 0)
            {
                GameMap.Hero.ResetJumps();
            }

            // Handling collision with a hero and an enemy
            if ((firstBody.CategoryBitMask & CollisionCategory.Hero) != 0 &&
                (secondBody.CategoryBitMask & CollisionCategory.Enemy) != 0)
            {
                //secondBody.Node.RemoveFromParent();
                //map.Enemies.Remove((EnemySprite)secondBody.Node);
            }

            // Handling collision with a enemy and a spell
            if ((firstBody.CategoryBitMask & CollisionCategory.Enemy) != 0 &&
                (secondBody.CategoryBitMask & CollisionCategory.Spell) != 0)
            {
                if(((ShotSprite)secondBody.Node).Type == SpellType.Hero)
                {
                    secondBody.Node.RemoveFromParent();
                    firstBody.Node.RemoveFromParent();
                    GameMap.Enemies.Remove((EnemySprite)firstBody.Node);    
                }
            }

            // Handling collision with a spell and a platform
            if((firstBody.CategoryBitMask & CollisionCategory.Spell) != 0 &&
               (secondBody.CategoryBitMask & CollisionCategory.Platform) != 0)
            {
                firstBody.Node.RemoveFromParent();
            }
            if((firstBody.CategoryBitMask & CollisionCategory.Hero) != 0 &&
               (secondBody.CategoryBitMask & CollisionCategory.Spell) != 0)
            {
                if(((ShotSprite)secondBody.Node).Type == SpellType.Enemy)
                {
                    SetEndGameCondition();
                }
            }

        }


        /// <summary>
        /// Draws scene background
        /// </summary>
        void DrawBackground() {

            // TODO: optimize background rendering

            SKSpriteNode background = new SKSpriteNode();
            for (nfloat i = 0; i < GameMap.Width; i += _windowWidth)
            {
                var bgpart = SKSpriteNode.FromImageNamed("GameStage.png");
                bgpart.AnchorPoint = new CGPoint(0, 0);
                bgpart.ZPosition = -1;
                bgpart.Size = new CGSize(_windowWidth, _windowHeight);
                bgpart.Position = new CGPoint(i, 0);
                AddChild(bgpart);
            }
        }

        public override void MouseDown(NSEvent theEvent)
        {
            GameMap.Hero.ShootSpell(theEvent.LocationInNode(this));
            /*var enemy = EnemySprite.CreateEnemyAt(theEvent.LocationInNode(this));
            map.Enemies.Add(enemy);
            AddChild(enemy);*/
        }


        #region Key Handling

        bool _leftKeyPressed, _rightKeyPressed;


        public override void KeyDown(NSEvent theEvent)
        {
            // Do something when key is pressed
            switch(theEvent.KeyCode) 
            {
                case LEFT_KEY:
                case A_KEY:
                    _leftKeyPressed = true;
                    break;
                case RIGHT_KEY:
                case D_KEY:
                    _rightKeyPressed = true;
                    break;
                case UP_KEY:
                case W_KEY:
                    GameMap.Hero.Jump();
                    break;
            }
        }

        public override void KeyUp(NSEvent theEvent)
        {
            // Release variables for stop moving
            switch (theEvent.KeyCode)
            {
                case LEFT_KEY:
                case A_KEY:
                    _leftKeyPressed = false;
                    break;
                case RIGHT_KEY:
                case D_KEY:
                    _rightKeyPressed = false;
                    break;
            }
        }

        #endregion

        #region Movement

        /// <summary>
        /// Handle the hero movement
        /// </summary>
        public void DoHeroMovement()
        {
            if (_leftKeyPressed)
                GameMap.Hero.MoveLeft();
            if (_rightKeyPressed)
                GameMap.Hero.MoveRight();
        }

        #endregion

        #region Game Conditions

        /// <summary>
        /// Return if the game is in the end condition .
        /// </summary>
        /// <returns><c>true</c>, if end game condition was occured, <c>false</c> otherwise.</returns>
        bool IsEndGameCondition()
        {
            return GameMap.Enemies.Count == 0 || GameMap.Hero.Position.Y < 0 || _endgame;
        }


        bool IsWin()
        {
            return GameMap.Enemies.Count == 0;
        }

        void SetEndGameCondition()
        {
            _endgame = true;
        }

        #endregion

        public override void Update(double currentTime)
        {
            DoHeroMovement();
            UpdateCamera();

            // End game condititon
            if(IsEndGameCondition())
            {
                if(IsWin())
                {
                    NSAlert alert = new NSAlert
                    {
                        MessageText = "You win!",
                        AlertStyle = NSAlertStyle.Informational
                    };
                    alert.RunModal();
                }
                else
                {
                    NSAlert alert = new NSAlert
                    {
                        MessageText = "You lose!",
                        AlertStyle = NSAlertStyle.Informational
                    };
                    alert.RunModal();
                }
                var menuScene = FromFile<MenuScene>("GameScenes/MenuScene");
                menuScene.ScaleMode = SKSceneScaleMode.ResizeFill;
                View.PresentScene(menuScene);
            }

            foreach(var enemy in GameMap.Enemies)
            {
                enemy.AttackIfCooldowned(GameMap.Hero.Position);
            }
            //Camera.Position = new CGPoint(map.Hero.Position.X, Camera.Position.Y);
        }

    }
}
