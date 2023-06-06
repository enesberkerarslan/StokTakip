using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using StokTakip.Models;

namespace StokTakip.Security 
{
    public class MyRoles : RoleProvider
    {
        public override string ApplicationName
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string []roller=null;
            StokTakipEntities s = new StokTakipEntities();
            Kullanıcı user = s.Kullanıcı.FirstOrDefault(x => x.KullaniciAdi == username);
            string tip = user.Tip;

            char[] rol = tip.ToCharArray();
            roller = new string[rol.Length];
            for(int i=0; i<rol.Length;i++)
            {
                roller[i] = rol[i].ToString();
            }


            return roller;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}