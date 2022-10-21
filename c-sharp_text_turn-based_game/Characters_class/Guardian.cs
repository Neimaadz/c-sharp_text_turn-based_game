using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Guardian : Character, IAlive, IHolyDamage
    {
        private int CountAttackOff = 0;  // Compteur de Round attaque off

        string IHolyDamage.Name { get => Name; set => Name = value; }

        Type IPain.CharacterType { get => GetType(); set => GetType(); }
        string IPain.Name { get => Name; set => Name = value; }
        int IPain.CurrentLife { get => CurrentLife; set => CurrentLife = value; }
        int IPain.CurrentAttackNumber { get => CurrentAttackNumber; set => CurrentAttackNumber = value; }
        int IPain.TotalAttackNumber { get => TotalAttackNumber; set => TotalAttackNumber = value; }
        int IPain.CountAttackOff { get => CountAttackOff; set => CountAttackOff = value; }


        public Guardian(string name) : base(name, 50, 150, 50, 50, 150, 150, 3, 3)
        {
        }


        // Appler à chaque début round
        public override void OnEachRound()
        {
            CurrentAttackNumber = TotalAttackNumber;    // Réinitialisation des points d'actions
            (this as IPain).IsSensitiveToPain();    // Check si on est affecté par la douleur
        }



        // =======================================================================
        // (Gardien) Bonus contre-attaque doublé
        // =======================================================================
        public override void ActionCounterAttack(List<Tuple<int, Character>> characters, Character target, int margeAttack)
        {
            CurrentAttackNumber -= 1;   // On retire -1 point d'attaque

            Console.WriteLine("{0} lance Contre-attaque", Name);

            int bonusAttack = (margeAttack * (-1)) * 2; // Bonus contre-attaque doublé
            int margeCounterAttack = (JetAttack() + bonusAttack) - target.JetDefense();
            int damageDeal = margeCounterAttack * Damage / 100;

            DealDamage(characters, target, margeCounterAttack, damageDeal);
        }


        // =======================================================================
        // (Gardien) Inflige des dégâts sacrés
        // =======================================================================
        public override void DealDamage(List<Tuple<int, Character>> characters, Character target, int margeAttack, int damageDeal)
        {
            switch (margeAttack)
            {
                //============================ Attaque réussi ===========================================================
                case int n when n > 0:

                    // Gardien : Inflige des dégâts sacrés
                    (this as IHolyDamage).DealHolyDamage(target, damageDeal);

                    //============================ Cas de la cible ===========================================================

                    // Si cible est sensible à la douleur
                    if (target is IPain)
                    {
                        (target as IPain).Pain(damageDeal);     // damageDeal = dégat subis
                    }

                    IsCharacterDead(characters, target);
                    break;

                //============================ Defense de l'adversaire réussi ===========================================================
                case int n when n <= 0:
                    Console.WriteLine("Echec de l'attaque...");

                    if (target.CurrentAttackNumber > 0)    // Si le défenseur qui contre-attaque possède assez de point d'attaque
                    {
                        target.ActionCounterAttack(characters, this, margeAttack);  // Defenseur contre attaque
                    }

                    break;
            }
        }






    }
}
