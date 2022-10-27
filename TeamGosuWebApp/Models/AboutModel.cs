using System;
using System.Reflection;

namespace TeamGosuWebApp.Models {
    public class AboutModel {        
        private String _assemblyVersion;
        private String _assemblyName;

        public AboutModel(String assemblyVersion, String assemblyName) {
            _assemblyVersion = assemblyVersion;
            _assemblyName = assemblyName;
        }

        public string GetAppNameVersion() {
            return _assemblyName + "-" + _assemblyVersion;
        }
    }
}
