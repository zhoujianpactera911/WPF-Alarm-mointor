using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Communication
{
    public interface ICommunicationBase
    {
        bool Connection();
        Task<bool> Send(int slave_add, byte func_code, int start_mem_add, int len);
        void Close();
    }
}
