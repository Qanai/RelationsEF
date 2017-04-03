using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelationsEF.DAL
{
    public class MockNewInitializer : CreateDatabaseIfNotExists<RelationsContext>
    {
        protected override void Seed(RelationsContext context)
        {
            base.Seed(context);

            MockHelper.Seed(context);
        }
    }
}
