using Ninject.Modules;
using DAL.Interfaces;
using DAL.RepositoryPgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectSystems.Util.Ninject
{
    public class ReposModule : NinjectModule
    {
        private string connectionString;
        public ReposModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IDbRepos>().To<DbReposPgs>().InSingletonScope().WithConstructorArgument(connectionString);
        }
    }
}
