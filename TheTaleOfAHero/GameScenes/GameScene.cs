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

        // TODO: remove magic numbers

        const string MAP_FILE = "Map.txt";

        Map GameMap { get; set; }


        public GameScene(IntPtr handle) : base(handle)
        {
            // Create map object from the file
            GameMap = Map.CreateMapFromFile(MAP_FILE);

            // Setup scene size to match map size
            Scene.Size = new CGSize(GameMap.Width, GameMap.Height);

            Camera = new SKCameraNode()
            {
                Position = new CGPoint(1600 / 2, 1000 / 2)
            };

            // Setup gravity
            PhysicsWorld.Gravity = new CGVector(0, -5);

            // Assign contact delegate to this object (impl. ISKPhysicsContactDelegate)
            PhysicsWorld.ContactDelegate = this;
        }

        public override void DidMoveToView(SKView view)
        {
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

        void UpdateCamera()
        {
            var heroPosition = GameMap.Hero.Position;
            int mapSegment = 0;
            for (int border = 1600; border <= GameMap.Width; border += 1600)
            {
                ++mapSegment;
                if (GameMap.Hero.Position.X < border && GameMap.Hero.Position.X > border - 1600)
                    break;
            }
            Camera.Position = new CGPoint(1600 * (mapSegment - 1) + 1600 / 2, 1000 / 2);
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
        }



        void DrawBackground() {

            // TODO: optimize background rendering

            SKSpriteNode background = new SKSpriteNode();
            for (int i = 0; i < GameMap.Width; i += 1600)
            {
                var bgpart = SKSpriteNode.FromImageNamed("GameStage.png");
                bgpart.AnchorPoint = new CGPoint(0, 0);
                bgpart.ZPosition = -1;
                bgpart.Size = new CGSize(1600, 1000);
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
                case 123:
                    _leftKeyPressed = true;
                    break;
                case 124:
                    _rightKeyPressed = true;
                    break;
                case 126:
                    GameMap.Hero.Jump();
                    break;
            }
            // 124 - right
            // 123 - left
            // 126 - jump
        }

        public override void KeyUp(NSEvent theEvent)
        {
            // Release variables for stop moving
            switch (theEvent.KeyCode)
            {
                case 123:
                    _leftKeyPressed = false;
                    break;
                case 124:
                    _rightKeyPressed = false;
                    break;
            }
        }

        #endregion

        #region Movement

        public void DoHeroMovement()
        {
            if (_leftKeyPressed)
                GameMap.Hero.MoveLeft();
            if (_rightKeyPressed)
                GameMap.Hero.MoveRight();
        }

        #endregion

        #region Game Condition

        bool IsEndGameCondition()
        {
            return GameMap.Enemies.Count == 0 || GameMap.Hero.Position.Y < 0;
        }

        #endregion

        public override void Update(double currentTime)
        {
            DoHeroMovement();
            UpdateCamera();
            // End game condititon
            if(IsEndGameCondition())
            {
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
