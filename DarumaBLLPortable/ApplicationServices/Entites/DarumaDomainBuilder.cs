using System;
using DarumaBLLPortable.Common;
using DarumaBLLPortable.Domain;

namespace DarumaBLLPortable.ApplicationServices.Entites
{
    public class DarumaDomainBuilder : DarumaDomain
    {
        #region factory

        public DarumaDomainBuilder() : base()
        {
        }      

        public static DarumaDomain BuildDaruma(DarumaDTO darumaDTO)
        {
            try
            {
                return new DarumaDomainBuilder
                {
                    Id = darumaDTO.Id,
                    Wish = darumaDTO.Wish,
                    Theme = darumaDTO.Theme,
                    CreateDate = darumaDTO.CreateDate,
                    Status = darumaDTO.Status,
                    CurrentQuoteKey = darumaDTO.CurrentQuoteKey
                };
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static DarumaDomain BuildDaruma(DarumaView darumaView)
        {
            return new DarumaDomainBuilder
            {
                Id = darumaView.Id,
                Wish = darumaView.Wish,
                Theme = darumaView.Theme,
                CreateDate = darumaView.CreateDate,
                Status = darumaView.Status,
                CurrentQuoteKey = darumaView.CurrentQuoteKey
            };
        }

        #endregion
    }
}
