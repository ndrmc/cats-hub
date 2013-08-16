using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using DRMFSS.BLL.Services;
using DRMFSS.Web.Controllers;
using DRMFSS.BLL;
namespace DRMFSS.Web.Test
{
    [TestFixture]
    public class TranlationControllerTest
    {
        #region SetUp / TearDown

        private TransactionController _transactionController;

        [SetUp]
        public void Init()
        {
            var tranlations = new List<Translation>
                                  {
                                      new Translation{LanguageCode = "LOO1",Phrase = "PHRASE ONE",TranslatedText = "PHRASE TRANSLATED",TranslationID = 0},
                                      new Translation{LanguageCode = "L002",Phrase = "PHRASE TWO",TranslatedText = "PHRASE TRANSLATED TWO",TranslationID = 1}
                                  };

            
        }

        
        #endregion
    }
}
