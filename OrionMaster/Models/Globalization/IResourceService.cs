using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrionMaster.Models.Globalization
{
    public interface IResourceService
    {
        string GetResource(string resourceName, string resourceKey);
        Dictionary<string, Dictionary<string, string>> GetRessourcesByName(string resourceName);
    }
}
