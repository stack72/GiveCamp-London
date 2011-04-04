using GiveCampLondon.Services;
using StructureMap.Configuration.DSL;

namespace GiveCampLondon.Configuration
{
    public class StructureMapRegistry : Registry
    {
        public StructureMapRegistry()
        {
            Scan(x =>
            {
                x.TheCallingAssembly();
                x.WithDefaultConventions();
            });

            this.For<SiteDataContext>().Use(new SiteDataContext());
            this.For<DocumentPersister>().Use(new DocumentPersister());
        }
    }
}
