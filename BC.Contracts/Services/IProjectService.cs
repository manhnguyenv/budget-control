using BC.ViewModel.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC.Contracts.Services
{
    public interface IProjectService
    {
        RequestCommonVM GetByIdProject(int idProject);
        RequestCommonVM Start();
    }
}
