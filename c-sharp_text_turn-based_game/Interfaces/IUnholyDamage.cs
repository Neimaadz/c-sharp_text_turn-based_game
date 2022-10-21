using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public interface IUnholyDamage
    {
        public string Name { get; set; }


        public void DealUnholyDamage(Character target, int damageDeal)
        {
            if (target is IBlessed)
            {
                Console.WriteLine("{0} inflige des dégats impies", Name);
                Character.DealCommonDamage(target, damageDeal * 2); // dégats communs x2
            }
            else
            {
                Character.DealCommonDamage(target, damageDeal);
            }
        }



    }
}
