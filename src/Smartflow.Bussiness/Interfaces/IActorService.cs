using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Interfaces
{
    public interface IActorService : IQuery<IList<User>, string>, IQuery<IList<User>, Dictionary<string, string>>
    {
        string GetOrganizationCode(string id);

        IList<User> GetActorByOrganization(string organizationCodes);

        IList<User> GetActorByRole(string id);
    }
}
