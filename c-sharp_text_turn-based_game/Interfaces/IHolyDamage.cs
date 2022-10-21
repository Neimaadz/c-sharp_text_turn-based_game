using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public interface IHolyDamage
    {
        public string Name { get; set; }


        public void DealHolyDamage(Character target, int damageDeal)
        {
            if (target is ICursed)
            {
                Console.WriteLine("{0} inflige des dégats sacrés", Name);
                Character.DealCommonDamage(target, damageDeal * 2); // dégats communs x2
            }
            else
            {
                Character.DealCommonDamage(target, damageDeal);
            }
        }



    }
}
