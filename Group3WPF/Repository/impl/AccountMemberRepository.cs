using Group3WPF.Context;
using Group3WPF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3WPF.Repository.impl
{
    internal class AccountMemberRepository
    {
        private readonly MyContext _context;

        public AccountMemberRepository(MyContext context)
        {
            _context = context;
        }

        public  AccountMember GetAccountByUsernameAndPasswordAsync(string username, string password)
        {
            AccountMember member = _context.AccountMember
                .FirstOrDefault(m => m.Username == username && m.Password == password);
            return member;
        }
    }
}
