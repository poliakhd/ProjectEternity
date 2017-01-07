using System.Collections.Generic;
using ProjectEternity.App.Models.UI;

namespace ProjectEternity.App.Providers.Interfaces
{
    public interface IMenuProvider
    {
        IEnumerable<MenuItem> GetMainItems();
        IEnumerable<MenuItem> GetOptionItems();
    }
}