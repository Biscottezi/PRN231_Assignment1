using System;
using System.Collections.Generic;
using BusinessLogic;
using BusinessLogic.RequestModel;

namespace DataAccess.Repositories
{
    public interface IMemberRepository
    {
        IEnumerable<MemberViewModel> GetMemberList();
        MemberViewModel GetMemberById(int id);
        bool LoginAdmin(String email, String password);
        MemberViewModel LoginMember(String email, String password);
        void CreateMember(MemberCreateModel createModel);
        void UpdateMember(int id, MemberCreateModel requestModel);
        void DeleteMember(int id);
    }
}