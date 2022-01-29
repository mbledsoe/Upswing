using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public interface IOutputWriter<TFileModel>
    {
        void WriteOutput(TFileModel fileModel, string generatedOutput);
    }
}
