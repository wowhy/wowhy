namespace HyLibrary.Reflection.MetadataProvider
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public abstract class MetadataProviderBase
    {
        public virtual void AddProvider(MetadataProviderBase provider)
        {
        }

        public virtual void InsertProvider(int index, MetadataProviderBase provider)
        {
        }

        public virtual MetadataProviderBase[] GetProviders()
        {
            return new MetadataProviderBase[0];
        }
    }
}