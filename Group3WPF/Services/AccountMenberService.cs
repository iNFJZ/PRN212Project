using Group3WPF.Context;
using Group3WPF.Models;
using Group3WPF.Repository.impl;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group3WPF.Services
{
    internal class AccountMenberService
    {

        public AccountMember LoginAccountAsync(string username, string password)
        {
            MyContext myContext = new MyContext();
            AccountMemberRepository accountMemberRepository = new AccountMemberRepository(myContext);
            return accountMemberRepository.GetAccountByUsernameAndPasswordAsync(username, password);
        }
    }
}
