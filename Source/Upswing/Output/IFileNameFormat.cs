using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Upswing
{
    public interface IFileNameFormat<TModel>
    {
        public string GetName(TModel model);
    }
}
