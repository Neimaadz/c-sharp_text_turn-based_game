using System;
namespace LAOUSSING_Damien_DM_IPI_2021_2022
{
    public interface IUndead : ICursed
    {
        public bool IsUndead()
        {
            return true;
        }
    }
}
