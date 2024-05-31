namespace FinaFlow.Api.Common.Api
{
    public static class AppExtension
    {
        public static void ConfigureDevEnvironmente(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

    }
}
