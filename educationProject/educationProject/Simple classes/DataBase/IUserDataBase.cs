namespace educationProject
{
    public interface IUserDataBase
    {
        bool IsExist(string login);

        void AddUser(string login);

    }
}
