using System;

using AppKit;
using SpriteKit;
using Foundation;
using CoreGraphics;


namespace TheTaleOfAHero
{
    public class MenuScene : SKScene
    {

        // TODO: change texture on button hovering

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
            // Start button
            var start_button = SKSpriteNode.FromImageNamed(START_BUTTON_IMAGE);
            start_button.Name = "start";
            start_button.Position = new CGPoint(Frame.GetMidX(), Frame.GetMidY() * 1.25);
            AddChild(start_button);

            // Settings button
            var settings_button = SKSpriteNode.FromImageNamed(SETTINGS_BUTTON_IMAGE);
            settings_button.Name = "settings";
            settings_button.Position = new CGPoint(Frame.GetMidX(), Frame.GetMidY() * 0.75);
            AddChild(settings_button);

            // Exit button
            var exit_button = SKSpriteNode.FromImageNamed(EXIT_BUTTON_IMAGE);
            exit_button.Name = "exit";
            exit_button.Position = new CGPoint(Frame.GetMidX(), Frame.GetMidY() * 0.25);
            AddChild(exit_button);
        }

        public override void MouseDown(NSEvent theEvent)
        {
            var position = theEvent.LocationInNode(this);
            var element = GetNodeAtPoint(position);
            switch(element.Name)
            {
                case "start":
                    var gameScene = SKNode.FromFile<GameScene>("GameScenes/GameScene");
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
