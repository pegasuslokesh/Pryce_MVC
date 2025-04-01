using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pryce_MVC.Repositories;
using Pryce_MVC.Models;
using Pryce_MVC.Data;
using System.Data;

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
    public async Task<int> InsertReligionAsync(string religion, string religionL, DateTime created_Date)
    {
        var referenceId = new SqlParameter("@ReferenceID", SqlDbType.Int) { Direction = ParameterDirection.Output };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Set_ReligionMaster_Insert @Religion, @Religion_L, @Field1, @Field2, @Field3, @Field4, @Field5, @Field6, @Field7, @IsActive, @CreatedBy, @CreatedDate, @ModifiedBy, @ModifiedDate, @ReferenceID OUTPUT",
            new SqlParameter("@Religion", religion),
            new SqlParameter("@Religion_L", religionL),
            new SqlParameter("@Field1", ""),
            new SqlParameter("@Field2",""),
            new SqlParameter("@Field3",""),
            new SqlParameter("@Field4", ""),
            new SqlParameter("@Field5", ""),
            new SqlParameter("@Field6", false),
            new SqlParameter("@Field7", ""),
            new SqlParameter("@IsActive", true),
            new SqlParameter("@CreatedBy", "7640"),
            new SqlParameter("@CreatedDate", created_Date),
            new SqlParameter("@ModifiedBy", "7640"),
            new SqlParameter("@ModifiedDate", created_Date),
            referenceId
        );

        return (int)referenceId.Value;
    }
    public async Task<int> UpdateReligionAsync(
    int religionId,
    string religion,
    string religionL
    //string field1,
    //string field2,
    //string field3,
    //string field4,
    //string field5,
    //bool field6,
    //DateTime? field7,
   /* bool isActive*/)
    //string modifiedBy,
    //DateTime modifiedDate)
    {
        var referenceId = new SqlParameter("@ReferenceID", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Set_ReligionMaster_Update @Religion_Id, @Religion, @Religion_L, @Field1, @Field2, @Field3, @Field4, @Field5, @Field6, @Field7, @IsActive, @ModifiedBy, @ModifiedDate, @ReferenceID OUTPUT",
            new SqlParameter("@Religion_Id", religionId),
            new SqlParameter("@Religion", religion ),
            new SqlParameter("@Religion_L", religionL),
            new SqlParameter("@Field1", ""),
            new SqlParameter("@Field2", ""),
            new SqlParameter("@Field3", ""),
            new SqlParameter("@Field4", ""),
            new SqlParameter("@Field5", ""),
            new SqlParameter("@Field6", false),
            new SqlParameter("@Field7", ""),
            new SqlParameter("@IsActive", true),
            new SqlParameter("@ModifiedBy", "admin"),
            new SqlParameter("@ModifiedDate", DateTime.Now),
            referenceId
        );

        return (int)referenceId.Value;
    }
    public async Task<int> DeleteReligionAsync(int religionId, bool isActive, string modifiedBy, DateTime modifiedDate)
    {
        var referenceId = new SqlParameter("@ReferenceID", SqlDbType.Int)
        {
            Direction = ParameterDirection.Output
        };

        await _context.Database.ExecuteSqlRawAsync(
            "EXEC sp_Set_ReligionMaster_RowStatus @Religion_Id, @IsActive, @ModifiedBy, @ModifiedDate, @ReferenceID OUTPUT",
            new SqlParameter("@Religion_Id", religionId),
            new SqlParameter("@IsActive", isActive),
            new SqlParameter("@ModifiedBy", modifiedBy),
            new SqlParameter("@ModifiedDate", modifiedDate),
            referenceId
        );

        return (int)referenceId.Value;
    }
    public async Task<List<Company_Master>> GetCompaniesAsync(int companyId, string companyName, int optype)
    {
        var parameters = new[]
        {
            new SqlParameter("@Company_Id", SqlDbType.Int) { Value = (object)companyId ?? DBNull.Value },
            new SqlParameter("@Company_Name", SqlDbType.NVarChar, 100) { Value = (object)companyName ?? DBNull.Value },
            new SqlParameter("@Optype", SqlDbType.Int) { Value = optype }
        };
        if (optype != 7)
        {

            var dtoResults = await _context.CompanyMasters // Use the DTO
                .FromSqlRaw("EXEC sp_Set_CompanyMaster_SelectRow @Company_Id, @Company_Name, @Optype", parameters)
                .ToListAsync();

            var companyMasters = dtoResults.Select(dto => new Company_Master
            {
                Company_Id = dto.Company_Id,
                Company_Code = dto.Company_Code,
                Parent_Company_Id = dto.Parent_Company_Id,
                Company_Name = dto.Company_Name,
                Company_Name_L = dto.Company_Name_L,
                Logo_Path = dto.Logo_Path,
                Emp_Id = dto.Emp_Id,
                Currency_Id = dto.Currency_Id,
                Country_Id = dto.Country_Id,
                Commerical_License_No =dto.Commerical_License_No, // Default value
                Field1 = dto.Field1,
                Field2 = dto.Field2,
                Field3 = dto.Field3,
                Field4 = dto.Field4,
                Field5 = dto.Field5,
                Field6 = dto.Field6,
                Field7 = dto.Field7,
                IsActive = dto.IsActive,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate,
                ParentCompany = dto.ParentCompany
            }).ToList();
            //new code
           

            return companyMasters;
        }
        else
        {
            var dtoResults = await _context.Set<Company_Limited>()
                .FromSqlRaw("EXEC sp_Set_CompanyMaster_SelectRow @Company_Id, @Company_Name, @Optype", parameters)
                .ToListAsync();

            var companyMasters = dtoResults.Select(dto => new Company_Master
            {
                Company_Id = dto.Company_Id,
                Company_Code = null,
                Parent_Company_Id = null,
                Company_Name = dto.Company_Name,
                Company_Name_L = "N/A",
                Logo_Path = dto.Logo_Path,
                Emp_Id = null,
                Currency_Id = null,
                Country_Id = null,
                Commerical_License_No = "N/A",
                Field1 = null,
                Field2 = null,
                Field3 = null,
                Field4 = null,
                Field5 = null,
                Field6 = null,
                Field7 = null,
                IsActive = true,
                CreatedBy = null,
                CreatedDate = null,
                ModifiedBy = null,
                ModifiedDate = null,
                ParentCompany = null
            }).ToList();
            return companyMasters;
        }


    }
    //Get currency for New form sp

    public async Task<IEnumerable<Currency_Master>> ExecuteCurrencySPAsync(int currencyId, string currencyName, int optype)
    {
        var parameters = new[]
        {
        new SqlParameter("@Currency_Id", currencyId),
        new SqlParameter("@Currency_Name", currencyName ?? (object)DBNull.Value),
        new SqlParameter("@Optype", optype)
    };

        return await _context.CurrencyMasters
            .FromSqlRaw("EXEC sp_Sys_CurrencyMaster_SelectRow @Currency_Id, @Currency_Name, @Optype", parameters)
            .ToListAsync();
    }

}







