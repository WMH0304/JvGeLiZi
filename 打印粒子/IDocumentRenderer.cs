using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace 打印粒子
{
    public interface IDocumentRenderer
    {
        void Render(FlowDocument doc, Object data);
    }
}
