using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingGrounds
{
    public class Mob
    {
        public int Health { get; set; }
        public int Agility { get; set; }
        public int Speed { get; set; }
        public int Attack { get; set; }
        public string Type { get; set; }
        public int Id { get; set; }
        public int KillStreak { get; set; }
        public int Breedability { get; set; } 
        public string Parents { get; set; }

        public static Random rnd = new Random();
        

        public Mob( string type, int id, int health, int attack, int speed, int agility, string parents, int breedability)
        {
            this.Type = type;
            this.Id = id;
            this.Health = SetHealth(health);
            this.Speed = SetSpeed(speed);
            this.Agility = SetAgility(agility);
            this.Attack = SetAttack(attack, agility);
            this.Parents = parents;
            this.Breedability = SetBreedability(breedability);
        }
        public static int SetHealth(int health)
        {
            return (health += Mob.rnd.Next(-2, 2));
        }
        public static int SetAttack(int attack, int agility)
        {
            return (attack += Mob.rnd.Next(-2, 2) + agility);
        }
        public static int SetSpeed(int speed)
        {
            return (speed += Mob.rnd.Next(-2, 2));
        }
        public static int SetAgility(int agility)
        {
            return (agility += Mob.rnd.Next(-2, 2));
        }
        public static int SetBreedability(int breedability)
        {
            return (breedability += Mob.rnd.Next(-1, 1));
        }


    }

    
}
