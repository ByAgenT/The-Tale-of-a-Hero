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
            PhysicsWorld.Gravity = new CGVector(0, (nfloat)(-0.1));

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


            DrawBackground();
            foreach(var enemy in map.Enemies)
            {
                AddChild(enemy);
            }
            foreach(var platform in map.Platforms)
            {
                AddChild(platform);
            }
            AddChild(map.Hero);

        }

        [Export("didBeginContact:")]
        public void DidBeginContact(SKPhysicsContact contact)
        {
            // TODO: handle collision of the nodes
            //throw new System.NotImplementedException();
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
            //simpleEnemy.PhysicsBody.ApplyForce(new CGVector(100, 0));
            //simpleEnemy.PhysicsBody.Velocity = new CGVector(100, 0);
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
            }
            // 124 - right
            // 123 - left
        }

        public override void KeyUp(NSEvent theEvent)
        {
            // Release variables for stop moving
            switch(theEvent.KeyCode)
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

        public override void Update(double currentTime)
        {
            // Called before each frame is rendered
            DoHeroMovement();
            //Camera.Position = new CGPoint(map.Hero.Position.X, Camera.Position.Y);
        }
    }
}
