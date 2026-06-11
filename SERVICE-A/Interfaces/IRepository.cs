using Configuration.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Interfaces
{
    public interface IRepository
    {
        Task<List<Parameters>> GetParametersAsync(string applicationName);
    }
}
