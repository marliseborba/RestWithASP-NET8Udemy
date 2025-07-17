using RestWithASP_NET8Udemy.Data.VO;
using RestWithASP_NET8Udemy.Model;

namespace RestWithASP_NET8Udemy.Repository
{
    public interface IUserRepository
    {
        User ValidadeCredentials(UserVO user);

        User ValidadeCredentials(string username);

        bool RevokeToken(string username);

        User RefreshUserInfo(User user);
    }
}
