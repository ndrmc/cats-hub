using System.ComponentModel;
using System.Reflection;
using System;
using System.Globalization;

namespace DRMFSS.Shared
{
    //using ResourceFactory;

    public class DBLocalizedDisplayNameAttribute : DisplayNameAttribute
    {
        private string _displayFunctionName;
        private Type _resourceFactory;
        private string[] param = {"CreateGlobalResourceProvider"};
        private Type _resourceProvider;

       public DBLocalizedDisplayNameAttribute(Type resourceFactory, string displayFunctionName)
       {
           _resourceFactory = resourceFactory;
           _displayFunctionName = displayFunctionName;
       }

        public override string DisplayName
        {
            get
            {
                Type ty = _resourceFactory;
                MethodInfo[] mi = ty.GetMethods();
                MethodInfo methodInfo = ty.GetMethod(this.param[0]); //_displayFunctionName);
                var o = Activator.CreateInstance(ty);
                DBResourceProvider result = methodInfo.Invoke(o, new object[] { _displayFunctionName }) as DBResourceProvider;//null);
                return result.GetObject(_displayFunctionName, CultureInfo.CurrentUICulture).ToString();

               // return result.ToString();
            }
        }
    }
}