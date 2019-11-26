using System.Collections.Generic;
using System.Threading.Tasks;
using CrystalProcess.Models;

namespace CrystalProcess.Repositories
{
    public interface IStageRepository
    {
        Task<IList<Stage>> Get();
        Task Add(Stage entity);
    }
}