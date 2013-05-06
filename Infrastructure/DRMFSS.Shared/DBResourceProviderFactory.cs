using System;
using System.Web.Compilation;
using System.Web;
using System.Diagnostics;
using System.Globalization;

namespace DRMFSS.Shared
{

    public class DBResourceProviderFactory : ResourceProviderFactory
    {

        public override IResourceProvider CreateGlobalResourceProvider(string classKey)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "DBResourceProviderFactory.CreateGlobalResourceProvider({0})", classKey));
            return new DBResourceProvider(classKey); 
        }

        public override IResourceProvider CreateLocalResourceProvider(string virtualPath)
        {
            Debug.WriteLine(String.Format(CultureInfo.InvariantCulture, "DBResourceProviderFactory.CreateLocalResourceProvider({0}", virtualPath));

            // we should always get a path from  the runtime
            string classKey = virtualPath;
            if (!string.IsNullOrEmpty(virtualPath))
            {
                //the line below may not be needed in asp .net mvc 
                virtualPath = virtualPath.Remove(0, 1);
                classKey = virtualPath.Remove(0, virtualPath.IndexOf('/') + 1);
            }

            return new DBResourceProvider(classKey);
        }
    }
}