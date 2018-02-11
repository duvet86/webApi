using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Test_API.Edm
{
    public partial class DBEntities
    {
        [DbFunction("DBContext.Store", "IsValidUser")]
        public bool IsValidUser(string userName, string password)
        {
            var objectContext = ((IObjectContextAdapter)this).ObjectContext;

            var parameters = new List<ObjectParameter>
            {
                new ObjectParameter("p_UserName", userName),
                new ObjectParameter("p_Password", password)
            };

            return objectContext.CreateQuery<bool>("DBContext.Store.IsValidUser(@p_UserName, @p_Password)", parameters.ToArray())
                .Execute(MergeOption.NoTracking)
                .FirstOrDefault();
        }
    }
}