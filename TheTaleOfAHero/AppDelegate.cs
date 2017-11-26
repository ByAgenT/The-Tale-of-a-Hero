using System;

using AppKit;
using SpriteKit;
using Foundation;


namespace TheTaleOfAHero
{
    public partial class AppDelegate : NSApplicationDelegate
    {
        public override void DidFinishLaunching(NSNotification notification)
        {

            // Set windows size for comfort playing
            MyGameView.Window.SetFrame(new CoreGraphics.CGRect(0, 0, 1600, 1000), true, true);

            // Getting GameScene for presenting on the window
            // TODO: replace with MenuScene
            var scene = SKNode.FromFile<GameScene>("GameScenes/GameScene");

            // Set the scale mode to scale to fit the window
            scene.ScaleMode = SKSceneScaleMode.ResizeFill;


            // Enable debug information
            MyGameView.ShowsPhysics = true;
            MyGameView.ShowsFPS = true;
            MyGameView.ShowsNodeCount = true;

            // SpriteKit applies additional optimizations to improve rendering performance
            MyGameView.IgnoresSiblingOrder = true;


            // Present scene on the window
            MyGameView.PresentScene(scene);
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }
    }
}
