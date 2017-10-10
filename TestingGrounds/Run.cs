using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestingGrounds
{
    class Run
    {
        public static int indexCounter = 0;
        public static Random rnd = new Random();
        public static void RunSim()
        {
            List<Mob> MobList = InitialCreaton();
            
            do
            {
                Printer(MobList);
                BattlePhase(MobList);
                BreedingPhase(MobList);
            } while (Console.ReadKey().Key != ConsoleKey.Escape);
            
            
            
        }
        //Creates the "prime" creatures, the first iteration, generation Zero
        private static List<Mob> InitialCreaton()
        {
            List<Mob> list = new List<Mob>();
            for (int i = 0; i < 5; i++)
            {
                list.Add(new Mob("Wolf", i,50,15,15,15,"Prime",5));
                indexCounter++;
            }
            return (list); 
        }
        //puts all creatures agains eachother, the quickest attacks first, targeting the weak first.
        //Each kill grants the killer with 1/3 its attack in health.

        //Rules: if pray agility is twice as mutch as killer agility, pray takes no damage,
        //and has a sligh chance of hitting killer with full attack, ignoring agility.
        //has pray half of killers agility or more, same agility or more, 150% or more(up to 200%), 
        //pray avoids 25%,50%,75% of killer attack( in that order)
        private static List<Mob> BattlePhase(List<Mob> list)
        {
            var speedList = from sl in list
                            orderby sl.Speed descending
                            select sl;
            var healthList = from hl in list
                             orderby hl.Health ascending
                             select hl;

            //Mob(i) checks for pray
            for (int i = 0; i < speedList.Count(); i++)
            {
                //checks if killer is dead.
                if (speedList.ElementAt(i).Health > 0)
                {
                    //loops thru pray(j)
                    for (int j = 0; j < healthList.Count(); j++)
                    {
                        //mob(i) checks if mob(j) is alive and is not itself
                        if (healthList.ElementAt(j).Health > 0 && speedList.ElementAt(i).Id != healthList.ElementAt(j).Id)
                        {
                            healthList.ElementAt(j).Health -= speedList.ElementAt(i).Attack;
                            if (healthList.ElementAt(j).Health < 0)
                            {
                                speedList.ElementAt(i).Health += speedList.ElementAt(i).Attack / 3;
                            }

                        }
                    }
                }
                 
            }
            //makes a new list with all the dead mobs removed
            var results = from res in speedList
                          orderby res.Id ascending
                          select res;
            for (int i = 0; i < list.Count; i++)
            {
                list.ElementAt(i).Health = speedList.ElementAt(i).Health;
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (list.ElementAt(i).Health <= 0)
                {
                    list.Remove(list.ElementAt(i));
                }
            }
            return (list);
            
        }
        //breeds the creatures to the new generation, This is totaly random and 
        //if they breed and how many they breed is determend by breedability.
        private static void BreedingPhase(List<Mob> list)
        {
            List<int> hasbreed = new List<int>();
            List<int> hasNotBreed = new List<int>();
            int breedCykles = list.Count / 2;
            int firstMate;
            int secondMate;

            for (int i = 0; i < list.Count; i++)
            {
                hasNotBreed.Add(list.ElementAt(i).Id);
            }

            for (int i = 0; i < breedCykles; i++)
            {
                firstMate = rnd.Next(hasNotBreed.Count);
                hasbreed.Add(firstMate);
                hasNotBreed.Remove(firstMate);
                secondMate = rnd.Next(hasNotBreed.Count);
                hasbreed.Add(secondMate);
                hasNotBreed.Remove(secondMate);

                var firstMateStats = from fms in list
                                     where fms.Id == firstMate
                                     select fms;
                var secondMateStats = from sms in list
                                      where sms.Id == secondMate
                                      select sms;

                int tempHealth = (firstMateStats.First().Health + secondMateStats.First().Health) / 2;
                int tempAttack = (firstMateStats.First().Attack + secondMateStats.First().Attack) / 2;
                int tempSpeed = (firstMateStats.First().Speed + secondMateStats.First().Speed) / 2;
                int tempAgility = (firstMateStats.First().Agility + secondMateStats.First().Agility) / 2;
                int tempBreedablilty = (firstMateStats.First().Breedability + secondMateStats.First().Breedability) / 2;
                string tempParents = firstMateStats.First().Id.ToString("000") + secondMateStats.First().Id.ToString("000");

                list.Add(new Mob("Wolf", indexCounter, tempHealth, tempAttack, tempSpeed, tempAgility, tempParents, tempBreedablilty));
            }


        }

        

        private static void Printer(List<Mob> list)
        {
            Console.Clear();
            Console.WriteLine("Press Any key to continue(not ESC tho)");
            Console.WriteLine("TYPE,    ID, HEALTH, ATTACK, SPEED, AGILITY, KILLSTREAK, Breed, Parents");
            foreach (Mob listItem in list)
            {
                Console.WriteLine(
                    $"{listItem.Type}, " +
                    $"{listItem.Id.ToString("00000")}, " +
                    $"{listItem.Health.ToString("000000")}, " +
                    $"{listItem.Attack.ToString("000000")}, " +
                    $"{listItem.Speed.ToString("00000")}, " +
                    $"{listItem.Agility.ToString("0000000")}, " +
                    $"{listItem.KillStreak.ToString("0000000000")}, " +
                    $"{listItem.Breedability.ToString("00000")}, " +
                    $"{listItem.Parents} "
                    );
            }
        }
    }
}
