using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;
using TheTaleOfAHero.Models;


namespace TheTaleOfAHero
{
    public class MenuScene : SKScene
    {

        const string MENU_RESUORCES = "Menu/";

        const string BACKGROUND_IMAGE = MENU_RESUORCES + "background.png";
        const string START_BUTTON_IMAGE = MENU_RESUORCES + "start_button.png";
        const string START_PRESSED_BUTTON_IMAGE = MENU_RESUORCES + "start_button_pressed.png";
        const string SETTINGS_BUTTON_IMAGE = MENU_RESUORCES + "settings_button.png";
        const string SETTINGS_PRESSED_BUTTON_IMAGE = MENU_RESUORCES + "settings_button_pressed.png";
        const string EXIT_BUTTON_IMAGE = MENU_RESUORCES + "exit_button.png";
        const string EXIT_PRESSED_BUTTON_IMAGE = MENU_RESUORCES + "exit_button_pressed.png";
        const string LOGO_IMAGE = MENU_RESUORCES + "logo.png";

        public MenuScene(IntPtr handle) : base(handle)
        {
            UserInteractionEnabled = true;
        }

        public override void DidMoveToView(SKView view)
        {
            RenderScene();
        }

        void RenderScene()
        {
            RenderBackground();
            RenderLogo();
            RenderButtons();
        }

        /// <summary>
        /// Renders the background.
        /// </summary>
        void RenderBackground()
        {
            var background = SKSpriteNode.FromImageNamed(BACKGROUND_IMAGE);
            background.AnchorPoint = new CGPoint(0, 0);
            background.ZPosition = -1;
            background.Size = Frame.Size;
            AddChild(background);
        }

        /// <summary>
        /// Renders the logo.
        /// </summary>
        void RenderLogo()
        {
            var logo = SKSpriteNode.FromImageNamed(LOGO_IMAGE);
            logo.Position = new CGPoint(Frame.GetMidX(), Frame.GetMidY() * 1.75);
            AddChild(logo);
        }

        /// <summary>
        /// Render the buttons.
        /// </summary>
        void RenderButtons()
        {
            var start_button = Button.CreateButtonAt(
                SKTexture.FromImageNamed(START_BUTTON_IMAGE),
                SKTexture.FromImageNamed(START_PRESSED_BUTTON_IMAGE),
                "start",
                new CGPoint(Frame.GetMidX(), Frame.GetMidY() * 1.25)
            );
            var settings_button = Button.CreateButtonAt(
                SKTexture.FromImageNamed(SETTINGS_BUTTON_IMAGE),
                SKTexture.FromImageNamed(SETTINGS_PRESSED_BUTTON_IMAGE),
                "settings",
                new CGPoint(Frame.GetMidX(), Frame.GetMidY() * 0.75)
            );
            var exit_button = Button.CreateButtonAt(
                SKTexture.FromImageNamed(EXIT_BUTTON_IMAGE),
                SKTexture.FromImageNamed(EXIT_PRESSED_BUTTON_IMAGE),
                "exit",
                new CGPoint(Frame.GetMidX(), Frame.GetMidY() * 0.25)
            );

            AddChild(start_button);
            AddChild(settings_button);
            AddChild(exit_button);
        }

        public override void MouseDown(NSEvent theEvent)
        {
            var position = theEvent.LocationInNode(this);
            var element = GetNodeAtPoint(position);
            switch(element.Name)
            {
                case "start":
                    var gameScene = FromFile<GameScene>("GameScenes/GameScene");
                    gameScene.ScaleMode = SKSceneScaleMode.ResizeFill;
                    View.PresentScene(gameScene);
                    break;
                case "settings":
                    break;
                case "exit":
                    NSApplication.SharedApplication.Terminate(this);
                    break;
            }
        }

        public override void Update(double currentTime)
        {
            // Called before each frame is rendered
        }
    }
}
