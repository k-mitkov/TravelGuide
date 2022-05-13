using System;
using System.Collections.Generic;
using System.Text;
using TravelGuide.Enums;

namespace TravelGuide.Models
{
    public class SupportedLanguage
    {
        #region Constructor

        public SupportedLanguage(LanguageEnum languageType, string flagSource)
        {
            LanguageType = languageType;
            FlagSource = flagSource;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Име на езика
        /// </summary>
        public string Name { get => Enum.GetName(typeof(LanguageEnum), LanguageType); }

        /// <summary>
        /// Език
        /// </summary>
        public LanguageEnum LanguageType { get; private set; }

        /// <summary>
        /// Флаг на държавата
        /// </summary>
        public string FlagSource { get; set; }

        #endregion
    }
}
