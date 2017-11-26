using System;
using SpriteKit;
using CoreGraphics;


namespace TheTaleOfAHero.Models
{
    public class EnemySprite : SKSpriteNode
    {
        const string RESOURCE_PATH = "Enemy/";

        const string ENEMY_DUTY_IMAGE = RESOURCE_PATH + "EnemyDuty.png";
        const string ENEMY_ATTACK_IMAGE = RESOURCE_PATH + "EnemyAttack.png";

        public EnemySprite()
        {
            Texture = SKTexture.FromImageNamed(ENEMY_DUTY_IMAGE);
            Size = Texture.Size;
            PhysicsBody = SKPhysicsBody.CreateRectangularBody(Size);
            PhysicsBody.CategoryBitMask = CollisionCategory.Enemy;
            PhysicsBody.ContactTestBitMask = CollisionCategory.Hero | CollisionCategory.Spell | CollisionCategory.Platform;
        }

        public static EnemySprite CreateEnemyAt(CGPoint point) {
            return new EnemySprite()
            {
                Position = point
            };
        }

        // TODO: implement ability to fire
        // TODO: implement behavior
        // TODO: put all enemy behavior in this file

    }
}
