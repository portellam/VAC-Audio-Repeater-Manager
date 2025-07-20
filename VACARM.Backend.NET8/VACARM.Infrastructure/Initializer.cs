using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VACARM.Infrastructure.Contexts;

namespace VACARM.Infrastructure
{
  public class Initializer : DropCreateDatabaseIfModelChanges<Context>
  {
  }
}
