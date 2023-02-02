using AS_AssN.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AS_AssN.Services
{
    public class MemberService
    {
        private readonly MemberDbContext _db;
        private readonly UserManager<AppUser> _user;

        public MemberService(MemberDbContext db, UserManager<AppUser> user)
        {
            _db = db;
            _user = user;
        }

        public List<AppUser> GetAll()
        {
            return _db.AppUsers.OrderBy(m => m.Id).ToList();
        }

        public AppUser? GetCustomerByUN(string username)
        {
            AppUser? customer = _db.AppUsers.FirstOrDefault(
            x => x.UserName.ToUpper().Equals(username.ToUpper()));

            return customer;
        }

        public AppUser? GetCustomerById(string id)
        {

            AppUser? customer = _db.AppUsers.FirstOrDefault(
                x => x.Id.Equals(id));
            return customer;
        }

        public void AddCustomer(AppUser customer)
        {

            _db.AppUsers.Add(customer);
            _db.SaveChanges();

        }
        public void UpdateCustomer(AppUser customer)
        {
            _db.AppUsers.Update(customer);
            _db.SaveChanges();
        }








    }
}
