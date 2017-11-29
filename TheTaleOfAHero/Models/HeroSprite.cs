using System;
using SpriteKit;
using CoreGraphics;


namespace TheTaleOfAHero.Models
{
    public class HeroSprite : SKSpriteNode
    {
        const string RESOURCE_PATH = "Hero/";
        const string HERO_STAND_IMAGE = RESOURCE_PATH + "Hero.png";

        int _jumpsAvailiable = 2;


        public HeroSprite()
        {
            Texture = SKTexture.FromImageNamed(HERO_STAND_IMAGE);
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
            RunAction(SKAction.MoveBy(-3, 0, 0));
        }

        public void MoveRight() 
        {
            RunAction(SKAction.MoveBy(3, 0, 0));
        }

        public void Jump()
        {
            if(_jumpsAvailiable > 0)
            {
                PhysicsBody.ApplyImpulse(new CGVector(0, 80));
                PhysicsBody.ApplyForce(new CGVector(0, 500));
                --_jumpsAvailiable;    
            }

        }

        public void ResetJumps()
        {
            _jumpsAvailiable = 2;
        }

        #endregion

        #region Shooting

        public void ShootSpell()
        {
            throw new NotImplementedException();
        }

        #endregion

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
