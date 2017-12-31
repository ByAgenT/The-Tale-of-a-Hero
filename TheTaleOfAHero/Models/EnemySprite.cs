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

        int Cooldown { get; set; }

        public EnemySprite()
        {
            Texture = SKTexture.FromImageNamed(ENEMY_DUTY_IMAGE);
            Size = Texture.Size;
            PhysicsBody = SKPhysicsBody.CreateRectangularBody(Size);
            PhysicsBody.AllowsRotation = false;
            PhysicsBody.CategoryBitMask = CollisionCategory.Enemy;
            PhysicsBody.ContactTestBitMask = CollisionCategory.Hero | CollisionCategory.Spell | CollisionCategory.Platform;
            PhysicsBody.CollisionBitMask ^= CollisionCategory.Spell;
        }

        public void ShootSpell(CGPoint position)
        {
            Cooldown = 100;
            var vector = new CGVector(position.X - Position.X, position.Y - Position.Y);


            var spell = ShotSprite.CreateShotAt(Position, SpellType.Enemy);
            Parent.AddChild(spell);
            spell.AttackByVector(vector);
            spell.ZRotation = (nfloat)Math.Atan2(vector.dy, vector.dx);
        }

        public void AttackIfCooledDown(CGPoint position)
        {
            if (Cooldown == 0)
                ShootSpell(position);
            else
                Cooldown--;
        }


        public static EnemySprite CreateEnemyAt(CGPoint point) {
            return new EnemySprite()
            {
                Position = point
            };
        }

    }
}
