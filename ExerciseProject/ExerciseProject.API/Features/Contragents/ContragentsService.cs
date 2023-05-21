using Microsoft.Data.SqlClient;

namespace ExerciseProject.API.Features.Contragents
{
    public class ContragentsService : IContragentsService
    {
        private readonly string connectionString;

        public ContragentsService(IConfiguration config)
        {
            this.connectionString = config["ConnectionString"];
        }

        public async Task<bool> Create(ContragentDto contragent)
        {
            if (await ContragentsExists(contragent.VAT, contragent.UserId) == false)
            {
                using (var connection = new SqlConnection(this.connectionString))
                {
                    var command = new SqlCommand(@"INSERT INTO Contragents (Name, Address, Mail, UserId, VAT) 
                                               VALUES
                                               (@name, @address, @mail, @userId, @vat)", connection);

                    command.Parameters.AddWithValue("name", contragent.Name);
                    command.Parameters.AddWithValue("address", contragent.Address);
                    command.Parameters.AddWithValue("mail", contragent.Mail);
                    command.Parameters.AddWithValue("userId", contragent.UserId);
                    command.Parameters.AddWithValue("vat", contragent.VAT);
                    
                    await connection.OpenAsync();
                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }

            return false;
        }

        public async Task<IEnumerable<ContragentDto>> GetContragentsByUserId(int userId)
        {
            var contragents = new List<ContragentDto>();

            using (var connection = new SqlConnection(this.connectionString))
            {
                var command = new SqlCommand("SELECT * FROM Contragents WHERE UserId = @userId", connection);

                command.Parameters.AddWithValue("userId", userId);

                await connection.OpenAsync();
                var reader = await command.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    var contragent = new ContragentDto()
                    {
                        Name = (string)reader["Name"],
                        Address = (string)reader["Address"],
                        Mail = (string)reader["Mail"],
                        UserId = (int)reader["UserId"],
                        VAT = (string)reader["VAT"]
                    };

                    contragents.Add(contragent);
                }
            }

            return contragents;
        }

        private async Task<bool> ContragentsExists(string vat, int userId)
        {
            var result = 0;

            using (var connection = new SqlConnection(this.connectionString))
            {
                var command = new SqlCommand("SELECT COUNT(*) FROM Contragents WHERE UserId = @userId AND VAT = @vat", connection);

                command.Parameters.AddWithValue("userId", userId);
                command.Parameters.AddWithValue("vat", vat);

                await connection.OpenAsync();
                result = (int)await command.ExecuteScalarAsync();
            }

            return result > 0;
        }
    }
}