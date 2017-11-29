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
        Map map;

        public GameScene(IntPtr handle) : base(handle)
        {
            map = new Map(4800, 1000);
            Scene.Size = new CGSize(map.Width, map.Height);

            Camera = new SKCameraNode()
            {
                Position = new CGPoint(800, 500)
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
            // TODO: move to map factory
            map.Enemies.Add(EnemySprite.CreateEnemyAt(new CGPoint(800, 500)));
            map.Platforms.Add(PlatformSprite.CreatePlatformAt(PlatformType.Short, new CGPoint(800, 250)));
            map.Hero = HeroSprite.CreateHeroAt(new CGPoint(Frame.GetMidX() * 0.25, Frame.GetMidY()));
            map.Platforms.Add(PlatformSprite.CreatePlatformAt(PlatformType.Medium, new CGPoint(Frame.GetMidX() * 0.25, Frame.GetMidY() * 0.25)));


            DrawBackground();
            foreach (var enemy in map.Enemies)
            {
                AddChild(enemy);
            }
            foreach (var platform in map.Platforms)
            {
                AddChild(platform);
            }
            AddChild(map.Hero);

        }

        [Export("didBeginContact:")]
        public void DidBeginContact(SKPhysicsContact contact)
        {
            // Initialize variables for bodies
            SKPhysicsBody firstBody, secondBody;

            // Detect smaller object
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
                map.Hero.ResetJumps();
            }

            // Handling collision with a hero and an enemy
            if ((firstBody.CategoryBitMask & CollisionCategory.Hero) != 0 &&
                (secondBody.CategoryBitMask & CollisionCategory.Enemy) != 0)
            {
                secondBody.Node.RemoveFromParent();
                map.Enemies.Remove((EnemySprite)secondBody.Node);
            }
        }



        void DrawBackground() {

            // TODO: optimize background rendering

            SKSpriteNode background = new SKSpriteNode();
            for (int i = 0; i < map.Width; i += 1600)
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
            AddChild(EnemySprite.CreateEnemyAt(theEvent.LocationInNode(this)));
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
                    map.Hero.Jump();
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
                map.Hero.MoveLeft();
            if (_rightKeyPressed)
                map.Hero.MoveRight();
        }

        #endregion

        #region Game Condition

        bool IsEndGameCondition()
        {
            return map.Enemies.Count == 0 || map.Hero.Position.Y < 0;
        }

        #endregion

        public override void Update(double currentTime)
        {
            DoHeroMovement();

            // End game condititon
            if(IsEndGameCondition())
            {
                var menuScene = FromFile<MenuScene>("GameScenes/MenuScene");
                menuScene.ScaleMode = SKSceneScaleMode.ResizeFill;
                View.PresentScene(menuScene);
            }
            //Camera.Position = new CGPoint(map.Hero.Position.X, Camera.Position.Y);
        }

    }
}
