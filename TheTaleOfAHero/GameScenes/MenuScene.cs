using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;


namespace TheTaleOfAHero
{
    public class MenuScene : SKScene
    {
        const string MENU_RESUORCES = "Menu/";

        const string MENU_BACKGROUND_IMAGE = MENU_RESUORCES + "background.png";
        const string MENU_START_BUTTON_IMAGE = MENU_RESUORCES + "start_button.png";
        const string MENU_START_PRESSED_BUTTON_IMAGE = MENU_RESUORCES + "start_button_pressed.png";
        const string MENU_SETTINGS_BUTTON_IMAGE = MENU_RESUORCES + "settings_button.png";
        const string MENU_SETTINGS_PRESSED_BUTTON_IMAGE = MENU_RESUORCES + "settings_button_pressed.png";

        public MenuScene(IntPtr handle) : base(handle)
        {
        }

        public override void DidMoveToView(SKView view)
        {
            // Setup your scene here
            RenderScene();
        }

        void RenderScene()
        {
            RenderBackground();
            RenderLogo();
            RenderButtons();
        }

        void RenderBackground()
        {
            var background = SKSpriteNode.FromImageNamed("Menu/background.png");
            background.AnchorPoint = new CGPoint(0, 0);
            background.ZPosition = -1;
            background.Size = Frame.Size;
            AddChild(background);
        }

        void RenderLogo()
        {
            var logo = SKSpriteNode.FromImageNamed("Menu/logo.png");
            logo.Position = new CGPoint(Frame.GetMidX(), Frame.GetMidY() * 1.75);
            AddChild(logo);
        }

        void RenderButtons()
        {
            
        }

        public override void MouseDown(NSEvent theEvent)
        {
            
        }

        public override void Update(double currentTime)
        {
            // Called before each frame is rendered
        }
    }
}
