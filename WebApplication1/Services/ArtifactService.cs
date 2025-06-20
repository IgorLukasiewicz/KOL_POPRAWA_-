
using Microsoft.Data.SqlClient;
using WebApplication1.DTOs;
using WebApplication1.Services;

public class ArtifactService : IArtifactService
{
    private readonly string _connectionString;

    public ArtifactService(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConn");
    }

public async Task<int> NewArtifactAndProject(NewArtifactAndProjectDTO input)
{
    await using SqlConnection conn = new SqlConnection(_connectionString);
    await conn.OpenAsync();
    await using var transaction = (SqlTransaction)await conn.BeginTransactionAsync();

    try
    {
        // Walidacja unikalności artefaktu i projektu
        if (await RecordExistsAsync(conn, transaction, "Artifact", "ArtifactId", input.Artifact.ArtifactId))
            throw new InvalidOperationException($"Artefakt o ID {input.Artifact.ArtifactId} już istnieje.");

        if (await RecordExistsAsync(conn, transaction, "Preservation_Project", "ProjectId", input.Project.ProjectId))
            throw new InvalidOperationException($"Projekt o ID {input.Project.ProjectId} już istnieje.");

        // Dodanie artefaktu
        var artifactCmd = new SqlCommand(@"
            INSERT INTO Artifact (ArtifactId, Name, OriginDate, InstitutionId)
            VALUES (@ArtifactId, @Name, @OriginDate, @InstitutionId);", conn, transaction);
        artifactCmd.Parameters.AddWithValue("@ArtifactId", input.Artifact.ArtifactId);
        artifactCmd.Parameters.AddWithValue("@Name", input.Artifact.Name);
        artifactCmd.Parameters.AddWithValue("@OriginDate", input.Artifact.OrginalDate);
        artifactCmd.Parameters.AddWithValue("@InstitutionId", input.Artifact.InstitutionId);
        await artifactCmd.ExecuteNonQueryAsync();

        // Dodanie projektu
        var projectCmd = new SqlCommand(@"
            INSERT INTO Preservation_Project (ProjectId, Objective, StartDate, EndDate, ArtifactId)
            VALUES (@ProjectId, @Objective, @StartDate, @EndDate, @ArtifactId);", conn, transaction);
        projectCmd.Parameters.AddWithValue("@ProjectId", input.Project.ProjectId);
        projectCmd.Parameters.AddWithValue("@Objective", input.Project.Objective);
        projectCmd.Parameters.AddWithValue("@StartDate", input.Project.StartDate);
        projectCmd.Parameters.AddWithValue("@EndDate", (object?)input.Project.EndDate ?? DBNull.Value);
        projectCmd.Parameters.AddWithValue("@ArtifactId", input.Artifact.ArtifactId);
        await projectCmd.ExecuteNonQueryAsync();

        await transaction.CommitAsync();
        return input.Project.ProjectId;
    }
    catch
    {
        await transaction.RollbackAsync();
        throw;
    }
}

private async Task<bool> RecordExistsAsync(SqlConnection conn, SqlTransaction transaction, string table, string column, object value)
{
    var query = $"SELECT 1 FROM {table} WHERE {column} = @Value";
    using var cmd = new SqlCommand(query, conn, transaction);
    cmd.Parameters.AddWithValue("@Value", value);
    return await cmd.ExecuteScalarAsync() is not null;
}

}