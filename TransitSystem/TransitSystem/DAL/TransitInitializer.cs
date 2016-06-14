using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using TransitSystem.Models;

namespace TransitSystem.DAL
{
    public class TransitInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TransitContext>
    {
        protected override void Seed(TransitContext context)
        {

        }
    }
}