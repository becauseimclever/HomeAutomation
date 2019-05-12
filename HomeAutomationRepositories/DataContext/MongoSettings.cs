namespace HomeAutomationRepositories.DataContext
{
    public class MongoSettings
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string RoomCollection { get; set; }
        public string UserClaimCollection { get; set; }
    }
}