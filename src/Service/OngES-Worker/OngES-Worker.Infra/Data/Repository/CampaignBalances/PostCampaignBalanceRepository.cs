using Core.Domain.Entities;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Core.Domain.Interfaces.CampaignBalances;

namespace  Infra.Data.Repositories.CampaignBalances
{
    public class PostCampaignBalanceRepository: IPostCampaignBalanceRepository
    {
        private readonly string _connectionString;
        private readonly ILogger<PostCampaignBalanceRepository> _logger;
        public PostCampaignBalanceRepository(IConfiguration configuration, ILogger<PostCampaignBalanceRepository> logger)
        {
            _connectionString = configuration.GetConnectionString("DB_SQL_ONGES")
                                    ?? throw new InvalidOperationException("Connection string 'DB_SQL_ONGES' not found.");

            _logger = logger;
        }

        private IDbConnection CreateConnection()=> new SqlConnection(_connectionString);

        public async Task<int> PostCampaignBalanceAsync(Donation donation)
        {
            try
            {
                using var connection = CreateConnection();
                string sql = @"UPDATE dbo.campaign SET value_total_collected = value_total_collected + @Value WHERE id_campaign = @IdCampaign";

                var result = await connection.ExecuteScalarAsync(sql, donation);
                if (result == null)
                    throw new InvalidOperationException("Erro ao atualizar saldo da campanha");

                return Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while getting users count.");
                throw;
            }
        }
       
        
        }
    }

