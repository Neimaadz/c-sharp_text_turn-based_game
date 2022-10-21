using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Vampire : Character, IUndead
    {

        public Vampire(string name) : base(name, 100, 100, 120, 50, 300, 300, 2, 2)
        {
        }


        // =======================================================================
        // (Vampire) Se soigne de la moitié des dégâts qu’il inflige
        // =======================================================================
        public override void DealDamage(List<Tuple<int, Character>> characters, Character target, int margeAttack, int damageDeal)
        {
            switch (margeAttack)
            {
                //============================ Attaque réussi ===========================================================
                case int n when n > 0:

                    // Dégats communs
                    DealCommonDamage(target, damageDeal);

                    // Vampire : se soigne de la moitié des dégâts qu’il inflige
                    StealLife(damageDeal);

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


        // =======================================================================
        // (Vampire) Se soigne de la moitié des dégâts qu’il inflige
        // =======================================================================
        private void StealLife(int damageDeal)
        {
            int damageHeal = damageDeal / 2;
            Console.WriteLine("{0} vole de la vie", Name);
            Console.WriteLine("{0} : +{1} PDV", Name, damageHeal);
            CurrentLife += damageHeal;

            if (CurrentLife >= MaximumLife)  // Pour caper la vie
            {
                CurrentLife = MaximumLife;
            }
        }



    }
}
