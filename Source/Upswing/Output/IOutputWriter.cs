using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public interface IOutputWriter<TModel>
    {
        void Write(TModel templateModel, string generatedOutput);
    }
}
