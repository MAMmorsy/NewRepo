using OrionMaster.Models.Globalization;
using System;
using System.ComponentModel;

namespace OrionMaster
{
    public class CustomDisplayNameAttribute : DisplayNameAttribute
    {
        private static IResourceService _resourceService;
        private string _resourceName;
        private string _resourceKey;

        public CustomDisplayNameAttribute(string resourceName, string resourceKey)
        {
            _resourceName = resourceName;
            _resourceKey = resourceKey;
        }

        public override string DisplayName
        {
            get
            {
                CheckProvider();
                return _resourceService.GetResource(_resourceName, _resourceKey);
            }
        }

        public static void RegisterProvider(IResourceService provider)
        {
            _resourceService = provider;
        }

        private void CheckProvider()
        {
            if (null == _resourceService)
                throw new Exception("Resource provider is not set");
        }
    }

    namespace MVC.Models
    {
        public class TestModel
        {
            [CustomDisplayName("Home", "EnterYourNationalityKey")]
            public string Name { get; set; }
        }
    }
}