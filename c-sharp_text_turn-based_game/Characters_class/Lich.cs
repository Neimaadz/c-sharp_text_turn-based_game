using System;
using System.Collections.Generic;

namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public class Lich : Character, IUndead, IUnholyDamage
    {
        string IUnholyDamage.Name { get => Name; set => Name = value; }


        public Lich(string name) : base(name, 75, 125, 80, 50, 125, 125, 3, 3)
        {
        }


        // =======================================================================
        // (Liche) Inflige des dégâts impies
        // =======================================================================
        public override void DealDamage(List<Tuple<int, Character>> characters, Character target, int margeAttack, int damageDeal)
        {
            switch (margeAttack)
            {
                //============================ Attaque réussi ===========================================================
                case int n when n > 0:

                    // Liche : Inflige des dégâts impies
                    (this as IUnholyDamage).DealUnholyDamage(target, damageDeal);

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
