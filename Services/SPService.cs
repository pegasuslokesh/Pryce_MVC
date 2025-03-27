using Microsoft.Data.SqlClient;
using Pryce_MVC.Models;
using Pryce_MVC.Repositories;
using Pryce_MVC.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class SPService : ISPService
{
    private readonly ISPRepository _spRepository;

    public SPService(ISPRepository spRepository)
    {
        _spRepository = spRepository;
    }

    public async Task<IEnumerable<ReligionMaster>> GetReligionDataAsync(int religionId, string religion, int optype)
    {
        return await _spRepository.ExecuteReligionSPAsync(religionId, religion, optype);
    }
}

