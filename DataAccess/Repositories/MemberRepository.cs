using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using BusinessLogic;
using BusinessLogic.RequestModel;
using DataAccess.DataAccess;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repositories
{
    public class MemberRepository : IMemberRepository
    {
        private IMapper mapper;
        public MemberRepository(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public IEnumerable<MemberViewModel> GetMemberList()
        {
            try
            {
                var members = MemberDAO.Instance.GetMemberList();
                var memberList = mapper.Map<IEnumerable<Member>, IEnumerable<MemberViewModel>>(members);
                return memberList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public MemberViewModel GetMemberById(int id)
        {
            try
            {
                var mem = MemberDAO.Instance.GetMemberById(id);
                var member = mapper.Map<Member, MemberViewModel>(mem);
                return member;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool LoginAdmin(String email, String password)
        {
            // IConfiguration config = new ConfigurationBuilder()
            //                             .SetBasePath(Directory.GetCurrentDirectory())
            //                             .AddJsonFile("appsettings.json", true, true)
            //                             .Build();
            // String adminEmail = config["AdminCredential:email"];
            // String adminPassword = config["AdminCredential:password"];
            return email.Equals("admin@estore.com") && password.Equals("admin@@");
        }

        public MemberViewModel LoginMember(String email, String password)
        {
            try
            {
                var mem = MemberDAO.Instance.GetMemberLogin(email, password);
                var member = mapper.Map<Member, MemberViewModel>(mem);
                return member;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CreateMember(MemberCreateModel createModel)
        {
            try
            {
                var member = mapper.Map<MemberCreateModel, Member>(createModel);
                MemberDAO.Instance.Create(member);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateMember(int id, MemberCreateModel requestModel)
        {
            try
            {
                var member = mapper.Map<MemberCreateModel, Member>(requestModel);
                member.MemberId = id;
                MemberDAO.Instance.Update(member);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void DeleteMember(int id)
        {
            try
            {
                MemberDAO.Instance.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}