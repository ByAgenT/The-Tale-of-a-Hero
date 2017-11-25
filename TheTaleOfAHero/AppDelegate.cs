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
            MyGameView.Window.SetFrame(new CoreGraphics.CGRect(0, 0, 1600, 1000), true, true);
            //MyGameView.Window.SetFrame(new CoreGraphics.CGRect(150, 70, 1600, 1000), true, true);

            var scene = SKNode.FromFile<GameScene>("MenuScene");

            // Set the scale mode to scale to fit the window
            //scene.ScaleMode = SKSceneScaleMode.ResizeFill;
            scene.ScaleMode = SKSceneScaleMode.AspectFill;

            MyGameView.ShowsPhysics = true;
            MyGameView.PresentScene(scene);

            // SpriteKit applies additional optimizations to improve rendering performance
            MyGameView.IgnoresSiblingOrder = true;

            MyGameView.ShowsFPS = true;
            MyGameView.ShowsNodeCount = true;
        }

        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender)
        {
            return true;
        }
    }
}
