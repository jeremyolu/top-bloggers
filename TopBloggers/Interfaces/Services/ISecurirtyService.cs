namespace TopBloggers.Interfaces.Services
{
    public interface ISecurirtyService
    {
        string Encrypt(string password);
        string Decrypt(string password);
    }
}