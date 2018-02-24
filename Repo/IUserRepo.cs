using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBot.Repo
{
    public interface IUserRepo
    {
        void SalvarHistorico(Message message);
    }
}
