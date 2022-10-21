using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Robot : Character, IAlive
    {
        private int CountAttackOff = 0;  // Compteur de Round attaque off

        Type IPain.CharacterType { get => GetType(); set => GetType(); }
        string IPain.Name { get => Name; set => Name = value; }
        int IPain.CurrentLife { get => CurrentLife; set => CurrentLife = value; }
        int IPain.CurrentAttackNumber { get => CurrentAttackNumber; set => CurrentAttackNumber = value; }
        int IPain.TotalAttackNumber { get => TotalAttackNumber; set => TotalAttackNumber = value; }
        int IPain.CountAttackOff { get => CountAttackOff; set => CountAttackOff = value; }


        public Robot(string name) : base(name, 10, 100, 50, 50, 200, 200, 1, 1)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions

            // Robot : Au début de chaque round, le robot augmente son attaque de 50%
            IncreaseDamage();

            (this as IPain).IsSensitiveToPain();    // Check si on est affecté par la douleur
        }



        // =======================================================================
        // (Robot) Au début de chaque round, le robot augmente son attaque de 50%
        // =======================================================================
        private void IncreaseDamage()
        {
            int increaseDamage = (int)(Attack * 0.5);  // +50% attaque

            Console.WriteLine("{0} augmente de 50% son Attaque", Name);
            Console.WriteLine("{0} : +{1} Attaque", Name, increaseDamage);
            Console.WriteLine();

            Attack += increaseDamage;
        }


        // =======================================================================
        // (Robot) Pas de jets aléatoires. Il suffit d’ajouter 50 à la caractéristique
        // =======================================================================
        public override int JetInitiative()
        {
            return Initiative + 50;
        }

        public override int JetAttack()
        {
            return Attack + 50;
        }

        public override int JetDefense()
        {
            return Defense + 50;
        }



    }
}
