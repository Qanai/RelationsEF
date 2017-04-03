using RelationsEF.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.DAL
{
    class MockChangeInitializer : DropCreateDatabaseIfModelChanges<RelationsContext>
    {
        protected override void Seed(RelationsContext context)
        {
            base.Seed(context);

            MockHelper.Seed(context);
        }
    }
}
