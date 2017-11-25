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

        SKSpriteNode simpleEnemy;
        SKSpriteNode simpleBlock;

        public GameScene(IntPtr handle) : base(handle)
        {
            Scene.Size = new CGSize(1600, 1000);

            PhysicsWorld.Gravity = new CGVector(0, (nfloat)(-0.1));
            PhysicsWorld.ContactDelegate = this;

            // TODO: make GameScene size as the map size and implement camera
        }

        public override void DidMoveToView(SKView view)
        {
            RenderScene();
        }

        void RenderScene() {

            simpleEnemy = EnemySprite.CreateEnemyAt("simple", new CGPoint(this.Frame.GetMidX(), this.Frame.GetMidY()));
            simpleBlock = PlatformSprite.CreatePlatformAt(PlatformType.Short, new CGPoint(Frame.GetMidX(), Frame.GetMidY() * 0.5));


            AddChild(GetBackground());
            AddChild(simpleEnemy);
            AddChild(simpleBlock);
        }

        [Export("didBeginContact:")]
        public void DidBeginContact(SKPhysicsContact contact)
        {
            // TODO: handle collision of the nodes
            throw new System.NotImplementedException();
        }



        SKSpriteNode GetBackground() {
            var background = SKSpriteNode.FromImageNamed("GameStage.png");
            background.Name = "background";
            background.AnchorPoint = new CGPoint(0, 0);
            background.ZPosition = -1;
            background.Size = new CGSize(this.Scene.Size.Width, this.Scene.Size.Height);
            return background;
        }

        public override void MouseDown(NSEvent theEvent)
        {
            //simpleEnemy.PhysicsBody.ApplyForce(new CGVector(100, 0));
            //simpleEnemy.PhysicsBody.Velocity = new CGVector(100, 0);
            AddChild(EnemySprite.CreateEnemyAt("hey", theEvent.LocationInWindow));
        }

        public override void Update(double currentTime)
        {
            // Called before each frame is rendered
        }
    }
}
