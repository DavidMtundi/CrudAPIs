namespace FinalCRUD.WebApiConsume
{
    public interface IWebapi
    {
        // public List<Telcos> getAllAsync();
        public Task<List<Telcos>> GetallTelcosc();
    }
}
