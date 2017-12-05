using System;
using SpriteKit;
using CoreGraphics;

namespace TheTaleOfAHero.Models
{
    public class Button : SKSpriteNode
    {
        SKTexture _texture_passive;
        SKTexture _texture_active;

        public Button(SKTexture texture_passive, SKTexture texture_active, string name)
        {
            //UserInteractionEnabled = true;
            _texture_passive = texture_passive;
            _texture_active = texture_active;
            Name = name;
            Texture = _texture_passive;
            Size = Texture.Size;
        }


        public static Button CreateButtonAt(SKTexture texture_passive, SKTexture texture_active, string name, CGPoint position)
        {
            return new Button(texture_passive, texture_active, name)
            {
                Position = position
            };
        }


        public void SwitchTexture()
        {
            if (Texture == _texture_active)
                Texture = _texture_passive;
            else
                Texture = _texture_active;
        }
    }
}
