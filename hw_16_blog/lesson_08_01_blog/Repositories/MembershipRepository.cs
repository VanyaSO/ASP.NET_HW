using lesson_08_01_blog.Interfaces;
using lesson_08_01_blog.Models;
using Microsoft.EntityFrameworkCore;

namespace lesson_08_01_blog.Repositories;

public class MembershipRepository : IMembership
{
    private readonly ApplicationContext _applicationContext;
 
    public MembershipRepository(ApplicationContext applicationContext)
    {
        _applicationContext = applicationContext;
    }
 
    public async Task<IEnumerable<Membership>> GetAllMembershipsAsync()
    {
        return await _applicationContext.Memberships.OrderByDescending(e => e.Id).ToListAsync();
    }
 
    public async Task<Membership> GetMembershipAsync(int id)
    {
        return await _applicationContext.Memberships.FirstOrDefaultAsync(e => e.Id == id);
    }
 
    public async Task AddMembershipAsync(Membership membership)
    {
        _applicationContext.Memberships.Add(membership);
        await _applicationContext.SaveChangesAsync();
    }
 
    public async Task DeleteMembershipAsync(Membership membership)
    {
        _applicationContext.Memberships.Remove(membership);
        await _applicationContext.SaveChangesAsync();
    }
 
    public async Task<bool> ExistsMembershipByCodeAsync(string code)
    {
        return await _applicationContext.Memberships.AnyAsync(e => e.Code.Equals(code));
    }
 
    public async Task DisableMembershipCodeAsync(string code)
    {
        var currentMemberShip = await _applicationContext.Memberships.FirstOrDefaultAsync(e => e.Code.Equals(code));
        if (currentMemberShip != null)
        {
            currentMemberShip.IsEnable = false;
            await _applicationContext.SaveChangesAsync();
        }
    }
 
    public async Task<bool> EnableCodeMembershipByCodeAsync(string code)
    {
        var currentMemberShip = await _applicationContext.Memberships.FirstOrDefaultAsync(e => e.Code.Equals(code));
        if (currentMemberShip != null)
        {
            return currentMemberShip.IsEnable;
        }
        return false;
    }
}