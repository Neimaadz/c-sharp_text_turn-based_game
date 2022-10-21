using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Warrior : Character, IAlive
    {
        private int CountAttackOff = 0;  // Compteur de Round attaque off

        Type IPain.CharacterType { get => GetType(); set => GetType(); }
        string IPain.Name { get => Name; set => Name = value; }
        int IPain.CurrentLife { get => CurrentLife; set => CurrentLife = value; }
        int IPain.CurrentAttackNumber { get => CurrentAttackNumber; set => CurrentAttackNumber = value; }
        int IPain.TotalAttackNumber { get => TotalAttackNumber; set => TotalAttackNumber = value; }
        int IPain.CountAttackOff { get => CountAttackOff; set => CountAttackOff = value; }


        public Warrior(string name) : base(name, 100, 100, 50, 100, 200, 200, 2, 2)
        {
        }



        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions
            (this as IPain).IsSensitiveToPain();    // Check si on est affecté par la douleur
        }




        // =======================================================================
        // (Guerrier) perd la possibilité d’attaquer durant le tour actuel. Il ne peut pas être affecté par la douleur plus longtemps
        // =======================================================================
        void IPain.Pain(int damageTaken)
        {
            if (damageTaken > CurrentLife)
            {
                int percentChance = ((damageTaken - CurrentLife) * 2 / (CurrentLife + damageTaken)) * 100;
                int randomValue = new Random().Next(100);

                if (randomValue < percentChance)
                {
                    Console.WriteLine("{0} est affaiblit durant ce round en cours !", Name);
                    CountAttackOff = 1;
                    CurrentAttackNumber = 0;

                }
            }
        }




    }
}
