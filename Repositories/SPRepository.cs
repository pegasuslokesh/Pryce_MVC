using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pryce_MVC.Repositories;
using Pryce_MVC.Models;
using Pryce_MVC.Data;

public class SPRepository : ISPRepository
{
    private readonly ApplicationDbContext _context;

    public SPRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReligionMaster>> ExecuteReligionSPAsync(int religionId, string religion, int optype)
    {
        var parameters = new[]
        {
            new SqlParameter("@Religion_Id", religionId),
            new SqlParameter("@Religion", religion ?? (object)DBNull.Value),
            new SqlParameter("@Optype", optype)
        };

        return await _context.Set<ReligionMaster>()
            .FromSqlRaw("EXEC sp_Set_ReligionMaster_SelectRow @Religion_Id, @Religion, @Optype", parameters)
            .ToListAsync();
    }
}

