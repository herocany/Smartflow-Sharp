using Smartflow.Bussiness.Models;
using Smartflow.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smartflow.Bussiness.Interfaces
{
    public interface IActorService 
    {
        User GetUserByID(string id);

        IList<User> GetUserByRoleIDs(IEnumerable<string> ids);

        string GetOrganizationCode(string id);

        IList<User> GetActorByOrganization(IEnumerable<string> organizationCodes);

        IList<User> GetActorByRole(IEnumerable<string> ids);

        IList<User> Query(Dictionary<string, string> queryArg);
    }
}
