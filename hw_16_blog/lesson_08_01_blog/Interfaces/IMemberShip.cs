using lesson_08_01_blog.Models;

namespace lesson_08_01_blog.Interfaces;

public interface IMembership
{
    Task<IEnumerable<Membership>> GetAllMembershipsAsync();
    Task<Membership> GetMembershipAsync(int id);
    Task<bool> ExistsMembershipByCodeAsync(string code);
    Task<bool> EnableCodeMembershipByCodeAsync(string code);
    Task DisableMembershipCodeAsync(string code);
 
    Task AddMembershipAsync(Membership membership);
    Task DeleteMembershipAsync(Membership membership);
}