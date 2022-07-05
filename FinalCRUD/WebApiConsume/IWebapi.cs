namespace FinalCRUD.WebApiConsume
{
    public interface IWebapi
    {
        Telcos AddTelcos(Telcos telcos);

        // public List<Telcos> getAllAsync();
        public Task<List<Telcos>> GetallTelcosc();
    }
}
