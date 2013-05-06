using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using DRMFSS.BLL.MetaModels;

namespace DRMFSS.BLL
{
  
    partial class Unit
    {
        public class Constants
        {
            public const int BAG = 1;
            public const int CARTON = 2;
            public const int BUNDLE = 3;
            public const int CAN = 4;
            public const int SILO = 7;
            
        }

        public static BLL.Unit GetUnitByName(string name)
        {
            return new DRMFSSEntities1().Units.Where(u => u.Name == name).SingleOrDefault();
        }

    }
}
