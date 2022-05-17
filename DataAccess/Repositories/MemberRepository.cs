using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using BusinessLogic;
using DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories
{
    public class MemberRepository
    {
        private IMapper mapper;
        public MemberRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<MemberObject> GetMemberList()
        {
            try
            {
                var members = MemberDAO.Instance.GetMemberList();
                var memberList = mapper.Map<IEnumerable<Member>, IEnumerable<MemberObject>>(members);
                return memberList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MemberObject GetMemberById(int id)
        {
            try
            {
                var mem = MemberDAO.Instance.GetMemberById(id);
                var member = mapper.Map<Member, MemberObject>(mem);
                return member;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool LoginAdmin(String email, String password)
        {
            IConfiguration config = new ConfigurationBuilder()
                                        .SetBasePath(Directory.GetCurrentDirectory())
                                        .AddJsonFile("appsettings.json", true, true)
                                        .Build();
            String adminEmail = config["AdminCredential:email"];
            String adminPassword = config["AdminCredential:password"];
            if(email == adminEmail && password == adminPassword)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public MemberObject LoginMember(String email, String password)
        {
            try
            {
                var mem = MemberDAO.Instance.GetMemberLogin(email, password);
                var member = mapper.Map<Member, MemberObject>(mem);
                return member;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateMember(MemberObject memberObject)
        {
            try
            {
                var member = mapper.Map<MemberObject, Member>(memberObject);
                MemberDAO.Instance.Create(member);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateMember(MemberObject memberObject)
        {
            try
            {
                var member = mapper.Map<MemberObject, Member>(memberObject);
                MemberDAO.Instance.Update(member);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteMember(MemberObject memberObject)
        {
            try
            {
                var member = mapper.Map<MemberObject, Member>(memberObject);
                MemberDAO.Instance.Delete(member);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}