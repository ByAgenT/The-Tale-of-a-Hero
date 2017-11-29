using System;
using SpriteKit;
using CoreGraphics;


namespace TheTaleOfAHero.Models
{
    public class HeroSprite : SKSpriteNode
    {
        const string RESOURCE_PATH = "Hero/";
        const string HERO_STAND_IMAGE = RESOURCE_PATH + "Hero.png";
        const string HERO_JUMP_IMAGE = RESOURCE_PATH + "HeroJump.png";
        const string HERO_MOVING_IMAGE = RESOURCE_PATH + "HeroMoving.png";

        int _jumpsAvailiable = 2;

        SKTexture _heroStand, _heroJump, _heroMoving;

        public HeroSprite()
        {
            _heroJump = SKTexture.FromImageNamed(HERO_JUMP_IMAGE);
            _heroStand = SKTexture.FromImageNamed(HERO_STAND_IMAGE);
            _heroMoving = SKTexture.FromImageNamed(HERO_MOVING_IMAGE);

            Texture = _heroStand;
            Size = Texture.Size;
            PhysicsBody = SKPhysicsBody.CreateRectangularBody(Size);
            PhysicsBody.CategoryBitMask = CollisionCategory.Hero;
            PhysicsBody.ContactTestBitMask = CollisionCategory.Enemy | CollisionCategory.Spell;
            //PhysicsBody.AngularDamping = 0; 
            PhysicsBody.AllowsRotation = false;
        }

        #region Hero Movement

        public void MoveLeft()
        {
            // Flip texture left
            if (XScale > 0)
                XScale *= -1;

            // TODO: slow down this shit
            // Change walking texture
            if (Texture == _heroStand)
                ApplyTexture(_heroMoving);
            else if (Texture == _heroMoving)
                ApplyTexture(_heroStand);
            
            RunAction(SKAction.MoveBy(-3, 0, 0));
        }

        public void MoveRight() 
        {
            // Flip texture right
            if (XScale < 0)
                XScale *= -1;

            // Change walking texture
            if (Texture == _heroStand)
                ApplyTexture(_heroMoving);
            else if (Texture == _heroMoving)
                ApplyTexture(_heroStand);
            
            RunAction(SKAction.MoveBy(3, 0, 0));
        }

        public void Jump()
        {
            if(_jumpsAvailiable > 0)
            {
                ApplyTexture(_heroJump);
                PhysicsBody.ApplyImpulse(new CGVector(0, 80));
                PhysicsBody.ApplyForce(new CGVector(0, 500));
                --_jumpsAvailiable;    
            }

        }

        public void ResetJumps()
        {
            ApplyTexture(_heroStand);
            _jumpsAvailiable = 2;
        }

        #endregion

        #region Shooting

        public void ShootSpell()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Textures

        /// <summary>
        /// Applies the texture.
        /// </summary>
        /// <param name="texture">Texture.</param>
        public void ApplyTexture(SKTexture texture)
        {
            Texture = texture;
            Size = Texture.Size;
        }

        #endregion

        /// <summary>
        /// Creates the hero at the given point.
        /// </summary>
        /// <returns>The <see cref="T:TheTaleOfAHero.Models.HeroSprite"/>.</returns>
        /// <param name="point">Point.</param>
        public static HeroSprite CreateHeroAt(CGPoint point) 
        {
            return new HeroSprite()
            {
                Position = point
            }; 
        }

        // TODO: lock rotation by physics
    }
}
